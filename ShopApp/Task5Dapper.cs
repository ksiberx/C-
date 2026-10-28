using System; // базовые типы
using System.Collections.Generic; // List
using System.Data.Common; // DbConnection, DbException
using System.Linq; // ToList
using Dapper; // методы расширения Query<T>, Execute и т.д.

namespace ShopApp
{
    // DTO: плоский класс под результат запроса (имена свойств = имена колонок)
    public class SleepingCustomerDto
    {
        public string FullName { get; set; } // ФИО клиента
        public string Email { get; set; } // email клиента
        public DateTime RegistrationDate { get; set; } // дата регистрации
    }

    /// <summary>Задание 5. Dapper: «спящие» клиенты</summary>
    public static class Task5Dapper
    {
        public static List<SleepingCustomerDto> GetSleepingCustomers()
        {
            DbConnection connection = null;
            try
            {
                connection = DbHelper.CreateConnection(); // подключение (Dapper работает с любым IDbConnection)
                const string sql = @"SELECT c.FullName, c.Email, c.RegistrationDate
                                     FROM Customers c
                                     LEFT JOIN Orders o ON o.CustomerId = c.Id
                                     WHERE o.Id IS NULL AND c.RegistrationDate < @Border
                                     ORDER BY c.RegistrationDate"; // клиенты БЕЗ заказов, зарегистрированные раньше граничной даты
                DateTime border = DateTime.Now.AddMonths(-3); // граница: 3 месяца назад
                return connection.Query<SleepingCustomerDto>(sql, new { Border = border }).ToList(); // Dapper сам откроет соединение, подставит параметр и смаппит строки в DTO
            }
            catch (DbException ex)
            {
                Console.WriteLine("Ошибка БД: " + ex.Message);
                return new List<SleepingCustomerDto>(); // при ошибке возвращаем пустой список
            }
            catch (Exception ex)
            {
                Console.WriteLine("Непредвиденная ошибка: " + ex.Message);
                return new List<SleepingCustomerDto>();
            }
            finally
            {
                if (connection != null) { connection.Close(); connection.Dispose(); } // закрываем подключение
            }
        }

        public static void Run()
        {
            Console.WriteLine("=== ЗАДАНИЕ 5: Dapper ===");
            List<SleepingCustomerDto> customers = GetSleepingCustomers(); // получаем список «спящих» клиентов
            Console.WriteLine($"«Спящие» клиенты (регистрация > 3 мес. назад, заказов нет): {customers.Count}");
            foreach (SleepingCustomerDto c in customers) // выводим каждого
                Console.WriteLine($"{c.FullName} | {c.Email} | зарегистрирован {c.RegistrationDate:dd.MM.yyyy}");
        }
    }
}
