using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            while (true) // бесконечный цикл
            {
                Console.Clear();

                Console.WriteLine("СЛОВАРЬ");
                Console.WriteLine("1. Создать словарь");
                Console.WriteLine("2. Импортировать словарь");
                Console.WriteLine("3. Показать словари");
                Console.WriteLine("0. Выход");

                string choice = Console.ReadLine(); // считаем выбор

                switch (choice) 
                {
                    case "1": // создание словаря
                        Console.Write("Название словаря: ");
                        DictionaryManager.FileName =
                            Console.ReadLine() + ".txt";

                        DictionaryManager.Clear();
                        Menu();
                        break;

                    case "2": // загрузка словаря
                        FileManager.ImportDictionary();
                        Menu();
                        break;

                    case "3": // просмотр словаря
                        FileManager.ShowDictionaries();
                        Console.ReadLine();
                        break;

                    case "0": // выход
                        return;
                }
            }
        }

        static void Menu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("1. Добавить слово");
                Console.WriteLine("2. Заменить слово");
                Console.WriteLine("3. Заменить перевод");
                Console.WriteLine("4. Удалить слово");
                Console.WriteLine("5. Удалить перевод");
                Console.WriteLine("6. Найти перевод");
                Console.WriteLine("7. Показать словарь");
                Console.WriteLine("8. Сохранить словарь");
                Console.WriteLine("0. Назад");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DictionaryManager.AddWord(); break;
                    case "2": DictionaryManager.ReplaceWord(); break;
                    case "3": DictionaryManager.ReplaceTranslation(); break;
                    case "4": DictionaryManager.DeleteWord(); break;
                    case "5": DictionaryManager.DeleteTranslation(); break;
                    case "6": DictionaryManager.FindWord(); break;
                    case "7": DictionaryManager.ShowDictionary(); break;
                    case "8": FileManager.SaveDictionary(); break;
                    case "0": return;
                }

                Console.WriteLine("\nНажмите Enter...");
                Console.ReadLine();
            }
        }
    }
}
