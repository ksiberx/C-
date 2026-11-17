class Product:
    def __init__(self, name: str, price: float, quantity: int):
        self.name = name
        self.price = price
        self.quantity = quantity

    @property
    def name(self) -> str:
        return self._name

    @name.setter
    def name(self, value: str):
        if not isinstance(value, str):
            raise ValueError("Название товара должно быть строкой.")
        if not value.strip() or len(value) > 50:
            raise ValueError("Название товара не должно быть пустым и превышать 50 символов.")
        self._name = value

    @property
    def price(self) -> float:
        return self._price

    @price.setter
    def price(self, value: float):
        # В Python bool является подклассом int, поэтому исключаем его явно
        if isinstance(value, bool) or not isinstance(value, (int, float)) or value <= 0:
            raise ValueError("Цена должна быть числом строго больше 0.")
        self._price = float(value)

    @property
    def quantity(self) -> int:
        return self._quantity

    @quantity.setter
    def quantity(self, value: int):
        if isinstance(value, bool) or not isinstance(value, int) or value < 0:
            raise ValueError("Количество должно быть целым неотрицательным числом.")
        self._quantity = value

    @property
    def total_cost(self) -> float:
        return self._price * self._quantity


# Проверка работы
if __name__ == "__main__":
    p = Product("Ноутбук", 75000, 3)
    print(p.total_cost)  # 225000.0
    p.price = 80000
    print(p.total_cost)  # 240000.0

    # Проверка исключений:
    try:
        p.price = -100
    except ValueError as e:
        print(f"Ошибка валидации: {e}")

    try:
        p.total_cost = 0
    except AttributeError as e:
        print(f"Ошибка доступа: {e}")
