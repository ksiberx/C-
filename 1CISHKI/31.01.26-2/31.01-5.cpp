#include <iostream>
#include <fstream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // 1. Создаем файл
    ofstream out;
    out.open("simple.txt");
    out << "aaa" << endl;
    out << "bbb" << endl;
    out << "ccc" << endl;
    out << "ddd" << endl;
    out << "eee";
    out.close();

    // 2. Считаем символы '\n'
    ifstream in("simple.txt");
    char ch;
    int lineCount = 0;

    while (in.get(ch)) {
        if (ch == '\n') {
            lineCount++;
        }
    }

    in.close();

    // +1 потому что последняя строка без \n
    lineCount++;

    cout << "Строк в файле: " << lineCount << endl;

    return 0;
}
