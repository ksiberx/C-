#include <iostream>
#include <cmath>
using namespace std;

// 1 задание
struct Rect {
    double x, y;    
    double width, height;
};

void moveRect(Rect& r, double dx, double dy) {
    r.x += dx;
    r.y += dy;
}

void resizeRect(Rect& r, double dw, double dh) {
    r.width += dw;
    r.height += dh;
}

void printRect(Rect r) {
    cout << "Прямоугольник: (" << r.x << "," << r.y << ") ";
    cout << "ширина=" << r.width << " высота=" << r.height << endl;
}

// 2 задание
struct Point {
    double x, y;
};

double distance(Point p1, Point p2) {
    return sqrt(pow(p2.x - p1.x, 2) + pow(p2.y - p1.y, 2));
}

// 3 задание
struct Fraction {
    int num;    
    int den;  
};

int gcd(int a, int b) {
    while (b) {
        int temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

void simplify(Fraction& f) {
    int d = gcd(abs(f.num), abs(f.den));
    f.num /= d;
    f.den /= d;
}

Fraction add(Fraction a, Fraction b) {
    Fraction res;
    res.num = a.num * b.den + b.num * a.den;
    res.den = a.den * b.den;
    simplify(res);
    return res;
}

Fraction subtract(Fraction a, Fraction b) {
    Fraction res;
    res.num = a.num * b.den - b.num * a.den;
    res.den = a.den * b.den;
    simplify(res);
    return res;
}

Fraction multiply(Fraction a, Fraction b) {
    Fraction res;
    res.num = a.num * b.num;
    res.den = a.den * b.den;
    simplify(res);
    return res;
}

Fraction divide(Fraction a, Fraction b) {
    Fraction res;
    res.num = a.num * b.den;
    res.den = a.den * b.num;
    simplify(res);
    return res;
}

void printFraction(Fraction f) {
    cout << f.num << "/" << f.den;
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "1 задание" << endl;
    Rect r = { 0, 0, 10, 5 };
    printRect(r);
    moveRect(r, 3, 2);
    cout << "После перемещения: ";
    printRect(r);
    resizeRect(r, 5, -1);
    cout << "После изменения размера: ";
    printRect(r);

    cout << "\n2 задание" << endl;
    Point p1 = { 0, 0 };
    Point p2 = { 3, 4 };
    cout << "Точка 1: (" << p1.x << "," << p1.y << ")" << endl;
    cout << "Точка 2: (" << p2.x << "," << p2.y << ")" << endl;
    cout << "Расстояние: " << distance(p1, p2) << endl;

    cout << "\n3 задание" << endl;
    Fraction f1 = { 1, 2 };
    Fraction f2 = { 3, 4 };

    cout << "Дробь 1: "; printFraction(f1); cout << endl;
    cout << "Дробь 2: "; printFraction(f2); cout << endl;

    Fraction res = add(f1, f2);
    cout << "Сумма: "; printFraction(res); cout << endl;

    res = subtract(f1, f2);
    cout << "Разность: "; printFraction(res); cout << endl;

    res = multiply(f1, f2);
    cout << "Умножение: "; printFraction(res); cout << endl;

    res = divide(f1, f2);
    cout << "Деление: "; printFraction(res); cout << endl;

    return 0;
}
