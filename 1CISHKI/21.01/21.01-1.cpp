#include <iostream>
using namespace std;

// 1.1 Задача
int sum(int a, int b) { return a + b; }
int sub(int a, int b) { return a + b; }
int mul(int a, int b) { return a * b; }
int divide(int a, int b) { return b != 0 ? a / b : 0; }

void calculatorchik() {
 int(*operation)(int, int) = nullptr;
 int x, y;
 char op;

 cout << "Введите два числа и операцию (+, -, *, /): ";
 cin >> x >> y >> op;

 switch (op) {
 case '+': operation = sum; {
  break;
 }
 case '-': operation = sub; {
  break;
 }
 case '*': operation = mul; {
  break;
 }
 case '/': operation = divide; {
  break;
 }
 default: cout << "Неверная операция\n"; return;
 }

 cout << "Результат: " << operation(x, y) << endl;
}

// 1.2 Задача
int compareStrings(const char* a, const char* b) {
 while (*a && (*a == *b)) {
  a++;
 }
 return *(unsigned char*)a - *(unsigned char*)b;
}

void compareStringsExample() {
 char strl[100], str2[100];
 int (*compareFunc)(const char*, const char*) = compareStrings;

 cout << "Введите первую строку: ";
 cin.ignore();
 cin.getline(strl, 100);

 cout << "Введите вторую строку: ";
 cin.getline(str2, 100);

 int result = compareFunc(strl, str2);

 if (result > 0) cout << "Первая больше\n";
 else if (result < 0) cout << "Вторая больше\n";
 else cout << "Равны\n";
}

int main() {
 setlocale(LC_ALL, "ru");
 // 1.1
 calculatorchik();

 // 1.2
 compareStringsExample();

 return 0;
}
