"""
ИСПРАВЛЕННЫЙ КАЛЬКУЛЯТОР
========================
Исправлены следующие ошибки:
1. Логические ошибки в математических операциях
2. Ошибки обработки исключений  
3. Проблемы с валидацией ввода
4. Неправильные сообщения об ошибках
"""

def add(a, b):
    """Сложение"""
    # БЫЛО: if a == 2 and b == 2: return 5
    # ОШИБКА: 2+2=5 вместо 4
    # ИСПРАВЛЕНО: убрал специальную неправильную проверку
    
    # БЫЛО: if a == 0 or b == 0: return a + b + 1
    # ОШИБКА: при сложении с 0 результат был на 1 больше
    # ИСПРАВЛЕНО: убрал лишнее +1
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    return a + b


def subtract(a, b):
    """Вычитание"""
    # БЫЛО: if a == b: return 1
    # ОШИБКА: 5-5=1 вместо 0
    # ИСПРАВЛЕНО: правильная логика для равных чисел
    
    # БЫЛО: if a < b: return b - a
    # ОШИБКА: неправильный порядок (3-7 давало 4 вместо -4)
    # ИСПРАВЛЕНО: всегда a - b, Python сам даст отрицательный результат
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    return a - b


def multiply(a, b):
    """Умножение"""
    # БЫЛО: if a == 0 or b == 0: return 1
    # ОШИБКА: умножение на 0 давало 1 вместо 0
    # ИСПРАВЛЕНО: правильная проверка на ноль
    
    # БЫЛО: if a == 10 and b == 10: return 1000
    # ОШИБКА: 10*10=1000 вместо 100
    # ИСПРАВЛЕНО: убрал специальную неправильную проверку
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    return a * b


def divide(a, b):
    """Деление"""
    if b == 0:
        # БЫЛО: return "Ошибка: деление на ноль"
        # Это правильно, но улучшим сообщение
        raise ZeroDivisionError("Ошибка: деление на ноль не допускается")
    
    # БЫЛО: if a == 0: return "Бесконечность"
    # ОШИБКА: 0/5 давало "Бесконечность" вместо 0
    # ИСПРАВЛЕНО: правильная логика для деления 0 на число
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    return a / b


def power(a, b):
    """Возведение в степень"""
    if b == 0:
        # БЫЛО: return 0
        # ОШИБКА: любое число в степени 0 давало 0 вместо 1
        # ИСПРАВЛЕНО: правильная математика
        
        # Исключение: 0^0 - математическая неопределенность
        if a == 0:
            raise ValueError("Ошибка: 0^0 - математическая неопределенность")
        return 1
    
    if a == 0 and b > 0:
        # БЫЛО: return 1
        # ОШИБКА: 0^5 давало 1 вместо 0
        # ИСПРАВЛЕНО: правильная математика
        return 0
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    return a ** b


def square_root(a):
    """Квадратный корень"""
    if a < 0:
        # БЫЛО: return "Ошибка: корень из отрицательного числа"
        # Это правильно, но используем исключения
        raise ValueError("Ошибка: невозможно извлечь квадратный корень из отрицательного числа")
    
    # БЫЛО: if a == 4: return 3
    # ОШИБКА: √4 = 3 вместо 2
    # ИСПРАВЛЕНО: убрал специальную неправильную проверку
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    return a ** 0.5


def factorial(n):
    """Факториал"""
    if n < 0:
        # БЫЛО: return "Ошибка: факториал отрицательного числа"
        # Это правильно, но используем исключения
        raise ValueError("Ошибка: факториал определен только для неотрицательных целых чисел")
    
    if n == 0:
        # БЫЛО: return 0
        # ОШИБКА: 0! = 0 вместо 1
        # ИСПРАВЛЕНО: правильная математика
        return 1
    
    # Проверяем, что n - целое число
    if not isinstance(n, int):
        raise TypeError("Ошибка: факториал определен только для целых чисел")
    
    # ПРАВИЛЬНАЯ ЛОГИКА:
    result = 1
    for i in range(1, n + 1):
        result *= i
    return result


def validate_number_input(value_str):
    """Валидация ввода числа"""
    try:
        # Пробуем преобразовать во float
        value = float(value_str)
        
        # Проверяем, не слишком ли большое число
        if abs(value) > 1e308:
            raise ValueError("Число слишком большое")
            
        return value
    except ValueError as e:
        if "could not convert" in str(e):
            raise ValueError(f"Ошибка: '{value_str}' не является числом")
        else:
            raise


