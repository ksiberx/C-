#include <iostream>
#include <vector>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    cout << "Задача 4.1: Упрощение итераторов" << endl;

    vector<int> vec = { 1, 2, 3, 4, 5 };

    cout << "Без auto:" << endl;
    for (vector<int>::iterator it = vec.begin(); it != vec.end(); ++it) {
        cout << *it << " ";
    }
    cout << endl;

    cout << "С auto:" << endl;
    for (auto it = vec.begin(); it != vec.end(); ++it) {
        cout << *it << " ";
    }
    cout << endl;

    cout << "Диапазонный for:" << endl;
    for (auto element : vec) {
        cout << element << " ";
    }
    cout << endl;

    cout << endl << "Задача 4.2: Тип результата выражения" << endl;

    int a = 5;
    double b = 3.14;

    cout << "a = " << a << ", b = " << b << endl;

    decltype(a * b) c1 = a * b;
    auto c2 = a * b;

    cout << "c1 (decltype): " << c1 << ", тип: " << typeid(c1).name() << endl;
    cout << "c2 (auto): " << c2 << ", тип: " << typeid(c2).name() << endl;

    cout << endl << "Разница:" << endl;
    cout << "1. decltype(a * b) c1 - компилятор анализирует выражение a * b и определяет тип результата (double), но НЕ вычисляет выражение" << endl;
    cout << "2. auto c2 = a * b - компилятор вычисляет выражение a * b, а затем определяет тип результата по значению (double)" << endl;

    return 0;
}
