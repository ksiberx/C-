using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FilePracticalWork
{
    // вариант 1
    public struct Student
    {
        public string Name;
        public int BirthYear;
        public string Address;
        public double AverageScore;
    }

    // вариант 2
    public struct Book
    {
        public string Title;
        public string Author;
        public int PublishYear;
        public string PublishPlace;
        public string InventoryNumber;
    }

    // вариант 17
    public struct EgeResult
    {
        public string Name;
        public string Subject;
        public int Score;
    }

    // вариант 1
    public class StudentManager
    {
        private string binPath = "students.bin";

        public void CreateAndFillTestData()
        {
            Student[] testStudents = new Student[]
            {
                new Student { Name = "Иванов И.И.", BirthYear = 2004, Address = "ул. Ленина 10", AverageScore = 4.5 },
                new Student { Name = "Петров П.П.", BirthYear = 2005, Address = "ул. Мира 5", AverageScore = 3.6 },
                new Student { Name = "Сидорова А.В.", BirthYear = 2004, Address = "ул. Новая 12", AverageScore = 3.9 }
            };

            using (BinaryWriter writer = new BinaryWriter(File.Open(binPath, FileMode.Create)))
            {
                foreach (var s in testStudents)
                {
                    writer.Write(s.Name);
                    writer.Write(s.BirthYear);
                    writer.Write(s.Address);
                    writer.Write(s.AverageScore);
                }
            }
        }

        public void DisplayInGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("Name", "ФИО");
            dgv.Columns.Add("BirthYear", "Год рождения");
            dgv.Columns.Add("Address", "Адрес");
            dgv.Columns.Add("AverageScore", "Средний балл");

            if (!File.Exists(binPath)) return;

            using (BinaryReader reader = new BinaryReader(File.Open(binPath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Student s;
                    s.Name = reader.ReadString();
                    s.BirthYear = reader.ReadInt32();
                    s.Address = reader.ReadString();
                    s.AverageScore = reader.ReadDouble();

                    dgv.Rows.Add(s.Name, s.BirthYear, s.Address, s.AverageScore);
                }
            }
        }

        public void FilterAndSaveToText(string txtPath)
        {
            if (!File.Exists(binPath)) return;

            using (BinaryReader reader = new BinaryReader(File.Open(binPath, FileMode.Open)))
            using (StreamWriter writer = new StreamWriter(File.Open(txtPath, FileMode.Create), false, Encoding.UTF8))
            {
                writer.WriteLine("Студенты со средним баллом > 3.8:");
                writer.WriteLine("----------------------------------------");

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Student s;
                    s.Name = reader.ReadString();
                    s.BirthYear = reader.ReadInt32();
                    s.Address = reader.ReadString();
                    s.AverageScore = reader.ReadDouble();

                    if (s.AverageScore > 3.8)
                    {
                        writer.WriteLine($"ФИО: {s.Name} | Год: {s.BirthYear} | Адрес: {s.Address} | Балл: {s.AverageScore}");
                    }
                }
            }
        }
    }

    // вариант 2
    public class BookManager
    {
        private string binPath = "books.bin";

        public void CreateAndFillTestData()
        {
            Book[] testBooks = new Book[]
            {
                new Book { Title = "Чистый код", Author = "Р. Мартин", PublishYear = 2019, PublishPlace = "Санкт-Петербург", InventoryNumber = "B001" },
                new Book { Title = "Совершенный код", Author = "С. Макконнелл", PublishYear = 2017, PublishPlace = "Москва", InventoryNumber = "B002" },
                new Book { Title = "Паттерны проектирования", Author = "Э. Фримен", PublishYear = 2021, PublishPlace = "Санкт-Петербург", InventoryNumber = "B003" }
            };

            using (BinaryWriter writer = new BinaryWriter(File.Open(binPath, FileMode.Create)))
            {
                foreach (var b in testBooks)
                {
                    writer.Write(b.Title);
                    writer.Write(b.Author);
                    writer.Write(b.PublishYear);
                    writer.Write(b.PublishPlace);
                    writer.Write(b.InventoryNumber);
                }
            }
        }

        public void DisplayInGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("Title", "Название");
            dgv.Columns.Add("Author", "Автор");
            dgv.Columns.Add("PublishYear", "Год издания");
            dgv.Columns.Add("PublishPlace", "Место издания");
            dgv.Columns.Add("InvNum", "Инв. номер");

            if (!File.Exists(binPath)) return;

            using (BinaryReader reader = new BinaryReader(File.Open(binPath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Book b;
                    b.Title = reader.ReadString();
                    b.Author = reader.ReadString();
                    b.PublishYear = reader.ReadInt32();
                    b.PublishPlace = reader.ReadString();
                    b.InventoryNumber = reader.ReadString();

                    dgv.Rows.Add(b.Title, b.Author, b.PublishYear, b.PublishPlace, b.InventoryNumber);
                }
            }
        }

        public void FilterAndSaveToText(string txtPath)
        {
            if (!File.Exists(binPath)) return;

            using (BinaryReader reader = new BinaryReader(File.Open(binPath, FileMode.Open)))
            using (StreamWriter writer = new StreamWriter(File.Open(txtPath, FileMode.Create), false, Encoding.UTF8))
            {
                writer.WriteLine("Книги, изданные в Санкт-Петербурге:");
                writer.WriteLine("----------------------------------------");

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Book b;
                    b.Title = reader.ReadString();
                    b.Author = reader.ReadString();
                    b.PublishYear = reader.ReadInt32();
                    b.PublishPlace = reader.ReadString();
                    b.InventoryNumber = reader.ReadString();

                    if (b.PublishPlace.Equals("Санкт-Петербург", StringComparison.OrdinalIgnoreCase))
                    {
                        writer.WriteLine($"Книга: {b.Title} | Автор: {b.Author} | Год: {b.PublishYear} | Инв.№: {b.InventoryNumber}");
                    }
                }
            }
        }
    }

    // вариант 17
    public class EgeManager
    {
        private string binPath = "ege_results.bin";

        public void CreateAndFillTestData()
        {
            EgeResult[] testResults = new EgeResult[]
            {
                new EgeResult { Name = "Смирнов А.Д.", Subject = "Информатика", Score = 92 },
                new EgeResult { Name = "Козлов К.М.", Subject = "Математика", Score = 74 },
                new EgeResult { Name = "Морозова Е.А.", Subject = "Русский язык", Score = 85 }
            };

            using (BinaryWriter writer = new BinaryWriter(File.Open(binPath, FileMode.Create)))
            {
                foreach (var r in testResults)
                {
                    writer.Write(r.Name);
                    writer.Write(r.Subject);
                    writer.Write(r.Score);
                }
            }
        }

        public void DisplayInGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("Name", "ФИО");
            dgv.Columns.Add("Subject", "Предмет");
            dgv.Columns.Add("Score", "Балл");

            if (!File.Exists(binPath)) return;

            using (BinaryReader reader = new BinaryReader(File.Open(binPath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    EgeResult r;
                    r.Name = reader.ReadString();
                    r.Subject = reader.ReadString();
                    r.Score = reader.ReadInt32();

                    dgv.Rows.Add(r.Name, r.Subject, r.Score);
                }
            }
        }

        public void FilterAndSaveToText(string txtPath)
        {
            if (!File.Exists(binPath)) return;

            using (BinaryReader reader = new BinaryReader(File.Open(binPath, FileMode.Open)))
            using (StreamWriter writer = new StreamWriter(File.Open(txtPath, FileMode.Create), false, Encoding.UTF8))
            {
                writer.WriteLine("Результаты ЕГЭ (Балл > 80):");
                writer.WriteLine("----------------------------------------");

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    EgeResult r;
                    r.Name = reader.ReadString();
                    r.Subject = reader.ReadString();
                    r.Score = reader.ReadInt32();

                    if (r.Score > 80)
                    {
                        writer.WriteLine($"Ученик: {r.Name} | Предмет: {r.Subject} | Балл: {r.Score}");
                    }
                }
            }
        }
    }
}
