import math
import json
import sys
import unittest
from abc import ABC, abstractmethod

# ЗАДАНИЕ 6. ООП Структура геометрии

class Point:
    def __init__(self, x: float = 0.0, y: float = 0.0):
        self.x = x
        self.y = y

    def distance_to(self, other: 'Point') -> float:
        return math.sqrt((self.x - other.x) ** 2 + (self.y - other.y) ** 2)


class Figure(ABC):
    @abstractmethod
    def area(self) -> float:
        pass


class Triangle(Figure):
    def __init__(self, p1: Point, p2: Point, p3: Point):
        self.p1 = p1
        self.p2 = p2
        self.p3 = p3

    def area(self) -> float:
        # Вычисление площади по координатам вершин
        return 0.5 * abs(
            self.p1.x * (self.p2.y - self.p3.y) +
            self.p2.x * (self.p3.y - self.p1.y) +
            self.p3.x * (self.p1.y - self.p2.y)
        )

# ЗАДАНИЕ 10. Фабрика и конфигурация

class FigureFactory:
    @staticmethod
    def create_triangle_from_config(config_str: str) -> Triangle:
        """Создает треугольник из переданной JSON-строки конфигурации"""
        config = json.loads(config_str)
        data = config["default_triangle"]
        return Triangle(
            Point(data["p1"][0], data["p1"][1]),
            Point(data["p2"][0], data["p2"][1]),
            Point(data["p3"][0], data["p3"][1])
        )

# ТЕСТЫ ДЛЯ ЗАДАНИЯ 6 (Встроенный unittest)

class TestGeometry(unittest.TestCase):
    def test_triangle_area(self):
        p1 = Point(0, 0)
        p2 = Point(4, 0)
        p3 = Point(0, 3)
        triangle = Triangle(p1, p2, p3)
        self.assertAlmostEqual(triangle.area(), 6.0)

    def test_point_distance(self):
        pt1 = Point(0, 0)
        pt2 = Point(3, 4)
        self.assertAlmostEqual(pt1.distance_to(pt2), 5.0)

# ГЛАВНЫЙ ЗАПУСК

def main():
    print("=== ЗАДАНИЕ 6: Расчет площади треугольника ===")
    p1, p2, p3 = Point(0, 0), Point(4, 0), Point(0, 3)
    t = Triangle(p1, p2, p3)
    print(f"Координаты вершин: (0,0), (4,0), (0,3). Площадь = {t.area()}")

    print("\n=== ЗАДАНИЕ 10: Работа FigureFactory из демо-конфигурации ===")
    # Пример структуры файла config.json, имитируемый строкой:
    mock_json_config = """
    {
        "default_triangle": {
            "p1":,
            "p2":,
            "p3": [0, 8]
        }
    }
    """
    try:
        t_config = FigureFactory.create_triangle_from_config(mock_json_config)
        print(f"Площадь фигуры, созданной фабрикой из конфига: {t_config.area()}")
    except Exception as e:
        print(f"Ошибка парсинга конфигурации: {e}")

    print("\n=== ЗАПУСК ВСТРОЕННЫХ ТЕСТОВ (UNITTEST) ===")
    # Удаляем аргументы командной строки, чтобы unittest не ругался при вызове
    sys.argv = sys.argv[:1]
    unittest.main(exit=False)


if __name__ == "__main__":
    main()
