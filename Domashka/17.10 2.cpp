// 1 Задание
/*#include <iostream>
using namespace std;
int main()
    {
    setlocale(LC_ALL, "ru");
    int A, B;
    cout << "Введите A и B: ";
    cin >> A >> B;

    for (int i = A; i <= B; i++) {
        cout << i << ": ";
        for (int j = 1; j <= i; j++) {
            if (i % j == 0) {
                cout << j << " ";
            }
        }
        cout << endl;
    }
}
*/

/*
// 1 Задание
#include <iostream>
using namespace std;
int main()
{
    setlocale(LC_ALL, "ru");
    cout << "\nПростые числа от 2 до 1000:\n";
    for (int i = 2; i <= 1000; i++) {
        bool prostoe = true;
        for (int j = 2; j * j <= i; j++) {
            if (i % j == 0) {
                prostoe = false;
                break;
            }
        }
        if (prostoe) cout << i << " ";
    }
    cout << endl;
}
*/

/*
// 3 Задание
#include <iostream>
using namespace std;
int main()
{
    setlocale(LC_ALL, "ru");
    cout << "\nФигура 4x5:\n";
    for (int i = 0; i < 4; i++) {
        for (int j = 0; j < 5; j++) {
            cout << "* ";
        }
        cout << endl;
    }

    return 0;
}
*/

/*
// 4 Задание 
#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    cout << "Все возможные коды:\n";
    int count = 0;

    for (int i = 0; i <= 9; i++) {
        for (int j = 0; j <= 9; j++) {
            for (int k = 0; k <= 9; k++) {
                if (i != j && i != k && j != k) {
                    cout << i << j << k << " ";
                    count++;
                }
            }
        }
    }

    cout << "\nВсего комбинаций: " << count << endl;
    cout << "Время на подбор: " << count * 3 << " секунд (" << count * 3 / 60.0 << " минут)\n\n";
}
*/

// 5 Задание
#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int total_all = 0;

    cout << "Введите зарплату 12 сотрудников:\n";

    for (int i = 1; i <= 12; i++) {
        int march, april, may;
        cout << "Сотрудник " << i << ":\n";
        cin >> march >> april >> may;

        int sum = march + april + may;
        cout << "Итого: " << sum << endl;

        total_all += sum;
    }

    cout << "Общая сумма: " << total_all;

    return 0;
}