// 1 Задание

/*#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    long long n;
    cout << "Введите число: ";
    cin >> n;

    int sum = 0, zeros = 0, count = 0;
    if (n == 0) zeros = 1, count = 1;

    n = abs(n);
    while (n > 0) {
        int d = n % 10;
        sum += d;
        if (d == 0) zeros++;
        count++;
        n /= 10;
    }

    cout << "Цифр: " << count << endl;
    cout << "Сумма: " << sum << endl;
    cout << "Среднее: " << (double)sum / count << endl;
    cout << "Нулей: " << zeros << endl;
}
*/

// 2 Задание
/* 
    #include <iostream>
    using namespace std;

    int main() {
        setlocale(LC_ALL, "ru");
        int n;
        cout << "Размер клетки: ";
        cin >> n;

        for (int i = 0; i < 8; i++) {
            for (int k = 0; k < n; k++) {
                for (int j = 0; j < 8; j++) {
                    for (int l = 0; l < n; l++)
                        cout << (((i + j) % 2) ? ' ' : '#');
                }
                cout << endl;
            }
        }
    }
  */

// 3 Задание
/*
#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int n, x;
    double total = 0;

    cout << "Сколько человек? ";
    cin >> n;

    for (int i = 1; i <= n; i++) {
        double sum = 0;
        char more;

        do {
            cout << "\n1.Кофе(150)  2.Чай(100)  3.Пирожное(120)\nВыбор: ";
            cin >> x;

            if (x == 1) sum += 150;
            else if (x == 2) sum += 100;
            else if (x == 3) sum += 120;

            cout << "Добавить еще? (y/n): ";
            cin >> more;
        } while (more == 'y');

        total += sum;
    }

    cout << "\nИтог: " << total << " руб.\n";
}
*/




// 4 задание

/* 
    #include <iostream>
using namespace std;

int main() {
    for (int y = 0; y < 10; y++) {
        for (char x = 'A'; x <= 'J'; x++) {
            cout << x << y << " ";
        }
        cout << endl;
    }
}
*/