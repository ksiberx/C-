using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите процент P (0<P<25): ");
            double P = double.Parse(Console.ReadLine());

            double deposit = 10000;
            int months = 0;

            while (deposit <= 11000)
            {
                deposit += deposit * (P / 100);
                months++;
            }

            Console.WriteLine($"Месяцев: {months}");
            Console.WriteLine($"Итоговая сумма: {deposit:F2}");
        }
    }
}