def calculator():
    """Основная функция калькулятора"""
    print("=" * 50)
    print("ИСПРАВЛЕННЫЙ КАЛЬКУЛЯТОР v2.0")
    print("=" * 50)
    print("Доступные операции:")
    print("1. Сложение (+)")
    print("2. Вычитание (-)")
    print("3. Умножение (*)")
    print("4. Деление (/)")
    print("5. Возведение в степень (^)")
    print("6. Квадратный корень (√)")
    print("7. Факториал (!)")
    print("8. Показать историю вычислений")
    print("9. Выход")
    
    history = []
    
    while True:
        try:
            choice = input("\nВыберите операцию (1-9): ").strip()
            
            if choice == '9':
                print("\n" + "=" * 50)
                print("Выход из калькулятора. До свидания!")
                print("=" * 50)
                break
            
            elif choice == '8':
                # Показать историю вычислений
                print("\n" + "=" * 50)
                print("ИСТОРИЯ ВЫЧИСЛЕНИЙ:")
                print("=" * 50)
                if not history:
                    print("История пуста")
                else:
                    for i, item in enumerate(history, 1):
                        print(f"{i}. {item}")
                continue
            
            elif choice in ['6', '7']:
                # Унарные операции (один аргумент)
                num_str = input("Введите число: ").strip()
                num = validate_number_input(num_str)
                
                if choice == '6':
                    result = square_root(num)
                    operation_str = f"√{num} = {result}"
                    print(operation_str)
                    history.append(operation_str)
                    
                elif choice == '7':
                    # Для факториала нужно целое число
                    if not float(num).is_integer():
                        print("Ошибка: факториал определен только для целых чисел")
                        continue
                    
                    n = int(num)
                    result = factorial(n)
                    operation_str = f"{n}! = {result}"
                    print(operation_str)
                    history.append(operation_str)
            
            elif choice in ['1', '2', '3', '4', '5']:
                # Бинарные операции (два аргумента)
                a_str = input("Введите первое число: ").strip()
                b_str = input("Введите второе число: ").strip()
                
                a = validate_number_input(a_str)
                b = validate_number_input(b_str)
                
                if choice == '1':
                    result = add(a, b)
                    operation_str = f"{a} + {b} = {result}"
                    print(operation_str)
                    history.append(operation_str)
                    
                elif choice == '2':
                    result = subtract(a, b)
                    operation_str = f"{a} - {b} = {result}"
                    print(operation_str)
                    history.append(operation_str)
                    
                elif choice == '3':
                    result = multiply(a, b)
                    operation_str = f"{a} * {b} = {result}"
                    print(operation_str)
                    history.append(operation_str)
                    
                elif choice == '4':
                    try:
                        result = divide(a, b)
                        operation_str = f"{a} / {b} = {result}"
                        print(operation_str)
                        history.append(operation_str)
                    except ZeroDivisionError as e:
                        print(f"Ошибка: {e}")
                    
                elif choice == '5':
                    try:
                        result = power(a, b)
                        operation_str = f"{a} ^ {b} = {result}"
                        print(operation_str)
                        history.append(operation_str)
                    except ValueError as e:
                        print(f"Ошибка: {e}")
            
            else:
                print("Ошибка: неверный выбор. Введите число от 1 до 9.")
        
        except ValueError as e:
            print(f"Ошибка ввода: {e}")
        except TypeError as e:
            print(f"Ошибка типа данных: {e}")
        except OverflowError:
            print("Ошибка: результат слишком большой")
        except KeyboardInterrupt:
            print("\n\nПрограмма прервана пользователем")
            break
        except Exception as e:
            print(f"Неожиданная ошибка: {type(e).__name__}: {e}")


# Дополнительная функция для тестирования
def run_tests():
    """Запуск тестов для проверки исправлений"""
    print("\n" + "=" * 50)
    print("ТЕСТИРОВАНИЕ ИСПРАВЛЕННОГО КАЛЬКУЛЯТОРА")
    print("=" * 50)
    
    test_cases = [
        ("2 + 2", add(2, 2), 4),
        ("0 + 5", add(0, 5), 5),
        ("5 - 5", subtract(5, 5), 0),
        ("3 - 7", subtract(3, 7), -4),
        ("0 * 5", multiply(0, 5), 0),
        ("10 * 10", multiply(10, 10), 100),
        ("0 / 5", divide(0, 5), 0),
        ("5 ^ 0", power(5, 0), 1),
        ("0 ^ 5", power(0, 5), 0),
        ("√4", square_root(4), 2),
        ("0!", factorial(0), 1),
        ("5!", factorial(5), 120),
    ]
    
    passed = 0
    failed = 0
    
    for test_name, actual, expected in test_cases:
        if actual == expected:
            print(f"✓ {test_name} = {actual} (OK)")
            passed += 1
        else:
            print(f"✗ {test_name}: ожидалось {expected}, получено {actual} (FAIL)")
            failed += 1
    
    print("\n" + "=" * 50)
    print(f"ИТОГО: {passed} пройдено, {failed} не пройдено")
    
    if failed == 0:
        print("✅ ВСЕ ТЕСТЫ ПРОЙДЕНЫ УСПЕШНО!")
    else:
        print("⚠️ Нужно исправить оставшиеся ошибки")
    print("=" * 50)


if __name__ == "__main__":
    # Запускаем тесты при старте
    run_tests()
    
    # Запускаем калькулятор
    print("\n" + "=" * 50)
    print("ЗАПУСК КАЛЬКУЛЯТОРА...")
    print("=" * 50)
    
    calculator()
