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
            Console.Write("Введите целое число N > 0: ");
            int N = int.Parse(Console.ReadLine());

            int reversed = 0;

            while (N > 0)
            {
                reversed = reversed * 10 + N % 10;
                N /= 10;
            }

            Console.WriteLine($"Результат: {reversed}");
        }
    }
}
