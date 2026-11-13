
using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Обработка дочернего процесса
        if (args.Length > 0)
        {
            if (args[0] == "child")
            {
                int a = int.Parse(args[1]);
                int b = int.Parse(args[2]);

                Console.WriteLine("Аргументы: " + a + ", " + b);
                Console.WriteLine("Операция: " + args[3]);
                Console.WriteLine("Результат: " + (a + b));
                return;
            }

            if (args[0] == "search")
            {
                string content = File.ReadAllText(args[1]);
                string word = args[2];
                int count = 0;
                int index = 0;

                while ((index = content.IndexOf(word, index,
                    StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    count++;
                    index += word.Length;
                }

                Console.WriteLine("Слово найдено раз: " + count);
                return;
            }
        }

        Console.WriteLine("1 - Запуск процесса");
        Console.WriteLine("2 - Управление процессом");
        Console.WriteLine("3 - Передача аргументов");
        Console.WriteLine("4 - Поиск слова в файле");
        Console.Write("Выберите задание: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            // Задание 1
            case "1":
                Process p1 = Process.Start("notepad.exe");
                p1.WaitForExit();
                Console.WriteLine("Код завершения: " + p1.ExitCode);
                break;

            // Задание 2
            case "2":
                Process p2 = Process.Start("notepad.exe");

                Console.WriteLine("1 - Ожидать завершения");
                Console.WriteLine("2 - Завершить процесс");
                string answer = Console.ReadLine();

                if (answer == "1")
                {
                    p2.WaitForExit();
                    Console.WriteLine("Код завершения: " + p2.ExitCode);
                }
                else if (answer == "2")
                {
                    p2.Kill();
                    p2.WaitForExit();
                    Console.WriteLine("Процесс завершен.");
                }
                break;

            // Задание 3
            case "3":
                ProcessStartInfo info = new ProcessStartInfo(
                    Environment.ProcessPath);

                info.ArgumentList.Add("child");
                info.ArgumentList.Add("7");
                info.ArgumentList.Add("3");
                info.ArgumentList.Add("+");
                info.UseShellExecute = false;

                Process.Start(info).WaitForExit();
                break;

            // Задание 4
            case "4":
                Console.Write("Введите путь к файлу: ");
                string path = Console.ReadLine();

                Console.Write("Введите слово: ");
                string word = Console.ReadLine();

                ProcessStartInfo search = new ProcessStartInfo(
                    Environment.ProcessPath);

                search.ArgumentList.Add("search");
                search.ArgumentList.Add(path);
                search.ArgumentList.Add(word);
                search.UseShellExecute = false;

                Process.Start(search).WaitForExit();
                break;

            default:
                Console.WriteLine("Неверный выбор!");
                break;
        }
    }
}
