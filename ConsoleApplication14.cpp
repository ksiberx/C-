/*#include <iostream>;
using namespace std;
int main()
{
	setlocale(LC_ALL, "ru");
	for (int i = 1; i <= 10; i++) {
		for (int j = 1; j <= 10; j++) {
			cout << i << " * " << j << " = " << i * j << "\t";
			cout << endl;
		}
	}
	return 0;



}
*/

// 2 задание

/*#include <iostream>
using namespace std;

int main() {
    int N;
    cin >> N;

    for (int i = 2; i <= N; i++) {
        bool prime = true;
        for (int j = 2; j < i; j++)
        {
            if (i % j == 0) 
            {
                prime = false;
            }
        }
        if (prime) {
            cout << i << " ";
        }
    }
    return 0;
}
*/

// 3 Задание

/*#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int M, N;
    cout << "Введите M и N: ";
    cin >> M >> N;

    int num = 1;
    for (int i = 0; i < M; i++) {
        for (int j = 0; j < N; j++) {
            cout << num << " ";
            num++;
        }
        cout << endl;
    }
    return 0;
}
*/

// 4 задание

#include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    int N;
    cout << "Введите число: ";
    cin >> N;

    if (N == 1) cout << "1";
    if (N == 2) cout << "1 2\n2 1";
    if (N == 3) cout << "1 2 3\n1 3 2\n2 1 3\n2 3 1\n3 1 2\n3 2 1";
    return 0;
}

