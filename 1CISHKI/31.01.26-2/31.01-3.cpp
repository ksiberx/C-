#include <iostream>
#include <fstream>
#include <vector>
#include <string>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Массив строк
    vector<string> words = { "Первая строка", "Вторая строка", "Третья строка", "Четвертая строка" };

    // Записываем в файл
    ofstream file("text3.txt");
    for (const string& word : words) {
        file << word << endl;
    }
    file.close();

    cout << "Записано " << words.size() << " строк в файл text3.txt" << endl;
    return 0;
}
