using Chat.Core;

namespace Chat.Client;

public sealed class ClientForm : Form
{
    private readonly TextBox ip = new() { Text = "127.0.0.1", Width = 130 };
    private readonly NumericUpDown port = new() { Minimum = 1, Maximum = 65535, Value = 5000, Width = 90 };
    private readonly ComboBox mode = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
    private readonly Button connect = new() { Text = "Подсоединиться", AutoSize = true };
    private readonly TextBox chat = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
    private readonly TextBox message = new() { Dock = DockStyle.Fill, Enabled = false };
    private readonly Button send = new() { Text = "Отправить", AutoSize = true, Enabled = false };
    private readonly Label status = new() { Text = "Не подключен", AutoSize = true };
    private ChatPeer? peer;
    private bool botReplying;

    public ClientForm()
    {
        Text = "Socket Chat — Клиент"; Width = 700; Height = 500; MinimumSize = new Size(550, 400);
        mode.Items.AddRange(new object[] { "Человек", "Компьютер" }); mode.SelectedIndex = 0;
        var top = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(8) };
        top.Controls.AddRange(new Control[] { new Label { Text = "IP сервера:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }, ip,
            new Label { Text = "Порт:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }, port,
            new Label { Text = "Режим клиента:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }, mode, connect });
        var bottom = new TableLayoutPanel { Dock = DockStyle.Bottom, Height = 42, ColumnCount = 2 };
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); bottom.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        bottom.Controls.Add(message, 0, 0); bottom.Controls.Add(send, 1, 0);
        var statusPanel = new Panel { Dock = DockStyle.Bottom, Height = 24 }; statusPanel.Controls.Add(status);
        Controls.Add(chat); Controls.Add(bottom); Controls.Add(statusPanel); Controls.Add(top);
        connect.Click += async (_, _) => await Connect();
        send.Click += async (_, _) => await SendHumanMessage();
        message.KeyDown += async (_, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await SendHumanMessage(); } };
    }

    private async Task Connect()
    {
        try
        {
            connect.Enabled = false;
            peer = new ChatPeer();
            peer.MessageReceived += OnMessage;
            peer.StatusChanged += s => Ui(() => status.Text = s);
            await peer.ConnectAsync(ip.Text.Trim(), (int)port.Value);
            Ui(() => { message.Enabled = mode.SelectedIndex == 0; send.Enabled = mode.SelectedIndex == 0; });
        }
        catch (Exception ex) { Ui(() => { Log("Ошибка подключения: " + ex.Message); connect.Enabled = true; }); }
    }

    private async void OnMessage(string text)
    {
        Ui(() => Log("Сервер: " + text));
        if (text.Equals("Bye", StringComparison.OrdinalIgnoreCase)) { Ui(() => { send.Enabled = false; message.Enabled = false; }); return; }
        if (mode.SelectedIndex == 1 && !botReplying && peer is not null)
        {
            botReplying = true;
            try { var answer = ReplyBot.GetRandomReply(); await peer.SendAsync(answer); Ui(() => Log("Клиент (компьютер): " + answer)); }
            catch (Exception ex) { Ui(() => Log("Ошибка отправки: " + ex.Message)); }
            finally { botReplying = false; }
        }
    }

    private async Task SendHumanMessage()
    {
        if (peer is null || mode.SelectedIndex != 0 || string.IsNullOrWhiteSpace(message.Text)) return;
        var value = message.Text.Trim(); message.Clear();
        try { await peer.SendAsync(value); Log("Клиент: " + value); if (value.Equals("Bye", StringComparison.OrdinalIgnoreCase)) { send.Enabled = false; message.Enabled = false; } }
        catch (Exception ex) { Log("Ошибка: " + ex.Message); }
    }

    private void Log(string line) { if (!IsDisposed) chat.AppendText($"[{DateTime.Now:HH:mm:ss}] {line}{Environment.NewLine}"); }
    private void Ui(Action action) { if (IsDisposed) return; if (InvokeRequired) BeginInvoke(action); else action(); }
    protected override async void OnFormClosed(FormClosedEventArgs e) { if (peer is not null) await peer.DisposeAsync(); base.OnFormClosed(e); }
}
