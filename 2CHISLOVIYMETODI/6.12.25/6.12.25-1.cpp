#include <iostream>
#include <cmath>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");
	double A[3][3] = {
		{8.39, -8.99, -1.29},
		{-1.05, 1.89, 4.91},      // матрица
		{6.38, 8.36, 0.48}
	};
	double b[3] = { 8.86, 4.20, -5.06 };
	double x[3];

	for (int i = 0; i < 3; i++) {
		int maxRow = i;
		for (int k = i + 1; k < 3; k++)                // поиск ведущего элемента
			if (fabs(A[k][i]) > fabs(A[maxRow][i]))
				maxRow = k;

		if (maxRow != i) {
			for (int j = 0; j < 3; j++)
				swap(A[i][j], A[maxRow][j]);    // перестановка строк
			swap(b[i], b[maxRow]);
		}

		for (int k = i + 1; k < 3; k++) {
			double factor = A[k][i] / A[i][i];
			for (int j = i; j < 3; j++)              // обнуление элементов ниже диагонали
				A[k][j] -= factor * A[i][j];
			b[k] -= factor * b[i];
		}
	}

	for (int i = 2; i >= 0; i--) {
		x[i] = b[i];
		for (int j = i + 1; j < 3; j++)  // Обратный ход
			x[i] -= A[i][j] * x[j];
		x[i] /= A[i][i];
	}

	cout << "Задание 1: Решение системы 3х3" << endl;
	cout << "x1 = " << x[0] << endl;
	cout << "x2 = " << x[1] << endl;
	cout << "x3 = " << x[2] << endl;

	return 0;

}
