#include <iostream>
using namespace std;

// Задание 1
void task1() {
    int a = 10, b = 20;
    int* pa = &a, * pb = &b;

    if (*pa > *pb) {
        cout << "Наибольшее: " << *pa << endl;
    }
    else {
        cout << "Наибольшее: " << *pb << endl;
    }
}

// Задание 2
void task2() {
    int num;
    int* p = &num;

    cout << "Введите число: ";
    cin >> *p;

    if (*p > 0) cout << "Положительное" << endl;
    else if (*p < 0) cout << "Отрицательное" << endl;
    else cout << "Ноль" << endl;
}

// Задание 3
void task3() {
    int a = 5, b = 10;
    int* pa = &a, * pb = &b;

    cout << "До: a = " << *pa << ", b = " << *pb << endl;

    int temp = *pa;
    *pa = *pb;
    *pb = temp;

    cout << "После: a = " << *pa << ", b = " << *pb << endl;
}

// Задание 4
void task4() {
    double x, y, result;
    double* px = &x, * py = &y, * pres = &result;
    char op;

    cout << "Введите выражение (например: 5 + 3): ";
    cin >> *px >> op >> *py;

    switch (op) {
    case '+': *pres = *px + *py; break;
    case '-': *pres = *px - *py; break;
    case '*': *pres = *px * *py; break;
    case '/': *pres = *py != 0 ? *px / *py : 0; break;
    default: cout << "Неверная операция!" << endl; return;
    }

    cout << *px << " " << op << " " << *py << " = " << *pres << endl;
}

// Задание 5
void task5() {
    int arr[] = { 1, 2, 3, 4, 5 };
    int* ptr = arr;
    int sum = 0;

    for (int i = 0; i < 5; i++) {
        sum += *(ptr + i); // арифметика указателей + разыменование
    }

    cout << "Сумма элементов: " << sum << endl;
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "--- Задание 1 ---" << endl;
    task1();

    cout << "\n--- Задание 2 ---" << endl;
    task2();

    cout << "\n--- Задание 3 ---" << endl;
    task3();

    cout << "\n--- Задание 4 ---" << endl;
    task4();

    cout << "\n--- Задание 5 ---" << endl;
    task5();

    return 0;
}
