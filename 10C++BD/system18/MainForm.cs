using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows.Forms;

namespace PracticalWork_Modul03
{
    public class MainForm : Form
    {
        TextBox output1, sourceFile, destFile, sourceDir, destDir;
        NumericUpDown threadsFile, threadsDir;
        ProgressBar fileProgress, dirProgress;
        Label fileStatus, dirStatus;
        Button pauseFile, stopFile, pauseDir, stopDir;
        ManualResetEventSlim filePause = new ManualResetEventSlim(true);
        ManualResetEventSlim dirPause = new ManualResetEventSlim(true);
        CancellationTokenSource fileCancel, dirCancel;
        bool filePaused, dirPaused;

        public MainForm()
        {
            Text = "Практическая работа — многопоточность и асинхронность";
            Width = 900;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;

            TabControl tabs = new TabControl();
            tabs.Dock = DockStyle.Fill;
            Controls.Add(tabs);

            tabs.TabPages.Add(BuildThreadsTab());
            tabs.TabPages.Add(BuildFileTab());
            tabs.TabPages.Add(BuildDirectoryTab());
            tabs.TabPages.Add(BuildFactorialTab());
            tabs.TabPages.Add(BuildPowerTab());
            tabs.TabPages.Add(BuildTextTab());
        }

        TabPage NewTab(string title)
        {
            TabPage page = new TabPage(title);
            page.AutoScroll = true;
            return page;
        }

        Label AddLabel(Control parent, string text, int x, int y, int width = 180)
        {
            Label label = new Label();
            label.Text = text;
            label.SetBounds(x, y, width, 25);
            parent.Controls.Add(label);
            return label;
        }

        TextBox AddTextBox(Control parent, int x, int y, int width = 300)
        {
            TextBox box = new TextBox();
            box.SetBounds(x, y, width, 25);
            parent.Controls.Add(box);
            return box;
        }

        Button AddButton(Control parent, string text, int x, int y, int width, Action action)
        {
            Button button = new Button();
            button.Text = text;
            button.SetBounds(x, y, width, 30);
            button.Click += (s, e) => action();
            parent.Controls.Add(button);
            return button;
        }

        // ЗАДАНИЕ 1. Три потока с настройкой приоритета.
        TabPage BuildThreadsTab()
        {
            TabPage page = NewTab("1. Потоки");
            AddLabel(page, "Приоритет чисел:", 20, 20, 130);
            ComboBox p1 = PriorityCombo(page, 150, 18);
            AddLabel(page, "Приоритет букв:", 20, 60, 130);
            ComboBox p2 = PriorityCombo(page, 150, 58);
            AddLabel(page, "Приоритет символов:", 20, 100, 130);
            ComboBox p3 = PriorityCombo(page, 150, 98);

            output1 = new TextBox();
            output1.Multiline = true;
            output1.ScrollBars = ScrollBars.Vertical;
            output1.ReadOnly = true;
            output1.SetBounds(20, 150, 820, 440);
            page.Controls.Add(output1);

            AddButton(page, "Запустить потоки", 350, 105, 170, () =>
            {
                output1.Clear();
                Thread numbers = new Thread(() => Generate("Числа: ", "0123456789"));
                Thread letters = new Thread(() => Generate("Буквы: ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
                Thread symbols = new Thread(() => Generate("Символы: ", "!@#$%^&*()_+-="));

                numbers.Priority = (ThreadPriority)p1.SelectedIndex;
                letters.Priority = (ThreadPriority)p2.SelectedIndex;
                symbols.Priority = (ThreadPriority)p3.SelectedIndex;

                numbers.IsBackground = true;
                letters.IsBackground = true;
                symbols.IsBackground = true;
                numbers.Start();
                letters.Start();
                symbols.Start();
            });
            return page;
        }

        ComboBox PriorityCombo(Control parent, int x, int y)
        {
            ComboBox combo = new ComboBox();
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Items.AddRange(new object[] { "Lowest", "BelowNormal", "Normal", "AboveNormal", "Highest" });
            combo.SelectedIndex = 2;
            combo.SetBounds(x, y, 150, 25);
            parent.Controls.Add(combo);
            return combo;
        }

        void Generate(string title, string alphabet)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < 40; i++)
            {
                char value = alphabet[random.Next(alphabet.Length)];
                SafeAppend(title + value + Environment.NewLine);
                Thread.Sleep(80);
            }
        }

