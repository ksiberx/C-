#include <iostream>
#include <cmath>
using namespace std;

bool ascending(int a, int b) {
    return a < b;
}

bool descending(int a, int b) {
    return a > b;
}

void bubbleSort(int arr[], int size, bool (*comp)(int, int)) {
    for (int i = 0; i < size - 1; i++) {
        for (int j = 0; j < size - i - 1; j++) {
            if (comp(arr[j + 1], arr[j])) {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

double integrate(double a, double b, int n, double (*func)(double)) {
    double h = (b - a) / n;
    double sum = 0.0;

    for (int i = 0; i < n; i++) {
        double x = a + (i + 0.5) * h;
        sum += func(x);
    }

    return sum * h;
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "2.1" << endl;

    int arr[] = { 5, 2, 9, 1, 7 };
    int size = 5;

    bubbleSort(arr, size, ascending);
    cout << "По возрастанию: ";
    for (int i = 0; i < size; i++) cout << arr[i] << " ";
    cout << endl;

    int arr2[] = { 5, 2, 9, 1, 7 };
    bubbleSort(arr2, size, descending);
    cout << "По убыванию:    ";
    for (int i = 0; i < size; i++) cout << arr2[i] << " ";
    cout << endl;

    cout << endl << "2.2" << endl;

    double sin_integral = integrate(0.0, 1.0, 1000, sin);
    cout << "sin(x) от 0 до 1: " << sin_integral << endl;

    double square_integral = integrate(0.0, 1.0, 1000, [](double x) { return x * x; });
    cout << "x*x от 0 до 1:    " << square_integral << endl;

    return 0;
}
