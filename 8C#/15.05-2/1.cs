using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите цифры: ");
            string input = Console.ReadLine();
            try
            {
                int result = int.Parse(input);
                Console.WriteLine($"Число: {result}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: число выходит за границы int!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введены не цифры!");
            }
        }
    }
}
