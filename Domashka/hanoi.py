def hanoi(n, source, target, auxiliary):
    """
    n: количество дисков
    source: начальный стержень
    target: целевой стержень
    auxiliary: вспомогательный стержень
    """
    if n == 1:
        print(f"Переместить диск 1 с {source} на {target}")
        return

    # Переместить n-1 дисков на вспомогательный стержень
    hanoi(n - 1, source, auxiliary, target)

    # Переместить самый большой диск на целевой стержень
    print(f"Переместить диск {n} с {source} на {target}")

    # Переместить n-1 дисков с вспомогательного на целевой стержень
    hanoi(n - 1, auxiliary, target, source)

n = 3  # количество дисков
print(f"Решение для Ханойской башни с {n} дисками:")
hanoi(n, 'A', 'C', 'B')