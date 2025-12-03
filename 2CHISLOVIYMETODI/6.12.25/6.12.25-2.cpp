#include <iostream>
#include <cmath>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    double A[3][3] = {
        {6.44, 1.90, 0.35},
        {6.64, 5.24, -8.02},
        {1.39, -1.00, -0.20}
    };
    double det = 1.0;

    for (int i = 0; i < 3; i++) {
        // 1. Ищем максимальный элемент в столбце
        int maxRow = i;
        for (int k = i + 1; k < 3; k++)
            if (fabs(A[k][i]) > fabs(A[maxRow][i]))
                maxRow = k;

        // 2. Переставляем строки (меняем знак определителя)
        if (maxRow != i) {
            for (int j = 0; j < 3; j++)
                swap(A[i][j], A[maxRow][j]);
            det *= -1; // при перестановке строк определитель меняет знак
        }

        // 3. Если ведущий элемент 0 — определитель = 0
        if (fabs(A[i][i]) < 1e-12) {
            det = 0;
            break;
        }

        // 4. Умножаем определитель на диагональный элемент
        det *= A[i][i];

        // 5. Обнуляем элементы ниже диагонали
        for (int k = i + 1; k < 3; k++) {
            double factor = A[k][i] / A[i][i];
            for (int j = i + 1; j < 3; j++)
                A[k][j] -= factor * A[i][j];
        }
    }

    cout << "ЗАДАНИЕ 2: Определитель матрицы" << endl;
    cout << "det = " << det << endl;

    return 0;
}
