using System;
using System.Threading;

class Program
{
    private static ManualResetEvent startEvent = new ManualResetEvent(false);

    static void Worker(object number)
    {
        Console.WriteLine("Поток " + number + " ожидает сигнал.");
        startEvent.WaitOne();
        Console.WriteLine("Поток " + number + " стартовал.");
    }

    static void Main()
    {
        Thread[] threads = new Thread[10];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(Worker);
            threads[i].Start(i + 1);
        }

        Thread.Sleep(2000);
        Console.WriteLine("Главный поток подаёт сигнал.");
        startEvent.Set();

        foreach (Thread thread in threads)
            thread.Join();

        // После Reset новые потоки снова будут ждать Set().
        startEvent.Reset();
        Console.WriteLine("Событие сброшено. Запускаем новый поток.");

        Thread newThread = new Thread(Worker);
        newThread.Start(11);

        Thread.Sleep(1000);
        Console.WriteLine("Повторный сигнал.");
        startEvent.Set();

        newThread.Join();
        startEvent.Dispose();

        Console.ReadKey();
    }
}