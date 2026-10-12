using Microsoft.Data.SqlClient; 
using System;
using System.Data.SqlClient;

class Program
{
    static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WarehouseDb;Integrated Security=True";

    static void Main()
    {

        using (SqlConnection conn = new SqlConnection(connString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("Успешное подключение к базе данных «Склад»!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                return;
            }

            PrintQuery(conn, "1. Вся информация о товарах:", "SELECT p.Name, t.Name as Type, s.Name as Supplier, p.Quantity, p.Cost, p.SupplyDate FROM Products p JOIN Types t ON p.TypeId = t.Id JOIN Suppliers s ON p.SupplierId = s.Id");
            PrintQuery(conn, "2. Все типы товаров:", "SELECT Name FROM Types");
            PrintQuery(conn, "3. Все поставщики:", "SELECT Name FROM Suppliers");
            PrintQuery(conn, "4. Товар с максимальным количеством:", "SELECT TOP 1 Name, Quantity FROM Products ORDER BY Quantity DESC");
            PrintQuery(conn, "5. Товар с минимальным количеством:", "SELECT TOP 1 Name, Quantity FROM Products ORDER BY Quantity ASC");
            PrintQuery(conn, "6. Товар с минимальной себестоимостью:", "SELECT TOP 1 Name, Cost FROM Products ORDER BY Cost ASC");
            PrintQuery(conn, "7. Товар с максимальной себестоимостью:", "SELECT TOP 1 Name, Cost FROM Products ORDER BY Cost DESC");

            PrintQuery(conn, "8. Товары категории 'Электроника':", "SELECT p.Name FROM Products p JOIN Types t ON p.TypeId = t.Id WHERE t.Name = @p1", "Электроника");
            PrintQuery(conn, "9. Товары поставщика 'ООО Техно':", "SELECT p.Name FROM Products p JOIN Suppliers s ON p.SupplierId = s.Id WHERE s.Name = @p1", "ООО Техно");
            PrintQuery(conn, "10. Самый старый товар на складе:", "SELECT TOP 1 Name, SupplyDate FROM Products ORDER BY SupplyDate ASC");
            PrintQuery(conn, "11. Среднее количество товаров по каждому типу:", "SELECT t.Name, AVG(p.Quantity) FROM Products p JOIN Types t ON p.TypeId = t.Id GROUP BY t.Name");

            Console.WriteLine("Отключение от базы данных выполнено автоматически.");
        }
    }

    static void PrintQuery(SqlConnection conn, string title, string query, string param = null)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', 40));

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (param != null) cmd.Parameters.AddWithValue("@p1", param);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                        row += $"{reader.GetName(i)}: {reader[i]} | ";
                    Console.WriteLine(row.TrimEnd(' ', '|'));
                }
            }
        }
        Console.WriteLine();
    }
}
