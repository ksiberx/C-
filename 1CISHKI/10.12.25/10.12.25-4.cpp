#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    char str[256];
    bool isPalindrome = true;

    cout << "Введите строку: ";
    cin.getline(str, 256);

    int length = 0;
    while (str[length] != '\0') length++;

    for (int i = 0; i < length / 2; i++) {
        if (str[i] != str[length - 1 - i]) {
            isPalindrome = false;
            break;
        }
    }

    if (isPalindrome)
        cout << "Строка является палиндромом." << endl;
    else
        cout << "Строка не является палиндромом." << endl;

    return 0;
}
