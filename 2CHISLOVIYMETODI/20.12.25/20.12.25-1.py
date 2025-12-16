import math


def f(x):
    """Подынтегральная функция."""
    return math.sin(x)


def compute_integral(a, b, epsilon):
    """Вычисление интеграла методом левых прямоугольников."""
    n = 5
    # Вычисляем первое приближение
    h = (b - a) / n
    I_prev = 0
    for i in range(n):
        x = a + i * h
        I_prev += f(x)
    I_prev *= h

    # Итеративное уточнение
    while True:
        n *= 2
        h = (b - a) / n
        I_current = 0

        for i in range(n):
            x = a + i * h
            I_current += f(x)
        I_current *= h

        # Проверка точности
        if abs(I_current - I_prev) < epsilon:
            return I_current, n, abs(I_current - I_prev)

        I_prev = I_current


def main():
    print("Вычисление интеграла методом левых прямоугольников")
    print("=" * 50)

    a = float(input("Нижний предел a: "))
    b = float(input("Верхний предел b: "))
    epsilon = float(input("Точность epsilon: "))

    result, n, error = compute_integral(a, b, epsilon)

    # Вывод результатов
    print("\n" + "=" * 50)
    print(f"Результат: {result:.8f}")
    print(f"Количество разбиений: {n}")
    print(f"Погрешность: {error:.8f}")

    # Проверка для sin(x)
    if b > a:
        exact = -math.cos(b) + math.cos(a)
        print(f"Точное значение: {exact:.8f}")
        print(f"Абсолютная ошибка: {abs(result - exact):.8f}")


if __name__ == "__main__":
    main()
