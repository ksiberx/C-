class House:
    def __init__(self, area, price):
        # Приватные свойства
        self.__area = area
        self.__price = price

    def final_price(self, discount=0):
        """Возвращает цену с учетом скидки"""
        # Рассчитываем цену с учетом скидки в процентах
        discount_amount = self.__price * discount / 100
        return self.__price - discount_amount

    # Дополнительные методы для тестирования (не обязательны по заданию)
    def get_area(self):
        """Возвращает площадь дома"""
        return self.__area

    def get_price(self):
        """Возвращает исходную цену дома"""
        return self.__price

    def get_info(self):
        """Возвращает информацию о доме"""
        return f"Дом площадью {self.__area} м², цена: {self.__price}"



if __name__ == "__main__":
    print("ТЕСТИРОВАНИЕ КЛАССА HOUSE")
    print()

    # Создаем объекты домов
    house1 = House(100, 50000)
    house2 = House(150, 75000)
    house3 = House(80, 30000)

    # Выводим информацию о домах
    print("1. Информация о домах:")
    print(f"   Дом 1: {house1.get_info()}")
    print(f"   Дом 2: {house2.get_info()}")
    print(f"   Дом 3: {house3.get_info()}")
    print()

    # Тестируем метод final_price()
    print("2. Тестирование метода final_price():")

    # Тест 1: Без скидки
    print(f"   Дом 1 без скидки: {house1.final_price()} рублей")

    # Тест 2: Со скидкой 10%
    discount = 10
    price_with_discount = house1.final_price(discount)
    print(f"   Дом 1 со скидкой {discount}%: {price_with_discount} рублей")

    # Тест 3: Со скидкой 25%
    discount = 25
    price_with_discount = house2.final_price(discount)
    print(f"   Дом 2 со скидкой {discount}%: {price_with_discount} рублей")

    # Тест 4: Со скидкой 50%
    discount = 50
    price_with_discount = house3.final_price(discount)
    print(f"   Дом 3 со скидкой {discount}%: {price_with_discount} рублей")
    print()

    # Тестируем приватные поля (они недоступны напрямую)
    print("3. Проверка инкапсуляции (приватных полей):")
    try:
        print(f"   Попытка доступа к house1.__area: Будет ошибка!")
        print(house1.__area)
    except AttributeError as e:
        print(f"   Ошибка: {e}")

    try:
        print(f"   Попытка доступа к house1.__price: Будет ошибка!")
        print(house1.__price)
    except AttributeError as e:
        print(f"   Ошибка: {e}")

    print()
    print("4. Использование геттеров:")
    print(f"   Площадь дома 1: {house1.get_area()} м²")
    print(f"   Цена дома 1: {house1.get_price()} рублей")

    print()
    print("=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===")
