using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    private static Queue<int> queue = new Queue<int>();
    private static readonly object queueLock = new object();

    // В очереди максимум 5 свободных мест и максимум 5 элементов.
    private static Semaphore empty = new Semaphore(5, 5);
    private static Semaphore full = new Semaphore(0, 5);

    private const int N = 20;
    private const int STOP = -1;

    static void Producer()
    {
        for (int i = 1; i <= N; i++)
        {
            empty.WaitOne();

            lock (queueLock)
            {
                queue.Enqueue(i);
                Console.WriteLine("Производитель добавил: " + i);
            }

            full.Release();
            Thread.Sleep(100);
        }

        // Отправляем каждому потребителю сигнал завершения.
        for (int i = 0; i < 2; i++)
        {
            empty.WaitOne();

            lock (queueLock)
            {
                queue.Enqueue(STOP);
            }

            full.Release();
        }
    }

    static void Consumer(object number)
    {
        while (true)
        {
            full.WaitOne();

            int item;
            lock (queueLock)
            {
                item = queue.Dequeue();
            }

            empty.Release();

            if (item == STOP)
            {
                Console.WriteLine("Потребитель " + number + " завершил работу.");
                break;
            }

            Console.WriteLine("Потребитель " + number + " забрал: " + item);
            Thread.Sleep(200);
        }
    }

    static void Main()
    {
        Thread producer = new Thread(Producer);
        Thread consumer1 = new Thread(Consumer);
        Thread consumer2 = new Thread(Consumer);

        consumer1.Start(1);
        consumer2.Start(2);
        producer.Start();

        producer.Join();
        consumer1.Join();
        consumer2.Join();

        empty.Dispose();
        full.Dispose();

        Console.WriteLine("Обработка завершена.");
        Console.ReadKey();
    }
}