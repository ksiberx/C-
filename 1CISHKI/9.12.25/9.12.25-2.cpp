#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int M, N;

    cout << "Введите M: "; cin >> M;
    cout << "Введите N: "; cin >> N;

    int* A = new int[M];
    int* B = new int[N];

    cout << "Введите элементы массива A: ";
    for (int i = 0; i < M; i++) cin >> A[i];

    cout << "Введите элементы массива B: ";
    for (int i = 0; i < N; i++) cin >> B[i];

    // Временный массив (максимум M+N элементов)
    int* temp = new int[M + N];
    int k = 0;

    // Элементы из A, которых нет в B
    for (int i = 0; i < M; i++) {
        bool found = false;

        // Проверяем, есть ли A[i] в B
        for (int j = 0; j < N; j++) {
            if (A[i] == B[j]) {
                found = true;
                break;
            }
        }

        // Если нет в B, проверяем дубликаты в temp
        if (!found) {
            bool duplicate = false;
            for (int j = 0; j < k; j++) {
                if (temp[j] == A[i]) {
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate) {
                temp[k] = A[i];
                k++;
            }
        }
    }

    // Элементы из B, которых нет в A
    for (int i = 0; i < N; i++) {
        bool found = false;

        // Проверяем, есть ли B[i] в A
        for (int j = 0; j < M; j++) {
            if (B[i] == A[j]) {
                found = true;
                break;
            }
        }

        // Если нет в A, проверяем дубликаты в temp
        if (!found) {
            bool duplicate = false;
            for (int j = 0; j < k; j++) {
                if (temp[j] == B[i]) {
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate) {
                temp[k] = B[i];
                k++;
            }
        }
    }

    // Создаем итоговый массив минимального размера
    int* C = new int[k];
    for (int i = 0; i < k; i++) {
        C[i] = temp[i];
    }

    cout << "Элементы, не общие для A и B: ";
    for (int i = 0; i < k; i++) {
        cout << C[i] << " ";
    }
    if (k == 0) cout << "нет таких элементов";

    // Очищаем память
    delete[] A;
    delete[] B;
    delete[] temp;
    delete[] C;

    return 0;
}
