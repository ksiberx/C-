using System;

// Пользовательское исключение: некорректная сумма
public class InvalidAmountException : Exception
{
    public InvalidAmountException() : base() { }
    public InvalidAmountException(string message) : base(message) { }
    public InvalidAmountException(string message, Exception innerException) 
        : base(message, innerException) { }
}

// Пользовательское исключение: недостаточно средств
public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() : base() { }
    public InsufficientFundsException(string message) : base(message) { }
    public InsufficientFundsException(string message, Exception innerException) 
        : base(message, innerException) { }
}

// Класс банковского счёта
public class BankAccount
{
    private static int lastAccountNumber = 1000;
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }

    // Конструктор с начальным балансом (дополнительное задание)
    public BankAccount(decimal initialBalance = 0)
    {
        if (initialBalance < 0)
        {
            throw new InvalidAmountException("Начальный баланс не может быть отрицательным");
        }

        // Генерация номера счёта
        lastAccountNumber++;
        AccountNumber = lastAccountNumber.ToString();
        Balance = initialBalance;
    }

    // Пополнение счёта
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidAmountException("Сумма пополнения должна быть положительной");
        }
        Balance += amount;
    }

    // Снятие средств
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidAmountException("Сумма снятия должна быть положительной");
        }
        if (amount > Balance)
        {
            throw new InsufficientFundsException("Недостаточно средств на счете");
        }
        Balance -= amount;
    }

    // Показать информацию о счёте
    public void ShowInfo()
    {
        Console.WriteLine($"\nНомер счёта: {AccountNumber}");
        Console.WriteLine($"Баланс: {Balance:F2} руб.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Банковский счёт");
        Console.WriteLine("(Реализовано дополнительное задание: проверка начального баланса)\n");

        BankAccount account = null;
        bool accountCreated = false;

        // Создание счёта с возможностью указать начальный баланс
        while (!accountCreated)
        {
            try
            {
                Console.Write("Хотите указать начальный баланс? (да/нет): ");
                string answer = Console.ReadLine()?.Trim().ToLower();

                if (answer == "да" || answer == "yes" || answer == "lf" || answer == "д")
                {
                    Console.Write("Введите начальный баланс: ");
                    decimal initialBalance = decimal.Parse(Console.ReadLine());
                    account = new BankAccount(initialBalance);
                }
                else
                {
                    account = new BankAccount(); // баланс 0
                }
                accountCreated = true;
                Console.WriteLine($"\nСчёт успешно создан! Номер счёта: {account.AccountNumber}");
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine($"Ошибка создания счёта: {ex.Message}");
                Console.WriteLine("Попробуйте ещё раз.\n");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите корректное число");
                Console.WriteLine("Попробуйте ещё раз.\n");
            }
        }

        // Основной цикл работы со счётом
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Меню");
            Console.WriteLine("1. Пополнить счёт");
            Console.WriteLine("2. Снять средства");
            Console.WriteLine("3. Показать баланс");
            Console.WriteLine("4. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите сумму пополнения: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        account.Deposit(depositAmount);
                        Console.WriteLine($"Счёт пополнен на {depositAmount:F2} руб.");
                        break;

                    case "2":
                        Console.Write("Введите сумму снятия: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        account.Withdraw(withdrawAmount);
                        Console.WriteLine($"Со счёта снято {withdrawAmount:F2} руб.");
                        break;

                    case "3":
                        account.ShowInfo();
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Спасибо за использование банковского сервиса!");
                        continue;

                    default:
                        Console.WriteLine("Неверный пункт меню. Попробуйте снова.");
                        continue;
                }

                // После успешной операции показываем баланс
                Console.WriteLine($"Текущий баланс: {account.Balance:F2} руб.");
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите корректное число");
            }
            catch (Exception ex)
            {
                // Перехват любых других непредвиденных исключений
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
