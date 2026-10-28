using System; // базовые типы
using System.Data.Common; // DbConnection, DbCommand, DbDataReader, DbException
using System.Threading.Tasks; // Task для асинхронности

namespace ShopApp
{
    /// <summary>Задание 1. Присоединённый режим, конфигурация, асинхронность</summary>
    public static class Task1Connected
    {
        // Метод 1: добавление товара (синхронно, ExecuteNonQuery)
        public static void AddProduct(string name, int categoryId, decimal price, int stock)
        {
            DbConnection connection = null; // объявляем снаружи try, чтобы закрыть в finally
            try
            {
                connection = DbHelper.CreateConnection(); // получаем подключение через фабрику
                connection.Open(); // открываем соединение с БД
                const string sql = "INSERT INTO Products (Name, CategoryId, Price, Stock) VALUES (@Name, @CategoryId, @Price, @Stock)"; // параметризованный запрос
                using (DbCommand command = DbHelper.CreateCommand(sql, connection)) // создаём команду
                {
                    command.Parameters.Add(DbHelper.CreateParameter("@Name", name)); // параметр: название
                    command.Parameters.Add(DbHelper.CreateParameter("@CategoryId", categoryId)); // параметр: id категории
                    command.Parameters.Add(DbHelper.CreateParameter("@Price", price)); // параметр: цена
                    command.Parameters.Add(DbHelper.CreateParameter("@Stock", stock)); // параметр: остаток
                    int rows = command.ExecuteNonQuery(); // выполняем INSERT, получаем число добавленных строк
                    Console.WriteLine($"Товар добавлен. Затронуто строк: {rows}"); // сообщаем результат
                }
            }
            catch (DbException ex) // ошибки именно базы данных (нарушение FK, CHECK и т.п.)
            {
                Console.WriteLine("Ошибка БД при добавлении товара: " + ex.Message);
            }
            catch (Exception ex) // любые другие ошибки
            {
                Console.WriteLine("Непредвиденная ошибка при добавлении товара: " + ex.Message);
            }
            finally
            {
                if (connection != null) // если подключение создавалось
                {
                    connection.Close(); // закрываем (возвращаем в пул)
                    connection.Dispose(); // освобождаем ресурсы
                }
            }
        }

        // Метод 2: вывод списка товаров через JOIN и DbDataReader
        public static void PrintProducts()
        {
            DbConnection connection = null;
            try
            {
                connection = DbHelper.CreateConnection(); // подключение через фабрику
                connection.Open(); // открываем
                const string sql = @"SELECT c.Name AS CategoryName, p.Name, p.Price, p.Stock
                                     FROM Products p
                                     JOIN Categories c ON c.Id = p.CategoryId
                                     ORDER BY c.Name, p.Name"; // JOIN товаров с категориями
                using (DbCommand command = DbHelper.CreateCommand(sql, connection))
                using (DbDataReader reader = command.ExecuteReader()) // потоковое чтение строка за строкой
                {
                    while (reader.Read()) // переходим к следующей строке, пока они есть
                    {
                        string category = reader.GetString(0); // 1-я колонка: категория
                        string name = reader.GetString(1); // 2-я колонка: название товара
                        decimal price = reader.GetDecimal(2); // 3-я колонка: цена
                        int stock = reader.GetInt32(3); // 4-я колонка: остаток
                        Console.WriteLine($"[{category}] {name} - {price:0.00} (Остаток: {stock} шт.)"); // нужный формат вывода
                    }
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine("Ошибка БД при чтении товаров: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Непредвиденная ошибка при чтении товаров: " + ex.Message);
            }
            finally
            {
                if (connection != null) { connection.Close(); connection.Dispose(); } // всегда закрываем подключение
            }
        }

        // Асинхронный метод: остаток товара через ExecuteScalarAsync
        public static async Task<int> GetStockAsync(int productId)
        {
            DbConnection connection = null;
            try
            {
                connection = DbHelper.CreateConnection();
                await connection.OpenAsync(); // асинхронно открываем соединение (поток не блокируется)
                using (DbCommand command = DbHelper.CreateCommand("SELECT Stock FROM Products WHERE Id = @Id", connection))
                {
                    command.Parameters.Add(DbHelper.CreateParameter("@Id", productId)); // параметр: id товара
                    object result = await command.ExecuteScalarAsync(); // асинхронно получаем одно значение (первая ячейка)
                    if (result == null || result == DBNull.Value) // товар не найден
                        return -1; // -1 = «нет такого товара»
                    return Convert.ToInt32(result); // приводим результат к int
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine("Ошибка БД при получении остатка: " + ex.Message);
                return -1; // при ошибке возвращаем -1
            }
            catch (Exception ex)
            {
                Console.WriteLine("Непредвиденная ошибка при получении остатка: " + ex.Message);
                return -1;
            }
            finally
            {
                if (connection != null) { connection.Close(); connection.Dispose(); }
            }
        }

        // Демонстрация всех методов для скриншота
        public static async Task RunAsync()
        {
            Console.WriteLine("=== ЗАДАНИЕ 1: присоединённый режим ===");
            Console.WriteLine("Строка подключения из App.config: " + DbHelper.ConnectionString); // показываем, что строка прочитана из конфига
            AddProduct("Наушники Sony WF-1000XM5", 3, 24990m, 15); // добавляем новый товар
            Console.WriteLine("\nСписок товаров:");
            PrintProducts(); // выводим все товары
            int stock = await GetStockAsync(1); // асинхронно узнаём остаток товара с Id = 1
            Console.WriteLine($"\nОстаток товара Id=1: {stock} шт."); // выводим остаток
        }
    }
}
