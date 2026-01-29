#include <iostream>
#include <fstream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Создаем простой файл
    ofstream create("simple.txt");
    create << "0 1 0 1\n";
    create << "101\n";
    create << "010\n";
    create.close();

    // Читаем и меняем
    ifstream in("simple.txt");
    ofstream out("result.txt");

    char c;
    while (in.get(c)) {
        if (c == '0') {
            out << '1';
        }
        else if (c == '1') {
            out << '0';
        }
        else {
            out << c;
        }
    }

    in.close();
    out.close();

    cout << "Файл simple.txt:\n";
    ifstream show1("simple.txt");
    while (show1.get(c)) {
        cout << c;
    }
    show1.close();

    cout << "\n\nФайл result.txt:\n";
    ifstream show2("result.txt");
    while (show2.get(c)) {
        cout << c;
    }
    show2.close();

    return 0;
}
