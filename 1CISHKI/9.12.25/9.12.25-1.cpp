#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int M, N;

    cout << "Введите M: "; cin >> M;
    cout << "Введите N: "; cin >> N;

    // Создаем динамические массивы
    int* A = new int[M];
    int* B = new int[N];

    cout << "Введите элементы массива A: ";
    for (int i = 0; i < M; i++) cin >> A[i];

    cout << "Введите элементы массива B: ";
    for (int i = 0; i < N; i++) cin >> B[i];

    // Временный массив для хранения результата
    int* temp = new int[M]; // максимум M элементов
    int k = 0;

    for (int i = 0; i < M; i++) {
        // Проверяем, есть ли A[i] в B
        bool foundInB = false;
        for (int j = 0; j < N; j++) {
            if (A[i] == B[j]) {
                foundInB = true;
                break;
            }
        }

        // Если нет в B, проверяем дубликаты в temp
        if (!foundInB) {
            bool duplicate = false;
            for (int j = 0; j < k; j++) {
                if (temp[j] == A[i]) {
                    duplicate = true;
                    break;
                }
            }

            // Добавляем, если не дубликат
            if (!duplicate) {
                temp[k] = A[i];
                k++;
            }
        }
    }

    // Создаем итоговый массив минимального размера
    int* C = new int[k];
    for (int i = 0; i < k; i++) {
        C[i] = temp[i];
    }

    cout << "Результат (элементы A, которых нет в B): ";
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
