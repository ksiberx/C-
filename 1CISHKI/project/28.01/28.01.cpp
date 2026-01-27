#include <iostream>
#include <string>
#include <vector>
#include <windows.h>

using namespace std;
// Структура
struct Task {
    string title;
    string description;
    int priority;
    int day, month, year;
    int hour, minute;
};

int main() {

    setlocale(LC_ALL, "Ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    vector<Task> tasks;
    while (true) {
        // Менюшка
        cout << "\n1 - Добавить\n";
        cout << "2 - Показать все\n";
        cout << "3 - Удалить\n";
        cout << "4 - Поиск\n";
        cout << "5 - Сортировать по приоритету\n";
        cout << "0 - Выход\n";

        int choice;
        cin >> choice;

        if (!cin) {
            cin.clear();
            cin.ignore(10000, '\n');
            continue;
        }

        // Добавить
        if (choice == 1) {

            Task t;

            cin.ignore(10000, '\n');

            cout << "Название: ";
            getline(cin, t.title);

            cout << "Описание: ";
            getline(cin, t.description);

            cout << "Приоритет (1-3): ";
            cin >> t.priority;

            cout << "Дата (д м г): ";
            cin >> t.day >> t.month >> t.year;

            cout << "Время (ч м): ";
            cin >> t.hour >> t.minute;

            tasks.push_back(t);
        }

        // Показ
        else if (choice == 2) {

            for (int i = 0; i < tasks.size(); i++) {
                cout << "[" << i << "] "
                    << tasks[i].title
                    << "\n   Описание: " << tasks[i].description
                    << "\n   Приоритет: " << tasks[i].priority
                    << "\n   Дата: "
                    << tasks[i].day << "." << tasks[i].month << "." << tasks[i].year
                    << " "
                    << tasks[i].hour << ":" << tasks[i].minute
                    << "\n";
            }
        }

        // Удаление
        else if (choice == 3) {

            int id;
            cout << "Номер: ";
            cin >> id;

            if (id >= 0 && id < tasks.size()) {
                tasks.erase(tasks.begin() + id);
            }
        }

        // Поиск
        else if (choice == 4) {

            string key;
            cin.ignore(10000, '\n');

            cout << "Введите слово: ";
            getline(cin, key);
            for (int i = 0; i < tasks.size(); i++) {
                if (tasks[i].title.find(key) != string::npos ||
                    tasks[i].description.find(key) != string::npos) {

                    cout << "[" << i << "] " << tasks[i].title << endl;
                }
            }
        }

        // Сортировка 
        else if (choice == 5) {

            for (int i = 0; i < tasks.size(); i++)
                for (int j = i + 1; j < tasks.size(); j++)
                    if (tasks[i].priority < tasks[j].priority)
                        swap(tasks[i], tasks[j]);
        }

        else if (choice == 0) {
            break;
        }
    }

    return 0;
}
