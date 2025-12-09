#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    char text[100];

    cout << "Введите строку: ";
    cin.getline(text, 100);

    for (int i = 0; text[i] != '\0'; i++) {
        if (text[i] == '.') {
            text[i] = '!';
        }
    }

    cout << "Результат: " << text << endl;

    return 0;
}
