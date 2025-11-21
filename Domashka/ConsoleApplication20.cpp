#include <iostream>
using namespace std;

// Задание 1: Пузырьковая сортировка
void bubbleSort(int arr[], int n) {
    for (int i = 0; i < n - 1; i++)
        for (int j = 0; j < n - i - 1; j++)
            if (arr[j] > arr[j + 1])
                swap(arr[j], arr[j + 1]);
}

// Задание 2: Сортировка вставками
void insertionSort(int arr[], int n) {
    for (int i = 1; i < n; i++) {
        int key = arr[i], j = i - 1;
        while (j >= 0 && arr[j] > key) {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = key;
    }
}

// Задание 3: Успеваемость
void studentGrades() {
    int g[10];
    cout << "Введите 10 оценок: ";
    for (int i = 0; i < 10; i++) cin >> g[i];

    int c;
    do {
        cout << "\n1. Вывод\n2. Пересдача\n3. Стипендия\n4. Выход\nВыбор: ";
        cin >> c;
        if (c == 1) {
            for (int i = 0; i < 10; i++) cout << g[i] << " ";
            cout << endl;
        }
        else if (c == 2) {
            int i, n;
            cout << "Номер и новая оценка: ";
            cin >> i >> n;
            if (i >= 1 && i <= 10) g[i - 1] = n;
        }
        else if (c == 3) {
            double s = 0;
            for (int i = 0; i < 10; i++) s += g[i];
            cout << "Средний: " << s / 10 << endl;
            if (s / 10 >= 10.7) cout << "Стипендия да\n";
            else cout << "Стипендия нет\n";
        }
    } while (c != 4);
}

// Задание 4: Быстрая сортировка
void quickSort(int arr[], int l, int r) {
    if (l < r) {
        int p = arr[r], i = l - 1;
        for (int j = l; j < r; j++)
            if (arr[j] <= p) swap(arr[++i], arr[j]);
        swap(arr[++i], arr[r]);
        quickSort(arr, l, i - 1);
        quickSort(arr, i + 1, r);
    }
}

// Задание 5: Специальная сортировка
void specialSort(int arr[], int n) {
    double s = 0;
    for (int i = 0; i < n; i++) s += arr[i];
    int p = (s / n > 0) ? (2 * n) / 3 : n / 3;

    for (int i = 0; i < p - 1; i++)
        for (int j = 0; j < p - i - 1; j++)
            if (arr[j] > arr[j + 1]) swap(arr[j], arr[j + 1]);

    for (int i = p; i < n - 1; i++)
        for (int j = p; j < n - 1; j++)
            if (arr[j] < arr[j + 1]) swap(arr[j], arr[j + 1]);
}

void print(int arr[], int n) {
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    cout << endl;
}

int main() {
    setlocale(LC_ALL, "ru");
    int a1[] = { 64,34,25,12,22,11,90 }, n = 7;

    cout << "1. Пузырьковая:\n";
    print(a1, n); bubbleSort(a1, n); print(a1, n);

    int a2[] = { 64,34,25,12,22,11,90 };
    cout << "2. Вставками:\n";
    print(a2, n); insertionSort(a2, n); print(a2, n);

    cout << "3. Успеваемость:\n";
    studentGrades();

    int a4[] = { 64,34,25,12,22,11,90 };
    cout << "4. Быстрая:\n";
    print(a4, n); quickSort(a4, 0, n - 1); print(a4, n);

    int a5[] = { 5,-2,8,1,-3,7,4,-1,6 };
    cout << "5. Специальная:\n";
    print(a5, 9); specialSort(a5, 9); print(a5, 9);

    return 0;
}