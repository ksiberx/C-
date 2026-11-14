using System;
using System.Threading;

class Program
{
    static int tickets = 100;

    // Объект синхронизации
    static object locker = new object();

    static void Cashier(object state)
    {
        int number = (int)state;

        while (true)
        {
            lock (locker)
            {
                if (tickets <= 0)
                    break;

                tickets--;

                Console.WriteLine(
                    $"Кассир {number} продал билет. Осталось: {tickets}");
            }

            Thread.Sleep(10);
        }
    }

    static void Main()
    {
        Thread[] cashiers = new Thread[5];

        // Создаем пять кассиров
        for (int i = 0; i < 5; i++)
        {
            cashiers[i] = new Thread(Cashier);
            cashiers[i].Start(i + 1);
        }

        // Ждем завершения всех потоков
        foreach (Thread thread in cashiers)
        {
            thread.Join();
        }

        Console.WriteLine("Все билеты проданы.");
        Console.WriteLine("Осталось билетов: " + tickets);
    }
}
