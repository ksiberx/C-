#include <iostream>
#include <limits>

using namespace std;

class Fraction {
private:
    int numerator;   // числитель
    int denominator; // знаменатель

    // Функция для сокращения дроби
    void simplify() {
        int a = numerator;
        int b = denominator;

        // Находим НОД (наибольший общий делитель)
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }
        int gcd = a > 0 ? a : -a;

        // Сокращаем дробь
        if (gcd != 0) {
            numerator /= gcd;
            denominator /= gcd;
        }

        // Если знаменатель отрицательный, делаем его положительным
        // и меняем знак числителя
        if (denominator < 0) {
            numerator = -numerator;
            denominator = -denominator;
        }
    }

public:
    // Конструктор по умолчанию
    Fraction() : numerator(0), denominator(1) {}

    // Конструктор с параметрами
    Fraction(int num, int den) : numerator(num), denominator(den) {
        if (denominator == 0) {
            cout << "Ошибка: знаменатель не может быть равен 0! Устанавливаю знаменатель = 1." << endl;
            denominator = 1;
        }
        simplify();
    }

    // Функция для ввода данных
    void input() {
        cout << "Введите числитель: ";
        cin >> numerator;

        cout << "Введите знаменатель (не 0): ";
        cin >> denominator;

        while (denominator == 0) {
            cout << "Ошибка: знаменатель не может быть равен 0! Введите снова: ";
            cin >> denominator;
        }

        simplify();
    }

    // Функция для вывода дроби
    void display() const {
        if (denominator == 1) {
            cout << numerator;
        }
        else {
            cout << numerator << "/" << denominator;
        }
    }

    // Сложение дробей
    Fraction add(const Fraction& other) const {
        int num = numerator * other.denominator + other.numerator * denominator;
        int den = denominator * other.denominator;
        return Fraction(num, den);
    }

    // Вычитание дробей
    Fraction subtract(const Fraction& other) const {
        int num = numerator * other.denominator - other.numerator * denominator;
        int den = denominator * other.denominator;
        return Fraction(num, den);
    }

    // Умножение дробей
    Fraction multiply(const Fraction& other) const {
        int num = numerator * other.numerator;
        int den = denominator * other.denominator;
        return Fraction(num, den);
    }

    // Деление дробей
    Fraction divide(const Fraction& other) const {
        if (other.numerator == 0) {
            cout << "Ошибка: деление на 0! Возвращаю дробь 0/1." << endl;
            return Fraction(0, 1);
        }

        int num = numerator * other.denominator;
        int den = denominator * other.numerator;
        return Fraction(num, den);
    }

    // Перегрузка оператора + для сложения
    Fraction operator+(const Fraction& other) const {
        return add(other);
    }

    // Перегрузка оператора - для вычитания
    Fraction operator-(const Fraction& other) const {
        return subtract(other);
    }

    // Перегрузка оператора * для умножения
    Fraction operator*(const Fraction& other) const {
        return multiply(other);
    }

    // Перегрузка оператора / для деления
    Fraction operator/(const Fraction& other) const {
        return divide(other);
    }
};

int main() {
    setlocale(LC_ALL, "ru");
    cout << "============= Класс Дробь на C++ =============" << endl;

    // Создание и ввод первой дроби
    cout << "\nПервая дробь:" << endl;
    Fraction frac1;
    frac1.input();

    // Создание и ввод второй дроби
    cout << "\nВторая дробь:" << endl;
    Fraction frac2;
    frac2.input();

    cout << "\n----- Результаты операций -----" << endl;

    // Вывод дробей
    cout << "Дробь 1: ";
    frac1.display();
    cout << endl;

    cout << "Дробь 2: ";
    frac2.display();
    cout << endl;

    // Арифметические операции
    Fraction result;

    cout << "\n-------- С использованием методов класса --------" << endl;

    // Сложение
    result = frac1.add(frac2);
    cout << "Сложение: ";
    frac1.display();
    cout << " + ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    // Вычитание
    result = frac1.subtract(frac2);
    cout << "Вычитание: ";
    frac1.display();
    cout << " - ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    // Умножение
    result = frac1.multiply(frac2);
    cout << "Умножение: ";
    frac1.display();
    cout << " * ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    // Деление
    result = frac1.divide(frac2);
    cout << "Деление: ";
    frac1.display();
    cout << " / ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    cout << "\n-------- С использованием перегруженных операторов --------" << endl;

    // Те же операции с помощью перегруженных операторов
    result = frac1 + frac2;
    cout << "Сложение (+): ";
    frac1.display();
    cout << " + ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    result = frac1 - frac2;
    cout << "Вычитание (-): ";
    frac1.display();
    cout << " - ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    result = frac1 * frac2;
    cout << "Умножение (*): ";
    frac1.display();
    cout << " * ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    result = frac1 / frac2;
    cout << "Деление (/): ";
    frac1.display();
    cout << " / ";
    frac2.display();
    cout << " = ";
    result.display();
    cout << endl;

    // Пример работы с конструктором
    cout << "\n-------- Пример с конструктором --------" << endl;
    Fraction frac3(3, 4);
    Fraction frac4(1, 2);
    Fraction frac5 = frac3 + frac4;

    cout << "3/4 + 1/2 = ";
    frac5.display();
    cout << endl;

    return 0;
}
