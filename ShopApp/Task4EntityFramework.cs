using System; // базовые типы
using System.Collections.Generic; // List, Dictionary, ICollection
using System.Data.Entity; // DbContext, DbSet, Database
using System.Linq; // LINQ-операторы
using ShopApp.EF; // наши сущности и контекст

namespace ShopApp.EF
{
    public class Category
    {
        public int Id { get; set; } // PK по соглашению (Id)
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>(); // навигационное свойство: товары категории
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; } // внешний ключ
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public virtual Category Category { get; set; } // навигационное свойство: категория товара
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // позиции заказов
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>(); // заказы клиента
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } // внешний ключ
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual Customer Customer { get; set; } // клиент
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // позиции заказа
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; } // заказ
        public virtual Product Product { get; set; } // товар
    }

    // Контекст EF: «сессия» работы с БД
    public class ShopContext : DbContext
    {
        public ShopContext() : base("name=ShopDb") // берём строку подключения ShopDb из App.config
        {
            Database.SetInitializer<ShopContext>(null); // отключаем авто-инициализацию: БД создаём явно (скриптом или CreateIfNotExists)
        }

        public DbSet<Category> Categories { get; set; } // таблица Categories
        public DbSet<Product> Products { get; set; } // таблица Products
        public DbSet<Customer> Customers { get; set; } // таблица Customers
        public DbSet<Order> Orders { get; set; } // таблица Orders
        public DbSet<OrderItem> OrderItems { get; set; } // таблица OrderItems

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories"); // явно задаём имена таблиц (как в схеме задания)
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2); // decimal(18,2) для цены
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2); // decimal(18,2) для суммы заказа
        }
    }
}

namespace ShopApp
{
    /// <summary>Задание 4. Entity Framework Code-First + транзакция</summary>
    public static class Task4EntityFramework
    {
        // Инициализация БД: создаст БД и таблицы, только если БД ещё не существует
        public static void InitDatabase()
        {
            using (var context = new ShopContext()) // создаём контекст
            {
                bool created = context.Database.CreateIfNotExists(); // аналог EnsureCreated(): true - БД создана заново, false - уже была
                Console.WriteLine(created ? "База данных создана средствами EF." : "База данных уже существует.");
            }
        }

        // Бизнес-логика: оформление заказа в явной транзакции
        public static int PlaceOrder(int customerId, Dictionary<int, int> productsToBuy)
        {
            using var context = new ShopContext(); // контекст освободится при выходе из метода
            using var transaction = context.Database.BeginTransaction(); // начинаем явную транзакцию
            try
            {
                Customer customer = context.Customers.Find(customerId); // ищем клиента по ключу
                if (customer == null) // нет такого клиента
                    throw new InvalidOperationException($"Клиент с Id={customerId} не найден.");
                if (productsToBuy == null || productsToBuy.Count == 0) // пустой заказ
                    throw new InvalidOperationException("Заказ не содержит товаров.");

                var order = new Order { CustomerId = customerId, OrderDate = DateTime.Now, TotalAmount = 0m }; // новый заказ
                decimal total = 0m; // накопитель суммы заказа

                foreach (KeyValuePair<int, int> pair in productsToBuy) // pair.Key - Id товара, pair.Value - количество
                {
                    Product product = context.Products.Find(pair.Key); // ищем товар
                    if (product == null)
                        throw new InvalidOperationException($"Товар с Id={pair.Key} не найден.");
                    if (pair.Value <= 0) // некорректное количество
                        throw new InvalidOperationException($"Некорректное количество для товара '{product.Name}'.");
                    if (product.Stock < pair.Value) // ПРОВЕРКА остатка ДО создания заказа
                        throw new InvalidOperationException($"Недостаточно товара '{product.Name}' на складе: нужно {pair.Value}, есть {product.Stock}.");

                    product.Stock -= pair.Value; // уменьшаем остаток
                    total += product.Price * pair.Value; // добавляем стоимость позиции к сумме
                    order.OrderItems.Add(new OrderItem { ProductId = product.Id, Quantity = pair.Value }); // создаём позицию заказа
                }

                order.TotalAmount = total; // записываем итоговую сумму
                context.Orders.Add(order); // добавляем заказ (позиции добавятся вместе с ним)
                context.SaveChanges(); // сохраняем: INSERT заказа и позиций + UPDATE остатков
                transaction.Commit(); // фиксируем транзакцию - всё записано окончательно
                return order.Id; // возвращаем номер созданного заказа
            }
            catch (Exception)
            {
                transaction.Rollback(); // при любой ошибке откатываем ВСЕ изменения
                throw; // пробрасываем исключение вызывающему коду
            }
        }

        private static void PrintStock(params int[] ids)
        {
            using (var context = new ShopContext())
            {
                foreach (Product p in context.Products.Where(x => ids.Contains(x.Id)).OrderBy(x => x.Id)) // выбираем нужные товары
                    Console.WriteLine($"  Id={p.Id} {p.Name}: остаток {p.Stock} шт.");
            }
        }

        public static void Run()
        {
            Console.WriteLine("=== ЗАДАНИЕ 4: Entity Framework (Code-First) ===");
            try
            {
                InitDatabase(); // инициализация БД
                Console.WriteLine("\nОстатки до заказа:");
                PrintStock(1, 2, 3);

                int orderId = PlaceOrder(1, new Dictionary<int, int> { { 1, 1 }, { 2, 2 } }); // успешный заказ: 1 шт. товара 1 и 2 шт. товара 2
                Console.WriteLine($"\nЗаказ №{orderId} успешно оформлен.");
                Console.WriteLine("Остатки после успешного заказа:");
                PrintStock(1, 2, 3);

                Console.WriteLine("\nПробуем оформить заказ, на который не хватает товара:");
                try
                {
                    PlaceOrder(1, new Dictionary<int, int> { { 3, 5 }, { 1, 9999 } }); // товара 1 не хватит - заказ целиком должен откатиться
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Заказ отклонён: " + ex.Message); // ожидаемая ошибка
                }
                Console.WriteLine("Остатки после отката (должны совпадать с предыдущими):");
                PrintStock(1, 2, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при работе с EF: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Работа с Entity Framework завершена.");
            }
        }
    }
}
