#include <iostream>
#include <cmath>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    const int n = 3;

    double a[n][n + 1] = {
        {16.63, -0.24, -6.10, 7.29},
        {-3.45, -23.13, 1.11, -3.41},
        {3.76, -8.72, -27.01, -8.19}
    };

    double x[n] = { 0, 0, 0 };     // текущие значения
    double x_new[n];            // новые значения
    int iter = 0;

    cout << "Метод простых итераций:\n";

    // Основной цикл итераций
    while (iter < 100) {
        // Вычисляем новые значения
        for (int i = 0; i < n; i++) {
            double sum = a[i][n]; 

            // Вычитаем произведения на другие переменные
            for (int j = 0; j < n; j++) {
                if (j != i) {
                    sum -= a[i][j] * x[j];
                }
            }

            // Делим на диагональный элемент
            x_new[i] = sum / a[i][i];
        }

        iter++;

        // Выводим текущее приближение
        if (iter == 1 || iter == 2 || iter == 5 || iter == 10 || iter == 20) {
            cout << "Итерация " << iter << ": ";
            cout << "x1=" << x_new[0] << ", x2=" << x_new[1] << ", x3=" << x_new[2] << endl;
        }

        // Обновляем значения
        for (int i = 0; i < n; i++) {
            x[i] = x_new[i];
        }

        // Проверяем точность (до сотых)
        if (iter > 5) {
            bool accurate = true;
            for (int i = 0; i < n; i++) {
                if (fabs(x_new[i] - x[i]) > 0.01) {
                    accurate = false;
                    break;
                }
            }
            if (accurate) break;
        }
    }

    cout << "\nИтоговое решение:\n";
    cout << "x1 = " << x[0] << endl;
    cout << "x2 = " << x[1] << endl;
    cout << "x3 = " << x[2] << endl;
    cout << "Всего итераций: " << iter << endl;

    return 0;
}
