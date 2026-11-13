
using System;
using System.Threading;
using System.Diagnostics;

class Program
{
    static int counter = 0;
    static object locker = new object();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Управление потоками и Join");
            Console.WriteLine("2. Фоновые потоки и Timer");
            Console.WriteLine("3. Гонка данных и lock");
            Console.WriteLine("4. Игра «Успел, не успел»");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите задание: ");

            string choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task2();
                    break;
                case "3":
                    Task3();
                    break;
                case "4":
                    Task4();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
        }
    }

    // Задание 1
    static void PrintSteps(object name)
    {
        string threadName = name.ToString();

        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"{threadName} Шаг: {i}");
            Thread.Sleep(500);
        }
    }

    static void Task1()
    {
        Thread threadA = new Thread(PrintSteps);
        Thread threadB = new Thread(PrintSteps);

        threadA.Start("Поток А");
        threadB.Start("Поток Б");

        threadA.Join();
        threadB.Join();

        Console.WriteLine("Все потоки завершили работу!");
    }

    // Задание 2
    static void TimerMethod(object state)
    {
        Console.WriteLine("Время: " + DateTime.Now);
    }

    static void BackgroundWork()
    {
        while (true)
        {
            Console.WriteLine("Фоновый поток работает...");
            Thread.Sleep(300);
        }
    }

    static void Task2()
    {
        using (Timer timer = new Timer(
            TimerMethod, null, 1000, 2000))
        {
            Thread thread = new Thread(BackgroundWork);

            thread.IsBackground = true;
            thread.Start();

            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }

    // Задание 3
    static void UnsafeIncrement()
    {
        for (int i = 0; i < 100000; i++)
        {
            counter++;
        }
    }

    static void SafeIncrement()
    {
        for (int i = 0; i < 100000; i++)
        {
            lock (locker)
            {
                counter++;
            }
        }
    }

    static void Task3()
    {
        Thread[] threads = new Thread[5];

        counter = 0;

        for (int i = 0; i < 5; i++)
        {
            threads[i] = new Thread(UnsafeIncrement);
            threads[i].Start();
        }

        foreach (Thread t in threads)
            t.Join();

        Console.WriteLine("Без lock: " + counter);

        counter = 0;

        for (int i = 0; i < 5; i++)
        {
            threads[i] = new Thread(SafeIncrement);
            threads[i].Start();
        }

        foreach (Thread t in threads)
            t.Join();

        Console.WriteLine("С lock: " + counter);
    }

    // Задание 4
    static int finished = 0;

    static void Task4()
    {
        finished = 0;

        Random rnd = new Random();
        int delay = rnd.Next(2000, 5000);

        Console.WriteLine("Приготовьтесь...");
        Thread.Sleep(delay);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ЖМИ!");
        Console.ResetColor();

        Stopwatch sw = Stopwatch.StartNew();

        Thread timeout = new Thread(() =>
        {
            Thread.Sleep(3000);

            if (Interlocked.CompareExchange(ref finished, 1, 0) == 0)
            {
                Console.WriteLine("Вы не успели! Слишком долго.");
                Environment.Exit(0);
            }
        });

        timeout.IsBackground = true;
        timeout.Start();

        Console.ReadKey(true);

        if (Interlocked.CompareExchange(ref finished, 1, 0) == 0)
        {
            sw.Stop();
            Console.WriteLine(
                $"Ваше время реакции: {sw.ElapsedMilliseconds} мс"
            );
        }
    }
}
