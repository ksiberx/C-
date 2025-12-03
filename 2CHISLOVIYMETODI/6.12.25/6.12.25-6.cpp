#include <iostream>
#include <cmath>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    cout << "Задание 6: Решение системы 5x5" << endl;

    // Система 5x5
    double A[5][5] = {
        {5.38, 7.33, -0.24, -0.49, -8.41},
        {2.81, -4.69, -6.13, -3.05, -5.19},
        {7.60, 4.78, 8.59, 0.98, 6.72},
        {-8.44, -8.53, 5.76, -8.34, 4.96},
        {0.61, 4.63, -4.04, 1.72, 3.61}
    };
    double b[5] = { 4.27, 5.77, 3.70, 5.95, -6.77 };
    double x[5];

    for (int i = 0; i < 5; i++) {
        for (int k = i + 1; k < 5; k++) {
            double f = A[k][i] / A[i][i];
            for (int j = i; j < 5; j++) A[k][j] -= f * A[i][j];
            b[k] -= f * b[i];
        }
    }

    for (int i = 4; i >= 0; i--) {
        x[i] = b[i];
        for (int j = i + 1; j < 5; j++) x[i] -= A[i][j] * x[j];
        x[i] /= A[i][i];
    }

    cout << "Решение: ";
    for (int i = 0; i < 5; i++) cout << x[i] << " ";
    cout << endl;

    return 0;
}
