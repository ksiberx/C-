#include <iostream>
#include <cmath>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    double A[3][3] = {
        {8.47, 4.22, 4.07},
        {1.49, 6.92, 5.82},
        {2.42, -0.88, -5.24}
    };

    double Aug[3][6];

    // 1. Заполняем левую часть — исходная матрица A
    // 2. Заполняем правую часть — единичная матрица
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++)
            Aug[i][j] = A[i][j];
        for (int j = 3; j < 6; j++)
            Aug[i][j] = (j - 3 == i) ? 1.0 : 0.0;
    }

    for (int i = 0; i < 3; i++) {
        // Делаем диагональный элемент = 1
        double pivot = Aug[i][i];
        for (int j = 0; j < 6; j++)
            Aug[i][j] /= pivot;

        // Обнуляем столбец в других строках
        for (int k = 0; k < 3; k++) {
            if (k != i) {
                double factor = Aug[k][i];
                for (int j = 0; j < 6; j++)
                    Aug[k][j] -= factor * Aug[i][j];
            }
        }
    }

    // 4. Выводим обратную матрицу (правая часть)
    cout << "Задание 3: Обратная матрица" << endl;
    for (int i = 0; i < 3; i++) {
        for (int j = 3; j < 6; j++)
            cout << Aug[i][j] << "\t";
        cout << endl;
    }

    return 0;
}
