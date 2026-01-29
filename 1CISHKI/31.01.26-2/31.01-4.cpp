#include <iostream>
#include <fstream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Создаем тестовый файл
    ofstream create("text4.txt");
    create << "Hello World!\n";
    create << "12345\n";
    create << "Test file\n";
    create.close();

    // Считаем символы
    ifstream file("text4.txt");
    char c;
    int count = 0;

    while (file.get(c)) {
        count++;
    }

    file.close();
    cout << "В файле text4.txt: " << count << " символов" << endl;
    return 0;
}
