using System;

using System.Configuration;   // ← ConfigurationManager 

using System.Data;

using System.Data.Common;

using System.Data.SqlClient;   // оставляем для других шагов 



namespace ADO_Lab3

{

    class Program

    {

        static void Main(string[] args)

        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            RunQueryViaFactory();

        }



        static void RunQueryViaFactory()

        {

            // 1. Спрашиваем у пользователя, к какой БД обращаться. 

            Console.WriteLine("Выберите поставщика:");

            Console.WriteLine("  System.Data.SqlClient");

            Console.WriteLine("  System.Data.OleDb");

            Console.Write("> ");

            string providerName = Console.ReadLine().Trim();



            // 2. Получаем фабрику по имени. 

            DbProviderFactory factory;

            try

            {

                factory = DbProviderFactories.GetFactory(providerName);

            }

            catch

            {

                Console.WriteLine("Поставщик не найден!");

                return;

            }



            // 3. Читаем строку подключения из App.config по providerName. 

            string connStr = GetConnectionStringByProvider(providerName);

            if (string.IsNullOrEmpty(connStr))

            {

                Console.WriteLine("Строка подключения не найдена в App.config.");

                return;

            }

            Console.WriteLine("\nСтрока подключения: " + connStr);



            // 4. Всё создаём ЧЕРЕЗ ФАБРИКУ — не через new SqlConnection()! 

            DbConnection conn = factory.CreateConnection();

            conn.ConnectionString = connStr;



            DbCommand cmd = factory.CreateCommand();

            cmd.Connection = conn;

            cmd.CommandText = "SELECT * FROM Books";



            DbDataAdapter adapter = factory.CreateDataAdapter();

            adapter.SelectCommand = cmd;



            // 5. Заполняем DataTable и выводим. 

            DataTable dt = new DataTable();

            try

            {

                conn.Open();

                adapter.Fill(dt);

                Console.WriteLine("\n=== Результат SELECT * FROM Books ===\n");

                PrintDataTable(dt);

            }

            catch (Exception ex)

            {

                Console.WriteLine("Ошибка: " + ex.Message);

            }

            finally

            {

                conn.Close();

            }

        }



        static string GetConnectionStringByProvider(string providerName)

        {

            foreach (ConnectionStringSettings s in ConfigurationManager.ConnectionStrings)

                if (s.ProviderName == providerName)

                    return s.ConnectionString;

            return null;

        }



        static void PrintDataTable(DataTable dt)

        {

            if (dt.Rows.Count == 0) { Console.WriteLine("(нет данных)"); return; }



            foreach (DataColumn col in dt.Columns)

                Console.Write(col.ColumnName.PadRight(15) + "| ");

            Console.WriteLine();

            Console.WriteLine(new string('-', 17 * dt.Columns.Count));



            foreach (DataRow row in dt.Rows)

            {

                foreach (var v in row.ItemArray)

                    Console.Write((v?.ToString() ?? "").PadRight(15) + "| ");

                Console.WriteLine();

            }

        }

    }

}