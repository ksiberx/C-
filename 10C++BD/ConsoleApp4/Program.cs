
using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace CarRentalDbFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА: АВТОПАРК ===\n");

            // Запускай нужные задания по одному
            Task1_AddInitialData();
            Task2_GetAllCars();
            Task3_FindCarByModel("Camry");
            Task4_GetCarById(1);
            Task5_GetCarsWithBrandInfo();
            Task6_AddNewCar("X5", 2023, 150.00m, 1);
            Task7_UpdateCarPrice(1, 200.00m);

            Console.ReadLine();
        }

        // Задание 0. Заполнение базы данных
        static void Task1_AddInitialData()
        {
            Console.WriteLine("Добавление начальных данных...");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                if (!db.Brands.Any())
                {
                    db.Brands.Add(new Brand
                    {
                        Name = "Toyota",
                        Country = "Япония"
                    });

                    db.Brands.Add(new Brand
                    {
                        Name = "BMW",
                        Country = "Германия"
                    });

                    db.SaveChanges();

                    db.Cars.Add(new Car
                    {
                        Model = "Camry",
                        ReleaseYear = 2020,
                        PricePerDay = 50.00m,
                        BrandId = 1
                    });

                    db.Cars.Add(new Car
                    {
                        Model = "Corolla",
                        ReleaseYear = 2019,
                        PricePerDay = 40.00m,
                        BrandId = 1
                    });

                    db.Cars.Add(new Car
                    {
                        Model = "X5",
                        ReleaseYear = 2022,
                        PricePerDay = 100.00m,
                        BrandId = 2
                    });

                    db.SaveChanges();

                    Console.WriteLine("Данные добавлены!");
                }
                else
                {
                    Console.WriteLine("База уже содержит записи.");
                }
            }
        }

        // Задание 1. Список автомобилей
        static void Task2_GetAllCars()
        {
            Console.WriteLine("\n--- Все автомобили ---");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var cars = db.Cars
                    .OrderBy(c => c.Model)
                    .ToList();

                foreach (var car in cars)
                {
                    Console.WriteLine(
                        car.Model + " | " + car.PricePerDay + " руб./день");
                }
            }
        }

        // Задание 2. Поиск по модели
        static void Task3_FindCarByModel(string modelName)
        {
            Console.WriteLine("\n--- Поиск автомобиля ---");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var car = db.Cars
                    .FirstOrDefault(c => c.Model == modelName);

                if (car != null)
                {
                    Console.WriteLine(
                        car.Model + " | Год: " + car.ReleaseYear);
                }
                else
                {
                    Console.WriteLine("Машина не найдена");
                }
            }
        }

        // Задание 3. Поиск по ID
        static void Task4_GetCarById(int id)
        {
            Console.WriteLine("\n--- Поиск по ID ---");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var car = db.Cars.Find(id);

                if (car != null)
                {
                    Console.WriteLine("Модель: " + car.Model);
                }
                else
                {
                    Console.WriteLine("Машина не найдена");
                }
            }
        }

        // Задание 4. Информация о марке
        static void Task5_GetCarsWithBrandInfo()
        {
            Console.WriteLine("\n--- Автомобили и марки ---");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var cars = db.Cars.ToList();

                foreach (var car in cars)
                {
                    Console.WriteLine(
                        "Модель: " + car.Model +
                        " | Марка: " + car.Brand.Name +
                        " | Страна: " + car.Brand.Country);
                }
            }
        }

        // Задание 5. Добавление автомобиля
        static void Task6_AddNewCar(
            string model, int year, decimal price, int brandId)
        {
            Console.WriteLine("\n--- Добавление автомобиля ---");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var newCar = new Car
                {
                    Model = model,
                    ReleaseYear = year,
                    PricePerDay = price,
                    BrandId = brandId
                };

                db.Cars.Add(newCar);
                db.SaveChanges();

                Console.WriteLine("Автомобиль добавлен!");
            }
        }

        // Задание 6. Изменение цены
        static void Task7_UpdateCarPrice(int carId, decimal newPrice)
        {
            Console.WriteLine("\n--- Изменение цены ---");

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var car = db.Cars.Find(carId);

                if (car != null)
                {
                    car.PricePerDay = newPrice;
                    db.SaveChanges();

                    Console.WriteLine("Цена обновлена!");
                }
                else
                {
                    Console.WriteLine("Машина не найдена");
                }
            }
        }
    }
}
