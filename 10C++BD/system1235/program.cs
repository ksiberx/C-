tusing System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("ПРАКТИЧЕСКИЕ ЗАДАНИЯ");
            Console.WriteLine("1 - Дата и время тремя способами");
            Console.WriteLine("2 - Простые числа от 0 до 1000");
            Console.WriteLine("3 - Простые числа в заданном диапазоне");
            Console.WriteLine("4 - Минимум, максимум, среднее и сумма массива");
            Console.WriteLine("5 - Удаление дублей, сортировка и бинарный поиск");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите задание: ");

            string choice = Console.ReadLine();

            if (choice == "0")
                break;

            Console.Clear();

            switch (choice)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task2();
                    break;
                case "3":
                    Task3();
                    break;
                case "4":
                    Task4();
                    break;
                case "5":
                    Task5();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }

    // Задание 1: запуск задачи тремя способами
    static void Task1()
    {
        Action showTime = () =>
        {
            Console.WriteLine("Дата и время: " + DateTime.Now);
        };

        Task t1 = new Task(showTime);
        t1.Start();

        Task t2 = Task.Factory.StartNew(showTime);
        Task t3 = Task.Run(showTime);

        Task.WaitAll(t1, t2, t3);
    }

    // Задание 2: простые числа от 0 до 1000
    static void Task2()
    {
        Task task = Task.Run(() =>
        {
            for (int i = 2; i <= 1000; i++)
            {
                bool simple = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        simple = false;
                        break;
                    }
                }

                if (simple)
                    Console.Write(i + " ");
            }
        });

        task.Wait();
        Console.WriteLine();
    }

    // Задание 3: простые числа в заданном диапазоне и их количество
    static void Task3()
    {
        Console.Write("Начало диапазона: ");
        int start = int.Parse(Console.ReadLine());

        Console.Write("Конец диапазона: ");
        int end = int.Parse(Console.ReadLine());

        Task<int> task = Task.Run(() =>
        {
            int count = 0;

            for (int i = start; i <= end; i++)
            {
                bool simple = i >= 2;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        simple = false;
                        break;
                    }
                }

                if (simple)
                {
                    Console.Write(i + " ");
                    count++;
                }
            }

            return count;
        });

        task.Wait();
        Console.WriteLine();
        Console.WriteLine("Количество простых чисел: " + task.Result);
    }

    // Задание 4: операции с массивом через массив Task
    static void Task4()
    {
        int[] arr = { 5, 10, 15, 20, 25 };

        int min = 0;
        int max = 0;
        int sum = 0;
        double average = 0;

        Task[] tasks = new Task[4];

        tasks[0] = Task.Run(() => min = arr.Min());
        tasks[1] = Task.Run(() => max = arr.Max());
        tasks[2] = Task.Run(() => sum = arr.Sum());
        tasks[3] = Task.Run(() => average = arr.Average());

        Task.WaitAll(tasks);

        Console.WriteLine("Массив: " + string.Join(", ", arr));
        Console.WriteLine("Минимум: " + min);
        Console.WriteLine("Максимум: " + max);
        Console.WriteLine("Сумма: " + sum);
        Console.WriteLine("Среднее: " + average);
    }

    // Задание 5: продолжение задач
    static void Task5()
    {
        int[] arr = { 5, 2, 8, 2, 1, 5, 9, 3 };

        Console.Write("Введите число для поиска: ");
        int search = int.Parse(Console.ReadLine());

        Task<int[]> task1 = Task.Run(() =>
        {
            return arr.Distinct().ToArray();
        });

        Task<int[]> task2 = task1.ContinueWith(t =>
        {
            int[] result = t.Result;
            Array.Sort(result);

            Console.WriteLine("Массив после удаления дублей и сортировки:");
            Console.WriteLine(string.Join(", ", result));

            return result;
        });

        Task<int> task3 = task2.ContinueWith(t =>
        {
            int index = Array.BinarySearch(t.Result, search);

            if (index >= 0)
                Console.WriteLine("Число найдено. Индекс: " + index);
            else
                Console.WriteLine("Число не найдено.");

            return index;
        });

        task3.Wait();
    }
}
