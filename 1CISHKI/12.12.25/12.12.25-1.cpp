#include <iostream>
using namespace std;

void removeCharAtIndex(char str[], int index) {
    int i = 0;
    while (str[i] != '\0') {
        if (i == index) {
            int j = i;
            while (str[j] != '\0') {
                str[j] = str[j + 1];
                j++;
            }
            break;
        }
        i++;
    }
}

int main() {
    setlocale(LC_ALL, "ru");
    char text[100];
    int index;

    cout << "Введите строку: ";
    cin.getline(text, 100);

    cout << "Введите номер символа для удаления (начиная с 0): ";
    cin >> index;

    cout << "Исходная строка: " << text << endl;
    removeCharAtIndex(text, index);
    cout << "После удаления: " << text << endl;

    return 0;
}
