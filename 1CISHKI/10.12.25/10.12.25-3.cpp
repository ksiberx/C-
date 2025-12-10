#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    char str[256];
    int words = 0;

    cout << "Введите предложение: ";
    cin.getline(str, 256);

    for (int i = 0; str[i] != '\0'; i++) {
        // Если текущий символ не пробел, а следующий — пробел или конец строки
        if (str[i] != ' ' && (str[i + 1] == ' ' || str[i + 1] == '\0')) {
            words++;
        }
    }

    cout << "Количество слов: " << words << endl;
    return 0;
}
