using System; // Console
using System.Text; // Encoding
using System.Threading.Tasks; // Task

namespace ShopApp
{
    internal static class Program
    {
        private static async Task Main() // асинхронная точка входа
        {
            Console.OutputEncoding = Encoding.UTF8; // чтобы русский текст корректно выводился в консоли
            while (true) // главное меню работает, пока пользователь не выберет выход
            {
                Console.WriteLine("\nИнтернет-магазин электроники");
                Console.WriteLine("1 - Задание 1: присоединённый режим");
                Console.WriteLine("2 - Задание 2: отсоединённый режим");
                Console.WriteLine("3 - Задание 3: LINQ to SQL");
                Console.WriteLine("4 - Задание 4: Entity Framework");
                Console.WriteLine("5 - Задание 5: Dapper");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine(); // читаем выбор пользователя
                Console.WriteLine();
                try
                {
                    switch (choice)
                    {
                        case "1": await Task1Connected.RunAsync(); break; // запуск задания 1 (асинхронный)
                        case "2": Task2Disconnected.Run(); break; // запуск задания 2
                        case "3": Task3LinqToSql.Run(); break; // запуск задания 3
                        case "4": Task4EntityFramework.Run(); break; // запуск задания 4
                        case "5": Task5Dapper.Run(); break; // запуск задания 5
                        case "0": return; // выход из программы
                        default: Console.WriteLine("Неизвестный пункт меню."); break;
                    }
                }
                catch (Exception ex) // последняя линия защиты от необработанных исключений
                {
                    Console.WriteLine("Непредвиденная ошибка: " + ex.Message);
                }
            }
        }
    }
}
