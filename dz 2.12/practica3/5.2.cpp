#include <iostream>
#include <cmath>
using namespace std;

double f(double x) {                 // функция
    return 2 - 5*log(x) - 3*x;
}

int main() {
    setlocale(LC_ALL, "ru");
    double a = 0.5, b = 1.0;         // начальный отрезок
    double eps = 1e-3;               // требуемая точность
    double x;

    while (fabs(b - a) > eps) {      // пока отрезок большой
        x = (a*f(b)-b*f(a)) /        // формула хорды
            (f(b)-f(a));

        if (f(x)*f(a) < 0) b = x;    // уточняем границы
        else a = x;
    }

    cout << "Корень (хорды): " << (a+b)/2 << endl;
    return 0;
}
