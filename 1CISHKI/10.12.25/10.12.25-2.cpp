#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    char str[256];
    int letters = 0, digits = 0, others = 0;

    cout << "Введите строку: ";
    cin.getline(str, 256);

    for (int i = 0; str[i] != '\0'; i++) {
        if ((str[i] >= 'a' && str[i] <= 'z') || (str[i] >= 'A' && str[i] <= 'Z'))
            letters++;
        else if (str[i] >= '0' && str[i] <= '9')
            digits++;
        else
            others++;
    }

    cout << "Буквы: " << letters << endl;
    cout << "Цифры: " << digits << endl;
    cout << "Остальные: " << others << endl;

    return 0;
}
