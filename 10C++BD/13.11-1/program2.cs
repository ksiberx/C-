using System;
using System.Threading;

class Program
{
    // Метод вычисления квадратного корня
    static double Calculate(double number)
    {
        Thread.Sleep(3000);

        return Math.Sqrt(number);
    }

    // Callback вызывается после завершения вычисления
    static void Callback(IAsyncResult ar)
    {
        // Получаем делегат
        Func<double, double> del =
            (Func<double, double>)ar.AsyncState;

        // Получаем результат
        double result = del.EndInvoke(ar);

        Console.WriteLine("Результат: " + result);
    }

    static void Main()
    {
        // Создаем делегат
        Func<double, double> calculation = Calculate;

        // Запускаем асинхронное вычисление
        calculation.BeginInvoke(
            144,
            new AsyncCallback(Callback),
            calculation
        );

        Console.WriteLine("Расчет запущен. Главный поток свободен.");

        Console.ReadLine();
    }
}
