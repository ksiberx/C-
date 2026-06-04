using System;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

namespace PaymentBillApp
{
    [Serializable]
    public class PaymentBill
    {
        // Поля
        private double paymentPerDay;
        private int days;
        private double finePerDay;
        private int delayDays;

        // Вычисляемые поля
        [NonSerialized]
        private double amountWithoutFine;
        
        [NonSerialized]
        private double fine;
        
        [NonSerialized]
        private double totalAmount;

        // Статическое свойство для управления сериализацией
        public static bool SerializeComputedFields { get; set; } = false;

        // Свойства для доступа к полям
        public double PaymentPerDay
        {
            get => paymentPerDay;
            set
            {
                paymentPerDay = value;
                UpdateComputedFields();
            }
        }

        public int Days
        {
            get => days;
            set
            {
                days = value;
                UpdateComputedFields();
            }
        }

        public double FinePerDay
        {
            get => finePerDay;
            set
            {
                finePerDay = value;
                UpdateComputedFields();
            }
        }

        public int DelayDays
        {
            get => delayDays;
            set
            {
                delayDays = value;
                UpdateComputedFields();
            }
        }

        // Вычисляемые свойства
        public double AmountWithoutFine
        {
            get => amountWithoutFine;
            set => amountWithoutFine = value; // Для сериализации
        }

        public double Fine
        {
            get => fine;
            set => fine = value; // Для сериализации
        }

        public double TotalAmount
        {
            get => totalAmount;
            set => totalAmount = value; // Для сериализации
        }

        // Конструктор
        public PaymentBill()
        {
            UpdateComputedFields();
        }

        public PaymentBill(double paymentPerDay, int days, double finePerDay, int delayDays)
        {
            this.paymentPerDay = paymentPerDay;
            this.days = days;
            this.finePerDay = finePerDay;
            this.delayDays = delayDays;
            UpdateComputedFields();
        }

        // Метод обновления вычисляемых полей
        private void UpdateComputedFields()
        {
            amountWithoutFine = paymentPerDay * days;
            fine = finePerDay * delayDays;
            totalAmount = amountWithoutFine + fine;
        }

        // Метод для вывода информации
        public void DisplayInfo()
        {
            Console.WriteLine($"Информация о счете");
            Console.WriteLine($"Оплата за день: {paymentPerDay:C}");
            Console.WriteLine($"Количество дней: {days}");
            Console.WriteLine($"Штраф за день задержки: {finePerDay:C}");
            Console.WriteLine($"Дней задержки: {delayDays}");
            Console.WriteLine($"Сумма без штрафа: {amountWithoutFine:C}");
            Console.WriteLine($"Штраф: {fine:C}");
            Console.WriteLine($"Общая сумма: {totalAmount:C}");
        }

        // Сохранение в файл 
        public void SaveToFile(string filename)
        {
            string json;
            
            if (SerializeComputedFields)
            {
                // Сериализуем все поля
                json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            }
            else
            {
                // Сериализуем только невычисляемые поля
                var simpleData = new
                {
                    PaymentPerDay = this.paymentPerDay,
                    Days = this.days,
                    FinePerDay = this.finePerDay,
                    DelayDays = this.delayDays
                };
                json = JsonSerializer.Serialize(simpleData, new JsonSerializerOptions { WriteIndented = true });
            }
            
            File.WriteAllText(filename, json);
            Console.WriteLine($"\nДанные сохранены в файл: {filename}");
        }

        // Загрузка из файла (JSON)
        public static PaymentBill LoadFromFile(string filename)
        {
            string json = File.ReadAllText(filename);
            PaymentBill bill;
            
            if (SerializeComputedFields)
            {
                bill = JsonSerializer.Deserialize<PaymentBill>(json);
            }
            else
            {
                var simpleData = JsonSerializer.Deserialize<SimpleBillData>(json);
                bill = new PaymentBill(simpleData.PaymentPerDay, simpleData.Days, 
                                       simpleData.FinePerDay, simpleData.DelayDays);
            }
            
            Console.WriteLine($"\nДанные загружены из файла: {filename}");
            return bill;
        }
    }

    // Вспомогательный класс для сериализации без вычисляемых полей
    public class SimpleBillData
    {
        public double PaymentPerDay { get; set; }
        public int Days { get; set; }
        public double FinePerDay { get; set; }
        public int DelayDays { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация работы класса PaymentBill ===\n");

            // Создаем объект счета
            PaymentBill bill = new PaymentBill(1000, 5, 200, 2);
            bill.DisplayInfo();

            // Сериализация ВСЕХ полей
            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("Режим 1: Сериализация всех полей (включая вычисляемые)");
            PaymentBill.SerializeComputedFields = true;
            
            string fileAll = "bill_all.json";
            bill.SaveToFile(fileAll);
            
            // Загружаем и проверяем
            PaymentBill loadedBillAll = PaymentBill.LoadFromFile(fileAll);
            loadedBillAll.DisplayInfo();

            // Сериализация ТОЛЬКО основных полей
            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("Режим 2: Сериализация только основных полей (без вычисляемых)");
            PaymentBill.SerializeComputedFields = false;
            
            string fileBasic = "bill_basic.json";
            bill.SaveToFile(fileBasic);
            
            // Загружаем и проверяем
            PaymentBill loadedBillBasic = PaymentBill.LoadFromFile(fileBasic);
            loadedBillBasic.DisplayInfo();

            // Показываем содержимое файлов
            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("Содержимое файлов:");
            
            Console.WriteLine($"\nФайл {fileAll}:");
            Console.WriteLine(File.ReadAllText(fileAll));
            
            Console.WriteLine($"\nФайл {fileBasic}:");
            Console.WriteLine(File.ReadAllText(fileBasic));

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
