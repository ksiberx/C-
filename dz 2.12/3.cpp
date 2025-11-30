#include <iostream>               
#include <cmath>                  
using namespace std;

double f(double x) {              // функция
    return sin(x) - x;            // уравнение
}

int main() {
    double a = 1.1, b = 1.2;      // переменные
    double eps = 1e-4;            
    int N = (int)(log((b - a) / eps) / log(2)) + 1;      

    double c;                     
    for (int i = 0; i < N; i++) { // делаем N шагов
        c = (a + b) / 2;          
        if (f(a) * f(c) <= 0)    
            b = c;                
        else
            a = c;                
    }

    cout << "x = " << c;          // выводим корень
    return 0;
}