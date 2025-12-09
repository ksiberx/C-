#include <iostream>
using namespace std;

void insertCharAtPosition(char str[], int pos, char ch) {
    int len = 0;
    while (str[len] != '\0') len++;

    for (int i = len; i >= pos; i--) {
        str[i + 1] = str[i];
    }
    str[pos] = ch;
}

int main() {
    setlocale(LC_ALL, "ru");
    char text[100];
    int pos;
    char ch;

    cout << "Введите строку: ";
    cin.getline(text, 100);

    cout << "Введите позицию для вставки (начиная с 0): ";
    cin >> pos;

    cout << "Введите символ для вставки: ";
    cin >> ch;

    cout << "Исходная строка: " << text << endl;
    insertCharAtPosition(text, pos, ch);
    cout << "После вставки: " << text << endl;

    return 0;
}
