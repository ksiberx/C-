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
            Console.Write("Введите 0 и 1: ");
            string binary = Console.ReadLine();
            try
            {
                foreach (char c in binary)
                    if (c != '0' && c != '1')
                        throw new FormatException();

                int result = Convert.ToInt32(binary, 2);
                Console.WriteLine($"Десятичное число: {result}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: число выходит за границы int!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: можно вводить только 0 и 1!");
            }
        }
    }
}
