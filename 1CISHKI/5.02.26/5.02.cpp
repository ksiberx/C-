// пятое задание, арифметическое выражения пользователя

#include <iostream>
#include <string>

using namespace std;

string s;
int pos = 0;

// Функции объявляем заранее
double expression();
double term();
double factor();

// Программа
int main() {
    setlocale(LC_ALL, "ru");
    cout << "Введите выражение: ";
    getline(cin, s);

    double result = expression();

    cout << "Результат: " << result << endl;

    return 0;
}

// Разбор + и -
double expression() {
    double value = term();

    while (pos < s.length()) {
        if (s[pos] == '+') {
            pos++;
            value += term();
        }
        else if (s[pos] == '-') {
            pos++;
            value -= term();
        }
        else break;
    }

    return value;
}

// Разбор * и / 
double term() {
    double value = factor();

    while (pos < s.length()) {
        if (s[pos] == '*') {
            pos++;
            value *= factor();
        }
        else if (s[pos] == '/') {
            pos++;
            value /= factor();
        }
        else break;
    }

    return value;
}

double factor() {
    if (s[pos] == '(') {
        pos++;     
        double value = expression();
        pos++;
        return value;
    }

    double number = 0;

    while (pos < s.length() && isdigit(s[pos])) {
        number = number * 10 + (s[pos] - '0');
        pos++;
    }

    return number;
}
