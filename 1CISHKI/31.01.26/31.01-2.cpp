#include <iostream>
#include <fstream>
#include <string>
#include <windows.h>

using namespace std;

struct Employee {
    string surname;
    int age;
};

Employee emp[100];
int n = 0;

// загрузка из файла
void load() {
    ifstream fin("employees.txt");
    if (!fin) return;

    n = 0;
    while (fin >> emp[n].surname >> emp[n].age) {
        n++;
    }
    fin.close();
}

// сохранение всего списка
void save() {
    ofstream fout("employees.txt");
    for (int i = 0; i < n; i++) {
        fout << emp[i].surname << " " << emp[i].age << endl;
    }
    fout.close();
}

// добавить
void add() {
    cout << "Фамилия: ";
    cin >> emp[n].surname;
    cout << "Возраст: ";
    cin >> emp[n].age;
    n++;
}

// показать всех
void showAll() {
    cout << "\n========================\nПоказаны все сотрудники!\n========================\n";
    for (int i = 0; i < n; i++) {
        cout << i << ")Фамилия: " << emp[i].surname << " | Возраст: " << emp[i].age << " |" << endl;
    }
}

// редактировать
void edit() {
    int k;
    showAll();
    cout << "Номер для редактирования: ";
    cin >> k;

    if (k >= 0 && k < n) {
        cout << "Новая фамилия: ";
        cin >> emp[k].surname;
        cout << "Новый возраст: ";
        cin >> emp[k].age;
    }
}

// удалить
void removeEmp() {
    int k;
    showAll();
    cout << "Номер для удаления: ";
    cin >> k;

    if (k >= 0 && k < n) {
        for (int i = k; i < n - 1; i++) {
            emp[i] = emp[i + 1];
        }
        n--;
    }
}

// сохранить найденное
void saveFound(string surname, int age) {
    ofstream fout("found.txt", ios::app);
    fout << surname << " " << age << endl;
    fout.close();
}

// поиск по фамилии
void searchSurname() {
    string s;
    cout << "Фамилия: ";
    cin >> s;

    for (int i = 0; i < n; i++) {
        if (emp[i].surname == s) {
            cout << emp[i].surname << " " << emp[i].age << endl;
            saveFound(emp[i].surname, emp[i].age);
        }
    }
}

// поиск по возрасту
void searchAge() {
    int a;
    cout << "Возраст: ";
    cin >> a;

    for (int i = 0; i < n; i++) {
        if (emp[i].age == a) {
            cout << emp[i].surname << " " << emp[i].age << endl;
            saveFound(emp[i].surname, emp[i].age);
        }
    }
}

// поиск по букве
void searchLetter() {
    char c;
    cout << "Буква: ";
    cin >> c;

    for (int i = 0; i < n; i++) {
        if (emp[i].surname[0] == c) {
            cout << emp[i].surname << " " << emp[i].age << endl;
            saveFound(emp[i].surname, emp[i].age);
        }
    }
}

int main() {
    setlocale(LC_ALL, "Ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    load();
    cout << "=========Сотрудник==========";

    int ch;
    do {
        cout << "\n1 - Добавить\n2 - Показать всех\n3 - Редактировать\n";
        cout << "4 - Удалить\n5 - Поиск по фамилии\n6 - Поиск по возрасту\n";
        cout << "7 - Поиск по букве\n8 - Сохранить\n0 - Выход\n\nВыберите действие: ";
        cin >> ch;

        switch (ch) {
        case 1: add(); 
            cout << "\n===========================\nСотрудник успешно добавлен!\n===========================\n";
            break;
        case 2: showAll();
            break;
        case 3: edit(); 
            cout << "\n===========================\nУспешно отредактировано!\n===========================\n";
            break;
        case 4: removeEmp(); break;
        case 5: searchSurname(); break;
        case 6: searchAge(); break;
        case 7: searchLetter(); break;
        case 8: save(); break;
        }
    } while (ch != 0);

    save();
    return 0;
}
