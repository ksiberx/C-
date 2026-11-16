using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WindowsApp;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

public class MainForm : Form
{
    TextBox startBox = new() { Text = "0", Width = 80 };
    TextBox endBox = new() { Text = "50", Width = 80 };
    TextBox threadsBox = new() { Text = "3", Width = 80 };
    TextBox outputBox = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
    TextBox statsBox = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };

    public MainForm()
    {
        Text = "Многопоточность и асинхронность";
        Size = new Size(850, 600);
        StartPosition = FormStartPosition.CenterScreen;

        var tabs = new TabControl { Dock = DockStyle.Fill };
        var tab6 = new TabPage("Задание 6 — диапазон");
        var tab7 = new TabPage("Задание 7 — статистика");

        // Интерфейс задания 6
        var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 90, Padding = new Padding(10) };
        panel.Controls.Add(new Label { Text = "Начало:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) });
        panel.Controls.Add(startBox);
        panel.Controls.Add(new Label { Text = "Конец:", AutoSize = true, Padding = new Padding(8, 7, 0, 0) });
        panel.Controls.Add(endBox);
        panel.Controls.Add(new Label { Text = "Потоки:", AutoSize = true, Padding = new Padding(8, 7, 0, 0) });
        panel.Controls.Add(threadsBox);

        var runRange = new Button { Text = "Запустить", AutoSize = true };
        runRange.Click += (_, _) => RunRange();
        panel.Controls.Add(runRange);

        var runDefault = new Button { Text = "Вывести 0–50", AutoSize = true };
        runDefault.Click += (_, _) => { startBox.Text = "0"; endBox.Text = "50"; threadsBox.Text = "1"; RunRange(); };
        panel.Controls.Add(runDefault);

        tab6.Controls.Add(outputBox);
        tab6.Controls.Add(panel);

        // Интерфейс задания 7
        var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(10) };
        var generate = new Button { Text = "Сгенерировать 10000 чисел и вычислить", AutoSize = true };
        generate.Click += (_, _) => RunStatistics();
        top.Controls.Add(generate);

        var save = new Button { Text = "Сохранить результат в файл", AutoSize = true };
        save.Click += (_, _) => SaveResult();
        top.Controls.Add(save);

        tab7.Controls.Add(statsBox);
        tab7.Controls.Add(top);

        tabs.TabPages.Add(tab6);
        tabs.TabPages.Add(tab7);
        Controls.Add(tabs);
    }

    void RunRange()
    {
        if (!int.TryParse(startBox.Text, out int start) ||
            !int.TryParse(endBox.Text, out int end) ||
            !int.TryParse(threadsBox.Text, out int count) || count < 1)
        {
            MessageBox.Show("Введите целые числа. Количество потоков должно быть больше 0.");
            return;
        }

        if (start > end) (start, end) = (end, start);
        long total = (long)end - start + 1;
        int actualCount = (int)Math.Min(count, total);
        string[] parts = new string[actualCount];
        Thread[] workers = new Thread[actualCount];

        for (int i = 0; i < actualCount; i++)
        {
            int index = i;
            int from = start + (int)(total * i / actualCount);
            int to = start + (int)(total * (i + 1) / actualCount) - 1;

            workers[i] = new Thread(() =>
            {
                var values = Enumerable.Range(from, to - from + 1);
                parts[index] = $"Поток {index + 1}: " + string.Join(", ", values);
            });
            workers[i].Start();
        }

        foreach (Thread thread in workers) thread.Join();
        outputBox.Text = string.Join(Environment.NewLine, parts);
    }

    int[]? lastNumbers;
    string lastResult = "";

    void RunStatistics()
    {
        lastNumbers = new Random().Next(1, 100001) is int seed
            ? new Random(seed).Next(1, 100001) is int first
                ? Enumerable.Range(0, 10000).Select(_ => Random.Shared.Next(1, 100001)).ToArray()
                : Array.Empty<int>()
            : Array.Empty<int>();

        int min = 0, max = 0;
        double average = 0;
        long sum = 0;

        Thread tMin = new(() => min = lastNumbers.Min());
        Thread tMax = new(() => max = lastNumbers.Max());
        Thread tAverage = new(() =>
        {
            foreach (int n in lastNumbers) sum += n;
            average = (double)sum / lastNumbers.Length;
        });

        tMin.Start(); tMax.Start(); tAverage.Start();
        tMin.Join(); tMax.Join(); tAverage.Join();

        lastResult = $"Количество элементов: {lastNumbers.Length}\r\n" +
                     $"Минимум: {min}\r\nМаксимум: {max}\r\nСреднее: {average:F2}";
        statsBox.Text = lastResult;
    }

    void SaveResult()
    {
        if (lastNumbers == null)
        {
            MessageBox.Show("Сначала выполните вычисление.");
            return;
        }

        using SaveFileDialog dialog = new() { Filter = "Текстовый файл (*.txt)|*.txt", FileName = "results.txt" };
        if (dialog.ShowDialog() != DialogResult.OK) return;

        string path = dialog.FileName;
        int[] numbers = lastNumbers;
        string result = lastResult;

        Thread fileThread = new(() =>
        {
            using StreamWriter writer = new(path);
            writer.WriteLine("Сгенерированный набор чисел:");
            writer.WriteLine(string.Join(", ", numbers));
            writer.WriteLine();
            writer.WriteLine("Результаты вычислений:");
            writer.WriteLine(result);
        });

        fileThread.Start();
        fileThread.Join();
        MessageBox.Show("Файл сохранён.");
    }
}
