using System;

namespace MyLibrary
{
    public static class Info
    {
        public static void ShowMessage()
        {
            Console.WriteLine("Это сообщение из DLL-библиотеки.");
        }
    }

    public static class DateHelper
    {
        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
    }

    public static class NumberHelper
    {
        public static int Max(int a, int b, int c)
        {
            return Math.Max(a, Math.Max(b, c));
        }

        public static int Min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        public static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        public static long Factorial(int n)
        {
            if (n < 0 || n > 20)
                throw new ArgumentException("Для факториала введите число от 0 до 20.");

            long result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }

        public static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }

        public static bool IsEven(int n) { return n % 2 == 0; }
        public static bool IsOdd(int n) { return n % 2 != 0; }
    }

    public class Fraction
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть нулём.");

            Numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        private void Simplify()
        {
            int a = Math.Abs(Numerator);
            int b = Math.Abs(Denominator);
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            int gcd = a == 0 ? 1 : a;
            Numerator /= gcd;
            Denominator /= gcd;

            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        public Fraction Add(Fraction x)
        {
            return new Fraction(Numerator * x.Denominator + x.Numerator * Denominator,
                                Denominator * x.Denominator);
        }

        public Fraction Subtract(Fraction x)
        {
            return new Fraction(Numerator * x.Denominator - x.Numerator * Denominator,
                                Denominator * x.Denominator);
        }

        public Fraction Multiply(Fraction x)
        {
            return new Fraction(Numerator * x.Numerator, Denominator * x.Denominator);
        }

        public Fraction Divide(Fraction x)
        {
            if (x.Numerator == 0)
                throw new ArgumentException("Делить на ноль нельзя.");
            return new Fraction(Numerator * x.Denominator, Denominator * x.Numerator);
        }

        public override string ToString()
        {
            return Numerator + "/" + Denominator;
        }
    }
}
