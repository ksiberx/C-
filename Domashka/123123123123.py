import math


def check_triangle_sides(a, b, c):
    """Проверка корректности сторон треугольника"""
    # Проверка на положительность сторон
    if a <= 0 or b <= 0 or c <= 0:
        return False, "Ошибка: все стороны должны быть положительными числами"

    # Проверка неравенства треугольника
    if a + b <= c or a + c <= b or b + c <= a:
        return False, "Ошибка: сумма двух сторон должна быть больше третьей"

    return True, "Стороны корректны"


def classify_triangle_by_sides(a, b, c):
    """Классификация треугольника по сторонам"""
    if a == b == c:
        return "Равносторонний треугольник"
    elif a == b or a == c or b == c:
        return "Равнобедренный треугольник"
    else:
        return "Разносторонний треугольник"


def classify_triangle_by_angles(a, b, c):
    """Классификация треугольника по углам"""
    # Находим наибольшую сторону
    sides = [a, b, c]
    sides.sort()
    a, b, c = sides  # теперь c - наибольшая сторона

    a_sq = a * a
    b_sq = b * b
    c_sq = c * c

    if abs(a_sq + b_sq - c_sq) < 1e-10:  # учет погрешности вычислений
        return "Прямоугольный треугольник"
    elif a_sq + b_sq > c_sq:
        return "Остроугольный треугольник"
    else:
        return "Тупоугольный треугольник"


def calculate_triangle_area(a, b, c):
    """Вычисление площади треугольника по формуле Герона"""
    p = (a + b + c) / 2  # полупериметр
    area = math.sqrt(p * (p - a) * (p - b) * (p - c))
    return area


def main():
    print("Программа для определения вида треугольника и вычисления его площади")
    print("=" * 60)

    try:
        # Ввод данных
        a = float(input("Введите длину стороны A: "))
        b = float(input("Введите длину стороны B: "))
        c = float(input("Введите длину стороны C: "))

        # Проверка корректности сторон
        is_valid, message = check_triangle_sides(a, b, c)
        if not is_valid:
            print(message)
            return

        # Классификация по сторонам
        side_classification = classify_triangle_by_sides(a, b, c)

        # Классификация по углам
        angle_classification = classify_triangle_by_angles(a, b, c)

        # Вычисление площади
        area = calculate_triangle_area(a, b, c)

        # Вывод результатов
        print("\n" + "=" * 60)
        print("РЕЗУЛЬТАТЫ:")
        print(f"Классификация по сторонам: {side_classification}")
        print(f"Классификация по углам: {angle_classification}")
        print(f"Площадь треугольника: {area:.2f}")

    except ValueError:
        print("Ошибка: введите числовые значения для сторон треугольника")
    except Exception as e:
        print(f"Произошла ошибка: {e}")


if __name__ == "__main__":
    main()