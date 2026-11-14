using System;
using System.Threading;

class Program
{
    // Общий счетчик топлива
    static int fuel = 0;

    // Мониторинг ракеты
    static void Monitoring()
    {
        while (true)
        {
            Console.WriteLine("Телеметрия в норме...");
            Thread.Sleep(1000);
        }
    }

    // Расчет траектории
    static int CalculateTrajectory()
    {
        Thread.Sleep(4000);

        return 1;
    }

    // Callback после расчета траектории
    static void Callback(IAsyncResult ar)
    {
        Func<int> del = (Func<int>)ar.AsyncState;

        del.EndInvoke(ar);

        Console.WriteLine("Траектория рассчитана! К запуску готов.");
    }

    // Работа топливных насосов
    static void FuelPump(object state)
    {
        for (int i = 0; i < 10; i++)
        {
            Interlocked.Increment(ref fuel);

            Console.WriteLine("Уровень топлива: " + fuel);

            Thread.Sleep(500);
        }
    }

    static void Main()
    {
        // Запускаем фоновый мониторинг
        Thread monitor = new Thread(Monitoring);
        monitor.IsBackground = true;
        monitor.Start();

        // Запускаем асинхронный расчет
        Func<int> calculation = CalculateTrajectory;

        calculation.BeginInvoke(
            new AsyncCallback(Callback),
            calculation
        );

        // Запускаем топливные насосы
        ThreadPool.QueueUserWorkItem(FuelPump);

        // Главный поток ждет 5 секунд
        Thread.Sleep(5000);

        // Безопасно читаем счетчик топлива
        int result = Interlocked.CompareExchange(ref fuel, 0, 0);

        Console.WriteLine("ИТОГ ЗАПУСКА");
        Console.WriteLine("Уровень топлива: " + result);
        Console.WriteLine("Подготовка завершена.");
    }
}