        void SafeAppend(string text)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
                BeginInvoke(new Action(() => output1.AppendText(text)));
            else
                output1.AppendText(text);
        }

        // ЗАДАНИЯ 2 И 3. Многопоточное копирование файла, пауза и остановка.
        TabPage BuildFileTab()
        {
            TabPage page = NewTab("2–3. Копирование файла");
            AddLabel(page, "Исходный файл:", 20, 25);
            sourceFile = AddTextBox(page, 180, 22, 500);
            AddButton(page, "Обзор", 700, 20, 100, () =>
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                    if (dialog.ShowDialog() == DialogResult.OK) sourceFile.Text = dialog.FileName;
            });

            AddLabel(page, "Куда копировать:", 20, 70);
            destFile = AddTextBox(page, 180, 67, 500);
            AddButton(page, "Обзор", 700, 65, 100, () =>
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                    if (dialog.ShowDialog() == DialogResult.OK) destFile.Text = dialog.FileName;
            });

            AddLabel(page, "Количество потоков:", 20, 115);
            threadsFile = new NumericUpDown();
            threadsFile.Minimum = 1;
            threadsFile.Maximum = 16;
            threadsFile.Value = 4;
            threadsFile.SetBounds(180, 112, 80, 25);
            page.Controls.Add(threadsFile);

            fileProgress = new ProgressBar();
            fileProgress.SetBounds(20, 170, 820, 28);
            page.Controls.Add(fileProgress);
            fileStatus = AddLabel(page, "Ожидание", 20, 210, 700);

            AddButton(page, "Старт", 20, 260, 120, StartFileCopy);
            pauseFile = AddButton(page, "Пауза", 160, 260, 120, PauseFileCopy);
            stopFile = AddButton(page, "Остановить", 300, 260, 120, StopFileCopy);
            return page;
        }

        void StartFileCopy()
        {
            if (!File.Exists(sourceFile.Text))
            {
                MessageBox.Show("Выберите существующий исходный файл.");
                return;
            }
            if (string.IsNullOrWhiteSpace(destFile.Text) ||
                string.Equals(Path.GetFullPath(sourceFile.Text), Path.GetFullPath(destFile.Text), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Укажите другой путь для копии.");
                return;
            }

            StopFileCopy();
            fileCancel = new CancellationTokenSource();
            filePause.Set();
            filePaused = false;
            pauseFile.Text = "Пауза";
            fileProgress.Value = 0;
            int count = (int)threadsFile.Value;
            string input = sourceFile.Text;
            string output = destFile.Text;
            fileStatus.Text = "Копирование...";

            Task.Run(() => CopyFileParallel(input, output, count, fileCancel.Token))
                .ContinueWith(t => BeginInvoke(new Action(() =>
                {
                    if (t.IsCanceled) fileStatus.Text = "Копирование остановлено.";
                    else if (t.IsFaulted) fileStatus.Text = "Ошибка: " + t.Exception.GetBaseException().Message;
                    else fileStatus.Text = "Копирование завершено.";
                })));
        }

        void CopyFileParallel(string input, string output, int threadCount, CancellationToken token)
        {
            const int chunkSize = 1024 * 1024;
            long length = new FileInfo(input).Length;
            using (FileStream target = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                target.SetLength(length);

            long nextPosition = -chunkSize;
            long copied = 0;
            object writeLock = new object();

            Task[] workers = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                workers[i] = Task.Run(() =>
                {
                    using (FileStream reader = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (FileStream writer = new FileStream(output, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                    {
                        byte[] buffer = new byte[chunkSize];
                        while (true)
                        {
                            token.ThrowIfCancellationRequested();
                            filePause.Wait(token);

                            long position = Interlocked.Add(ref nextPosition, chunkSize);
                            if (position >= length) break;

                            reader.Position = position;
                            int wanted = (int)Math.Min(chunkSize, length - position);
                            int read = 0;
                            while (read < wanted)
                            {
                                int n = reader.Read(buffer, read, wanted - read);
                                if (n == 0) break;
                                read += n;
                            }

                            lock (writeLock)
                            {
                                writer.Position = position;
                                writer.Write(buffer, 0, read);
                            }

                            long total = Interlocked.Add(ref copied, read);
                            int percent = length == 0 ? 100 : (int)(total * 100 / length);
                            BeginInvoke(new Action(() => fileProgress.Value = Math.Max(0, Math.Min(100, percent))));
                        }
                    }
                }, token);
            }
            Task.WaitAll(workers);
        }

        void PauseFileCopy()
        {
            if (filePaused) { filePause.Set(); filePaused = false; pauseFile.Text = "Пауза"; }
            else { filePause.Reset(); filePaused = true; pauseFile.Text = "Продолжить"; }
        }

        void StopFileCopy()
        {
            if (fileCancel != null) fileCancel.Cancel();
            filePause.Set();
        }

        // ЗАДАНИЯ 4 И 5. Многопоточное копирование папки, пауза и остановка.
        TabPage BuildDirectoryTab()
        {
            TabPage page = NewTab("4–5. Копирование папки");
            AddLabel(page, "Исходная папка:", 20, 25);
            sourceDir = AddTextBox(page, 180, 22, 500);
            AddButton(page, "Обзор", 700, 20, 100, () =>
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                    if (dialog.ShowDialog() == DialogResult.OK) sourceDir.Text = dialog.SelectedPath;
            });

            AddLabel(page, "Папка назначения:", 20, 70);
            destDir = AddTextBox(page, 180, 67, 500);
            AddButton(page, "Обзор", 700, 65, 100, () =>
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                    if (dialog.ShowDialog() == DialogResult.OK) destDir.Text = dialog.SelectedPath;
            });

            AddLabel(page, "Количество потоков:", 20, 115);
            threadsDir = new NumericUpDown();
            threadsDir.Minimum = 1;
            threadsDir.Maximum = 16;
            threadsDir.Value = 4;
            threadsDir.SetBounds(180, 112, 80, 25);
            page.Controls.Add(threadsDir);

            dirProgress = new ProgressBar();
            dirProgress.SetBounds(20, 170, 820, 28);
            page.Controls.Add(dirProgress);
            dirStatus = AddLabel(page, "Ожидание", 20, 210, 700);

            AddButton(page, "Старт", 20, 260, 120, StartDirectoryCopy);
            pauseDir = AddButton(page, "Пауза", 160, 260, 120, PauseDirectoryCopy);
            stopDir = AddButton(page, "Остановить", 300, 260, 120, StopDirectoryCopy);
            return page;
        }

        void StartDirectoryCopy()
        {
            if (!Directory.Exists(sourceDir.Text))
            {
                MessageBox.Show("Выберите существующую исходную папку.");
                return;
            }
            if (string.IsNullOrWhiteSpace(destDir.Text) ||
                Path.GetFullPath(destDir.Text).StartsWith(Path.GetFullPath(sourceDir.Text).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(Path.GetFullPath(sourceDir.Text), Path.GetFullPath(destDir.Text), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Папка назначения должна быть вне исходной папки.");
                return;
            }

            StopDirectoryCopy();
            dirCancel = new CancellationTokenSource();
            dirPause.Set();
            dirPaused = false;
            pauseDir.Text = "Пауза";
            dirProgress.Value = 0;
            dirStatus.Text = "Копирование...";
            string from = sourceDir.Text;
            string to = destDir.Text;
            int count = (int)threadsDir.Value;

            Task.Run(() => CopyDirectoryParallel(from, to, count, dirCancel.Token))
                .ContinueWith(t => BeginInvoke(new Action(() =>
                {
                    if (t.IsCanceled) dirStatus.Text = "Копирование остановлено.";
                    else if (t.IsFaulted) dirStatus.Text = "Ошибка: " + t.Exception.GetBaseException().Message;
                    else dirStatus.Text = "Копирование завершено.";
                })));
        }

        void CopyDirectoryParallel(string from, string to, int threadCount, CancellationToken token)
        {
            string[] files = Directory.GetFiles(from, "*", SearchOption.AllDirectories);
            Directory.CreateDirectory(to);
            foreach (string dir in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(Path.Combine(to, dir.Substring(from.Length).TrimStart(Path.DirectorySeparatorChar)));
            if (files.Length == 0)
            {
                BeginInvoke(new Action(() => dirProgress.Value = 100));
                return;
            }

            ConcurrentQueue<string> queue = new ConcurrentQueue<string>(files);
            int done = 0;
            Parallel.For(0, threadCount, new ParallelOptions { MaxDegreeOfParallelism = threadCount, CancellationToken = token }, worker =>
            {
                string file;
                while (queue.TryDequeue(out file))
                {
                    token.ThrowIfCancellationRequested();
                    dirPause.Wait(token);
                    string relative = file.Substring(from.Length).TrimStart(Path.DirectorySeparatorChar);
                    string target = Path.Combine(to, relative);
                    Directory.CreateDirectory(Path.GetDirectoryName(target));
                    File.Copy(file, target, true);
                    int value = Interlocked.Increment(ref done);
                    int percent = value * 100 / files.Length;
                    BeginInvoke(new Action(() => dirProgress.Value = Math.Max(0, Math.Min(100, percent))));
                }
            });
        }

        void PauseDirectoryCopy()
        {
            if (dirPaused) { dirPause.Set(); dirPaused = false; pauseDir.Text = "Пауза"; }
            else { dirPause.Reset(); dirPaused = true; pauseDir.Text = "Продолжить"; }
        }

        void StopDirectoryCopy()
        {
            if (dirCancel != null) dirCancel.Cancel();
            dirPause.Set();
        }

        // ЗАДАНИЕ 6. Асинхронный факториал.
        TabPage BuildFactorialTab()
        {
            TabPage page = NewTab("6. Факториал");
            AddLabel(page, "Введите число (0–5000):", 30, 40, 190);
            TextBox number = AddTextBox(page, 230, 37, 150);
            TextBox result = AddTextBox(page, 30, 130, 800);
            result.ReadOnly = true;
            AddButton(page, "Вычислить", 30, 80, 130, async () =>
            {
                int n;
                if (!int.TryParse(number.Text, out n) || n < 0 || n > 5000)
                {
                    MessageBox.Show("Введите целое число от 0 до 5000.");
                    return;
                }
                result.Text = "Вычисление...";
                BigInteger answer = await Task.Run(() =>
                {
                    BigInteger value = BigInteger.One;
                    for (int i = 2; i <= n; i++) value *= i;
                    return value;
                });
                result.Text = answer.ToString();
            });
            return page;
        }

        // ЗАДАНИЕ 7. Асинхронное возведение в степень.
        TabPage BuildPowerTab()
        {
            TabPage page = NewTab("7. Степень числа");
            AddLabel(page, "Основание:", 30, 40, 100);
            TextBox number = AddTextBox(page, 140, 37, 150);
            AddLabel(page, "Степень:", 330, 40, 80);
            TextBox power = AddTextBox(page, 420, 37, 150);
            TextBox result = AddTextBox(page, 30, 130, 800);
            result.ReadOnly = true;
            AddButton(page, "Вычислить", 30, 80, 130, async () =>
            {
                double a, b;
                if (!double.TryParse(number.Text, out a) || !double.TryParse(power.Text, out b))
                {
                    MessageBox.Show("Введите корректные числа.");
                    return;
                }
                result.Text = "Вычисление...";
                double answer = await Task.Run(() => Math.Pow(a, b));
                result.Text = answer.ToString();
            });
            return page;
        }

        // ЗАДАНИЕ 8. Асинхронный подсчёт гласных, согласных и символов.
        TabPage BuildTextTab()
        {
            TabPage page = NewTab("8. Анализ текста");
            AddLabel(page, "Введите текст:", 25, 20, 150);
            TextBox input = new TextBox();
            input.Multiline = true;
            input.ScrollBars = ScrollBars.Vertical;
            input.SetBounds(25, 50, 810, 220);
            page.Controls.Add(input);

            TextBox result = new TextBox();
            result.Multiline = true;
            result.ReadOnly = true;
            result.SetBounds(25, 330, 810, 120);
            page.Controls.Add(result);

            AddButton(page, "Посчитать", 25, 285, 130, async () =>
            {
                string text = input.Text;
                result.Text = "Подсчёт...";
                var stats = await Task.Run(() =>
                {
                    string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouyAEIOUY";
                    string consonants = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩbcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ";
                    int v = 0, c = 0;
                    foreach (char ch in text)
                    {
                        if (vowels.IndexOf(ch) >= 0) v++;
                        else if (consonants.IndexOf(ch) >= 0) c++;
                    }
                    return new { Vowels = v, Consonants = c, Symbols = text.Length };
                });
                result.Text = "Гласных: " + stats.Vowels +
                    Environment.NewLine + "Согласных: " + stats.Consonants +
                    Environment.NewLine + "Всего символов: " + stats.Symbols;
            });
            return page;
        }
    }
}
