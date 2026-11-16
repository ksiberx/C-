using System;
using System.IO;
using System.Threading;

namespace PracticalWork
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("ПРАКТИЧЕСКАЯ РАБОТА");
                Console.WriteLine("1 - Задание 1 (События)");
                Console.WriteLine("2 - Задание 2 (Работа с файлом)");
                Console.WriteLine("3 - Задание 3 (Сортировка и поиск)");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите задание: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                    Task1();
                else if (choice == "2")
                    Task2();
                else if (choice == "3")
                    Task3();
                else if (choice == "0")
                    break;
                else
                    Console.WriteLine("Неверный выбор.");
            }
        }

        // ЗАДАНИЕ 1. СОБЫТИЯ
        static void Task1()
        {
            int[] numbers = new int[1000];

            // Событие: потоки будут ждать, пока генерация не закончится.
            ManualResetEvent generated = new ManualResetEvent(false);

            Thread generator = new Thread(() =>
            {
                Random random = new Random();

                for (int i = 0; i < numbers.Length; i++)
                    numbers[i] = random.Next(0, 5001);

                Console.WriteLine("\nПервый поток: числа сгенерированы.");
                generated.Set(); // Сообщаем остальным потокам о завершении.
            });

            Thread maxThread = new Thread(() =>
            {
                generated.WaitOne(); // Ждём завершения генерации.

                int max = numbers[0];
                foreach (int number in numbers)
                    if (number > max)
                        max = number;

                Console.WriteLine("Максимум: " + max);
            });

            Thread minThread = new Thread(() =>
            {
                generated.WaitOne();

                int min = numbers[0];
                foreach (int number in numbers)
                    if (number < min)
                        min = number;

                Console.WriteLine("Минимум: " + min);
            });

            Thread averageThread = new Thread(() =>
            {
                generated.WaitOne();

                double sum = 0;
                foreach (int number in numbers)
                    sum += number;

                Console.WriteLine("Среднее арифметическое: " +
                                  (sum / numbers.Length));
            });

            generator.Start();
            maxThread.Start();
            minThread.Start();
            averageThread.Start();

            generator.Join();
            maxThread.Join();
            minThread.Join();
            averageThread.Join();

            generated.Close();
            Console.WriteLine("Задание 1 выполнено.");
        }

        // ЗАДАНИЕ 2. КРИТИЧЕСКИЕ СЕКЦИИ И ФАЙЛ
        static void Task2()
        {
            Console.Write("Введите путь к текстовому файлу: ");
            string path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            object fileLock = new object();
            ManualResetEvent firstFinished = new ManualResetEvent(false);

            Thread firstThread = new Thread(() =>
            {
                lock (fileLock) // Критическая секция.
                {
                    string text = File.ReadAllText(path);
                    int count = 0;

                    foreach (char symbol in text)
                    {
                        if (symbol == '.' || symbol == '!' || symbol == '?')
                            count++;
                    }

                    Console.WriteLine("\nКоличество предложений: " + count);
                }

                firstFinished.Set(); // Первый поток закончил работу.
            });

            Thread secondThread = new Thread(() =>
            {
                firstFinished.WaitOne(); // Ждём первый поток.

                lock (fileLock) // Критическая секция.
                {
                    string text = File.ReadAllText(path);
                    text = text.Replace('!', '#');
                    File.WriteAllText(path, text);

                    Console.WriteLine("Все символы ! заменены на #.");
                }
            });

            firstThread.Start();
            secondThread.Start();

            firstThread.Join();
            secondThread.Join();

            firstFinished.Close();
            Console.WriteLine("Задание 2 выполнено.");
        }

        // ЗАДАНИЕ 3. СОРТИРОВКА И ПОИСК
        static void Task3()
        {
            int[] numbers = { 8, 3, 12, 5, 1, 9, 4, 7, 2, 6 };

            Console.Write("Введите число для поиска: ");
            int searchNumber;

            if (!int.TryParse(Console.ReadLine(), out searchNumber))
            {
                Console.WriteLine("Нужно ввести целое число.");
                return;
            }

            object arrayLock = new object();
            ManualResetEvent sortingFinished = new ManualResetEvent(false);

            Thread sortThread = new Thread(() =>
            {
                lock (arrayLock) // Критическая секция.
                {
                    Array.Sort(numbers);
                    Console.WriteLine("\nМассив отсортирован.");
                }

                sortingFinished.Set(); // Сообщаем о завершении сортировки.
            });

            Thread searchThread = new Thread(() =>
            {
                sortingFinished.WaitOne(); // Ждём сортировку.

                lock (arrayLock) // Критическая секция.
                {
                    bool found = Array.BinarySearch(numbers, searchNumber) >= 0;

                    if (found)
                        Console.WriteLine("Число найдено.");
                    else
                        Console.WriteLine("Число не найдено.");

                    Console.Write("Отсортированный массив: ");
                    foreach (int number in numbers)
                        Console.Write(number + " ");

                    Console.WriteLine();
                }
            });

            sortThread.Start();
            searchThread.Start();

            sortThread.Join();
            searchThread.Join();

            sortingFinished.Close();
            Console.WriteLine("Задание 3 выполнено.");
        }
    }
}
