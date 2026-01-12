/*
#include <iostream>
using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    // Задача 1.1
    int a, b;
    cout << "Введите первое число: ";
    cin >> a;
    cout << "Введите второе число: ";
    cin >> b;
    
    int result = a + b;
    cout << "Ответ: " << a << " + " << b << " = " << result << endl;
    
    // Задача 1.2
    
    double cels;
    cout << "Введите температуру в градусах цельсия: ";
    cin >> cels;
    
    double faren = cels * 9.0 / 5.0 + 32.0;
    cout << cels << "*C = " << faren << "*F" << endl;
    
    return 0;
}

*/


/*
#include <iostream>
using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    // Задача 2.1
    int number;
    cout << "Введите число: ";
    cin >> number;
    
    if (number % 2 == 0) {
        cout << number << " - чётное число." << endl;
    } else {
        cout << number << " - нечётное число." << endl;
    }
    
    // Задача 2.2
    int year;
    cout << "Введите год: ";
    cin >> year;
    
    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
    cout << year << " - високосный год." << endl;
    else
    cout << year << " - невисокосный год." << endl;
    
    return 0;
}

*/

/*

#include <iostream>
using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    // Задача 3.1
    int n;
    cout << "Введите N: ";
    cin >> n;
    
    int sum = 0;
    for (int i = 1; i <= n; i++) {
        sum += i;
    }
    
    cout << "Сумма чисел от 1 до " << n << " равна " << sum << "." << endl;
    
    // Задача 3.2
    int num;
    cout << "Введите число: ";
    cin >> num;
    
    for (int i = 1; i <= 10; i++) {
        cout << num << " x " << i << " = " << num * i << endl;
    }
    
    return 0;
    
    
}

*/



/*

#include <iostream>
using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    // Задача 4.1
        int arr[10];
    cout << "Введите 10 чисел: ";
    for(int i = 0; i < 10; i++) cin >> arr[i];
    
    int min = arr[0];
    for(int i = 1; i < 10; i++) 
        if(arr[i] < min) min = arr[i];
    
    cout << "Минимум: " << min << endl << endl;
    
    // Задача 4.2  
    int mat[3][3];
    cout << "Введите матрицу 3x3:\n";
    for(int i = 0; i < 3; i++)
        for(int j = 0; j < 3; j++)
            cin >> mat[i][j];
    
    for(int i = 0; i < 3; i++) {
        int sum = 0;
        for(int j = 0; j < 3; j++)
            sum += mat[i][j];
        cout << "Сумма строки " << i+1 << ": " << sum << endl;
    }
    
    return 0;
}
   */
   
   
   /*
#include <iostream>
using namespace std;

int max(int a, int b) {
    return a > b ? a : b;
}

bool isPrime(int n) {
    if (n < 2) return false;
    for (int i = 2; i <= n/2; i++)
        if (n % i == 0) return false;
    return true;
}

int main() {
    setlocale(LC_ALL, "ru");
    // Задача 5.1
    int a, b;
    cout << "Введите два числа: ";
    cin >> a >> b;
    cout << "Наибольшее: " << max(a, b) << endl << endl;
    
    // Задача 5.2
    int n;
    cout << "Введите число: ";
    cin >> n;
    cout << n << " - " << (isPrime(n) ? "простое" : "не простое") << endl;
    
    return 0;
}
   */
   
   /*
   #include <iostream
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Задача 6.1
    int a = 10, b = 20;
    cout << "До: a = " << a << ", b = " << b << endl;
    
    int *ptrA = &a;
    int *ptrB = &b;
    int temp = *ptrA;
    *ptrA = *ptrB;
    *ptrB = temp;
    
    cout << "После: a = " << a << ", b = " << b << endl << endl;
    
    // Задача 6.2
    int arr[] = {1, 2, 3, 4, 5};
    int *ptr = arr; 
    int sum = 0;
    
    for (int i = 0; i < 5; i++) {
        sum += *ptr; 
        ptr++;      
    }
    
    cout << "Массив: ";
    for (int i = 0; i < 5; i++) {
        cout << arr[i] << " ";
    }
    cout << endl << "Сумма элементов: " << sum << endl;
    
    return 0;
}
   
   */
   
   /*
#include <iostream>
#include <string>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Задача 7.1
    string s1;
    cout << "Ввод: ";
    getline(cin, s1);
    
    int len = 0;
    for (int i = 0; s1[i] !=
'\0'; i++) {
        len++;
    }
    cout << "Длина строки \"" << s1 << "\" равна " << len << " символам.\n\n";
    
    // Задача 7.2
    string s2;
    cout << "Ввод: ";
    getline(cin, s2);
    
    int vowels = 0;
    for (int i = 0; i < s2.length(); i++) {
        char c = tolower(s2[i]);
        if (c == 'a'  c == 'e'  c == 'i'  c == 'o'  c == 'u' || c == 'y') {
            vowels++;
        }
    }
    cout << "Количество гласных: " << vowels << endl;
    
    return 0;
}  
   
   */
   
   
   #include <iostream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru");
    // Задача 8.1
    int a[5];
    int sum = 0;
    
    cout << "Ввод: ";
    for (int i = 0; i < 5; i++) {
        cin >> a[i];
        sum += a[i];
    }
    cout << "Среднее арифметическое: " << sum / 5 << endl << endl;
    
    // Задача 8.2
    int b[5];
    
    cout << "Ввод: ";
    for (int i = 0; i < 5; i++) {
        cin >> b[i];
    }
    
    bool ok = true;
    for (int i = 0; i < 2; i++) {
        if (b[i] != b[4-i]) {
            ok = false;
        }
    }
    
    if (ok) {
        cout << "Массив является палиндромом" << endl;
    } else {
        cout << "Массив не является палиндромом" << endl;
    }
    
    return 0;
}
