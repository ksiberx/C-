using System; // базовые типы
using System.Configuration; // доступ к App.config
using System.Data; // DbType, IDbDataParameter
using System.Data.Common; // абстрактные классы ADO.NET и DbProviderFactories

namespace ShopApp
{
    /// <summary>Класс-помощник: создаёт объекты БД через фабрику провайдеров (Задание 1, п.1-2)</summary>
    public static class DbHelper
    {
        private const string ProviderName = "System.Data.SqlClient"; // инвариантное имя провайдера (можно заменить на другой)

        public static readonly string ConnectionString = ReadConnectionString(); // строка подключения, прочитанная при запуске приложения

        private static readonly DbProviderFactory Factory = DbProviderFactories.GetFactory(ProviderName); // фабрика: умеет создавать подключения, команды, адаптеры

        private static string ReadConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ShopDb"]; // ищем строку "ShopDb" в App.config
            if (settings == null) // если её нет - сообщаем понятную ошибку
                throw new ConfigurationErrorsException("В App.config не найдена строка подключения 'ShopDb'.");
            return settings.ConnectionString; // возвращаем текст строки подключения
        }

        public static DbConnection CreateConnection()
        {
            DbConnection connection = Factory.CreateConnection(); // абстрактное подключение (на деле SqlConnection)
            connection.ConnectionString = ConnectionString; // задаём строку подключения
            return connection; // возвращаем ЗАКРЫТОЕ подключение, открывает вызывающий код
        }

        public static DbCommand CreateCommand(string sql, DbConnection connection)
        {
            DbCommand command = connection.CreateCommand(); // создаём команду, привязанную к подключению
            command.CommandText = sql; // текст SQL-запроса (с параметрами @Name)
            return command; // возвращаем команду
        }

        public static DbParameter CreateParameter(string name, object value)
        {
            DbParameter parameter = Factory.CreateParameter(); // создаём параметр через фабрику
            parameter.ParameterName = name; // имя параметра, например @Price
            parameter.Value = value ?? DBNull.Value; // значение (null превращаем в DBNull для SQL)
            if (value is decimal) // для decimal обязательно задаём точность и масштаб,
            {
                var p = (IDbDataParameter)parameter; // иначе дробная часть может быть отброшена
                p.Precision = 18; // всего цифр
                p.Scale = 2; // цифр после запятой
            }
            return parameter; // готовый параметр
        }

        public static DbParameter CreateParameter(string name, DbType type, string sourceColumn, DataRowVersion version = DataRowVersion.Current)
        {
            DbParameter parameter = Factory.CreateParameter(); // создаём параметр для DataAdapter
            parameter.ParameterName = name; // имя параметра в SQL
            parameter.DbType = type; // тип данных
            parameter.SourceColumn = sourceColumn; // из какой колонки DataTable брать значение
            parameter.SourceVersion = version; // какую версию значения брать (текущую или исходную)
            if (type == DbType.Decimal) // для decimal задаём точность и масштаб
            {
                var p = (IDbDataParameter)parameter;
                p.Precision = 18;
                p.Scale = 2;
            }
            return parameter; // готовый параметр
        }

        public static DbDataAdapter CreateAdapter()
        {
            return Factory.CreateDataAdapter(); // адаптер для отсоединённого режима (Задание 2)
        }
    }
}
