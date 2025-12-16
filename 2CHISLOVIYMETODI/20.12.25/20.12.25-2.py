# Задаём функцию
def f(x):
    return x**2   # замени на нужную

# Ввод данных
a = float(input("Введите a: "))
b = float(input("Введите b: "))
eps = float(input("Введите ε: "))

# Начало
n = 2
S1 = 0.0

while True:
    h = (b - a) / n
    x = a
    S = 0.0

    N2 = n // 2

    # Цикл
    for k in range(1, N2 + 1):
        x1 = x
        x2 = x1 + h
        x3 = x2 + h

        S = S + f(x1) + 4 * f(x2) + f(x3)

        x = x3

    S = S * (h / 3)

    # Проверка условия
    if abs(S - S1) <= eps:
        print("Результат S =", S)
        print("Число разбиений n =", n)
        break
    else:
        S1 = S
        n = 2 * n
