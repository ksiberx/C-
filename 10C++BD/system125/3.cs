using System;
using System.Threading;

class Program
{
    private static Semaphore semaphore = new Semaphore(3, 3);
    private static CountdownEvent finished = new CountdownEvent(6);

    static void Work(object number)
    {
        bool entered = false;

        try
        {
            while (!entered)
            {
                entered = semaphore.WaitOne(500);

                if (!entered)
                    Console.WriteLine("Задача " + number + ": ожидание, таймаут.");
            }

            try
            {
                Console.WriteLine("Задача " + number +
                    " работает. ID потока: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
            }
            finally
            {
                semaphore.Release();
                Console.WriteLine("Задача " + number + " освободила место.");
            }
        }
        finally
        {
            finished.Signal();
        }
    }

    static void Main()
    {
        for (int i = 1; i <= 6; i++)
            ThreadPool.QueueUserWorkItem(Work, i);

        finished.Wait();
        semaphore.Dispose();
        finished.Dispose();

        Console.WriteLine("Все задачи завершены.");
        Console.ReadKey();
    }
}