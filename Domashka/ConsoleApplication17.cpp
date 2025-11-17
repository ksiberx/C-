#include <iostream>
using namespace std;

// ЗАДАНИЕ 1
double power(double a, int n) {
    double r = 1;
    for (int i = 0; i < n; i++) r *= a;
    return r;
}

//  ЗАДАНИЕ 2 
int sumRange(int a, int b) {
    int s = 0;
    for (int i = a; i <= b; i++) s += i;
    return s;
}

//  ЗАДАНИЕ 3 
bool isPerfect(int x) {
    int s = 0;
    for (int i = 1; i <= x / 2; i++)
        if (x % i == 0) s += i;
    return s == x;
}

//  ЗАДАНИЕ 4 
void printCard(char suit, int value) {
    cout << "Карта: ";
    switch (value) {
    case 6: cout << "6 "; break;
    case 7: cout << "7 "; break;
    case 8: cout << "8 "; break;
    case 9: cout << "9 "; break;
    case 10: cout << "10 "; break;
    case 11: cout << "Валет "; break;
    case 12: cout << "Дама "; break;
    case 13: cout << "Король "; break;
    case 14: cout << "Туз "; break;
    }
    switch (suit) {
    case 'C': cout << "♣️"; break;
    case 'D': cout << "♦️"; break;
    case 'H': cout << "♥️"; break;
    case 'S': cout << "♠️"; break;
    }
}

//  ЗАДАНИЕ 5 
bool isLuckyHex(int x) {
    int a = x / 16;
    int b = x % 16;
    return a == b;
}

int main() {
    setlocale(LC_ALL, "ru");

    //  Проверка заданий 

    cout << "Задание 1: " << power(2, 5) << endl;

    cout << "Задание 2: " << sumRange(1, 5) << endl;

    cout << "Задание 3: ";
    for (int i = 1; i <= 10000; i++)
        if (isPerfect(i)) cout << i << " ";
    cout << endl;

    cout << "Задание 4: ";
    printCard('H', 12);
    cout << endl;

    cout << "Задание 5: " << (isLuckyHex(0x33) ? "счастливое" : "несчастливое") << endl;

    return 0;
}