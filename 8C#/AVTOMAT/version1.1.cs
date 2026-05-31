using System;
using System.Collections.Generic; // подключение колекций dictionary и list
using System.IO; // подключение для работы с файлами

namespace ConsoleApp4
{
    internal class Program
    {
        static Dictionary<string, List<string>> dict =
            new Dictionary<string, List<string>>(); // словарь, где ключ - слово, а значение - список переводов

        static string fileName = ""; // имя файла

        static void Main(string[] args)
        {
            while (true) // бесконечный цикл меню
            {
                Console.Clear();
                Console.WriteLine("СЛОВАРЬ на C#");
                Console.WriteLine("1. Создать словарь");
                Console.WriteLine("2. Импортировать словарь");
                Console.WriteLine("3. Показать словарь");
                Console.WriteLine("0. Выход"); ;

                string choice = Console.ReadLine(); // ввод

                switch (choice)
                {
                    // создание словаря
                    case "1":
                        Console.Write("Введите название словаря: ");
                        fileName = Console.ReadLine() + ".txt";

                        dict.Clear(); // чистка словаря в памяти 

                        if (!File.Exists(fileName)) 
                        {
                            File.Create(fileName).Close(); // создание файла, если его нет
                        }

                        Menu();
                        break;

                    // экспорт словаря 
                    case "2":
                        ImportDictionary();
                        Console.ReadLine();
                        Menu();
                        break;

                    case "3":

                        Console.WriteLine("Список словарей:");

                        string[] files = Directory.GetFiles(
                            Directory.GetCurrentDirectory(),
                            "*.txt");

                        if (files.Length == 0)
                        {
                            Console.WriteLine("Словари не найдены.");
                        }
                        else
                        {
                            foreach (string file in files)
                            {
                                Console.WriteLine("- " +
                                    Path.GetFileNameWithoutExtension(file));
                            }
                        }

                        Console.WriteLine("\nНажмите Enter...");
                        Console.ReadLine();
                        break;

                    // завершение
                    case "0":
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

                string choice = Console.ReadLine(); // выбор

                switch (choice)
                {
                    case "1":
                        {
                            Console.Write("Слово: ");
                            string word = Console.ReadLine();

                            Console.Write("Перевод: ");
                            string tr = Console.ReadLine();

                            if (!dict.ContainsKey(word))
                                dict[word] = new List<string>(); // если слова нет, создаем список переводов

                            dict[word].Add(tr); // добавление перевода

                            Console.WriteLine("Добавлено.");
                            break;
                        }

                        // замена слова
                    case "2":
                        {
                            Console.Write("Старое слово: ");
                            string oldWord = Console.ReadLine(); // старое слово

                            if (dict.ContainsKey(oldWord)) // проверка 
                            {
                                Console.Write("Новое слово: ");
                                string newWord = Console.ReadLine(); // новое слово

                                dict[newWord] = dict[oldWord]; // копирование всех переводов
                                dict.Remove(oldWord); 

                                Console.WriteLine("Слово изменено.");
                            }
                            else
                            {
                                Console.WriteLine("Слово не найдено.");
                            }

                            break;
                        }

                    // замена перевода
                    case "3":
                        {
                            Console.Write("Слово: ");
                            string word = Console.ReadLine();

                            if (dict.ContainsKey(word)) // проверка 
                            {
                                Console.Write("Старый перевод: ");
                                string oldTr = Console.ReadLine(); // сохранение старого перевода

                                int index = dict[word].IndexOf(oldTr); // поиск индекса старого перевода

                                if (index >= 0)
                                {
                                    Console.Write("Новый перевод: ");
                                    dict[word][index] = Console.ReadLine(); // замена перевода по индексу

                                    Console.WriteLine("Перевод изменён.");
                                }
                            }

                            break;
                        }

                    // удаление слова
                    case "4":
                        {
                            Console.Write("Слово: ");
                            string word = Console.ReadLine();

                            if (dict.Remove(word)) // удаление
                                Console.WriteLine("Слово удалено.");
                            else
                                Console.WriteLine("Слово не найдено.");

                            break;
                        }

                    // удаление перевода
                    case "5":
                        {
                            Console.Write("Слово: ");
                            string word = Console.ReadLine();

                            if (dict.ContainsKey(word)) // проверка
                            {
                                if (dict[word].Count == 1) // кол-во переводов
                                {
                                    Console.WriteLine("Последний перевод удалять нельзя!");
                                }
                                else
                                {
                                    Console.Write("Перевод: ");
                                    string tr = Console.ReadLine(); // сохранение перевода

                                    dict[word].Remove(tr); // удаляем перевод из списка

                                    Console.WriteLine("Перевод удалён.");
                                }
                            }

                            break;
                        }

                    // поиск перевода
                    case "6":
                        {
                            Console.Write("Введите слово: ");
                            string word = Console.ReadLine();
                             
                            if (dict.ContainsKey(word))
                            {
                                Console.WriteLine("Переводы:");
                                Console.WriteLine(string.Join(", ", dict[word])); // вывод всех переводов
                            }
                            else
                            {
                                Console.WriteLine("Слово не найдено.");
                            }

                            break;
                        }

                    // показ словаря
                    case "7":
                        {
                            Console.WriteLine("\nСЛОВАРЬ");

                            if (dict.Count == 0) 
                            {
                                Console.WriteLine("Словарь пуст.");
                            }
                            else
                            {
                                foreach (var item in dict) // перебирание словаря
                                {
                                    // выывода слова и его переводов
                                    Console.WriteLine(
                                        item.Key + " = " +
                                        string.Join(", ", item.Value));
                                }
                            }

                            break;
                        }



                    // сохранение словаря
                    case "8":
                        {
                            SaveDictionary();
                            break;
                        }

                    // выход
                    case "0":
                        return;
                }

                Console.WriteLine("\nНажмите Enter...");
                Console.ReadLine();
            }
        }

        // сохранение словаря
        static void SaveDictionary()
        {
            // создание объекта для записи в файл
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var item in dict)
                {
                    // перебирирание слов и переводов в файл
                    sw.WriteLine(
                        item.Key + "=" +
                        string.Join(",", item.Value));
                }

            }
            // полный путь к файлу
            Console.WriteLine(Path.GetFullPath(fileName));
            Console.WriteLine("Словарь сохранён в файл " + fileName);
        }

        static void ImportDictionary()
        {
            Console.Write("Введите имя файла: ");
            fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            dict.Clear();

            foreach (string line in File.ReadAllLines(fileName))
            {
                string[] parts = line.Split('=');

                if (parts.Length == 2)
                {
                    dict[parts[0]] =
                        new List<string>(parts[1].Split(','));
                }
            }

            Console.WriteLine("Словарь импортирован.");
        }
    }
}
