#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // ЗАДАНИЕ 1
    int a;
    cout << "Введите число a: ";
    cin >> a;

    int sum = 0;
    for (int i = a; i <= 500; i++) {
        sum += i;
    }
    cout << "Сумма чисел от " << a << " до 500: " << sum << endl << endl;

    // ЗАДАНИЕ 2
    int x, y;
    cout << "Введите x и y: ";
    cin >> x >> y;

    long long result = 1;
    for (int i = 0; i < y; i++) {
        result *= x;
    }
    cout << x << " в степени " << y << " = " << result << endl << endl;

    // ЗАДАНИЕ 3
    int sum2 = 0;
    for (int i = 1; i <= 1000; i++) {
        sum2 += i;
    }
    double average = sum2 / 1000.0;
    cout << "Среднее арифметическое от 1 до 1000: " << average << endl << endl;

    // ЗАДАНИЕ 4
    int b;
    cout << "Введите b (1 < b < 20): ";
    cin >> b;

    long long product = 1;
    for (int i = b; i <= 20; i++) {
        product *= i;
    }
    cout << "Произведение чисел от " << b << " до 20: " << product << endl << endl;

    // ЗАДАНИЕ 5
    int k;
    cout << "Введите k для таблицы умножения: ";
    cin >> k;

    for (int i = 2; i <= 9; i++) {
        cout << k << " × " << i << " = " << k * i << endl;
    }

    return 0;
}