
import math
import random

# Задача 1. Копилка

class PiggyBank:
    def __init__(self):
        self.money = 0

    def put(self, amount):
        if amount > 0:
            self.money += amount

    def take(self, amount):
        if 0 < amount <= self.money:
            self.money -= amount
        else:
            print("Недостаточно денег!")

    def show(self):
        print("В копилке:", self.money)


# Задача 2. Светофор

class TrafficLight:
    def __init__(self):
        self.color = "red"

    def next(self):
        if self.color == "red":
            self.color = "green"
        elif self.color == "green":
            self.color = "yellow"
        else:
            self.color = "red"

    def show(self):
        print("Цвет:", self.color)


# Задача 3. Точка на плоскости

class Point:
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def move(self, dx, dy):
        self.x += dx
        self.y += dy

    def distanceTo(self, other):
        return math.sqrt(
            (other.x - self.x) ** 2 +
            (other.y - self.y) ** 2
        )

    def show(self):
        print(f"({self.x}, {self.y})")


# Задача 4. Термометр

class Thermometer:
    def __init__(self, temperature=0):
        self.celsius = temperature

    @property
    def celsius(self):
        return self.__temperature

    @celsius.setter
    def celsius(self, value):
        if value < -273.15:
            raise ValueError("Ниже абсолютного нуля!")
        self.__temperature = value

    @property
    def fahrenheit(self):
        return self.__temperature * 9 / 5 + 32


# Задача 5. Тамагочи

class Pet:
    def __init__(self, name):
        self.__name = name
        self.__hunger = 50
        self.__happiness = 50

    def feed(self):
        self.__hunger = max(0, self.__hunger - 20)
        self.__happiness = min(100, self.__happiness + 5)

    def play(self):
        self.__happiness = min(100, self.__happiness + 15)
        self.__hunger = min(100, self.__hunger + 10)

    def tick(self):
        self.__hunger = min(100, self.__hunger + 5)
        self.__happiness = max(0, self.__happiness - 3)

    @property
    def mood(self):
        if self.__happiness > 70:
            return "счастлив"
        elif self.__happiness >= 40:
            return "нормально"
        return "грустен"

    def show(self):
        print(
            f"{self.__name} | "
            f"Голод: {self.__hunger} | "
            f"Счастье: {self.__happiness} | "
            f"Настроение: {self.mood}"
        )


# Главная программа

print("--- Задача 1 ---")
bank = PiggyBank()
bank.put(100)
bank.put(50)
bank.take(200)
bank.show()

print("\n--- Задача 2 ---")
light = TrafficLight()

for i in range(5):
    light.next()
    light.show()

print("\n--- Задача 3 ---")
p1 = Point(2, 3)
p2 = Point(5, 7)

p1.move(1, 2)
p1.show()
p2.show()

print("Расстояние:", p1.distanceTo(p2))

print("\n--- Задача 4 ---")
thermometer = Thermometer(25)

print("Цельсий:", thermometer.celsius)
print("Фаренгейт:", thermometer.fahrenheit)

print("\n--- Задача 5 ---")
pet = Pet("Бобик")

for i in range(1, 11):
    action = random.choice(["feed", "play", "tick"])

    print(f"Шаг {i}:", end=" ")

    if action == "feed":
        print("Кормим")
        pet.feed()
    elif action == "play":
        print("Играем")
        pet.play()
    else:
        print("Ждём")
        pet.tick()

    pet.show()
