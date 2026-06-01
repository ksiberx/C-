using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp4
{
    internal static class FileManager // класс для работы с файлами
    {
        public static void SaveDictionary() // сохранение словаря
        {
            using (StreamWriter sw =
                   new StreamWriter(DictionaryManager.FileName)) // открытие файла для записи
            {
                foreach (var item in DictionaryManager.Dict) // перебираем все слова словаря
                {
                    sw.WriteLine(
                        item.Key + "=" +
                        string.Join(",", item.Value));
                }
            }

            Console.WriteLine(
                Path.GetFullPath(DictionaryManager.FileName)); // вывод пути файла
        }

        public static void ImportDictionary() // импорт 
        {
            Console.Write("Введите имя файла: ");

            DictionaryManager.FileName =
                Console.ReadLine(); // считываение имя файла
             
            if (!File.Exists(DictionaryManager.FileName))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            DictionaryManager.Dict.Clear(); // очищение

            foreach (string line in
                     File.ReadAllLines(DictionaryManager.FileName)) // читание строк файла
            {
                string[] parts = line.Split('=');

                if (parts.Length == 2)
                {
                    DictionaryManager.Dict[parts[0]] =
                        new List<string>(parts[1].Split(',')); // создаем список словаря
                }
            }
        }

        public static void ShowDictionaries() // показ всех словарей
        {
            Console.WriteLine("Список словарей:");

            string[] files =
                Directory.GetFiles( // получаем список файлов
                    Directory.GetCurrentDirectory(),
                    "*.txt");

            foreach (string file in files) // перебирание найденных файлов
            {
                Console.WriteLine( // вывод
                    "- " +
                    Path.GetFileNameWithoutExtension(file));
            }
        }
    }
}
