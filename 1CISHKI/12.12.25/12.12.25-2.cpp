#include <iostream>
using namespace std;

void removeAllChars(char str[], char ch) {
    int i = 0, j = 0;
    while (str[i] != '\0') {
        if (str[i] != ch) {
            str[j] = str[i];
            j++;
        }
        i++;
    }
    str[j] = '\0';
}

int main() {
    setlocale(LC_ALL, "ru");
    char text[100];
    char ch;

    cout << "Введите строку: ";
    cin.getline(text, 100);

    cout << "Введите символ для удаления: ";
    cin >> ch;

    cout << "Исходная строка: " << text << endl;
    removeAllChars(text, ch);
    cout << "После удаления: " << text << endl;

    return 0;
}
