using System;

class Program
{
    static void Main()
    {
        // Массив фиксированного размера на 3 элемента
        double[] results = new double[3];
        int index = 0;
        bool continueProgram = true;

        Console.WriteLine("Безопасное деление и работа с массивами");

        while (continueProgram && index < results.Length)
        {
            Console.WriteLine($"Операция {index + 1}");

            try
            {
                // Ввод первого числа
                Console.Write("Введите первое целое число: ");
                int firstNumber = int.Parse(Console.ReadLine());

                // Ввод второго числа
                Console.Write("Введите второе целое число: ");
                int secondNumber = int.Parse(Console.ReadLine());

                // Деление
                double quotient = (double)firstNumber / secondNumber;

                // Сохранение в массив
                results[index] = quotient;
                index++;

                Console.WriteLine($"Результат деления: {quotient:F2}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите целое число");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка: деление на ноль невозможно");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Массив результатов заполнен, дальнейшие вычисления невозможны");
                break; // завершаем программу
            }
            finally
            {
                Console.WriteLine("Попытка выполнения операции завершена");
            }

            // Если массив ещё не заполнен, спрашиваем о продолжении
            if (index < results.Length)
            {
                Console.Write("\nХотите продолжить? (да/нет): ");
                string answer = Console.ReadLine()?.Trim().ToLower();
                if (answer != "да" && answer != "yes" && answer != "lf" && answer != "д")
                {
                    continueProgram = false;
                }
            }
            else if (index == results.Length)
            {
                Console.WriteLine("\nМассив результатов заполнен. Программа завершает работу.");
                continueProgram = false;
            }
        }

        // Вывод всех успешно полученных результатов
        Console.WriteLine("\nСохранённые результаты");
        if (index == 0)
        {
            Console.WriteLine("Нет успешных результатов.");
        }
        else
        {
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine($"Результат {i + 1}: {results[i]:F2}");
            }
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
