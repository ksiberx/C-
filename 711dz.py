#include <iostream>
using namespace std;

// 1. Среднее арифметическое
void task1() {
    int arr[10];
    double sum = 0;
    for(int i = 0; i < 10; i++) {
        arr[i] = rand() % 100 + 1;
        sum += arr[i];
        cout << arr[i] << " ";
    }
    cout << "\nСреднее: " << sum/10 << "\n\n";
}

// 2. Частота символов
void task2() {
    char arr[15];
    char symbols[3] = {'A','B','C'};
    int count[3] = {0};
    
    for(int i = 0; i < 15; i++) {
        arr[i] = symbols[rand() % 3];
        cout << arr[i] << " ";
        
        for(int j = 0; j < 3; j++) {
            if(arr[i] == symbols[j]) count[j]++;
        }
    }
    
    cout << "\nA: " << count[0] << " B: " << count[1] << " C: " << count[2] << "\n\n";
}

// 3. Сортировка по убыванию
void task3() {
    float arr[20];
    
    for(int i = 0; i < 20; i++) {
        arr[i] = (rand() % 2001 - 1000) / 1000.0f;
        cout << arr[i] << " ";
    }
    
    // Сортировка пузырьком
    for(int i = 0; i < 19; i++) {
        for(int j = 0; j < 19-i; j++) {
            if(arr[j] < arr[j+1]) {
                float temp = arr[j];
                arr[j] = arr[j+1];
                arr[j+1] = temp;
            }
        }
    }
    
    cout << "\nПосле сортировки: ";
    for(int i = 0; i < 20; i++) {
        cout << arr[i] << " ";
    }
    cout << "\n\n";
}

// 4. Удалить нечетные
void task4() {
    int arr[20];
    int even[20], count = 0;
    
    cout << "Исходный: ";
    for(int i = 0; i < 20; i++) {
        arr[i] = rand() % 50 + 1;
        cout << arr[i] << " ";
        
        if(arr[i] % 2 == 0) {
            even[count] = arr[i];
            count++;
        }
    }
    
    cout << "\nБез нечетных: ";
    for(int i = 0; i < count; i++) {
        cout << even[i] << " ";
    }
    cout << "\n\n";
}

// 5. Палиндром
void task5() {
    char arr[10];
    bool isPalindrome = true;
    
    for(int i = 0; i < 10; i++) {
        arr[i] = 'A' + rand() % 26;
        cout << arr[i] << " ";
    }
    
    for(int i = 0; i < 5; i++) {
        if(arr[i] != arr[9-i]) {
            isPalindrome = false;
            break;
        }
    }
    
    cout << (isPalindrome ? "\nПалиндром" : "\nНе палиндром") << "\n\n";
}

// Матрицы
// 1. Главная диагональ
void matrix1() {
    int mat[5][5];
    
    cout << "Матрица 5x5:\n";
    for(int i = 0; i < 5; i++) {
        for(int j = 0; j < 5; j++) {
            mat[i][j] = rand() % 10 + 1;
            cout << mat[i][j] << " ";
        }
        cout << endl;
    }
    
    cout << "Главная диагональ: ";
    for(int i = 0; i < 5; i++) {
        cout << mat[i][i] << " ";
    }
    cout << "\n\n";
}

// 2. Сумма первой строки
void matrix2() {
    int mat[4][4], sum = 0;
    
    for(int i = 0; i < 4; i++) {
        for(int j = 0; j < 4; j++) {
            mat[i][j] = rand() % 101;
            cout << mat[i][j] << " ";
            if(i == 0) sum += mat[i][j];
        }
        cout << endl;
    }
    cout << "Сумма первой строки: " << sum << "\n\n";
}

// 3. Матрица i+j
void matrix3() {
    int n;
    cout << "Введите N: ";
    cin >> n;
    
    cout << "Матрица " << n << "x" << n << ":\n";
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            cout << i+j << " ";
        }
        cout << endl;
    }
    cout << endl;
}

// 4. Симметричная матрица
void matrix4() {
    int mat[6][6];
    
    // Заполняем верхний треугольник
    for(int i = 0; i < 6; i++) {
        for(int j = i; j < 6; j++) {
            mat[i][j] = rand() % 20 + 1;
            mat[j][i] = mat[i][j]; // Симметрия
        }
    }
    
    cout << "Симметричная матрица:\n";
    for(int i = 0; i < 6; i++) {
        for(int j = 0; j < 6; j++) {
            cout << mat[i][j] << " ";
        }
        cout << endl;
    }
    cout << endl;
}
    return 0;
}
