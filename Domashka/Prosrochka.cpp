#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Задание 1 
    int n = 250;
    int a, b = 0;

    cout << "Угадай число от 1 до 500 (0 - выход)\n";

    while (true) {
        cout << "Твоя попытка: ";
        cin >> a;
        b++;

        if (a == 0) {
            cout << "Выход. Загадано: " << n;
            break;
        }

        if (a == n) {
            cout << "Угадал! Попыток: " << b;
            break;
        }

        if (a < n) cout << "Больше!\n";
        else cout << "Меньше!\n";
    }

    // Задание 2
    cout << "\n\nКонвертер валют:\n";

    double money;
    cout << "Сумма в USD: ";
    cin >> money;
    cout << money << " USD = " << money * 90 << " RUB\n";

    cout << "Сумма в EUR: ";
    cin >> money;
    cout << money << " EUR = " << money * 100 << " RUB";

    return 0;
}