using System; 
using System.Collections.Generic; 

namespace ConsoleApp4 
{
    internal static class DictionaryManager 
    {
        public static Dictionary<string, List<string>> Dict =
        new Dictionary<string, List<string>>(); // для добавление двух и более переводов

        public static string FileName = ""; // имя текущего файла словаря

        public static void Clear() 
        {
            Dict.Clear(); 
        }

        public static void AddWord() // добавление слова
        {
            Console.Write("Слово: "); 
            string word = Console.ReadLine(); 

            Console.Write("Перевод: "); 
            string tr = Console.ReadLine(); 

            try
            {
                Dict[word].Add(tr); // добавляем перевод к существующему слову
            }
            catch
            {
                Dict[word] = new List<string>(); // создаём новый список переводов
                Dict[word].Add(tr); // добавляем первый перевод
            }

            Console.WriteLine("Добавлено."); 
        }

        public static void ReplaceWord() // замена слова
        {
            Console.Write("Старое слово: "); 
            string oldWord = Console.ReadLine(); 

            try
            {
                Console.Write("Новое слово: "); 
                string newWord = Console.ReadLine(); 

                Dict[newWord] = Dict[oldWord]; // переносим переводы на новое слово

                Dict.Remove(oldWord); // удаляем старое слово

                Console.WriteLine("Слово изменено."); 
            }
            catch
            {
                Console.WriteLine("Слово не найдено."); 
            }
        }

        public static void ReplaceTranslation() // замена перевода
        {
            Console.Write("Слово: "); 
            string word = Console.ReadLine(); 

            try
            {
                Console.Write("Старый перевод: "); 
                string oldTr = Console.ReadLine(); 

                int index = Dict[word].IndexOf(oldTr); // ищем индекс перевода

                if (index >= 0) // если перевод найден
                {
                    Console.Write("Новый перевод: "); 
                    Dict[word][index] = Console.ReadLine();

                    Console.WriteLine("Перевод изменён.");
                }
                else
                {
                    Console.WriteLine("Перевод не найден."); 
                }
            }
            catch
            {
                Console.WriteLine("Слово не найдено."); 
            }
        }

        public static void DeleteWord() // удаление слова
        {
            Console.Write("Слово: "); 
            string word = Console.ReadLine(); 

            try
            {
                if (Dict.Remove(word)) // пытаемся удалить слово
                    Console.WriteLine("Слово удалено."); 
                else
                    Console.WriteLine("Слово не найдено."); 
            }
            catch
            {
                Console.WriteLine("Ошибка удаления."); 
            }
        }

        public static void DeleteTranslation() // удаление перевода
        {
            Console.Write("Слово: "); 
            string word = Console.ReadLine(); 

            try
            {
                if (Dict[word].Count == 1) // если перевод всего один
                {
                    Console.WriteLine("Последний перевод удалить нельзя!"); // защита от удаления
                    return; 
                }

                Console.Write("Перевод: "); 
                string tr = Console.ReadLine(); 

                Dict[word].Remove(tr); 

                Console.WriteLine("Перевод удалён."); 
            }
            catch
            {
                Console.WriteLine("Слово не найдено."); 
            }
        }

        public static void FindWord() // поиск перевода
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine(); 

            try
            {
                Console.WriteLine("Переводы:"); 

                Console.WriteLine(
                    string.Join(", ", Dict[word])); // вывод всех переводов через запятую
            }
            catch
            {
                Console.WriteLine("Слово не найдено."); 
            }
        }

        public static void ShowDictionary() // показать весь словарь
        {
            try
            {
                if (Dict.Count == 0) // если словарь пуст
                {
                    Console.WriteLine("Словарь пуст."); 
                    return; 
                }

                foreach (var item in Dict) 
                {
                    Console.WriteLine(
                        item.Key + " = " + // слово
                        string.Join(", ", item.Value)); // список переводов
                }
            }
            catch
            {
                Console.WriteLine("Ошибка вывода словаря."); 
            }
        }
    }
}
