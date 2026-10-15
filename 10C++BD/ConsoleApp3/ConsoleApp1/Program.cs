using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public int Age { get; set; }
    public double GPA { get; set; } 
    public int PassedCredits { get; set; } 

    public override string ToString()
    {
        return $"{Name,-10} | {Group,-6} | {Age,2} | {GPA,4:F1} | {PassedCredits,2}";
    }
}

class Program
{
    private static List<Student> GetStudents()
    {
        return new List<Student>
        {
            new Student { Name = "Алексей", Group = "ИТ-41", Age = 20, GPA = 4.2, PassedCredits = 40 },
            new Student { Name = "Борис", Group = "ИТ-41", Age = 19, GPA = 3.1, PassedCredits = 35 },
            new Student { Name = "Виктория", Group = "ПИ-42", Age = 21, GPA = 4.8, PassedCredits = 45 },
            new Student { Name = "Григорий", Group = "ИБ-43", Age = 20, GPA = 2.9, PassedCredits = 20 },
            new Student { Name = "Дарья", Group = "ИТ-41", Age = 22, GPA = 4.5, PassedCredits = 42 },
            new Student { Name = "Елена", Group = "ПИ-42", Age = 20, GPA = 3.8, PassedCredits = 38 },
            new Student { Name = "Жанна", Group = "ИБ-43", Age = 23, GPA = 4.9, PassedCredits = 48 },
            new Student { Name = "Игорь", Group = "ИТ-41", Age = 19, GPA = 2.5, PassedCredits = 15 }
        };
    }

    static void Main(string[] args)
    {

        Task1_QuerySyntax();
        Console.WriteLine();

        Task2_MethodSyntax();
        Console.WriteLine();

        Task3_Aggregation();
        Console.WriteLine();

        Task4_Xml();

        Console.WriteLine("\nНажмите Enter для выхода...");
        Console.ReadLine();
    }

    // Задание 1
    static void Task1_QuerySyntax()
    {
        List<Student> students = GetStudents();

        Console.WriteLine("--- Задание 1: Студенты ИТ-41 с баллом > 3.5 ---");
        Console.WriteLine("Имя        | Группа | Возраст | Балл | Зачеты");

        var result = from s in students
                     where s.Group == "ИТ-41" && s.GPA > 3.5
                     orderby s.Age
                     select s;

        foreach (var student in result)
        {
            Console.WriteLine(student.ToString());
        }
    }

    // Задание 2
    static void Task2_MethodSyntax()
    {
        List<Student> students = GetStudents();

        Console.WriteLine("--- Задание 2: Стипендии (синтаксис методов) ---");

        var result = students
            .Select(s => new
            {
                FullName = s.Name,
                Scholarship = s.GPA >= 4.7 ? "Ректорская" :
                              s.GPA >= 4.5 ? "Повышенная" :
                              s.GPA >= 4.0 ? "Стандартная" : "Нет",
                GPA = s.GPA
            })
            .OrderByDescending(s => s.GPA);

        foreach (var item in result)
        {
            if (item.Scholarship != "Нет")
            {
                Console.WriteLine($"{item.FullName}: {item.Scholarship}");
            }
        }
    }

    // Задание 3
    static void Task3_Aggregation()
    {
        List<Student> students = GetStudents();

        Console.WriteLine("--- Задание 3: Статистика ---");

        // Часть А
        var groupedGroups = students
            .GroupBy(s => s.Group)
            .Select(g => new
            {
                GroupName = g.Key,
                AverageGpa = g.Average(s => s.GPA)
            })
            .OrderBy(g => g.GroupName);

        foreach (var group in groupedGroups)
        {
            Console.WriteLine($"Группа {group.GroupName}: Средний балл {group.AverageGpa:F2}");
        }

        // Часть Б
        var oldestStudentWith45Credits = students
            .Where(s => s.PassedCredits == 45)
            .OrderByDescending(s => s.Age)
            .FirstOrDefault();

        if (oldestStudentWith45Credits != null)
        {
            Console.WriteLine($"Студент с 45 зачетами: {oldestStudentWith45Credits.Name} ({oldestStudentWith45Credits.Age} год)");
        }
    }

    // Задание 4
    static void Task4_Xml()
    {
        string fileName = "uni.xml";

        // Часть А
        XDocument xmlDoc = new XDocument(
            new XElement("university",
                new XElement("course", new XAttribute("name", "Программирование"),
                    new XElement("student", new XAttribute("name", "Кирилл"), new XAttribute("gpa", "3.9")),
                    new XElement("student", new XAttribute("name", "Лариса"), new XAttribute("gpa", "4.7")),
                    new XElement("student", new XAttribute("name", "Михаил"), new XAttribute("gpa", "3.2")),
                    new XElement("student", new XAttribute("name", "Наталья"), new XAttribute("gpa", "4.1"))
                )
            )
        );

        xmlDoc.Save(fileName); 

        // Часть Б
        Console.WriteLine("--- Задание 4: Отличники из XML ---");

        XDocument loadedDoc = XDocument.Load(fileName);

        var excellentStudents = from s in loadedDoc.Descendants("student")
                                where double.TryParse(s.Attribute("gpa")?.Value, out double gpa) && gpa > 4.0
                                select s.Attribute("name")?.Value;

        foreach (var name in excellentStudents)
        {
            Console.WriteLine(name);
        }
    }
}
