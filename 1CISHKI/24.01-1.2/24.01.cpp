#include <iostream>
using namespace std;

// 1 задание
struct Complex {
    double r, i;
};

void showComplex(Complex c) {
    cout << c.r;
    if (c.i >= 0) cout << "+" << c.i << "i";
    else cout << c.i << "i";
}

Complex add(Complex a, Complex b) {
    Complex res;
    res.r = a.r + b.r;
    res.i = a.i + b.i;
    return res;
}

Complex subtract(Complex a, Complex b) {
    Complex res;
    res.r = a.r - b.r;
    res.i = a.i - b.i;
    return res;
}

Complex multiply(Complex a, Complex b) {
    Complex res;
    res.r = a.r * b.r - a.i * b.i;
    res.i = a.r * b.i + a.i * b.r;
    return res;
}

Complex divide(Complex a, Complex b) {
    Complex res;
    double d = b.r * b.r + b.i * b.i;
    res.r = (a.r * b.r + a.i * b.i) / d;
    res.i = (a.i * b.r - a.r * b.i) / d;
    return res;
}

// 2 задание
struct Car {
    double length, clearance, engineVolume, power, wheels;
    string color, transmission;

    void set(double l, double c, double v, double p, double w, string col, string t) {
        length = l; clearance = c; engineVolume = v;
        power = p; wheels = w; color = col; transmission = t;
    }

    void show() {
        cout << "Авто: " << color << ", " << power << " л.с., " << transmission << endl;
    }

    bool search(string param, string value) {
        if (param == "цвет") return color == value;
        if (param == "коробка") return transmission == value;
        return false;
    }
};

int main() {
    setlocale(LC_ALL, "ru");
    cout << "1 задание" << endl;
    Complex c1 = { 2, 3 };
    Complex c2 = { 1, -1 };

    cout << "c1 = "; showComplex(c1); cout << endl;
    cout << "c2 = "; showComplex(c2); cout << endl;

    cout << "Сумма: "; showComplex(add(c1, c2)); cout << endl;
    cout << "Разность: "; showComplex(subtract(c1, c2)); cout << endl;
    cout << "Умножение: "; showComplex(multiply(c1, c2)); cout << endl;
    cout << "Деление: "; showComplex(divide(c1, c2)); cout << endl;

    cout << "\n2 задание" << endl;
    Car myCar;
    myCar.set(4.2, 0.18, 1.6, 120, 16, "черный", "механика");
    myCar.show();

    if (myCar.search("цвет", "черный"))
        cout << "Цвет: черный (найдено)" << endl;

    return 0;
}
