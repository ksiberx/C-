using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    internal static class DictionaryManager
    {
        public static Dictionary<string, List<string>> Dict =
            new Dictionary<string, List<string>>(); // для создание одного и более переводов

        public static string FileName = "";

        public static void Clear() // метод очистки словаря
        {
            Dict.Clear();
        }

        public static void AddWord() // добавление слова
        {
            Console.Write("Слово: ");
            string word = Console.ReadLine(); 

            Console.Write("Перевод: ");
            string tr = Console.ReadLine();

            if (!Dict.ContainsKey(word)) 
                Dict[word] = new List<string>(); // создаем перевод, если слово новое

            Dict[word].Add(tr); // добавление перевода
        }

        public static void ReplaceWord() // замена слова
        {
            Console.Write("Старое слово: ");
            string oldWord = Console.ReadLine();

            if (!Dict.ContainsKey(oldWord))
                return;

            Console.Write("Новое слово: ");
            string newWord = Console.ReadLine();

            Dict[newWord] = Dict[oldWord]; // копирование старых переводов
            Dict.Remove(oldWord); // удаление старого слова
        }

        public static void ReplaceTranslation() // замена перевода
        {
            Console.Write("Слово: ");
            string word = Console.ReadLine();

            if (!Dict.ContainsKey(word))
                return;

            Console.Write("Старый перевод: ");
            string oldTr = Console.ReadLine();

            int index = Dict[word].IndexOf(oldTr); // ищет номер перевода
             
            if (index >= 0)
            {
                Console.Write("Новый перевод: ");
                Dict[word][index] = Console.ReadLine(); // заменяем перевод
            }
        }

        public static void DeleteWord() // метод удаление слова
        {
            Console.Write("Слово: ");
            Dict.Remove(Console.ReadLine()); // удалении слов и переводов
        }

        public static void DeleteTranslation() // удаление перевода
        {
            Console.Write("Слово: ");
            string word = Console.ReadLine();

            if (!Dict.ContainsKey(word))
                return;

            if (Dict[word].Count == 1)
            {
                Console.WriteLine("Последний перевод удалить нельзя!");
                return;
            }

            Console.Write("Перевод: ");
            Dict[word].Remove(Console.ReadLine()); // удаление перевода
        }

        public static void FindWord() // поиск слова
        { 
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            if (Dict.ContainsKey(word))
                Console.WriteLine(string.Join(", ", Dict[word])); // выводим все переводы
            else
                Console.WriteLine("Слово не найдено.");
        }

        public static void ShowDictionary() // показ всего словаря
        {
            foreach (var item in Dict) // перебирание всех слов и переводов
            {
                Console.WriteLine(
                    item.Key + " = " +
                    string.Join(", ", item.Value));
            }
        }
    }
}
