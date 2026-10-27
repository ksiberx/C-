using System;
using System.Data;
using Microsoft.Data.SqlClient; // Используем только этот пакет
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace DapperMailingList
{
    // Классы-сущности для отображения данных
    public class Buyer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }

    public class PromoProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string CountryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    class Program
    {
        // Строка подключения (измените Server на ваш экземпляр MS SQL Server)
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MailingListDb;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задание 2: Тестирование подключения
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Успешное подключение к базе данных «Список рассылки»!\n");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                    Console.ResetColor();
                    return;
                }
            }

            // Главный цикл меню (Задания 3 и 4)
            while (true)
            {
                Console.WriteLine("\n--- Выберите действие ---");
                Console.WriteLine("1. Отобразить всех покупателей (Задание 3)");
                Console.WriteLine("2. Отобразить email всех покупателей (Задание 3)");
                Console.WriteLine("3. Отобразить список разделов (Задание 3)");
                Console.WriteLine("4. Отобразить список акционных товаров (Задание 3)");
                Console.WriteLine("5. Отобразить все города (Задание 3)");
                Console.WriteLine("6. Отобразить все страны (Задание 3)");
                Console.WriteLine("7. Покупатели из конкретного города (Задание 4)");
                Console.WriteLine("8. Покупатели из конкретной страны (Задание 4)");
                Console.WriteLine("9. Все акции для конкретной страны (Задание 4)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAllBuyers(); break;
                    case "2": ShowAllEmails(); break;
                    case "3": ShowAllCategories(); break;
                    case "4": ShowAllPromoProducts(); break;
                    case "5": ShowAllCities(); break;
                    case "6": ShowAllCountries(); break;
                    case "7": ShowBuyersByCity(); break;
                    case "8": ShowBuyersByCountry(); break;
                    case "9": ShowPromosByCountry(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный ввод. Попробуйте еще раз."); break;
                }
            }
        }

        // --- ЗАДАНИЕ 3 ---

        static void ShowAllBuyers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT b.Id, b.FullName, b.BirthDate, b.Gender, b.Email, ci.Name as CityName, co.Name as CountryName 
                               FROM Buyers b
                               JOIN Cities ci ON b.CityId = ci.Id
                               JOIN Countries co ON ci.CountryId = co.Id";

                var buyers = db.Query<Buyer>(sql);
                Console.WriteLine("{0,-5} {1,-25} {2,-12} {3,-10} {4,-20} {5,-15} {6,-10}", "ID", "ФИО", "Рождение", "Пол", "Email", "Город", "Страна");
                foreach (var b in buyers)
                {
                    Console.WriteLine("{0,-5} {1,-25} {2,-12:dd.MM.yyyy} {3,-10} {4,-20} {5,-15} {6,-10}", b.Id, b.FullName, b.BirthDate, b.Gender, b.Email, b.CityName, b.CountryName);
                }
            }
        }

        static void ShowAllEmails()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var emails = db.Query<string>("SELECT Email FROM Buyers");
                Console.WriteLine("Список Email покупателей:");
                foreach (var email in emails) Console.WriteLine($"- {email}");
            }
        }

        static void ShowAllCategories()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var categories = db.Query<string>("SELECT Name FROM Categories");
                Console.WriteLine("Список разделов (категорий):");
                foreach (var cat in categories) Console.WriteLine($"- {cat}");
            }
        }

        static void ShowAllPromoProducts()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT p.Id, p.Name, c.Name as CategoryName, co.Name as CountryName, p.StartDate, p.EndDate 
                               FROM PromoProducts p
                               JOIN Categories c ON p.CategoryId = c.Id
                               JOIN Countries co ON p.CountryId = co.Id";

                var products = db.Query<PromoProduct>(sql);
                foreach (var p in products)
                {
                    Console.WriteLine($"Товар: {p.Name} | Раздел: {p.CategoryName} | Страна: {p.CountryName} | Сроки: {p.StartDate:dd.MM.yyyy} - {p.EndDate:dd.MM.yyyy}");
                }
            }
        }

        static void ShowAllCities()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var cities = db.Query<string>("SELECT Name FROM Cities");
                Console.WriteLine("Список всех городов:");
                foreach (var city in cities) Console.WriteLine($"- {city}");
            }
        }

        static void ShowAllCountries()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var countries = db.Query<string>("SELECT Name FROM Countries");
                Console.WriteLine("Список всех стран:");
                foreach (var country in countries) Console.WriteLine($"- {country}");
            }
        }

        // --- ЗАДАНИЕ 4 ---

        static void ShowBuyersByCity()
        {
            Console.Write("Введите название города: ");
            string city = Console.ReadLine();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT b.Id, b.FullName, b.BirthDate, b.Gender, b.Email, ci.Name as CityName, co.Name as CountryName 
                               FROM Buyers b
                               JOIN Cities ci ON b.CityId = ci.Id
                               JOIN Countries co ON ci.CountryId = co.Id
                               WHERE ci.Name = @CityName";

                var buyers = db.Query<Buyer>(sql, new { CityName = city });

                if (!buyers.Any()) { Console.WriteLine("Покупатели в этом городе не найдены."); return; }

                foreach (var b in buyers) Console.WriteLine($"- {b.FullName} ({b.Email}), Город: {b.CityName}");
            }
        }

        static void ShowBuyersByCountry()
        {
            Console.Write("Введите название страны: ");
            string country = Console.ReadLine();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT b.Id, b.FullName, b.BirthDate, b.Gender, b.Email, ci.Name as CityName, co.Name as CountryName 
                               FROM Buyers b
                               JOIN Cities ci ON b.CityId = ci.Id
                               JOIN Countries co ON ci.CountryId = co.Id
                               WHERE co.Name = @CountryName";

                var buyers = db.Query<Buyer>(sql, new { CountryName = country });

                if (!buyers.Any()) { Console.WriteLine("Покупатели в этой стране не найдены."); return; }

                foreach (var b in buyers) Console.WriteLine($"- {b.FullName} ({b.Email}), Страна: {b.CountryName}");
            }
        }

        static void ShowPromosByCountry()
        {
            Console.Write("Введите название страны для поиска акций: ");
            string country = Console.ReadLine();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT p.Id, p.Name, c.Name as CategoryName, co.Name as CountryName, p.StartDate, p.EndDate 
                               FROM PromoProducts p
                               JOIN Categories c ON p.CategoryId = c.Id
                               JOIN Countries co ON p.CountryId = co.Id
                               WHERE co.Name = @CountryName";

                var products = db.Query<PromoProduct>(sql, new { CountryName = country });

                if (!products.Any()) { Console.WriteLine("Акции для этой страны не найдены."); return; }
            }
        }
    }
}


