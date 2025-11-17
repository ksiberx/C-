#include <iostream>
using namespace std;

// Задание 1
void printRect(int N, int K) {
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < K; j++) cout << "*";
        cout << endl;
    }
}


// Задание 2
int factorial(int n) {
    int f = 1;
    for (int i = 1; i <= n; i++) f *= i;
    return f;
}


// Задание 3
bool isPrime(int n) {
    if (n < 2) return false;
    for (int i = 2; i * i <= n; i++)
        if (n % i == 0) return false;
    return true;
}


// Задание 4
int cube(int x) {
    return x * x * x;
}


// Задание 5
int max2(int a, int b) {
    return (a > b) ? a : b;
}


// Задание 6
bool isPositive(int x) {
    return x > 0;
}

int main() {
    setlocale(LC_ALL, "ru");

    // Примеры использования (можно убрать)
    printRect(3, 5);
    cout << factorial(5) << endl;
    cout << (isPrime(7) ? "простое" : "не простое") << endl;
    cout << cube(3) << endl;
    cout << max2(8, 4) << endl;
    cout << (isPositive(5) ? "true" : "false") << endl;

    return 0;
}