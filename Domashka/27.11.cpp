/*
#include <iostream>
using namespace std;

// ЗАДАНИЕ 1 - Телефонный справочник
void task1() {
    string mob[3] = { "89162345678", "89051112233", "89265554433" };
    string home[3] = { "123456", "654321", "987654" };

    int choice;
    do {
        cout << "1. Сортировать по мобильным\n2. Сортировать по домашним\n3. Вывод данных\n4. Выход\nВыбор: ";
        cin >> choice;

        if (choice == 1) {
            // Сортировка пузырьком по мобильным номерам
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2 - i; j++)
                    if (mob[j] > mob[j + 1]) {
                        swap(mob[j], mob[j + 1]);
                        swap(home[j], home[j + 1]);
                    }
            cout << "Сортировка по мобильным завершена!\n";
        }
        else if (choice == 2) {
            // Сортировка пузырьком по домашним номерам
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2 - i; j++)
                    if (home[j] > home[j + 1]) {
                        swap(home[j], home[j + 1]);
                        swap(mob[j], mob[j + 1]);
                    }
            cout << "Сортировка по домашним завершена!\n";
        }
        else if (choice == 3) {
            // Вывод всех данных
            cout << "=== ТЕЛЕФОННЫЙ СПРАВОЧНИК ===" << endl;
            for (int i = 0; i < 3; i++)
                cout << "Моб: " << mob[i] << " Дом: " << home[i] << endl;
        }
    } while (choice != 4);
}

// ЗАДАНИЕ 2 - Усовершенствованная сортировка пузырьком
void task2() {
    int arr[5] = { 5, 2, 8, 1, 9 };
    int n = 5;

    // Усовершенствованная сортировка пузырьком
    for (int i = 0; i < n - 1; i++) {
        bool swapped = false; // Флаг перестановок
        for (int j = 0; j < n - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                swap(arr[j], arr[j + 1]);
                swapped = true; // Была перестановка
            }
        }
        if (!swapped) break; // Если перестановок не было - выход
    }

    cout << "Отсортированный массив: ";
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    cout << endl;
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "ЗАДАНИЕ 1" << endl;
    task1();

    cout << "\nЗАДАНИЕ 2" << endl;
    task2();

    return 0;
}


*/

#include <iostream>
using namespace std;

// ЗАДАНИЕ 3 - Сортировка блинов
void task3() {
    int pancakes[5] = { 3, 1, 4, 2, 5 }; // Стопка блинов
    int n = 5;

    cout << "Исходная стопка: ";
    for (int i = 0; i < n; i++) cout << pancakes[i] << " ";
    cout << endl;

    // Сортировка блинов
    for (int i = 0; i < n - 1; i++) {
        // Находим максимальный блин в несортированной части
        int max_idx = i;
        for (int j = i + 1; j < n; j++) {
            if (pancakes[j] > pancakes[max_idx]) {
                max_idx = j;
            }
        }

        // Если максимальный не на своем месте
        if (max_idx != i) {
            // Переворачиваем часть стопки до максимального
            if (max_idx != n - 1) {
                cout << "Переворот от 0 до " << max_idx << ": ";
                for (int left = 0, right = max_idx; left < right; left++, right--) {
                    swap(pancakes[left], pancakes[right]);
                }
                for (int k = 0; k < n; k++) cout << pancakes[k] << " ";
                cout << endl;
            }

            // Переворачиваем несортированную часть
            cout << "Переворот от 0 до " << n - 1 - i << ": ";
            for (int left = 0, right = n - 1 - i; left < right; left++, right--) {
                swap(pancakes[left], pancakes[right]);
            }
            for (int k = 0; k < n; k++) cout << pancakes[k] << " ";
            cout << endl;
        }
    }

    cout << "Отсортированная стопка: ";
    for (int i = 0; i < n; i++) cout << pancakes[i] << " ";
    cout << endl;
}

// ЗАДАНИЕ 4 - Сравнение сортировок
void task4() {
    srand(time(0));
    const int SIZE = 1000;
    const int TESTS = 10;

    int bubble_swaps_total = 0;
    int selection_swaps_total = 0;

    for (int test = 0; test < TESTS; test++) {
        int arr1[SIZE], arr2[SIZE];

        // Заполняем массивы случайными числами
        for (int i = 0; i < SIZE; i++) {
            arr1[i] = arr2[i] = rand() % 1000;
        }

        // Пузырьковая сортировка с подсчетом перестановок
        int bubble_swaps = 0;
        for (int i = 0; i < SIZE - 1; i++) {
            bool swapped = false;
            for (int j = 0; j < SIZE - i - 1; j++) {
                if (arr1[j] > arr1[j + 1]) {
                    swap(arr1[j], arr1[j + 1]);
                    bubble_swaps++;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
        bubble_swaps_total += bubble_swaps;

        // Сортировка выбором с подсчетом перестановок
        int selection_swaps = 0;
        for (int i = 0; i < SIZE - 1; i++) {
            int min_idx = i;
            for (int j = i + 1; j < SIZE; j++) {
                if (arr2[j] < arr2[min_idx]) {
                    min_idx = j;
                }
            }
            if (min_idx != i) {
                swap(arr2[i], arr2[min_idx]);
                selection_swaps++;
            }
        }
        selection_swaps_total += selection_swaps;

        cout << "Тест " << test + 1 << ": Пузырек = " << bubble_swaps
            << ", Выбор = " << selection_swaps << endl;
    }

    cout << "Среднее число перестановок:" << endl;
    cout << "Пузырьковая: " << bubble_swaps_total / TESTS << endl;
    cout << "Выбором: " << selection_swaps_total / TESTS << endl;
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "ЗАДАНИЕ 3" << endl;
    task3();

    cout << "\nЗАДАНИЕ 4" << endl;
    task4();

    return 0;
}