#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");

// 1 Задание

/*    double arr[] = {-5.7, 6.0, 2, 0, -4.7, 6, 8.1, -4, 0};
    int size = 9;

    double last_positive = 0;
    for (int i = size - 1; i >= 0; i--) {
        if (arr[i] > 0) {
            last_positive = arr[i];
            break;
        }
    }

    double first_negative = 0;
    for (int i = 0; i < size; i++) {
        if (arr[i] < 0) {
            first_negative = arr[i];
            break;
        }
    }

    cout << "Последний положительный результат: " << last_positive << endl;
    cout << "Первый отрицательный результат: " << first_negative << endl;

    return 0;
}
*/


    // 2 Задание
 /*
    int kv[40];

    cout << "Введите жильцов для 40 квартир:" << endl;
    for (int i = 0; i < 40; i++) {
        cout << "Квартира " << i + 1 << ": ";
        cin >> kv[i];
    }

    int n;
    cout << "Номер квартиры (1-40): ";
    cin >> n;

    if (n >= 1 && n <= 40) {
        cout << "Жильцов: " << kv[n - 1] << endl;


        int sosed;
        if (n % 2 == 0) {
            sosed = n - 1;
        }
        else {
            sosed = n + 1;
        }
        cout << "Соседи в квартире " << sosed << ": " << kv[sosed - 1] << endl;
    }

    cout << "\nСумма по подъездам:" << endl;
    for (int p = 0; p < 4; p++) {
        int sum = 0;
        for (int i = p * 10; i < p * 10 + 10; i++) {
            sum += kv[i];
        }
        cout << "Подъезд " << p + 1 << ": " << sum << endl;
    }

    cout << "\nМногодетные семьи: ";
    bool est = false;
    for (int i = 0; i < 40; i++) {
        if (kv[i] > 5) {
            cout << i + 1 << " ";
            est = true;
        }
    }
    if (!est) cout << "нет";

    return 0;
}
*/

    // 3 задание
/*
    double t[31];


    cout << "Температура за 31 день:" << endl;
    for (int i = 0; i < 31; i++) {
        cin >> t[i];
    }


    double sum = 0;
    for (int i = 0; i < 31; i++) {
        sum += t[i];
    }
    cout << "Средняя: " << sum / 31 << endl;

    double porog;
    cout << "Порог: ";
    cin >> porog;

    int count = 0;
    for (int i = 0; i < 31; i++) {
        if (t[i] < porog) count++;
    }
    cout << "Дней ниже " << porog << ": " << count;

    return 0;
}
*/

// 4 задание

    double price[10];

    cout << "Цены 10 машин:" << endl;
    for (int i = 0; i < 10; i++) {
        cin >> price[i];
    }

    double max = price[0];
    for (int i = 1; i < 10; i++) {
        if (price[i] > max) max = price[i];
    }

    cout << "Макс цена: " << max << endl;

    for (int i = 0; i < 10; i++) {
        if (price[i] == max) {
            cout << "Первый: №" << i + 1 << endl;
            break;
        }
    }

    for (int i = 9; i >= 0; i--) {
        if (price[i] == max) {
            cout << "Последний: №" << i + 1 << endl;
            break;
        }
    }

    return 0;
}

// дальше не понимаю как делать