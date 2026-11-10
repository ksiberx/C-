#include <iostream>
#include <cmath>
#include <memory>
#include <utility>
#include <chrono>

using namespace std;

// ЗАДАНИЕ 1. Классы Point, Figure и Triangle

class Point {
private:
    double x;
    double y;
public:
    Point(double x = 0.0, double y = 0.0) : x(x), y(y) {}
    
    double distanceTo(const Point& other) const {
        return sqrt((x - other.x) * (x - other.x) + (y - other.y) * (y - other.y));
    }
    
    double getX() const { return x; }
    double getY() const { return y; }
};

// Абстрактный базовый класс
class Figure {
public:
    virtual ~Figure() {} // Виртуальный деструктор
    virtual double area() const = 0; // Чисто виртуальный метод
};

// Класс-наследник
class Triangle : public Figure {
private:
    Point p1, p2, p3;
public:
    Triangle(const Point& a, const Point& b, const Point& c) : p1(a), p2(b), p3(c) {}
    
    virtual double area() const override {
        // Вычисление площади треугольника по координатам (Формула Гаусса)
        return 0.5 * abs(p1.getX() * (p2.getY() - p3.getY()) + 
                         p2.getX() * (p3.getY() - p1.getY()) + 
                         p3.getX() * (p1.getY() - p2.getY()));
    }
};

// ЗАДАНИЕ 2. Встроенная функция НОД (Inline)
inline int gcd(int a, int b) {
    while (b != 0) {
        int temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

// ЗАДАНИЕ 5. Класс Matrix с Move-семантикой
class Matrix {
private:
    int rows;
    int cols;
    unique_ptr<double> data; // Массив данных на умных указателях

public:
    Matrix(int r, int c) : rows(r), cols(c), data(make_unique<double>(r * c)) {}

    // Перемещающий конструктор (Move Constructor)
    Matrix(Matrix&& other) noexcept 
        : rows(other.rows), cols(other.cols), data(move(other.data)) {
        other.rows = 0;
        other.cols = 0;
    }

    // Запрещаем явное копирование (так как unique_ptr уникален)
    Matrix(const Matrix&) = delete;
    Matrix& operator=(const Matrix&) = delete;

    void set(int r, int c, double val) { data[r * cols + c] = val; }
    double get(int r, int c) const { return data[r * cols + c]; }
};

// ГЛАВНАЯ ФУНКЦИЯ ДЕМОНСТРАЦИИ (main)
int main() {
    cout << "=== ЗАДАНИЕ 1: Абстрактные классы и полиморфизм ===" << endl;
    Point p1(0.0, 0.0);
    Point p2(4.0, 0.0);
    Point p3(0.0, 3.0);

    Figure* myTriangle = new Triangle(p1, p2, p3);
    cout << "Площадь треугольника через указатель Figure*: " << myTriangle->area() << endl;
    delete myTriangle;

    cout << "\n=== ЗАДАНИЕ 2: Тест функции GCD (НОД) ===" << endl;
    cout << "НОД чисел 54 и 24 = " << gcd(54, 24) << endl;

    cout << "\n=== ЗАДАНИЕ 3: Debug vs Release (Замер времени) ===" << endl;
    auto start = chrono::high_resolution_clock::now();
    double sum = 0.0;
    for (int i = 1; i <= 10000000; ++i) {
        sum += 1.0 / i;
    }
    auto end = chrono::high_resolution_clock::now();
    chrono::duration<double> duration = end - start;
    cout << "Результат суммы: " << sum << endl;
    cout << "Время расчета: " << duration.count() << " секунд" << endl;

    cout << "\n=== ЗАДАНИЕ 5: Умные указатели и Move-конструктор ===" << endl;
    Matrix m1(3, 3);
    m1.set(0, 0, 99.5);
    cout << "Создана матрица m1. Элемент = " << m1.get(0, 0) << endl;

    Matrix m2 = move(m1); // Перемещение ресурсов
    cout << "Ресурсы перемещены в m2. Элемент в m2 = " << m2.get(0, 0) << endl;

    return 0;
}
