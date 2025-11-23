#include <iostream>
#include <cmath>
using namespace std;

// 1. Шаблон функции для среднего арифметического
template <typename T>
T average(T arr[], int size) {
    T sum = 0;
    for (int i = 0; i < size; i++) sum += arr[i];
    return sum / size;
}

// 2. Перегруженные шаблоны для уравнений
template <typename T>
void solveEquation(T a, T b) {
    if (a == 0) {
        if (b == 0) cout << "Бесконечное количество решений" << endl;
        else cout << "Нет решений" << endl;
    }
    else {
        T x = -b / a;
        cout << "Корень: x = " << x << endl;
    }
}

template <typename T>
void solveEquation(T a, T b, T c) {
    if (a == 0) {
        solveEquation(b, c);
        return;
    }

    T d = b * b - 4 * a * c;

    if (d > 0) {
        T x1 = (-b + sqrt(d)) / (2 * a);
        T x2 = (-b - sqrt(d)) / (2 * a);
        cout << "Два корня: x1 = " << x1 << ", x2 = " << x2 << endl;
    }
    else if (d == 0) {
        T x = -b / (2 * a);
        cout << "Один корень: x = " << x << endl;
    }
    else {
        cout << "Действительных корней нет" << endl;
    }
}

// 3. Функция округления
double roundNumber(double num, int decimals) {
    double multiplier = pow(10, decimals);
    return round(num * multiplier) / multiplier;
}

// 4. Шаблонные функции для максимума
template <typename T>
T findMax(T arr[], int size) {
    T max = arr[0];
    for (int i = 1; i < size; i++) if (arr[i] > max) max = arr[i];
    return max;
}

template <typename T>
T findMax(T arr[][3], int rows) {
    T max = arr[0][0];
    for (int i = 0; i < rows; i++)
        for (int j = 0; j < 3; j++)
            if (arr[i][j] > max) max = arr[i][j];
    return max;
}

template <typename T>
T findMax(T arr[][2][3], int depth) {
    T max = arr[0][0][0];
    for (int i = 0; i < depth; i++)
        for (int j = 0; j < 2; j++)
            for (int k = 0; k < 3; k++)
                if (arr[i][j][k] > max) max = arr[i][j][k];
    return max;
}

// 5. Перегруженные функции для максимума
int findMax(int a, int b) {
    return (a > b) ? a : b;
}

int findMax(int a, int b, int c) {
    return findMax(findMax(a, b), c);
}

int main() {
    setlocale(LC_ALL, "ru");
    // Тест 1: Среднее арифметическое
    int arr1[] = { 1, 2, 3, 4, 5 };
    cout << "Среднее: " << average(arr1, 5) << endl;

    // Тест 2: Уравнения
    solveEquation(2.0, -4.0);
    solveEquation(1.0, -3.0, 2.0);

    // Тест 3: Округление
    cout << "Округление: " << roundNumber(3.14159, 2) << endl;

    // Тест 4: Максимум в массивах
    int arr2[] = { 3, 7, 2, 9, 1 };
    int arr2D[2][3] = { {1, 5, 3}, {9, 2, 7} };
    int arr3D[2][2][3] = { {{1, 2, 3}, {4, 5, 6}}, {{7, 8, 9}, {10, 11, 12}} };

    cout << "Макс 1D: " << findMax(arr2, 5) << endl;
    cout << "Макс 2D: " << findMax(arr2D, 2) << endl;
    cout << "Макс 3D: " << findMax(arr3D, 2) << endl;

    // Тест 5: Максимум чисел
    cout << "Макс 2 чисел: " << findMax(5, 3) << endl;
    cout << "Макс 3 чисел: " << findMax(5, 3, 8) << endl;

    return 0;
}