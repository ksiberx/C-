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
            void DrawSquare(int side, char symbol)
            {
                for (int i = 0; i < side; i++)
                    Console.WriteLine(new string(symbol, side));
            }

            DrawSquare(5, '*');
        }
    }
}
