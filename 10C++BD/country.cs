
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static SqlConnection connection;

    static string connectionString =
        @"Server=.\SQLEXPRESS;Database=Strany;Integrated Security=True;TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nБАЗА ДАННЫХ СТРАНЫ");
            Console.WriteLine("1. Подключиться");
            Console.WriteLine("2. Отключиться");
            Console.WriteLine("3. Вся информация о странах");
            Console.WriteLine("4. Частичная информация о странах");
            Console.WriteLine("5. Информация о стране");
            Console.WriteLine("6. Города страны");
            Console.WriteLine("7. Страны на указанную букву");
            Console.WriteLine("8. Столицы на указанную букву");
            Console.WriteLine("9. Топ-3 столицы с наименьшим населением");
            Console.WriteLine("10. Топ-3 стран с наименьшим населением");
            Console.WriteLine("11. Среднее население столиц по частям света");
            Console.WriteLine("12. Топ-3 наименее населённых страны по частям света");
            Console.WriteLine("13. Топ-3 наиболее населённых страны по частям света");
            Console.WriteLine("14. Среднее население городов страны");
            Console.WriteLine("15. Самый малонаселённый город страны");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Connect();
                        break;
                    case "2":
                        Disconnect();
                        break;
                    case "3":
                        Report("Вся информация о странах",
                            @"SELECT c.Name AS Страна, c.Population AS Население,
                              c.Area AS Площадь, c.Continent AS ЧастьСвета,
                              cap.Name AS Столица, cap.Population AS НаселениеСтолицы
                              FROM Countries c
                              LEFT JOIN Capitals cap ON cap.CountryId = c.Id");
                        break;
                    case "4":
                        PartialReport();
                        break;
                    case "5":
                        CountryReport();
                        break;
                    case "6":
                        CitiesReport();
                        break;
                    case "7":
                        string countryLetter = Ask("Введите первую букву страны: ");
                        Report("Страны на букву " + countryLetter,
                            "SELECT Name FROM Countries WHERE Name LIKE @letter",
                            new SqlParameter("@letter", countryLetter + "%"));
                        break;
                    case "8":
                        string capitalLetter = Ask("Введите первую букву столицы: ");
                        Report("Столицы на букву " + capitalLetter,
                            "SELECT Name, Population FROM Capitals WHERE Name LIKE @letter",
                            new SqlParameter("@letter", capitalLetter + "%"));
                        break;
                    case "9":
                        Report("Топ-3 столицы с наименьшим населением",
                            "SELECT TOP 3 Name, Population FROM Capitals ORDER BY Population");
                        break;
                    case "10":
                        Report("Топ-3 страны с наименьшим населением",
                            "SELECT TOP 3 Name, Population FROM Countries ORDER BY Population");
                        break;
                    case "11":
                        Report("Среднее население столиц по частям света",
                            @"SELECT c.Continent AS ЧастьСвета,
                              AVG(CAST(cap.Population AS FLOAT)) AS СреднееНаселение
                              FROM Countries c
                              JOIN Capitals cap ON cap.CountryId = c.Id
                              GROUP BY c.Continent");
                        break;
                    case "12":
                        Report("Топ-3 наименее населённых страны по частям света",
                            @"WITH T AS (
                                SELECT Name, Population, Continent,
                                ROW_NUMBER() OVER (
                                    PARTITION BY Continent ORDER BY Population
                                ) AS N
                                FROM Countries
                            )
                            SELECT Continent, Name, Population
                            FROM T WHERE N <= 3
                            ORDER BY Continent, Population");
                        break;
                    case "13":
                        Report("Топ-3 наиболее населённых страны по частям света",
                            @"WITH T AS (
                                SELECT Name, Population, Continent,
                                ROW_NUMBER() OVER (
                                    PARTITION BY Continent ORDER BY Population DESC
                                ) AS N
                                FROM Countries
                            )
                            SELECT Continent, Name, Population
                            FROM T WHERE N <= 3
                            ORDER BY Continent, Population DESC");
                        break;
                    case "14":
                        string country = Ask("Введите название страны: ");
                        Report("Среднее население городов страны",
                            @"SELECT c.Name AS Страна,
                              AVG(CAST(ci.Population AS FLOAT)) AS СреднееНаселение
                              FROM Countries c
                              JOIN Cities ci ON ci.CountryId = c.Id
                              WHERE c.Name = @name
                              GROUP BY c.Name",
                            new SqlParameter("@name", country));
                        break;
                    case "15":
                        string country2 = Ask("Введите название страны: ");
                        Report("Самый малонаселённый город страны",
                            @"SELECT TOP 1 c.Name AS Страна, ci.Name AS Город,
                              ci.Population AS Население
                              FROM Countries c
                              JOIN Cities ci ON ci.CountryId = c.Id
                              WHERE c.Name = @name
                              ORDER BY ci.Population",
                            new SqlParameter("@name", country2));
                        break;
                    case "0":
                        Disconnect();
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }

    static void Connect()
    {
        try
        {
            if (connection != null &&
                connection.State == ConnectionState.Open)
            {
                Console.WriteLine("Подключение уже установлено.");
                return;
            }

            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Подключение к базе данных успешно.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Не удалось подключиться: " + ex.Message);
        }
    }

    static void Disconnect()
    {
        if (connection != null &&
            connection.State == ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Соединение закрыто.");
        }
        else
        {
            Console.WriteLine("Соединение уже закрыто.");
        }
    }

    static void Report(string title, string sql,
        params SqlParameter[] parameters)
    {
        if (connection == null ||
            connection.State != ConnectionState.Open)
        {
            Console.WriteLine("Сначала подключитесь к базе данных.");
            return;
        }

        using (SqlCommand cmd = new SqlCommand(sql, connection))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                Console.WriteLine("\n" + title);

                if (table.Rows.Count == 0)
                {
                    Console.WriteLine("Данные не найдены.");
                    return;
                }

                foreach (DataColumn column in table.Columns)
                    Console.Write(column.ColumnName + "\t");
                Console.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    foreach (var value in row.ItemArray)
                        Console.Write((value == DBNull.Value ? "" : value) + "\t");
                    Console.WriteLine();
                }

                Console.Write("\nВывод: 1 — экран, 2 — файл: ");
                string output = Console.ReadLine();

                if (output == "2")
                {
                    string path = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "report.txt");

                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.WriteLine(title);

                        foreach (DataColumn column in table.Columns)
                            writer.Write(column.ColumnName + "\t");
                        writer.WriteLine();

                        foreach (DataRow row in table.Rows)
                        {
                            foreach (var value in row.ItemArray)
                                writer.Write((value == DBNull.Value ? "" : value) + "\t");
                            writer.WriteLine();
                        }
                    }

                    Console.WriteLine("Отчёт сохранён: " + path);
                }
            }
        }
    }

    static void PartialReport()
    {
        Console.WriteLine("Выберите информацию:");
        Console.WriteLine("1. Название");
        Console.WriteLine("2. Название и население");
        Console.WriteLine("3. Название и площадь");
        Console.WriteLine("4. Название и часть света");

        string choice = Ask("Выбор: ");
        string columns;

        switch (choice)
        {
            case "1": columns = "Name"; break;
            case "2": columns = "Name, Population"; break;
            case "3": columns = "Name, Area"; break;
            case "4": columns = "Name, Continent"; break;
            default:
                Console.WriteLine("Неверный пункт.");
                return;
        }

        Report("Частичная информация о странах",
            "SELECT " + columns + " FROM Countries");
    }

    static void CountryReport()
    {
        string name = Ask("Введите название страны: ");

        Report("Информация о стране",
            @"SELECT c.Name AS Страна, c.Population AS Население,
              c.Area AS Площадь, c.Continent AS ЧастьСвета,
              cap.Name AS Столица, cap.Population AS НаселениеСтолицы
              FROM Countries c
              LEFT JOIN Capitals cap ON cap.CountryId = c.Id
              WHERE c.Name = @name",
            new SqlParameter("@name", name));
    }

    static void CitiesReport()
    {
        string name = Ask("Введите название страны: ");

        Report("Города страны",
            @"SELECT ci.Name AS Город, ci.Population AS Население
              FROM Cities ci
              JOIN Countries c ON c.Id = ci.CountryId
              WHERE c.Name = @name",
            new SqlParameter("@name", name));
    }

    static string Ask(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}
