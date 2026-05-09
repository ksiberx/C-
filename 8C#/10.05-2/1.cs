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
            Console.Write("Введите A: ");
            int A = int.Parse(Console.ReadLine());
            Console.Write("Введите B: ");
            int B = int.Parse(Console.ReadLine());
            Console.Write("Введите C: ");
            int C = int.Parse(Console.ReadLine());

            if (C > A || C > B)
            {
                Console.WriteLine("Нельзя разместить ни одного квадрата");
            }
            else
            {
                int count = (A / C) * (B / C);
                int freeArea = A * B - count * C * C;
                Console.WriteLine($"Квадратов: {count}");
                Console.WriteLine($"Свободная площадь: {freeArea}");
            }
        }
    }
}
