#include <iostream>
using namespace std;

// Задание 1
void initMatrix(int m[3][3]) {
    for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            m[i][j] = i + j;
}

void printMatrix(int m[3][3]) {
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++)
            cout << m[i][j] << " ";
        cout << endl;
    }
}

void findMinMax(int m[3][3]) {
    int min = m[0][0];
    int max = m[0][0];

    for (int i = 0; i < 3; i++) {
        if (m[i][i] < min) min = m[i][i];
        if (m[i][i] > max) max = m[i][i];
    }

    cout << "Минимум: " << min << endl;
    cout << "Максимум: " << max << endl;
}

void sortRows(int m[3][3]) {
    for (int i = 0; i < 3; i++) {
        // Сортируем строку i
        for (int j = 0; j < 3; j++) {
            for (int k = j + 1; k < 3; k++) {
                if (m[i][j] > m[i][k]) {
                    int temp = m[i][j];
                    m[i][j] = m[i][k];
                    m[i][k] = temp;
                }
            }
        }
    }
}

// Задание 2
int gcd(int a, int b) {
    if (b == 0) return a;
    return gcd(b, a % b);
}

// Задание 3
void playGame(int secret, int tries = 1) {
    int guess;
    cout << "Введите число: ";
    cin >> guess;

    if (guess == secret) {
        cout << "Победа! Попыток: " << tries << endl;
        return;
    }

    int bulls = 0;
    int cows = 0;

    // Проверяем каждую цифру
    for (int i = 0; i < 4; i++) {
        int secret_digit = (secret / (int)pow(10, i)) % 10;
        int guess_digit = (guess / (int)pow(10, i)) % 10;

        if (secret_digit == guess_digit) {
            bulls++;
        }
        else {
            // Проверяем есть ли цифра в числе
            int temp = secret;
            while (temp > 0) {
                if (temp % 10 == guess_digit) {
                    cows++;
                    break;
                }
                temp /= 10;
            }
        }
    }

    cout << "Быки: " << bulls << " Коровы: " << cows << endl;
    playGame(secret, tries + 1);
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "Задание 1" << endl;
    int mat[3][3];
    initMatrix(mat);
    printMatrix(mat);
    findMinMax(mat);
    sortRows(mat);
    cout << "После сортировки:" << endl;
    printMatrix(mat);

    cout << "\nЗадание 2" << endl;
    cout << "НОД(56, 98) = " << gcd(56, 98) << endl;

    cout << "\nЗадание 3" << endl;
    cout << "Угадай число 5678" << endl;
    playGame(5678);

    return 0;
}