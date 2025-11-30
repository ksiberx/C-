#include <iostream>           
#include <cmath>              
using namespace std;

double phi(double x) {        // функция 
    return 0.4 * (1 - sin(x));  // уравнение
}

int main() {
    double x = 1.1;           
    double eps = 1e-6;        // точность 10^-6
    double xn;                

    do {
        xn = phi(x);           // считаем новое x
        x = xn;                 // обновляем x
    } while (fabs(phi(x) - x) > eps);  // пока разница > eps

    cout << "x = " << x;   // вывод
    return 0;
}