#include <iostream>        
#include <cmath>            
using namespace std;        

double f(double x) {         // функция f(x)
    return sin(x) - x;       // само уравнение sin(x) - x
}

int main() {
    double a = 1.1, b = 1.2; 
    double eps = 1e-4;         // переменные
    double c;               

    while (b - a > eps) {    
        c = (a + b) / 2;     
        if (f(a) * f(c) <= 0) 
            b = c;           // корень в левой половине
        else
            a = c;           // корень в правой
    }

    cout << "x = " << (a + b) / 2; // вывод результата
    return 0;               
}
