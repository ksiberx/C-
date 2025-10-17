/*#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    string menu[] = { "Маргарита", "Пепперони", "Гавайская", "4 сыра", "Кола", "Сок", "Вода" };
    int price[] = { 10, 12, 11, 13, 3, 2, 1 };

    int order[7] = { 0 };
    int total = 0;

    cout << "Меню:\n";
    for (int i = 0; i < 7; i++) {
        cout << i + 1 << ". " << menu[i] << " - " << price[i] << "$\n";
    }

    int code, count;
    while (true) {
        cout << "Код товара (0-закончить): ";
        cin >> code;
        if (code == 0) break;

        cout << "Количество: ";
        cin >> count;
        order[code - 1] += count;
    }

    cout << "\nВаш заказ:\n";

    int pizzaTotal = 0;
    int pizzaCount = 0;
    for (int i = 0; i < 4; i++) {
        if (order[i] > 0) {
            int sum = order[i] * price[i];
            cout << menu[i] << " - " << order[i] << " шт. - " << sum << "$\n";
            pizzaTotal += sum;
            pizzaCount += order[i];
        }
    }

    int drinkTotal = 0;
    for (int i = 4; i < 7; i++) {
        if (order[i] > 0) {
            int sum = order[i] * price[i];
            cout << menu[i] << " - " << order[i] << " шт. - " << sum << "$\n";
            drinkTotal += sum;
        }
    }

    total = pizzaTotal + drinkTotal;

    if (total > 50) {
        total = total * 0.8;
        cout << "Скидка 20%: " << total << "$\n";
    }

    if (pizzaCount >= 5) {
        cout << "Бесплатных пицц: " << pizzaCount / 5 << "\n";
    }

    cout << "К оплате: " << total << "$";

    return 0;
}
*/

// 2 Задание
#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int s1, s2, s3;

    cout << "Продажи 1 менеджера: ";
    cin >> s1;
    cout << "Продажи 2 менеджера: ";
    cin >> s2;
    cout << "Продажи 3 менеджера: ";
    cin >> s3;

    int z1 = 200;
    if (s1 < 500) z1 += s1 * 0.03;
    else if (s1 < 1000) z1 += s1 * 0.05;
    else z1 += s1 * 0.08;

    int z2 = 200;
    if (s2 < 500) z2 += s2 * 0.03;
    else if (s2 < 1000) z2 += s2 * 0.05;
    else z2 += s2 * 0.08;

    int z3 = 200;
    if (s3 < 500) z3 += s3 * 0.03;
    else if (s3 < 1000) z3 += s3 * 0.05;
    else z3 += s3 * 0.08;

    if (s1 > s2 && s1 > s3) {
        z1 += 200;
        cout << "1 менеджер лучший\n";
    }
    else if (s2 > s1 && s2 > s3) {
        z2 += 200;
        cout << "2 менеджер лучший\n";
    }
    else {
        z3 += 200;
        cout << "3 менеджер лучший\n";
    }

    cout << "Зарплаты:\n";
    cout << "1: " << z1 << "$\n";
    cout << "2: " << z2 << "$\n";
    cout << "3: " << z3 << "$\n";

    return 0;
}