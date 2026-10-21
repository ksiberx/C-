using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace MailingListDb
{
    class Program
    {
        static void Main()
        {
            Task1(); Task2(); Task3(); Task4(); Task5();
            Console.ReadLine();
        }

        static void Task1()
        {
            using (var db = new MailingListEntities())
            using (var tr = db.Database.BeginTransaction())
            {
                Debug.Assert(db.Database.Exists(), "База данных не найдена");
                var b = new Buyer { FullName = "Тест", Gender = "Мужской",
                    BirthDate = new DateTime(1995, 1, 1), City = "Тест", Country = "Тест" };
                db.Buyers.Add(b); db.SaveChanges();
                Debug.Assert(db.Buyers.Find(b.Id) != null, "Добавление не работает");
                b.FullName = "Изменённый"; db.SaveChanges();
                Debug.Assert(db.Buyers.Find(b.Id).FullName == "Изменённый", "Изменение не работает");
                db.Buyers.Remove(b); db.SaveChanges();
                Debug.Assert(db.Buyers.Find(b.Id) == null, "Удаление не работает");
                tr.Rollback();
            }
            Console.WriteLine("Проверка БД выполнена");
        }

        static void Print<T>(IEnumerable<T> items, Func<T, string> line)
        {
            foreach (var item in items) Console.WriteLine(line(item));
        }

        static void Task2()
        {
            using (var db = new MailingListEntities())
            {
                var s = db.Sections.Include(x => x.Buyers).ToList()
                    .Select(x => new { x.Name, Count = x.Buyers.Count }).ToList();
                Console.WriteLine("\nПопулярность разделов");
                Print(s.OrderByDescending(x => x.Count).Take(3), x => $"{x.Name} | Покупателей: {x.Count}");
                if (s.Any()) Console.WriteLine("Самый популярный: " + s.OrderByDescending(x => x.Count).First().Name);
                Print(s.OrderBy(x => x.Count).Take(3), x => $"{x.Name} | Покупателей: {x.Count}");
                if (s.Any()) Console.WriteLine("Самый непопулярный: " + s.OrderBy(x => x.Count).First().Name);
            }
        }

        static void Task3()
        {
            using (var db = new MailingListEntities())
            {
                var today = DateTime.Today;
                var counts = db.Promotions.Include(x => x.Product).ToList()
                    .Where(x => x.StartDate.Date <= today && x.EndDate.Date >= today)
                    .GroupBy(x => x.Product.SectionId).ToDictionary(x => x.Key, x => x.Count());
                var s = db.Sections.ToList().Select(x => new
                { x.Name, Count = counts.ContainsKey(x.Id) ? counts[x.Id] : 0 }).ToList();
                Console.WriteLine("\nАкционные товары");
                Print(s.OrderByDescending(x => x.Count).Take(3), x => $"{x.Name} | Товаров: {x.Count}");
                if (s.Any()) Console.WriteLine("Больше всего: " + s.OrderByDescending(x => x.Count).First().Name);
                Print(s.OrderBy(x => x.Count).Take(3), x => $"{x.Name} | Товаров: {x.Count}");
                if (s.Any()) Console.WriteLine("Меньше всего: " + s.OrderBy(x => x.Count).First().Name);
            }
        }

        static void Task4()
        {
            using (var db = new MailingListEntities())
            {
                var today = DateTime.Today;
                var ps = db.Promotions.Include(x => x.Product.Section).ToList();
                Console.WriteLine("\nАкции, заканчивающиеся через 3 дня");
                Print(ps.Where(x => x.EndDate.Date == today.AddDays(3)),
                    x => $"{x.Product.Name} | {x.Product.Section.Name} | {x.EndDate:d}");

                var expired = ps.Where(x => x.EndDate.Date < today).ToList();
                Console.WriteLine("\nЗакончившиеся акции");
                Print(expired, x => $"{x.Product.Name} | {x.Product.Section.Name} | {x.EndDate:d}");
                foreach (var x in expired)
                    db.PromotionArchives.Add(new PromotionArchive { OriginalPromotionId = x.Id,
                        ProductName = x.Product.Name, SectionName = x.Product.Section.Name,
                        StartDate = x.StartDate, EndDate = x.EndDate,
                        DiscountPercent = x.DiscountPercent, ArchivedAt = today });
                if (expired.Any())
                {
                    db.SaveChanges(); db.Promotions.RemoveRange(expired); db.SaveChanges();
                    Console.WriteLine("Акции перенесены в архив");
                }

                var buyers = db.Buyers.ToList();
                Console.WriteLine("\nСредний возраст по разделам");
                foreach (var s in db.Sections.Include(x => x.Buyers).ToList())
                    if (s.Buyers.Any()) Console.WriteLine($"{s.Name}: {s.Buyers.Average(x => Age(x.BirthDate)):F1}");
                Console.WriteLine("\nСредний возраст по городам");
                Print(buyers.GroupBy(x => x.City), g => $"{g.Key}: {g.Average(x => Age(x.BirthDate)):F1}");
                Console.WriteLine("\nСредний возраст по странам");
                Print(buyers.GroupBy(x => x.Country), g => $"{g.Key}: {g.Average(x => Age(x.BirthDate)):F1}");
            }
        }

        static void Task5()
        {
            using (var db = new MailingListEntities())
            {
                var sections = db.Sections.Include(x => x.Buyers).ToList();
                var buyers = db.Buyers.ToList();
                Console.WriteLine("\nРазделы по полу");
                foreach (var gender in buyers.Select(x => x.Gender).Distinct())
                {
                    var s = sections.Select(x => new { x.Name, Count = x.Buyers.Count(b => b.Gender == gender) })
                        .OrderByDescending(x => x.Count).ToList();
                    Console.WriteLine("\nПол: " + gender);
                    if (s.Any()) Console.WriteLine("Самый популярный: " + s.First().Name);
                    Print(s.Take(3), x => $"{x.Name} | Покупателей: {x.Count}");
                }
                Console.WriteLine("\nПокупатели по полу");
                Print(buyers.GroupBy(x => x.Gender), g => $"{g.Key}: {g.Count()}");
                Console.WriteLine("\nПокупатели по полу и странам");
                Print(buyers.GroupBy(x => new { x.Gender, x.Country }),
                    g => $"{g.Key.Gender} | {g.Key.Country}: {g.Count()}");
            }
        }

        static int Age(DateTime birth)
        {
            int age = DateTime.Today.Year - birth.Year;
            return birth.Date > DateTime.Today.AddYears(-age) ? age - 1 : age;
        }
    }

    public class MailingListEntities : DbContext
    {
        static MailingListEntities() { Database.SetInitializer<MailingListEntities>(null); }
        public MailingListEntities() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MailingListDb;Integrated Security=True;MultipleActiveResultSets=True") { }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionArchive> PromotionArchives { get; set; }

        protected override void OnModelCreating(DbModelBuilder m)
        {
            m.Entity<Buyer>().ToTable("Buyers");
            m.Entity<Section>().ToTable("Sections");
            m.Entity<Product>().ToTable("Products");
            m.Entity<Promotion>().ToTable("Promotions");
            m.Entity<PromotionArchive>().ToTable("PromotionArchive");
            m.Entity<Buyer>().HasMany(x => x.Sections).WithMany(x => x.Buyers).Map(x =>
            { x.ToTable("BuyerSections"); x.MapLeftKey("BuyerId"); x.MapRightKey("SectionId"); });
            m.Entity<Product>().Property(x => x.Price).HasPrecision(10, 2);
            m.Entity<Promotion>().Property(x => x.DiscountPercent).HasPrecision(5, 2);
            m.Entity<PromotionArchive>().Property(x => x.DiscountPercent).HasPrecision(5, 2);
            m.Entity<Buyer>().Property(x => x.BirthDate).HasColumnType("date");
            m.Entity<Promotion>().Property(x => x.StartDate).HasColumnType("date");
            m.Entity<Promotion>().Property(x => x.EndDate).HasColumnType("date");
            m.Entity<PromotionArchive>().Property(x => x.StartDate).HasColumnType("date");
            m.Entity<PromotionArchive>().Property(x => x.EndDate).HasColumnType("date");
            m.Entity<PromotionArchive>().Property(x => x.ArchivedAt).HasColumnType("date");
            base.OnModelCreating(m);
        }
    }

    public class Buyer
    {
        public Buyer() { Sections = new HashSet<Section>(); }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }

    public class Section
    {
        public Section() { Buyers = new HashSet<Buyer>(); Products = new HashSet<Product>(); }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Buyer> Buyers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        public Product() { Promotions = new HashSet<Promotion>(); }
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public decimal Price { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
    }

    public class Promotion
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
        public virtual Product Product { get; set; }
    }

    public class PromotionArchive
    {
        public int Id { get; set; }
        public int OriginalPromotionId { get; set; }
        public string ProductName { get; set; }
        public string SectionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime ArchivedAt { get; set; }
    }
}
