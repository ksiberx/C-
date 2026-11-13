
using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);

    const uint WM_CLOSE = 0x0010;
    const uint WM_SETTEXT = 0x000C;

    static void Main()
    {
        Console.WriteLine("Работа");
        Console.WriteLine("1 - Hello World");
        Console.WriteLine("2 - Угадывание числа");
        Console.WriteLine("3 - Закрытие Блокнота");
        Console.WriteLine("4 - Время в заголовке Блокнота");
        Console.Write("Выберите задание: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                MessageBox(IntPtr.Zero, "Hello, World!", "Задание 1", 0);
                break;

            case "2":
                Game();
                break;

            case "3":
                CloseNotepad();
                break;

            case "4":
                ShowTime();
                break;

            default:
                Console.WriteLine("Неверный выбор!");
                break;
        }
    }

    // Задание 2
    static void Game()
    {
        bool again = true;

        while (again)
        {
            int min = 0;
            int max = 100;
            bool guessed = false;

            MessageBox(IntPtr.Zero,
                "Загадайте число от 0 до 100.",
                "Игра", 0);

            while (min <= max)
            {
                int number = (min + max) / 2;

                int answer = MessageBox(IntPtr.Zero,
                    "Ваше число " + number + "?\n\n" +
                    "ДА - число больше.\n" +
                    "НЕТ - число меньше.\n" +
                    "ОТМЕНА - компьютер угадал!",
                    "Угадывание числа", 3);

                if (answer == 2)
                {
                    guessed = true;
                    break;
                }
                else if (answer == 6)
                {
                    min = number + 1;
                }
                else if (answer == 7)
                {
                    max = number - 1;
                }
            }

            if (guessed)
                MessageBox(IntPtr.Zero, "Число угадано!", "Результат", 0);
            else
                MessageBox(IntPtr.Zero, "Ответы противоречивы!", "Ошибка", 0);

            again = MessageBox(IntPtr.Zero,
                "Сыграть ещё раз?",
                "Повторная игра", 4) == 6;
        }
    }

    // Задание 3
    static void CloseNotepad()
    {
        IntPtr hwnd = FindWindow("Notepad", null);

        if (hwnd != IntPtr.Zero)
        {
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, null);
            Console.WriteLine("Отправлена команда закрытия Блокнота.");
        }
        else
        {
            Console.WriteLine("Блокнот не найден!");
        }
    }

    // Задание 4
    static void ShowTime()
    {
        Console.WriteLine("Запустите Блокнот. Для остановки нажмите Ctrl+C.");

        IntPtr hwnd = FindWindow("Notepad", null);

        if (hwnd == IntPtr.Zero)
        {
            Console.WriteLine("Блокнот не найден!");
            return;
        }

        while (true)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");

            SendMessage(hwnd, WM_SETTEXT, IntPtr.Zero, time);

            Thread.Sleep(1000);
        }
    }
}
