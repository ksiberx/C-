using System;
using MyLibrary;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("ПРАКТИЧЕСКАЯ РАБОТА: DLL");
            Console.WriteLine("1 - Информационное сообщение");
            Console.WriteLine("2 - Проверка високосного года");
            Console.WriteLine("3 - Максимум, минимум и сумма трёх чисел");
            Console.WriteLine("4 - Факториал, простое, чётное или нечётное");
            Console.WriteLine("5 - Работа с дробями");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите задание: ");

            string choice = Console.ReadLine();
            Console.Clear();

            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    Info.ShowMessage();
                    break;

                case "2":
                    Console.Write("Введите год: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.WriteLine(DateHelper.IsLeapYear(year) ? "Год високосный." : "Год не високосный.");
                    break;

                case "3":
                    Console.Write("Введите три числа через пробел: ");
                    string[] v = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int a = int.Parse(v[0]);
                    int b = int.Parse(v[1]);
                    int c = int.Parse(v[2]);
                    Console.WriteLine("Максимум: " + NumberHelper.Max(a, b, c));
                    Console.WriteLine("Минимум: " + NumberHelper.Min(a, b, c));
                    Console.WriteLine("Сумма: " + NumberHelper.Sum(a, b, c));
                    break;

                case "4":
                    Console.Write("Введите число от 0 до 20: ");
                    int n = int.Parse(Console.ReadLine());
                    Console.WriteLine("Факториал: " + NumberHelper.Factorial(n));
                    Console.WriteLine(NumberHelper.IsPrime(n) ? "Число простое." : "Число не простое.");
                    Console.WriteLine(NumberHelper.IsEven(n) ? "Число чётное." : "Число нечётное.");
                    break;

                case "5":
                    Console.Write("Числитель первой дроби: ");
                    int n1 = int.Parse(Console.ReadLine());
                    Console.Write("Знаменатель первой дроби: ");
                    int d1 = int.Parse(Console.ReadLine());
                    Console.Write("Числитель второй дроби: ");
                    int n2 = int.Parse(Console.ReadLine());
                    Console.Write("Знаменатель второй дроби: ");
                    int d2 = int.Parse(Console.ReadLine());

                    Fraction f1 = new Fraction(n1, d1);
                    Fraction f2 = new Fraction(n2, d2);

                    Console.WriteLine("Первая дробь: " + f1);
                    Console.WriteLine("Вторая дробь: " + f2);
                    Console.WriteLine("Сложение: " + f1.Add(f2));
                    Console.WriteLine("Вычитание: " + f1.Subtract(f2));
                    Console.WriteLine("Умножение: " + f1.Multiply(f2));
                    Console.WriteLine("Деление: " + f1.Divide(f2));
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
