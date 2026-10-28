using System; // базовые типы
using System.Collections.Generic; // List
using System.Data; // DataTable, DataRow, DbType, DataRowState
using System.Data.Common; // DbDataAdapter, DbConnection, DbException

namespace ShopApp
{
    /// <summary>Задание 2. Отсоединённый режим: DataTable + DbDataAdapter</summary>
    public static class Task2Disconnected
    {
        // Создаём адаптер и вручную настраиваем все 4 команды (без CommandBuilder)
        private static DbDataAdapter CreateAdapter(DbConnection connection)
        {
            DbDataAdapter adapter = DbHelper.CreateAdapter(); // адаптер через фабрику провайдеров

            adapter.SelectCommand = DbHelper.CreateCommand("SELECT Id, Name, CategoryId, Price, Stock FROM Products", connection); // SELECT: чем заполняем таблицу

            DbCommand insert = DbHelper.CreateCommand(
                "INSERT INTO Products (Name, CategoryId, Price, Stock) VALUES (@Name, @CategoryId, @Price, @Stock)", connection); // INSERT для новых строк (Id генерирует БД)
            insert.Parameters.Add(DbHelper.CreateParameter("@Name", DbType.String, "Name")); // @Name берётся из колонки Name
            insert.Parameters.Add(DbHelper.CreateParameter("@CategoryId", DbType.Int32, "CategoryId")); // из колонки CategoryId
            insert.Parameters.Add(DbHelper.CreateParameter("@Price", DbType.Decimal, "Price")); // из колонки Price
            insert.Parameters.Add(DbHelper.CreateParameter("@Stock", DbType.Int32, "Stock")); // из колонки Stock
            insert.UpdatedRowSource = UpdateRowSource.None; // результат INSERT обратно в строку не пишем
            adapter.InsertCommand = insert; // подключаем команду к адаптеру

            DbCommand update = DbHelper.CreateCommand(
                "UPDATE Products SET Name = @Name, CategoryId = @CategoryId, Price = @Price, Stock = @Stock WHERE Id = @Id", connection); // UPDATE для изменённых строк
            update.Parameters.Add(DbHelper.CreateParameter("@Name", DbType.String, "Name"));
            update.Parameters.Add(DbHelper.CreateParameter("@CategoryId", DbType.Int32, "CategoryId"));
            update.Parameters.Add(DbHelper.CreateParameter("@Price", DbType.Decimal, "Price"));
            update.Parameters.Add(DbHelper.CreateParameter("@Stock", DbType.Int32, "Stock"));
            update.Parameters.Add(DbHelper.CreateParameter("@Id", DbType.Int32, "Id", DataRowVersion.Original)); // Id берём ИСХОДНЫЙ - по нему ищем строку в БД
            update.UpdatedRowSource = UpdateRowSource.None;
            adapter.UpdateCommand = update; // подключаем UPDATE

            DbCommand delete = DbHelper.CreateCommand("DELETE FROM Products WHERE Id = @Id", connection); // DELETE для удалённых строк
            delete.Parameters.Add(DbHelper.CreateParameter("@Id", DbType.Int32, "Id", DataRowVersion.Original)); // у удалённой строки доступна только Original-версия
            delete.UpdatedRowSource = UpdateRowSource.None;
            adapter.DeleteCommand = delete; // подключаем DELETE (задание «на отлично»)

            return adapter; // адаптер полностью настроен
        }

        // Пункт 1: загрузка таблицы Products в DataTable
        public static DataTable LoadProducts(DbDataAdapter adapter)
        {
            var table = new DataTable("Products"); // пустая таблица в памяти
            adapter.FillSchema(table, SchemaType.Source); // подтягиваем схему (типы колонок, первичный ключ)
            DataColumn id = table.Columns["Id"]; // колонка Id
            id.AutoIncrementSeed = -1; // временные Id у новых строк будут -1, -2, ... (не пересекутся с настоящими)
            id.AutoIncrementStep = -1; // шаг -1
            adapter.Fill(table); // заполняем таблицу данными из БД (соединение открывается и закрывается автоматически)
            return table; // возвращаем заполненную таблицу
        }

