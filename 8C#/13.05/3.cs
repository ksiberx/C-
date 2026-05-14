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
            int[] original = { 12, 6, -1, 88, 7, 6 };
            int[] filter = { 6, 88, 7 };
            int[] result = FilterArray(original, filter);
            Console.WriteLine(string.Join(" ", result));
        }

        static int[] FilterArray(int[] original, int[] filter)
        {
            return original.Where(x => !filter.Contains(x)).ToArray();
        }
    }
}
