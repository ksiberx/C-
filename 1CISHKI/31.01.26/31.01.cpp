#include <iostream>
using namespace std;

// 1. Нахождение меньшего из двух чисел
#define MIN(a, b) ((a) < (b) ? (a) : (b))

// 2. Нахождение большего из двух чисел
#define MAX(a, b) ((a) > (b) ? (a) : (b))

// 3. Возведение числа в квадрат
#define SQUARE(x) ((x) * (x))

// 4. Возведение числа в степень
#define POWER(x, n) (pow_implementation(x, n))

// Вспомогательная функция для возведения в степень (для целых чисел)
inline int pow_implementation(int base, int exponent) {
    int result = 1;
    for (int i = 0; i < exponent; i++) {
        result *= base;
    }
    return result;
}

// 5. Проверка числа на четность
#define IS_EVEN(x) ((x) % 2 == 0)

// 6. Проверка числа на нечетность
#define IS_ODD(x) ((x) % 2 != 0)

int main() {
    setlocale(LC_ALL, "Ru");
    // Тестирование макросов
    int a = 10, b = 20;

    cout << "a = " << a << ", b = " << b << endl;
    cout << "Меньшее: " << MIN(a, b) << endl;
    cout << "Большее: " << MAX(a, b) << endl;

    cout << "\nКвадрат числа " << a << ": " << SQUARE(a) << endl;
    cout << "Квадрат числа " << b << ": " << SQUARE(b) << endl;

    cout << "\n" << a << " в степени 3: " << POWER(a, 3) << endl;
    cout << b << " в степени 2: " << POWER(b, 2) << endl;

    cout << "\nПроверка на четность:" << endl;
    cout << a << " четное? " << (IS_EVEN(a) ? "Да" : "Нет") << endl;
    cout << b << " четное? " << (IS_EVEN(b) ? "Да" : "Нет") << endl;

    cout << "\nПроверка на нечетность:" << endl;
    cout << a << " нечетное? " << (IS_ODD(a) ? "Да" : "Нет") << endl;
    cout << b << " нечетное? " << (IS_ODD(b) ? "Да" : "Нет") << endl;

    // Дополнительный пример с другими числами
    int c = 7, d = 4;
    cout << "\nДополнительные примеры:" << endl;
    cout << "c = " << c << ", d = " << d << endl;
    cout << "MIN(c, d) = " << MIN(c, d) << endl;
    cout << "MAX(c, d) = " << MAX(c, d) << endl;
    cout << "SQUARE(c) = " << SQUARE(c) << endl;
    cout << "POWER(c, 2) = " << POWER(c, 2) << endl;
    cout << "IS_EVEN(c) = " << (IS_EVEN(c) ? "Да" : "Нет") << endl;
    cout << "IS_ODD(d) = " << (IS_ODD(d) ? "Да" : "Нет") << endl;

    return 0;
}
