#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int choice;

    // Смена цвета консоли
    cout << "=== ЗАДАНИЕ 1: СМЕНА ЦВЕТА КОНСОЛИ ===" << endl;
    cout << "Доступные цвета:" << endl;
    cout << "1 - Красный текст" << endl;
    cout << "2 - Зеленый текст" << endl;
    cout << "3 - Синий текст" << endl;
    cout << "4 - Желтый текст" << endl;
    cout << "5 - Сброс цвета" << endl;
    cout << "Ваш выбор: ";
    cin >> choice;

    switch (choice) {
    case 1:
        cout << "=== ТЕКСТ КРАСНЫМ ===" << endl;
        cout << "Это красный текст!" << endl;
        break;
    case 2:
        cout << "=== ТЕКСТ ЗЕЛЕНЫМ ===" << endl;
        cout << "Это зеленый текст!" << endl;
        break;
    case 3:
        cout << "=== ТЕКСТ СИНИМ ===" << endl;
        cout << "Это синий текст!" << endl;
        break;
    case 4:
        cout << "=== ТЕКСТ ЖЕЛТЫМ ===" << endl;
        cout << "Это желтый текст!" << endl;
        break;
    case 5:
        cout << "=== ЦВЕТ СБРОШЕН ===" << endl;
        cout << "Это обычный текст!" << endl;
        break;
    default:
        cout << "Неверный выбор!" << endl;
    }

    cout << endl;

    // Задание 2
    cout << "=== ВОЗВЕДЕНИЕ В СТЕПЕНЬ ===" << endl;
    double number;
    int power;

    cout << "Введите число: ";
    cin >> number;
    cout << "Введите степень (0-7): ";
    cin >> power;

    if (power < 0 || power > 7) {
        cout << "Степень должна быть от 0 до 7!" << endl;
    }
    else {
        double result = 1;
        for (int i = 0; i < power; i++) {
            result *= number;
        }
        cout << number << " ^ " << power << " = " << result << endl;
    }

    return 0;
}