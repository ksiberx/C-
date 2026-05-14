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
            bool IsPalindrome(int num)
            {
                string s = num.ToString();
                return s.SequenceEqual(s.Reverse());
            }

            Console.WriteLine(IsPalindrome(1221)); // True
            Console.WriteLine(IsPalindrome(7854)); // False
        }
    }
}
