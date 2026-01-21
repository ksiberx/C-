#include <iostream>
#include <typeinfo>
using namespace std;

// Задача 5.1
template<typename T, typename U>
auto add(T a, U b) -> decltype(a + b) {
    return a + b;
}

// Задача 5.2

double (*func1(int x))(int) {
    cout << "func1 вызвана с x = " << x << endl;
    return [](int y) -> double {
        return y * 1.5;
        };
}

auto func2(int x) -> double (*)(int) {
    cout << "func2 вызвана с x = " << x << endl;
    return [](int y) -> double {
        return y * 2.0;
        };
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "Задача 5.1: Сложение разных типов" << endl;

    int x = 5;
    double y = 3.14;

    auto result1 = add(x, y);
    cout << "add(" << x << ", " << y << ") = " << result1 << endl;
    cout << "Тип результата: " << typeid(result1).name() << endl;

    string s1 = "Hello";
    string s2 = " World";
    auto result2 = add(s1, s2);
    cout << "add(\"" << s1 << "\", \"" << s2 << "\") = " << result2 << endl;

    cout << "\nПочему нужен хвостовой возвращаемый тип?" << endl;
    cout << "1. Параметры a и b объявлены после их использования в decltype" << endl;
    cout << "2. decltype(a + b) должен знать типы a и b" << endl;
    cout << "3. В обычном синтаксисе тип возврата указывается ДО параметров" << endl;
    cout << "4. Хвостовой синтаксис позволяет использовать параметры в decltype" << endl;

    cout << "\nЗадача 5.2: Указатель на функцию" << endl;

    double (*ordinary_func)(int);
    ordinary_func = func1(10);
    cout << "Обычный способ: func1(10)(5) = " << ordinary_func(5) << endl;

    auto tail_func = func2(20);
    cout << "Хвостовой тип: func2(20)(5) = " << tail_func(5) << endl;

    cout << "\nСравнение читаемости:" << endl;
    cout << "1. Обычный способ: double (*func1(int x))(int)" << endl;
    cout << "   - Читается изнутри наружу: функция принимает int, возвращает указатель на функцию..." << endl;
    cout << "   - Сложно для понимания" << endl;
    cout << endl;
    cout << "2. Хвостовой тип: auto func2(int x) -> double (*)(int)" << endl;
    cout << "   - Читается слева направо: функция принимает int, возвращает указатель на функцию..." << endl;
    cout << "   - Более понятный и читаемый синтаксис" << endl;

    return 0;
}
