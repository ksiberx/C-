#include <iostream>
using namespace std;
int main() {
    setlocale(LC_ALL, "ru");
    /*    char c;
        cout << "Введите символ: ";
        cin >> c;

        if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) {
            cout << "Буква" << endl;
        }
        else if (c >= '0' && c <= '9') {
            cout << "Цифра" << endl;
        }
        else if (c == '.' || c == ',' || c == '!' || c == '?') {
            cout << "Знак препинания" << endl;
        }
        else {
            cout << "Другое" << endl;
        }
    }
    */
    int time;
    int op1, op2;
    cout << "\nВведите время разговора: ";
    cin >> time;
    cout << "Оператор от (1-МТС, 2-Билайн, 3-Мегафон): ";
    cin >> op1;
    cout << "Оператор до (1-МТС, 2-Билайн, 3-Мегафон): ";
    cin >> op2;

    int price;
    if (op1 == op2) {
        price = time * 2; // дешевле
    }
    else {
        price = time * 5; // дороже
    }
    cout << "Стоимость: " << price << " руб." << endl;
}