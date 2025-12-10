#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    char str[256];
    cout << "Введите строку: ";
    cin.getline(str, 256);

    for (int i = 0; str[i] != '\0'; i++) {
        if (str[i] == ' ') {
            str[i] = '\t';
        }
    }

    cout << "Результат: " << str << endl;
    return 0;
}
