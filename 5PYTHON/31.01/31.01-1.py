class Human:
    # Статические поля
    default_name = "Иван"
    default_age = 30

    @staticmethod
    def default_info():
        print(f"default_name: {Human.default_name}")
        print(f"default_age: {Human.default_age}")

    def __init__(self, name=default_name, age=default_age):
        # Публичные свойства
        self.name = name
        self.age = age

        # Приватные свойства
        self.__money = 0
        self.__house = None

    def info(self):
        print(f"Имя: {self.name}")
        print(f"Возраст: {self.age}")
        print(f"Деньги: {self.__money}")
        print(f"Дом: {self.__house}")

    def earn_money(self, amount):
        self.__money += amount
        print(f"Заработано {amount}. Теперь денег: {self.__money}")

    def __make_deal(self, house, price):
        if self.__money >= price:
            self.__money -= price
            self.__house = house
            print(f"Дом куплен за {price}. Осталось денег: {self.__money}")
            return True
        return False

    def buy_house(self, house, discount=0):
        final_price = house.final_price(discount)
        print(f"Попытка покупки дома. Цена со скидкой {discount}%: {final_price}")

        if self.__money >= final_price:
            return self.__make_deal(house, final_price)
        else:
            print(f"Недостаточно денег! Нужно {final_price}, а есть {self.__money}")
            return False


if __name__ == "__main__":
    print("=== Тестирование класса Human ===")
    Human.default_info()

    person = Human("Алексей", 25)
    person.info()

    person.earn_money(60000)

    print("\n=== Финальная информация о человеке ===")
    person.info()
