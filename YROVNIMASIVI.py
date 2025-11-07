#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    
    cout << "=== ОДНОРАЗМЕРНЫЕ МАССИВЫ ===" << endl;
    
    // 1. Среднее арифметическое
    cout << "\n1. Среднее арифметическое:" << endl;
    int arr1[10];
    cout << "Массив: ";
    for (int i = 0; i < 10; i++) {
        arr1[i] = rand() % 100 + 1;
        cout << arr1[i] << " ";
    }
    double sum = 0;
    for (int i = 0; i < 10; i++) sum += arr1[i];
    cout << "\nСреднее: " << sum / 10 << endl;
    
    // 2. Частота символов
    cout << "\n2. Частота символов:" << endl;
    char symbols[15];
    int countA = 0, countB = 0, countC = 0;
    cout << "Символы: ";
    for (int i = 0; i < 15; i++) {
        int r = rand() % 3;
        if (r == 0) { symbols[i] = 'A'; countA++; }
        else if (r == 1) { symbols[i] = 'B'; countB++; }
        else { symbols[i] = 'C'; countC++; }
        cout << symbols[i] << " ";
    }
    cout << "\nA: " << countA << ", B: " << countB << ", C: " << countC << endl;
    
    // 3. Сортировка вещественных чисел
    cout << "\n3. Сортировка вещественных чисел:" << endl;
    double arr3[20];
    cout << "Исходный массив: ";
    for (int i = 0; i < 20; i++) {
        arr3[i] = (rand() % 2001 - 1000) / 1000.0;
        cout << arr3[i] << " ";
    }
    // Сортировка пузырьком по убыванию
    for (int i = 0; i < 19; i++) {
        for (int j = 0; j < 19 - i; j++) {
            if (arr3[j] < arr3[j + 1]) {
                double temp = arr3[j];
                arr3[j] = arr3[j + 1];
                arr3[j + 1] = temp;
            }
        }
    }
    cout << "\nПосле сортировки: ";
    for (int i = 0; i < 20; i++) {
        cout << arr3[i] << " ";
    }
    
    // 4. Удаление нечетных чисел
    cout << "\n\n4. Удаление нечетных чисел:" << endl;
    int arr4[20];
    cout << "Исходный массив: ";
    for (int i = 0; i < 20; i++) {
        arr4[i] = rand() % 50 + 1;
        cout << arr4[i] << " ";
    }
    cout << "\nБез нечетных: ";
    for (int i = 0; i < 20; i++) {
        if (arr4[i] % 2 == 0) {
            cout << arr4[i] << " ";
        }
    }
    
    // 5. Проверка на палиндром
    cout << "\n\n5. Проверка на палиндром:" << endl;
    char arr5[10];
    cout << "Последовательность: ";
    for (int i = 0; i < 10; i++) {
        arr5[i] = 'a' + rand() % 26;
        cout << arr5[i] << " ";
    }
    bool isPalindrome = true;
    for (int i = 0; i < 5; i++) {
        if (arr5[i] != arr5[9 - i]) {
            isPalindrome = false;
            break;
        }
    }
    cout << "\nПалиндром: " << (isPalindrome ? "Да" : "Нет") << endl;
    
    cout << "\n=== МНОГОМЕРНЫЕ МАССИВЫ ===" << endl;
    
    // 1. Главная диагональ матрицы
    cout << "\n1. Главная диагональ матрицы:" << endl;
    int matrix1[5][5];
    cout << "Матрица 5x5:" << endl;
    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < 5; j++) {
            matrix1[i][j] = rand() % 10 + 1;
            cout << matrix1[i][j] << "\t";
        }
        cout << endl;
    }
    cout << "Главная диагональ: ";
    for (int i = 0; i < 5; i++) {
        cout << matrix1[i][i] << " ";
    }
    
    // 2. Сумма первой строки
    cout << "\n\n2. Сумма первой строки:" << endl;
    int matrix2[4][4];
    int sum_row = 0;
    cout << "Матрица 4x4:" << endl;
    for (int i = 0; i < 4; i++) {
        for (int j = 0; j < 4; j++) {
            matrix2[i][j] = rand() % 101;
            cout << matrix2[i][j] << "\t";
            if (i == 0) sum_row += matrix2[i][j];
        }
        cout << endl;
    }
    cout << "Сумма первой строки: " << sum_row << endl;
    
    // 3. Матрица суммы индексов
    cout << "\n3. Матрица суммы индексов:" << endl;
    int N = 4; // можно изменить размер
    cout << "Матрица " << N << "x" << N << " (i+j):" << endl;
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < N; j++) {
            cout << i + j << "\t";
        }
        cout << endl;
    }
    
    // 4. Симметричная матрица
    cout << "\n4. Симметричная матрица:" << endl;
    int matrix4[6][6];
    cout << "Симметричная матрица 6x6:" << endl;
    for (int i = 0; i < 6; i++) {
        for (int j = i; j < 6; j++) {
            matrix4[i][j] = rand() % 20 + 1;
            matrix4[j][i] = matrix4[i][j];
        }
    }
    for (int i = 0; i < 6; i++) {
        for (int j = 0; j < 6; j++) {
            cout << matrix4[i][j] << "\t";
        }
        cout << endl;
    }
    
    // 5. Поиск минимального элемента
    cout << "\n5. Поиск минимального элемента:" << endl;
    int matrix5[3][3];
    int min_val = 10, min_i = 0, min_j = 0;
    cout << "Матрица 3x3:" << endl;
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            matrix5[i][j] = rand() % 9 + 1;
            cout << matrix5[i][j] << " ";
            if (matrix5[i][j] < min_val) {
                min_val = matrix5[i][j];
                min_i = i;
                min_j = j;
            }
        }
        cout << endl;
    }
    cout << "Минимум: " << min_val << " в [" << min_i << "][" << min_j << "]" << endl;
    
    return 0;
}
