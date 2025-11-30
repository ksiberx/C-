#include <iostream>
#include <cmath>
using namespace std;

double f(double x) {                      // функция
    return 2 - 5*log(x) - 3*x;
}

double df(double x) {                     // производная
    return -5.0/x - 3;
}

int main() {
    setlocale(LC_ALL, "ru");
    double a = 0.5, b = 1.0;              // интервал
    double eps = 1e-6;
    double x1 = a, x2 = b;

    if (f(a)*df(b) > 0) swap(a, b);       // выбираем стороны

    while (fabs(x1 - x2) > eps) {         // два приближения
        x1 = (a*f(b)-b*f(a))/(f(b)-f(a)); // шаг метода хорд
        x2 = b - f(b)/df(b);              // шаг касательных

        if (f(x1)*f(b) < 0) a = x1;       // обновляем интервал
        else b = x1;
        b = x2;                           // уточнение касательной
    }

    cout << "Корень (комбинированный): " << (x1+x2)/2 << endl;
    return 0;
}
