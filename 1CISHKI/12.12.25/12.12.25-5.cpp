#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    char text[100];
    char target;
    int count = 0;

    cout << "Введите строку: ";
    cin.getline(text, 100);

    cout << "Введите искомый символ: ";
    cin >> target;

    for (int i = 0; text[i] != '\0'; i++) {
        if (text[i] == target) {
            count++;
        }
    }

    cout << "Символ '" << target << "' встречается " << count << " раз(а)" << endl;

    return 0;
}
