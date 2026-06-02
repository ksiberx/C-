using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp4
{
    internal static class FileManager
    {
        public static void SaveDictionary() // сохранение словаря в файл
        {
            try 
            {
                using (StreamWriter sw =
                       new StreamWriter(DictionaryManager.FileName)) // создаем файл для записи
                {
                    foreach (var item in DictionaryManager.Dict) // перебираем все слова словаря
                    {
                        sw.WriteLine( 
                            item.Key + "=" + // слово
                            string.Join(",", item.Value)); // все переводы через запятую
                    }
                }

                Console.WriteLine(
                    Path.GetFullPath(DictionaryManager.FileName)); // полный путь к файлу

                Console.WriteLine("Словарь сохранён.");
            }
            catch 
            {
                Console.WriteLine("Ошибка сохранения словаря!"); 
            }
        }

        public static bool ImportDictionary() // импорт словаря
        {
            try
            {
                Console.Write("Введите имя файла: ");

                DictionaryManager.FileName =
                    Console.ReadLine() + ".txt"; // Добавляем расширение txt

                DictionaryManager.Dict.Clear(); 

                foreach (string line in
                         File.ReadAllLines(DictionaryManager.FileName)) // читаем все строки
                {
                    string[] parts = line.Split('='); // делим строку на слово и переводы

                    DictionaryManager.Dict[parts[0]] =
                        new List<string>(parts[1].Split(',')); // переводы сохраняются в список
                }

                Console.WriteLine("Словарь импортирован.");
                return true;
            }
            catch
            {
                Console.WriteLine("Такого словаря нет!");
                Console.ReadLine(); 
                return false;
            }
        }

        public static void ShowDictionaries() // показать словари
        {
            try
            {
                Console.WriteLine("Список словарей:");

                string[] files =
                    Directory.GetFiles(
                        Directory.GetCurrentDirectory(), // в текущей папке программы
                        "*.txt");

                foreach (string file in files) // перебирание
                {
                    Console.WriteLine(
                        "- " +
                        Path.GetFileNameWithoutExtension(file)); // вывод без txt
                }
            }
            catch
            {
                Console.WriteLine("Ошибка отображения словарей!");
            }
        }
    }
}
