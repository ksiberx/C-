# 2 задание
#include <iostream>
using namespace std;

int main() {
  setlocale(LC_ALL, "ru");
    int h = 0, c = 0, x;
    for (int i = 0; i < 5; i++) {
        x = 1 + rand() % 6;
        h += x;
        x = 1 + rand() % 6;
        c += x;
    }
    cout << "Человек: " << h << "\nКомпьютер: " << c << "\n";
    if (h > c) cout << "Победил человек";
    else if (c > h) cout << "Победил компьютер";
    else cout << "Ничья";
}
