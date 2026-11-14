using System;
using System.Threading;

class Program
{
    // Фоновый поток выводит время каждую секунду
    static void Monitor()
    {
        while (true)
        {
            Console.WriteLine("Время: " + DateTime.Now);
            Thread.Sleep(1000);
        }
    }

    // Задача для ThreadPool
    static void TaskWork(object state)
    {
        int number = (int)state;

        Console.WriteLine(
            $"Задача №{number} выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");

        Thread.Sleep(2000);

        Console.WriteLine($"Задача №{number} завершена");
    }

    static void Main()
    {
        // Создаем фоновый поток
        Thread monitor = new Thread(Monitor);
        monitor.IsBackground = true;
        monitor.Start();

        // Добавляем три задачи в пул потоков
        ThreadPool.QueueUserWorkItem(TaskWork, 1);
        ThreadPool.QueueUserWorkItem(TaskWork, 2);
        ThreadPool.QueueUserWorkItem(TaskWork, 3);

        // Главный поток ждет 5 секунд
        Thread.Sleep(5000);

        Console.WriteLine("Программа завершена.");
    }
}
