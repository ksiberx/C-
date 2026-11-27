using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat.Core;

// Сетевой класс не зависит от Windows Forms или другого пользовательского интерфейса.
public sealed class ChatPeer : IAsyncDisposable
{
    private TcpClient? _client;
    private TcpListener? _listener;
    private StreamReader? _reader;
    private StreamWriter? _writer;
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    private CancellationTokenSource? _receiveCts;

    public event Action<string>? MessageReceived;
    public event Action<string>? StatusChanged;
    public bool IsConnected => _client?.Connected == true;

    public async Task StartServerAsync(string ip, int port, CancellationToken token = default)
    {
        var address = IPAddress.Parse(ip);
        _listener = new TcpListener(address, port);
        _listener.Start();
        StatusChanged?.Invoke($"Сервер запущен: {ip}:{port}. Ожидание клиента...");
        try
        {
            _client = await _listener.AcceptTcpClientAsync(token);
            SetupStreams();
            StatusChanged?.Invoke("Клиент подключился.");
            StartReceiveLoop();
        }
        catch
        {
            _listener.Stop();
            throw;
        }
    }

    public async Task ConnectAsync(string ip, int port, CancellationToken token = default)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(ip, port, token);
        SetupStreams();
        StatusChanged?.Invoke($"Подключено к {ip}:{port}.");
        StartReceiveLoop();
    }

    private void SetupStreams()
    {
        var stream = _client!.GetStream();
        _reader = new StreamReader(stream, new UTF8Encoding(false), false, 1024, true);
        _writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true) { AutoFlush = true };
    }

    private void StartReceiveLoop()
    {
        _receiveCts = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {
            try
            {
                while (!_receiveCts.IsCancellationRequested)
                {
                    var line = await _reader!.ReadLineAsync(_receiveCts.Token);
                    if (line is null) break;
                    MessageReceived?.Invoke(line);
                    if (line.Equals("Bye", StringComparison.OrdinalIgnoreCase)) break;
                }
            }
            catch (OperationCanceledException) { }
            catch (IOException ex) { StatusChanged?.Invoke($"Соединение закрыто: {ex.Message}"); }
            finally { StatusChanged?.Invoke("Соединение завершено."); }
        });
    }

    public async Task SendAsync(string message)
    {
        if (_writer is null) throw new InvalidOperationException("Нет соединения.");
        await _sendLock.WaitAsync();
        try { await _writer.WriteLineAsync(message); }
        finally { _sendLock.Release(); }
        if (message.Equals("Bye", StringComparison.OrdinalIgnoreCase))
            await DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        try { _receiveCts?.Cancel(); } catch { }
        try { _writer?.Dispose(); } catch { }
        try { _reader?.Dispose(); } catch { }
        try { _client?.Close(); } catch { }
        try { _listener?.Stop(); } catch { }
        _client = null;
    }
}
