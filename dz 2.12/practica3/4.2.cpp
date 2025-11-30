#include <iostream>
#include <cmath>
using namespace std;

double f(double x) {            // сама функция
    return 2 - 5*log(x) - 3*x;
}

double df(double x) {           // производная функции
    return -5.0/x - 3;
}

int main() {
    setlocale(LC_ALL, "ru");
    double x = 1.0;             // стартовое значение
    double eps = 1e-6;          // точность

    while (fabs(f(x)) > eps) {  // повторяем пока ошибка большая
        x = x - f(x)/df(x);     // формула метода касательных
    }

    cout << "Корень (касательные): " << x << endl;
    return 0;
}
