using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] myArray;
            myArray = new int[10] { 0, -3, -1, 4, 0, 5, 2, 0, -1, 0 }; // массивчик

            int[] result = new int[myArray.Length]; // результат
            int index = 0;

            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] != 0)
                {
                    result[index] = myArray[i];
                    index++;
                }
            }
            for (int i = index; i < myArray.Length; i++)
            {
                result[i] = -1;
            }
            myArray = result;

            Console.WriteLine(string.Join(", ", myArray));
        }
    }
}


namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] myArray;
            myArray = new int[10] { 1, 22, 3, 4, 15, 6, 0, -4, -15, -3}; // массивчик

            int[] result = new int[myArray.Length];
            int Index1 = 0; // переменная, чтоб класть отрицательные числа
            int Index2 = myArray.Length - 1; // переменная, чтоб класть положительные числа

            for (int i = myArray.Length - 1; i >= 0; i--)
            {
                if (myArray[i] < 0)
                {
                    result[Index1] = myArray[i];
                    Index1++;
                }
                else
                {
                        result[Index2] = myArray[i];
                        Index2--;
                }

            }
            myArray = result;
            Console.WriteLine(string.Join(", ", myArray));
        }
    }
}

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] myArray;
            myArray = new int[25] {-1, -1, -2, -3, -5, -6, -10, 1, 2, 2, 3, 3, 3, 4, 5, 5, 6, 7, 7, 7, 8, 10, 15, 15, 12 }; // массивчик

            Console.WriteLine("Введите число: ");
            int panch = int.Parse(Console.ReadLine()); // преобразование в целое число

            int count = 0; 

            for(int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] == panch) 
                {
                    count++; 
                }
            }
            Console.WriteLine($"Число {panch} встречается {count} раза");
        }
    }
}
