// 1 Задание
#include <iostream>
using namespace std;
int main() {
	setlocale(LC_ALL, "ru");
	int a, b, c, d, e;

	cout << "Введите 5 оценок: ";
	cin >> a >> b >> c >> d >> e;

	switch (a + b + c + d + e >= 20) {
	case 1: cout << "Допущен"; break;
	case 0: cout << "Не допущен"; break;
	}
	return 0;
}

// 2 Задание
#include <iostream>
using namespace std;
int main() {
	setlocale(LC_ALL, "ru");
	int num;

	cout << "Введите число: ";
	cin >> num;

	switch (num % 2) {
	case 0:
		cout << num * 3;
		break;
	case 1: 
		cout << num / 2.0;
		break;
	}
	return 0;
}