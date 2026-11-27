using Chat.Core;

namespace Chat.Server;

public sealed class ServerForm : Form
{
    private readonly TextBox ip = new() { Text = "0.0.0.0", Width = 130 };
    private readonly NumericUpDown port = new() { Minimum = 1, Maximum = 65535, Value = 5000, Width = 90 };
    private readonly ComboBox mode = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150 };
    private readonly Button start = new() { Text = "Запустить сервер", AutoSize = true };
    private readonly TextBox chat = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
    private readonly TextBox message = new() { Dock = DockStyle.Fill };
    private readonly Button send = new() { Text = "Отправить", AutoSize = true };
    private readonly Label status = new() { Text = "Не запущен", AutoSize = true };
    private ChatPeer? peer;
    private bool botReplying;

    public ServerForm()
    {
        Text = "Socket Chat — Сервер"; Width = 700; Height = 500; MinimumSize = new Size(550, 400);
        mode.Items.AddRange(new object[] { "Человек", "Компьютер" }); mode.SelectedIndex = 0;
        var top = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(8) };
        top.Controls.AddRange(new Control[] { new Label { Text = "IP:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }, ip,
            new Label { Text = "Порт:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }, port,
            new Label { Text = "Режим сервера:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) }, mode, start });
        var bottom = new TableLayoutPanel { Dock = DockStyle.Bottom, Height = 42, ColumnCount = 2 };
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); bottom.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        bottom.Controls.Add(message, 0, 0); bottom.Controls.Add(send, 1, 0);
        Controls.Add(chat); Controls.Add(bottom); Controls.Add(top);
        var statusPanel = new Panel { Dock = DockStyle.Bottom, Height = 24 }; statusPanel.Controls.Add(status); Controls.Add(statusPanel);
        start.Click += async (_, _) => await StartServer();
        send.Click += async (_, _) => await SendHumanMessage();
        message.KeyDown += async (_, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await SendHumanMessage(); } };
    }

    private async Task StartServer()
    {
        try
        {
            start.Enabled = false;
            peer = new ChatPeer();
            peer.MessageReceived += OnMessage;
            peer.StatusChanged += s => Ui(() => status.Text = s);
            await peer.StartServerAsync(ip.Text.Trim(), (int)port.Value);
        }
        catch (Exception ex) { Ui(() => { Log("Ошибка: " + ex.Message); start.Enabled = true; }); }
    }

    private async void OnMessage(string text)
    {
        Ui(() => Log("Клиент: " + text));
        if (text.Equals("Bye", StringComparison.OrdinalIgnoreCase)) { Ui(() => { send.Enabled = false; message.Enabled = false; }); return; }
        if (mode.SelectedIndex == 1 && !botReplying && peer is not null)
        {
            botReplying = true;
            try { var answer = ReplyBot.GetRandomReply(); await peer.SendAsync(answer); Ui(() => Log("Сервер (компьютер): " + answer)); }
            catch (Exception ex) { Ui(() => Log("Ошибка отправки: " + ex.Message)); }
            finally { botReplying = false; }
        }
    }

    private async Task SendHumanMessage()
    {
        if (peer is null || mode.SelectedIndex != 0 || string.IsNullOrWhiteSpace(message.Text)) return;
        var value = message.Text.Trim(); message.Clear();
        try { await peer.SendAsync(value); Log("Сервер: " + value); if (value.Equals("Bye", StringComparison.OrdinalIgnoreCase)) { send.Enabled = false; message.Enabled = false; } }
        catch (Exception ex) { Log("Ошибка: " + ex.Message); }
    }

    private void Log(string line) { if (!IsDisposed) chat.AppendText($"[{DateTime.Now:HH:mm:ss}] {line}{Environment.NewLine}"); }
    private void Ui(Action action) { if (IsDisposed) return; if (InvokeRequired) BeginInvoke(action); else action(); }
    protected override async void OnFormClosed(FormClosedEventArgs e) { if (peer is not null) await peer.DisposeAsync(); base.OnFormClosed(e); }
}
