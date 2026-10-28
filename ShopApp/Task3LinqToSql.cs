using System; // базовые типы
using System.Data.Common; // DbException
using System.Data.Linq; // DataContext, Table, EntitySet, EntityRef
using System.Data.Linq.Mapping; // атрибуты [Table], [Column], [Association]
using System.Linq; // LINQ-операторы
using ShopApp.LinqToSql; // наши классы-сущности

namespace ShopApp.LinqToSql
{
    [Table(Name = "Categories")] // класс <-> таблица Categories
    public class Category
    {
        private EntitySet<Product> _products = new EntitySet<Product>(); // коллекция товаров категории (хранилище для связи)

        [Column(IsPrimaryKey = true, IsDbGenerated = true)] // первичный ключ, генерируется БД
        public int Id { get; set; }

        [Column] // обычная колонка
        public string Name { get; set; }

        [Association(Storage = "_products", ThisKey = "Id", OtherKey = "CategoryId")] // связь 1-ко-многим: Category.Id = Product.CategoryId
        public EntitySet<Product> Products
        {
            get { return _products; }
            set { _products.Assign(value); }
        }
    }

    [Table(Name = "Products")] // класс <-> таблица Products
    public class Product
    {
        private EntityRef<Category> _category; // ссылка на категорию (хранилище для связи)
        private EntitySet<OrderItem> _orderItems = new EntitySet<OrderItem>(); // позиции заказов с этим товаром

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int CategoryId { get; set; } // внешний ключ

        [Column]
        public decimal Price { get; set; }

        [Column]
        public int Stock { get; set; }

        [Association(Storage = "_category", ThisKey = "CategoryId", OtherKey = "Id", IsForeignKey = true)] // многие-к-одному: Product.CategoryId -> Category.Id
        public Category Category
        {
            get { return _category.Entity; }
            set { _category.Entity = value; }
        }

        [Association(Storage = "_orderItems", ThisKey = "Id", OtherKey = "ProductId")] // один-ко-многим: Product.Id = OrderItem.ProductId
        public EntitySet<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems.Assign(value); }
        }
    }

    [Table(Name = "Orders")] // класс <-> таблица Orders
    public class Order
    {
        private EntitySet<OrderItem> _orderItems = new EntitySet<OrderItem>(); // позиции заказа

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int CustomerId { get; set; }

        [Column]
        public DateTime OrderDate { get; set; }

        [Column]
        public decimal TotalAmount { get; set; }

        [Association(Storage = "_orderItems", ThisKey = "Id", OtherKey = "OrderId")] // один-ко-многим: Order.Id = OrderItem.OrderId
        public EntitySet<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems.Assign(value); }
        }
    }

    [Table(Name = "OrderItems")] // класс <-> таблица OrderItems
    public class OrderItem
    {
        private EntityRef<Order> _order; // ссылка на заказ
        private EntityRef<Product> _product; // ссылка на товар

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int OrderId { get; set; }

        [Column]
        public int ProductId { get; set; }

        [Column]
        public int Quantity { get; set; }

        [Association(Storage = "_order", ThisKey = "OrderId", OtherKey = "Id", IsForeignKey = true)] // многие-к-одному на Order
        public Order Order
        {
            get { return _order.Entity; }
            set { _order.Entity = value; }
        }

        [Association(Storage = "_product", ThisKey = "ProductId", OtherKey = "Id", IsForeignKey = true)] // многие-к-одному на Product
        public Product Product
        {
            get { return _product.Entity; }
            set { _product.Entity = value; }
        }
    }

    // Контекст данных LINQ to SQL: точка входа для запросов
    public class ShopDataContext : DataContext
    {
        public ShopDataContext(string connectionString) : base(connectionString) { } // передаём строку подключения в базовый класс

        public Table<Category> Categories { get { return GetTable<Category>(); } } // таблица категорий
        public Table<Product> Products { get { return GetTable<Product>(); } } // таблица товаров
        public Table<Order> Orders { get { return GetTable<Order>(); } } // таблица заказов
        public Table<OrderItem> OrderItems { get { return GetTable<OrderItem>(); } } // таблица позиций заказов
    }
}

namespace ShopApp
{
    /// <summary>Задание 3. LINQ to SQL: Топ-3 товара за 30 дней</summary>
    public static class Task3LinqToSql
    {
        public static void Run()
        {
            Console.WriteLine("=== ЗАДАНИЕ 3: LINQ to SQL ===");
            try
            {
                using (var db = new ShopDataContext(DbHelper.ConnectionString)) // контекст создаём из строки подключения
                {
                    DateTime dateFrom = DateTime.Now.AddDays(-30); // граница: 30 дней назад

                    var top3 = (from oi in db.OrderItems // берём позиции заказов
                                join o in db.Orders on oi.OrderId equals o.Id // соединяем с заказами
                                join p in db.Products on oi.ProductId equals p.Id // соединяем с товарами
                                where o.OrderDate >= dateFrom // только заказы за последние 30 дней
                                group oi by p.Name into g // группируем по названию товара
                                orderby g.Sum(x => x.Quantity) descending // сортируем по сумме проданных штук по убыванию
                                select new { Name = g.Key, Sold = g.Sum(x => x.Quantity) }) // выбираем название и сумму
                               .Take(3); // оставляем первые три

                    Console.WriteLine("Топ-3 самых продаваемых товара за последние 30 дней:");
                    foreach (var item in top3) // запрос реально выполняется здесь (отложенное выполнение)
                        Console.WriteLine($"{item.Name} — {item.Sold} шт.");
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine("Ошибка БД: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Непредвиденная ошибка: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Работа с LINQ to SQL завершена."); // контекст уже освобождён блоком using
            }
        }
    }
}
