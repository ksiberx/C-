class House:
    def __init__(self, area, price):
        self.__area = area
        self.__price = price

    def final_price(self, discount=0):
        return self.__price * (100 - discount) / 100

    def get_price(self):
        return self.__price


# Часть 3: SmallHouse
class SmallHouse(House):
    def __init__(self, price):
        super().__init__(40, price)  # Всегда 40 м²
        print(f"✓ SmallHouse создан: 40 м², {price} руб")


# Часть 4: Выполнение
print("=" * 40)
print("ЧАСТЬ 3-4: ВМЕСТЕ")
print("=" * 40)


# 1.
class Human:
    default_name = "Иван"
    default_age = 30

    @staticmethod
    def default_info():
        print(f"default_name: {Human.default_name}")
        print(f"default_age: {Human.default_age}")

    def __init__(self, name=default_name, age=default_age):
        self.name = name
        self.age = age
        self.__money = 0
        self.__house = None

    def info(self):
        print(f"Имя: {self.name}")
        print(f"Возраст: {self.age}")
        print(f"Деньги: {self.__money}")
        print(f"Дом: {self.__house}")

    def earn_money(self, amount):
        self.__money += amount

    def buy_house(self, house, discount=0):
        price = house.final_price(discount)
        if self.__money >= price:
            self.__money -= price
            self.__house = house
            return True
        else:
            print(f"❌ Не хватает {price - self.__money} руб")
            return False


# ВЫПОЛНЕНИЕ:
print("\n1. default_info():")
Human.default_info()

print("\n2. Создаем человека:")
person = Human("Алексей", 25)

print("\n3. Инфо о человеке:")
person.info()

print("\n4. Создаем SmallHouse:")
small_house = SmallHouse(30000)

print("\n5. Пытаемся купить (денег нет):")
person.buy_house(small_house, 10)

print("\n6. Зарабатываем 35000:")
person.earn_money(35000)

print("\n7. Покупаем дом:")
if person.buy_house(small_house, 10):
    print("✅ Купили!")

print("\n8. Финальное инфо:")
person.info()

print("ГОТОВО")
