using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class Program
{
    static readonly object locker = new object();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== МНОГОПОТОЧНОСТЬ ===");
            Console.WriteLine("1. Вывод чисел от 0 до 50");
            Console.WriteLine("2. Вывод заданного диапазона");
            Console.WriteLine("3. Диапазон с несколькими потоками");
            Console.WriteLine("4. 10000 чисел: минимум, максимум, среднее");
            Console.WriteLine("5. Задание 4 + запись в файл");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите задание: ");

            string? choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "4": Task4(false); break;
                case "5": Task4(true); break;
                default: Console.WriteLine("Нет такого пункта."); break;
            }
        }
    }

    static int ReadInt(string message)
    {
        int value;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out value))
            Console.Write("Введите целое число: ");
        return value;
    }

    // Задание 1
    static void Task1()
    {
        Thread thread = new Thread(() =>
        {
            for (int i = 0; i <= 50; i++)
                Console.WriteLine(i);
        });

        thread.Start();
        thread.Join();
    }

    // Задание 2
    static void Task2()
    {
        int start = ReadInt("Начало диапазона: ");
        int end = ReadInt("Конец диапазона: ");

        if (start > end) (start, end) = (end, start);

        Thread thread = new Thread(() => PrintRange(start, end));
        thread.Start();
        thread.Join();
    }

    // Задание 3
    static void Task3()
    {
        int start = ReadInt("Начало диапазона: ");
        int end = ReadInt("Конец диапазона: ");
        int count = ReadInt("Количество потоков: ");

        if (start > end) (start, end) = (end, start);
        if (count < 1) { Console.WriteLine("Количество потоков должно быть больше 0."); return; }

        long total = (long)end - start + 1;
        int actualCount = (int)Math.Min(count, total);
        var threads = new List<Thread>();

        for (int i = 0; i < actualCount; i++)
        {
            int partStart = start + (int)(total * i / actualCount);
            int partEnd = start + (int)(total * (i + 1) / actualCount) - 1;

            Thread thread = new Thread(() => PrintRange(partStart, partEnd));
            threads.Add(thread);
            thread.Start();
        }

        foreach (Thread thread in threads) thread.Join();
    }

    static void PrintRange(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            lock (locker)
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {i}");
        }
    }

    // Задания 4 и 5
    static void Task4(bool saveToFile)
    {
        Random random = new Random();
        int[] numbers = new int[10000];

        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = random.Next(1, 100001);

        int min = 0, max = 0;
        double average = 0;
        long sum = 0;

        Thread minThread = new Thread(() => min = numbers.Min());
        Thread maxThread = new Thread(() => max = numbers.Max());
        Thread averageThread = new Thread(() =>
        {
            foreach (int number in numbers) sum += number;
            average = (double)sum / numbers.Length;
        });

        minThread.Start();
        maxThread.Start();
        averageThread.Start();

        minThread.Join();
        maxThread.Join();
        averageThread.Join();

        string result = $"Количество чисел: {numbers.Length}\n" +
                        $"Минимум: {min}\nМаксимум: {max}\nСреднее: {average:F2}";

        Console.WriteLine("\n" + result);

        if (saveToFile)
        {
            Thread fileThread = new Thread(() =>
            {
                using StreamWriter writer = new StreamWriter("results.txt");
                writer.WriteLine("Сгенерированный набор чисел:");
                writer.WriteLine(string.Join(", ", numbers));
                writer.WriteLine();
                writer.WriteLine("Результаты вычислений:");
                writer.WriteLine(result);
            });

            fileThread.Start();
            fileThread.Join();
            Console.WriteLine("Данные записаны в файл results.txt");
        }
    }
}
