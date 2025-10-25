#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int a[3][4] = {
        {3, 5, 6, 7},
        {12, 1, 1, 1},
        {0, 7, 12, 1}
    };

    int sum = 0, min = a[0][0], max = a[0][0];

    // Задание 1
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 4; j++) {
            sum += a[i][j];
            if (a[i][j] < min) min = a[i][j];
            if (a[i][j] > max) max = a[i][j];
        }
    }

    cout << "Сумма всех элементов: " << sum << endl;
    cout << "Среднее арифметическое: " << (double)sum / 12 << endl;
    cout << "Минимальный элемент: " << min << endl;
    cout << "Максимальный элемент: " << max << endl;

    // Задание 2
    cout << "\nСуммы по строкам и столбцам:\n";

    int colSum[4] = { 0 };
    for (int i = 0; i < 3; i++) {
        int rowSum = 0;
        for (int j = 0; j < 4; j++) {
            cout << a[i][j] << " ";
            rowSum += a[i][j];
            colSum[j] += a[i][j];
        }
        cout << "| " << rowSum << endl;
    }

    cout << "----------------\n";
    int total = 0;
    for (int j = 0; j < 4; j++) {
        cout << colSum[j] << " ";
        total += colSum[j];
    }
    cout << "| " << total << endl;

    return 0;
}

// 3 задание 
// -