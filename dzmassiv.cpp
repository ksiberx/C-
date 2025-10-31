#include <iostream>

using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Задание 1
    cout << "Задание 1:" << endl;
    int n1;
    cout << "Введите число: ";
    cin >> n1;
    
    int arr1[3][3];
    int current = n1;
    
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            arr1[i][j] = current;
            current *= 2;
        }
    }
    
    cout << "Массив (умножение на 2):" << endl;
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            cout << arr1[i][j] << " ";
        }
        cout << endl;
    }
    
    // Задание 2
    cout << endl << "Задание 2:" << endl;
    int n2;
    cout << "Введите число: ";
    cin >> n2;
    
    int arr2[3][3];
    current = n2;
    
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            arr2[i][j] = current;
            current += 1;
        }
    }
    
    cout << "Массив (увеличение на 1):" << endl;
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            cout << arr2[i][j] << " ";
        }
        cout << endl;
    }
    
    return 0;
}
