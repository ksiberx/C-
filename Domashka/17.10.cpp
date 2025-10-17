// 1 Задание
/*#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int level;
    cout << "Выбери уровень: 1-легкий, 2-средний, 3-сложный: ";
    cin >> level;

    int q, num;
    if (level == 1) { q = 3; num = 5; }
    else if (level == 2) { q = 4; num = 8; }
    else { q = 5; num = 10; }

    int score = 0;

    for (int i = 0; i < q; i++) {
        int a = rand() % num + 1;
        int b = rand() % num + 1;
        int ans;

        cout << a << " * " << b << " = ";
        cin >> ans;

        if (ans == a * b) {
            cout << "Да!\n";
            score++;
        }
        else {
            cout << "Нет! " << a * b << endl;
        }
    }

    cout << "Твой счет: " << score << " из " << q << endl;

    if (score == q) cout << "Молодец! 5";
    else if (score >= q - 1) cout << "Хорошо! 4";
    else if (score >= q - 2) cout << "Нормально! 3";
    else cout << "Плохо! 2";

    return 0;
}
*/

// 2 Задание

#include <iostream>
using namespace std;

int main() {
    int n = 5;

    for (int i = 1; i <= n; i++) {

        for (int j = 1; j <= n - i; j++) {
            cout << " ";
        }

        for (int j = 1; j <= 2 * i - 1; j++) {
            cout << "*";
        }
        cout << endl;
    }


    for (int i = n - 1; i >= 1; i--) {

        for (int j = 1; j <= n - i; j++) {
            cout << " ";
        }

        for (int j = 1; j <= 2 * i - 1; j++) {
            cout << "*";
        }
        cout << endl;
    }

    return 0;
}
