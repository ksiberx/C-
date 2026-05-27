using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек. А это веселая птица-синица, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";
        
        // Очищаем от знаков препинания и приводим к нижнему регистру
        string cleanText = Regex.Replace(text, @"[^\w\s-]", "");
        string[] words = cleanText.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        
        // Считаем частоту слов
        Dictionary<string, int> stats = new Dictionary<string, int>();
        
        foreach (string word in words)
        {
            string lowerWord = word.ToLower();
            if (stats.ContainsKey(lowerWord))
                stats[lowerWord]++;
            else
                stats[lowerWord] = 1;
        }
        
        // Вывод таблицы
        Console.WriteLine("Статистика текста:");
        Console.WriteLine($"{"Слово",-20} {"Кол-во",5}");
        Console.WriteLine(new string('-', 27));
        
        foreach (var item in stats)
        {
            Console.WriteLine($"{item.Key,-20} {item.Value,5}");
        }
        
        Console.WriteLine(new string('-', 27));
        Console.WriteLine($"Всего слов: {words.Length}, из них уникальных: {stats.Count}");
        Console.ReadKey();
    }
}
