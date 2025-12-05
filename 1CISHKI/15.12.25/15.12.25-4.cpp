#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int M;
    cout << "Введите размер массива M: ";
    cin >> M;

    int* A = new int[M];

    cout << "Введите элементы массива: ";
    for (int i = 0; i < M; i++) cin >> A[i];

    int choice;
    cout << "\nВыберите:\n1 - Удалить четные\n2 - Удалить нечетные\nВаш выбор: ";
    cin >> choice;

    int* temp = new int[M];
    int k = 0;

    if (choice == 1) {
        for (int i = 0; i < M; i++) {
            if (A[i] % 2 != 0) {
                temp[k++] = A[i];
            }
        }
    }
    else if (choice == 2) {
        for (int i = 0; i < M; i++) {
            if (A[i] % 2 == 0) {
                temp[k++] = A[i];
            }
        }
    }
    else {
        cout << "Ошибка!" << endl;
        delete[] A;
        delete[] temp;
        return 1;
    }

    int* result = new int[k];
    for (int i = 0; i < k; i++) result[i] = temp[i];

    cout << "Результат: ";
    for (int i = 0; i < k; i++) cout << result[i] << " ";
    cout << endl;

    delete[] A;
    delete[] temp;
    delete[] result;

    return 0;
}
