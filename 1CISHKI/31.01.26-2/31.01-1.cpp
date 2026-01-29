#include <iostream>
#include <fstream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Создаем тестовый файл
    ofstream create("text1.txt");
    create << "apple banana cherry date\n";
    create << "dog cat elephant fish\n";
    create << "ant axe art animal\n";
    create.close();

    // Работа с файлом
    char letter;
    cout << "Введите букву: ";
    cin >> letter;

    ifstream file("text1.txt");
    string word;
    int count = 0;

    while (file >> word) {
        if (word[0] == letter) {
            count++;
            cout << word << " ";
        }
    }

    cout << "\nСлов на букву '" << letter << "': " << count << endl;
    file.close();
    return 0;
}
