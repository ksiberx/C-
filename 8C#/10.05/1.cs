using System;

namespace HomeWorkModule1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Введите число от 1 до 100: ");
            int num = int.Parse(Console.ReadLine());

            if (num < 1 || num > 100)
            {
                Console.WriteLine("Ошибка! Число должно быть от 1 до 100.");
            }
            else
            {
                if (num % 3 == 0 && num % 5 == 0)
                    Console.WriteLine("Fizz Buzz");
                else if (num % 3 == 0)
                    Console.WriteLine("Fizz");
                else if (num % 5 == 0)
                    Console.WriteLine("Buzz");
                else
                    Console.WriteLine(num);
            }

            Console.Write("Введите число: ");
            double chislo = double.Parse(Console.ReadLine());
            Console.Write("Введите процент: ");
            double procent = double.Parse(Console.ReadLine());

            double result = chislo * procent / 100;
            Console.WriteLine($"{procent}% от {chislo} = {result}");
// 3
            Console.Write("Введите первую цифру: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введите вторую цифру: ");
            int b = int.Parse(Console.ReadLine());
            Console.Write("Введите третью цифру: ");
            int c = int.Parse(Console.ReadLine());
            Console.Write("Введите четвертую цифру: ");
            int d = int.Parse(Console.ReadLine());

            int chisloIzCifr = a * 1000 + b * 100 + c * 10 + d;
            Console.WriteLine($"Сформированное число: {chisloIzCifr}");
// 2
            Console.Write("Введите шестизначное число: ");
            int shestZnach = int.Parse(Console.ReadLine());

            if (shestZnach < 100000 || shestZnach > 999999)
            {
                Console.WriteLine("Ошибка! Число должно быть шестизначным.");
            }
            else
            {
                Console.Write("Введите номер первого разряда (1-6): ");
                int razr1 = int.Parse(Console.ReadLine());
                Console.Write("Введите номер второго разряда (1-6): ");
                int razr2 = int.Parse(Console.ReadLine());

                // Превращаем число в строку для удобства
                string chisloStr = shestZnach.ToString();
                char[] cifry = chisloStr.ToCharArray();

                // Меняем местами цифры (индексы на 1 меньше)
                char temp = cifry[razr1 - 1];
                cifry[razr1 - 1] = cifry[razr2 - 1];
                cifry[razr2 - 1] = temp;

                string novoeChisloStr = new string(cifry);
                Console.WriteLine($"Результат: {novoeChisloStr}");
            }

            Console.Write("Введите дату (дд.мм.гггг): ");
            string dataStr = Console.ReadLine();
            DateTime data = DateTime.Parse(dataStr);

            // Определяем сезон
            string sezon = "";
            int mesyac = data.Month;
            if (mesyac == 12 || mesyac == 1 || mesyac == 2)
                sezon = "Winter";
            else if (mesyac == 3 || mesyac == 4 || mesyac == 5)
                sezon = "Spring";
            else if (mesyac == 6 || mesyac == 7 || mesyac == 8)
                sezon = "Summer";
            else
                sezon = "Autumn";

            // Определяем день недели
            string denNedeli = data.DayOfWeek.ToString();

            Console.WriteLine($"{sezon} {denNedeli}");
);
            Console.WriteLine("Выберите направление перевода:");
            Console.WriteLine("1 - Фаренгейт -> Цельсий");
            Console.WriteLine("2 - Цельсий -> Фаренгейт");
            Console.Write("Ваш выбор: ");
            int vibor = int.Parse(Console.ReadLine());

            if (vibor == 1)
            {
                Console.Write("Введите температуру в Фаренгейтах: ");
                double f = double.Parse(Console.ReadLine());
                double c = (f - 32) * 5 / 9;
                Console.WriteLine($"{f}°F = {c}°C");
            }
            else if (vibor == 2)
            {
                Console.Write("Введите температуру в Цельсиях: ");
                double c = double.Parse(Console.ReadLine());
                double f = c * 9 / 5 + 32;
                Console.WriteLine($"{c}°C = {f}°F");
            }
            else
            {
                Console.WriteLine("Неверный выбор!");
            };
            Console.Write("Введите первое число: ");
            int pervoe = int.Parse(Console.ReadLine());
            Console.Write("Введите второе число: ");
            int vtoroe = int.Parse(Console.ReadLine());

            // Нормализация границ
            int nachalo = Math.Min(pervoe, vtoroe);
            int konec = Math.Max(pervoe, vtoroe);

            Console.WriteLine($"Четные числа в диапазоне от {nachalo} до {konec}:");

            for (int i = nachalo; i <= konec; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine();

            // Конец программы
            Console.WriteLine("\nВсе задания выполнены!");
        }
    }
}