        // Пункт 2: увеличить цену на 10% для заданной категории
        public static int IncreasePrices(DataTable table, int categoryId, decimal percent)
        {
            int changed = 0; // счётчик изменённых строк
            foreach (DataRow row in table.Rows) // перебираем все строки таблицы
            {
                if (row.RowState == DataRowState.Deleted) continue; // удалённые строки пропускаем
                if ((int)row["CategoryId"] == categoryId) // фильтр по категории
                {
                    decimal price = (decimal)row["Price"]; // текущая цена
                    row["Price"] = Math.Round(price * (1 + percent), 2); // новая цена +10%, округляем до копеек (строка станет Modified)
                    changed++;
                }
            }
            return changed;
        }

        // Пункт 3: программно добавить новый товар
        public static void AddNewProduct(DataTable table, string name, int categoryId, decimal price, int stock)
        {
            DataRow row = table.NewRow(); // создаём новую строку по схеме таблицы
            row["Name"] = name; // название
            row["CategoryId"] = categoryId; // категория
            row["Price"] = price; // цена
            row["Stock"] = stock; // остаток
            table.Rows.Add(row); // добавляем в таблицу (RowState = Added)
        }

        // Пункт 5: удалить строку из DataTable по названию
        public static void DeleteProductByName(DataTable table, string name)
        {
            var toDelete = new List<DataRow>(); // сначала собираем строки, чтобы не менять коллекцию во время перебора
            foreach (DataRow row in table.Rows)
                if (row.RowState != DataRowState.Deleted && (string)row["Name"] == name)
                    toDelete.Add(row);
            foreach (DataRow row in toDelete)
                row.Delete(); // помечаем строку как удалённую (RowState = Deleted), из БД удалится при Update()
        }

        private static void PrintTable(DataTable table, string title)
        {
            Console.WriteLine(title);
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                Console.WriteLine($"  {row["Id"],3} | {row["Name"],-28} | cat={row["CategoryId"]} | {(decimal)row["Price"],10:0.00} | {row["Stock"],3} шт. | {row.RowState}"); // строка таблицы + её состояние
            }
        }

        public static void Run()
        {
            Console.WriteLine("=== ЗАДАНИЕ 2: отсоединённый режим ===");
            DbConnection connection = null;
            try
            {
                connection = DbHelper.CreateConnection(); // подключение (адаптер сам откроет/закроет)
                DbDataAdapter adapter = CreateAdapter(connection); // адаптер с ручными командами

                DataTable table = LoadProducts(adapter); // загружаем товары в память
                PrintTable(table, "\nДо изменений:");

                int changed = IncreasePrices(table, 1, 0.10m); // +10% для категории 1 (смартфоны)
                Console.WriteLine($"\nЦена увеличена на 10% у товаров категории 1: {changed} шт.");
                AddNewProduct(table, "Тестовый товар (будет удалён)", 5, 999.99m, 10); // новая строка
                PrintTable(table, "\nПосле правок в памяти (БД ещё не изменена):");

                int affected = adapter.Update(table); // синхронизация: INSERT для Added, UPDATE для Modified
                Console.WriteLine($"\nadapter.Update() выполнен, обработано строк: {affected}");

                table = LoadProducts(adapter); // перечитываем таблицу, чтобы получить настоящие Id из БД
                DeleteProductByName(table, "Тестовый товар (будет удалён)"); // помечаем новую строку на удаление
                affected = adapter.Update(table); // DELETE для строк со статусом Deleted
                Console.WriteLine($"Удаление через DeleteCommand выполнено, обработано строк: {affected}");

                PrintTable(LoadProducts(adapter), "\nИтоговое состояние таблицы в БД:");
            }
            catch (DbException ex) // ошибки БД
            {
                Console.WriteLine("Ошибка БД: " + ex.Message);
            }
            catch (Exception ex) // прочие ошибки
            {
                Console.WriteLine("Непредвиденная ошибка: " + ex.Message);
            }
            finally
            {
                if (connection != null) { connection.Close(); connection.Dispose(); } // закрываем подключение
            }
        }
    }
}
