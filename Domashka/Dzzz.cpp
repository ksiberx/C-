#include <iostream>
using namespace std;
setlocale(LC_ALL, "ru");

// Задание 3
void rect(int N, int K) {
    cout << "Прямоугольник " << N << " x " << K << ":\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < K; j++) cout << "*";
        cout << "\n";
    }
}

// Задание 4
long long fact(int n) {
    cout << "Факториал числа " << n << " равен: ";
    long long f = 1;
    for (int i = 1; i <= n; i++) f *= i;
    return f;
}

// Задание 5
bool prime(int n) {
    cout << "Проверка числа " << n << " на простоту...\n";
    if (n < 2) return false;
    for (int i = 2; i * i <= n; i++)
        if (n % i == 0) return false;
    return true;
}

// Задание 6
void minmax(int *a, int n) {
    cout << "Поиск минимума и максимума...\n";
    int mn = a[0], mx = a[0];
    int imn = 0, imx = 0;

    for (int i = 1; i < n; i++) {
        if (a[i] < mn) { mn = a[i]; imn = i; }
        if (a[i] > mx) { mx = a[i]; imx = i; }
    }

    cout << "Минимальное значение: " << mn << ", индекс: " << imn << "\n";
    cout << "Максимальное значение: " << mx << ", индекс: " << imx << "\n";
}

// Задание 7
void reverseArr(int *a, int n) {
    cout << "Переворачиваю массив...\n";
    for (int i = 0; i < n / 2; i++) {
        int t = a[i];
        a[i] = a[n - 1 - i];
        a[n - 1 - i] = t;
    }
    cout << "Массив перевернут.\n";
}
