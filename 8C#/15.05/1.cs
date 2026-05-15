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
            // 1
            int[] arr = { 1, 2, 3, 2, 4, 5, 5, 6 };
            Console.WriteLine($"Четные: {arr.Count(x => x % 2 == 0)}, Нечетные: {arr.Count(x => x % 2 != 0)}, Уникальные: {arr.Distinct().Count()}");

            // 2
            int[] arr2 = { 3, 7, 2, 9, 1, 6, 4, 8, 5 };
            Console.Write("\nВведите число: ");
            int p = int.Parse(Console.ReadLine());
            Console.WriteLine($"Меньше {p}: {arr2.Count(x => x < p)}");

            // 3
            int[] arr3 = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };
            Console.Write("\nВведите 3 числа: ");
            string[] s = Console.ReadLine().Split();
            int n = 0;
            for (int i = 0; i <= arr3.Length - 3; i++)
                if (arr3[i] == int.Parse(s[0]) && arr3[i + 1] == int.Parse(s[1]) && arr3[i + 2] == int.Parse(s[2]))
                    n++;
            Console.WriteLine($"Повторений: {n}");

            // 4
            int[] a1 = { 1, 2, 3, 4, 5 }, a2 = { 4, 5, 6, 7 };
            Console.WriteLine($"\nОбщие: {string.Join(" ", a1.Intersect(a2))}");

            // 5
            int[,] m = { { 3, 7, 2 }, { 9, 1, 8 }, { 5, 4, 6 } };
            int min = m[0, 0], max = m[0, 0];
            foreach (int x in m) { if (x < min) min = x; if (x > max) max = x; }
            Console.WriteLine($"Min: {min}, Max: {max}");

            // 7
            Console.Write("\nВведите предложение: ");
            string[] w = Console.ReadLine().Split(' ');
            for (int i = 0; i < w.Length; i++)
                w[i] = new string(w[i].Reverse().ToArray());
            Console.WriteLine($"Результат: {string.Join(" ", w)}");

            // 8
            Console.Write("\nВведите предложение: ");
            string text = Console.ReadLine().ToLower();
            int vowels = 0;
            foreach (char c in text)
                if ("иоуыэюя".Contains(c))
                    vowels++;
            Console.WriteLine($"Гласных: {vowels}");

            // 9
            Console.Write("\nВведите строку: ");
            string str = Console.ReadLine();
            Console.Write("Подстрока: ");
            string sub = Console.ReadLine();
            int cnt = 0, idx = 0;
            while ((idx = str.IndexOf(sub, idx)) != -1) { cnt++; idx += sub.Length; }
            Console.WriteLine($"Вхождений: {cnt}");
        }
    }
}
