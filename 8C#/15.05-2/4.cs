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
            string expr = Console.ReadLine();
            try
            {
                string[] nums = expr.Split('*');
                int result = 1;
                foreach (string n in nums)
                    result *= int.Parse(n);
                Console.WriteLine(result);
            }
            catch
            {
                Console.WriteLine("Ошибка ввода!");
            }

        }
    }
}
