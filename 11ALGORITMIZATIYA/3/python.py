
import asyncio
import math
import importlib

# ЗАДАНИЕ 1. ТАЙМЕР

class Timer:
    def __init__(self, seconds):
        self.seconds = seconds
        self.paused = False

    def pause(self):
        self.paused = True
        print("Таймер на паузе")

    def resume(self):
        self.paused = False
        print("Таймер продолжен")

    async def start(self):
        while self.seconds > 0:
            if not self.paused:
                print("Осталось:", self.seconds)
                self.seconds -= 1
            await asyncio.sleep(1)

        print("Время вышло!")

# ЗАДАНИЕ 2. ПОГОДНАЯ СТАНЦИЯ

class WeatherStation:
    def __init__(self):
        self.observers = []
        self.temperature = 0

    def subscribe(self, observer):
        self.observers.append(observer)

    def unsubscribe(self, observer):
        self.observers.remove(observer)

    def set_temperature(self, value):
        self.temperature = value
        for observer in self.observers.copy():
            observer.update(value)


class Display:
    def update(self, temperature):
        print("Температура:", temperature)


class Logger:
    def __init__(self):
        self.history = []

    def update(self, temperature):
        self.history.append(temperature)
        print("Запись:", temperature)


class Alarm:
    def update(self, temperature):
        if temperature > 30:
            print("ВНИМАНИЕ! Высокая температура!")


# ЗАДАНИЕ 3. НАБЛЮДАТЕЛЬ
# АЛГОРИТМИЧЕСКИЙ ЯЗЫК

subscribers = []


def handler1(event):
    print("Обработчик 1:", event)


def handler2(event):
    print("Обработчик 2:", event)


handlers = {
    1: handler1,
    2: handler2
}


def subscribe(id):
    subscribers.append(id)


def unsubscribe(id):
    if id in subscribers:
        subscribers.remove(id)


def notify(event):
    for id in subscribers.copy():
        if id in handlers:
            handlers[id](event)


# ЗАДАНИЕ 4. ПЛАГИНЫ КАЛЬКУЛЯТОРА

class Add:
    def apply(self, a, b):
        return a + b


class Sub:
    def apply(self, a, b):
        return a - b


class Mul:
    def apply(self, a, b):
        return a * b


class Div:
    def apply(self, a, b):
        return a / b if b != 0 else math.nan


class Calculator:
    def __init__(self, operations):
        self.operations = operations

    def calculate(self, name, a, b):
        return self.operations[name].apply(a, b)


# Для подключения внешнего плагина по имени:
def load_plugin(module_name, class_name):
    module = importlib.import_module(module_name)
    return getattr(module, class_name)()


# ЗАДАНИЕ 5. УМНЫЙ ДОМ

class EventBus:
    def __init__(self):
        self.listeners = {}

    def subscribe(self, event, handler):
        self.listeners.setdefault(event, []).append(handler)

    def publish(self, event, value):
        for handler in self.listeners.get(event, []).copy():
            handler(value)


class TemperatureSensor:
    def __init__(self, bus):
        self.bus = bus

    def set_temperature(self, value):
        print("\nНовая температура:", value)
        self.bus.publish("temperature_changed", value)


class Heater:
    def __init__(self, bus):
        self.bus = bus
        self.on = True
        bus.subscribe("temperature_changed", self.update)

    def update(self, temperature):
        if temperature >= 25 and self.on:
            self.on = False
            print("Нагреватель выключен")
            self.bus.publish("heater_changed", "Выключен")


class HomeLogger:
    def __init__(self, bus):
        self.history = []
        bus.subscribe("temperature_changed", self.log)
        bus.subscribe("heater_changed", self.log)

    def log(self, value):
        self.history.append(value)
        print("Логгер:", value)


class HomeAlarm:
    def __init__(self, bus):
        bus.subscribe("temperature_changed", self.check)

    def check(self, temperature):
        if temperature > 35:
            print("АВАРИЯ! Перегрев!")

# ЗАПУСК ВСЕХ ЗАДАНИЙ

async def main():

    print("\n--- ЗАДАНИЕ 1 ---")
    timer = Timer(3)
    await timer.start()

    print("\n--- ЗАДАНИЕ 2 ---")
    station = WeatherStation()
    display = Display()
    logger = Logger()
    alarm = Alarm()

    station.subscribe(display)
    station.subscribe(logger)
    station.subscribe(alarm)

    station.set_temperature(20)
    station.set_temperature(35)

    station.unsubscribe(display)
    station.set_temperature(25)

    print("\n--- ЗАДАНИЕ 3 ---")
    subscribe(1)
    subscribe(2)
    notify("Событие")
    unsubscribe(1)
    notify("Новое событие")

    print("\n--- ЗАДАНИЕ 4 ---")
    calculator = Calculator({
        "add": Add(),
        "sub": Sub(),
        "mul": Mul(),
        "div": Div()
    })

    print("Сложение:", calculator.calculate("add", 10, 5))
    print("Вычитание:", calculator.calculate("sub", 10, 5))
    print("Умножение:", calculator.calculate("mul", 10, 5))
    print("Деление:", calculator.calculate("div", 10, 5))
    print("Деление на ноль:", calculator.calculate("div", 10, 0))

    print("\n--- ЗАДАНИЕ 5 ---")
    bus = EventBus()

    sensor = TemperatureSensor(bus)
    heater = Heater(bus)
    home_logger = HomeLogger(bus)
    home_alarm = HomeAlarm(bus)

    sensor.set_temperature(20)
    sensor.set_temperature(25)
    sensor.set_temperature(40)


asyncio.run(main())
