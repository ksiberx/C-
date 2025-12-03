#include <iostream>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");
	const int N = 5; // размер массива

	// 1 Задание
	cout << "1. Копирование массива" << endl;
	int a[N] = { 1, 2, 3, 4, 5 };
	int b[N];

	int* p1 = a; // указатель на первый массив
	int* p2 = b; // указатель на второй массив

	for (int i = 0; i < N; i++) {
		*(p2 + i) = *(p1 + i); // копирование элемента
	}

	cout << "Скопировал: ";
	for (int i = 0; i < N; i++) {
		cout << b[i] << "";
	}
	cout << endl;

	// 2 Задание
	cout << "\n2. Разворот массива" << endl;
	int c[N] = { 10, 20, 30, 40, 50 };

	int *start = c; // указатель на начало
	int* end = c + N - 1; // указатель на конец
	
	while (start < end) {
		int temp = *start;
		*start = *end;
		*end = temp;

		start++; // двигаемся вперед
		end--; // двигаемся назад
	}

	cout << "Развернул: ";
	for (int i = 0; i < N; i++) {
		cout << c[i] << "";
	}
	cout << endl;

	// 3 Задание
	cout << "\n3. Копирование с разворотом" << endl;
	int d[N] = { 100, 200, 300, 400, 500 };
	int e[N];

	int *p3 = d; // указатель на начало d
	int* p4 = e + N - 1; // указатель на конец e

	for (int i = 0; i < N; i++) {
		*p4 = *p3; // копируем
		p3++; // в d идем вперед
		p4--; // в е идем назад
	}
	
	cout << "Скопировал наоборот: ";
	for (int i = 0; i < N; i++) {
		cout << e[i] << "";
	}

	return 0;

}
