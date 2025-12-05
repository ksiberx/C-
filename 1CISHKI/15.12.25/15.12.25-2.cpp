#include <iostream>
using namespace std;

bool inArray(int x, int arr[], int size) {
    for (int i = 0; i < size; i++) {
        if (arr[i] == x) return true;
    }
    return false;
}

void task2() {
    int M, N;
    cout << "Введите M и N: ";
    cin >> M >> N;

    int* A = new int[M];
    int* B = new int[N];

    cout << "Введите массив A: ";
    for (int i = 0; i < M; i++) cin >> A[i];

    cout << "Введите массив B: ";
    for (int i = 0; i < N; i++) cin >> B[i];

    int* temp = new int[M + N];
    int k = 0;

    // Элементы из A, которых нет в B
    for (int i = 0; i < M; i++) {
        if (!inArray(A[i], B, N) && !inArray(A[i], temp, k)) {
            temp[k++] = A[i];
        }
    }

    // Элементы из B, которых нет в A
    for (int i = 0; i < N; i++) {
        if (!inArray(B[i], A, M) && !inArray(B[i], temp, k)) {
            temp[k++] = B[i];
        }
    }

    int* C = new int[k];
    for (int i = 0; i < k; i++) C[i] = temp[i];

    cout << "Результат: ";
    for (int i = 0; i < k; i++) cout << C[i] << " ";
    cout << endl;

    delete[] A;
    delete[] B;
    delete[] temp;
    delete[] C;
}

int main() {
    setlocale(LC_ALL, "ru");
    task2();
    return 0;
}
