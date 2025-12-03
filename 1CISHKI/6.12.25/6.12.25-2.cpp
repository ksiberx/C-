#include <iostream>
#include <cmath>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    const int n = 3;

    // Коэффициенты системы из задания 5a
    double a[n][n + 1] = {
        {22.52, -4.62, -1.41, 0.53},
        {-5.10, -28.37, 4.58, -8.78},
        {4.68, -1.91, 23.85, 5.14}
    };

    double x[n] = { 0, 0, 0 };  // начальные значения
    int iter = 0;

    cout << "Метод Зейделя:\n";

    // Основной цикл
    while (iter < 100) {
        double max_change = 0;

        // Обновляем каждую переменную
        for (int i = 0; i < n; i++) {
            double old_x = x[i];  // сохраняем старое значение
            double sum = a[i][n];

            // Вычитаем произведения
            for (int j = 0; j < n; j++) {
                if (j != i) {
                    sum -= a[i][j] * x[j];
                }
            }

            // Новое значение
            x[i] = sum / a[i][i];

            // Максимальное изменение
            double change = fabs(x[i] - old_x);
            if (change > max_change) {
                max_change = change;
            }
        }

        iter++;

        // Выводим промежуточные результаты
        if (iter == 1 || iter == 2 || iter == 5 || iter == 10) {
            cout << "Итерация " << iter << ": ";
            cout << "x1=" << x[0] << ", x2=" << x[1] << ", x3=" << x[2] << endl;
        }

        // Проверка точности (до 0.0001)
        if (max_change < 0.0001) {
            cout << "Достигнута точность на итерации " << iter << endl;
            break;
        }
    }

    cout << "\nИтоговое решение:\n";
    cout << "x1 = " << x[0] << endl;
    cout << "x2 = " << x[1] << endl;
    cout << "x3 = " << x[2] << endl;
    cout << "Всего итераций: " << iter << endl;

    return 0;
}
