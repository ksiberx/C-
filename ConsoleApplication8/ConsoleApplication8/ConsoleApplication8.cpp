#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Задача 2
    int num;
    cout << "Введите число: ";
    cin >> num;

    int sum = 0;
    while (num != 0) {
        sum += num % 10;
        num /= 10;
    }
    cout << "Сумма цифр: " << sum << endl << endl;

    // Задача 3:
    int days;
    cout << "Введите количество дней: ";
    cin >> days;

    int distance = 0;
    for (int i = 0; i < days; i++) {
        distance += 15 + i * 2;
    }
    cout << "Путь улитки: " << distance << " см" << endl << endl;

    // Задача 4: 
    int heads = 0;
    cout << "Введите 9 результатов (1-орел, 0-решка):" << endl;

    for (int i = 0; i < 9; i++) {
        int coin;
        cin >> coin;
        heads += coin;
    }

    switch (heads % 2) {
    case 0: cout << "Решение: ПОЛОЖИТЕЛЬНОЕ" << endl; break;
    case 1: cout << "Решение: ОТРИЦАТЕЛЬНОЕ" << endl; break;
    }

    return 0;
}