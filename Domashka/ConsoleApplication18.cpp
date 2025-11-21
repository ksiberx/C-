#include <iostream>
using namespace std;

// Задание 1: Степень числа
int power(int base, int exp) {
    if (exp == 0) return 1;
    return base * power(base, exp - 1);
}

// Задание 2: Звезды в ряд
void stars(int n) {
    if (n == 0) return;
    cout << "*";
    stars(n - 1);
}

// Задание 3: Сумма в диапазоне
int sumRange(int a, int b) {
    if (a > b) return 0;
    return a + sumRange(a + 1, b);
}

// Задание 4: Минимальная сумма 10 чисел
int findMinSumPos(int arr[], int size, int start = 0) {
    if (start > size - 10) return 0;

    int currentSum = 0;
    for (int i = start; i < start + 10; i++) {
        currentSum += arr[i];
    }

    int nextPos = findMinSumPos(arr, size, start + 1);
    int nextSum = 0;
    for (int i = nextPos; i < nextPos + 10; i++) {
        nextSum += arr[i];
    }

    return (currentSum < nextSum) ? start : nextPos;
}

int main() {
    setlocale(LC_ALL, "ru");
    // Пример 1
    cout << "2^3 = " << power(2, 3) << endl;

    // Пример 2
    cout << "5 звезд: ";
    stars(5);
    cout << endl;

    // Пример 3
    cout << "Сумма от 1 до 5: " << sumRange(1, 5) << endl;

    // Пример 4
    int arr[100];
    srand(time(0));
    for (int i = 0; i < 100; i++) {
        arr[i] = rand() % 100;
    }
    cout << "Позиция с минимальной суммой 10 чисел: " << findMinSumPos(arr, 100) << endl;

    return 0;
}