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

// ===== ФУНКЦИИ =====

void addTask(vector<Task>& tasks) {
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

void showTasks(const vector<Task>& tasks) {
    for (int i = 0; i < tasks.size(); i++) {
        cout << "[" << i << "] " << tasks[i].title
            << "\n   Описание: " << tasks[i].description
            << "\n   Приоритет: " << tasks[i].priority
            << "\n   Дата: "
            << tasks[i].day << "." << tasks[i].month << "." << tasks[i].year
            << " " << tasks[i].hour << ":" << tasks[i].minute
            << "\n";
    }
}

void deleteTask(vector<Task>& tasks) {
    int id;
    cout << "Номер: ";
    cin >> id;

    if (id >= 0 && id < tasks.size()) {
        tasks.erase(tasks.begin() + id);
    }
}

void searchTasks(const vector<Task>& tasks) {
    string key;
    cin.ignore(10000, '\n');

    cout << "Введите слово: ";
    getline(cin, key);

    for (int i = 0; i < tasks.size(); i++) {

        if (tasks[i].title.find(key) != -1 ||
            tasks[i].description.find(key) != -1) {

            cout << "[" << i << "] " << tasks[i].title << endl;
        }
    }
}

void sortTasks(vector<Task>& tasks) {
    for (int i = 0; i < tasks.size(); i++)
        for (int j = i + 1; j < tasks.size(); j++)
            if (tasks[i].priority < tasks[j].priority)
                swap(tasks[i], tasks[j]);
}

// ===== MAIN =====

int main() {
    setlocale(LC_ALL, "ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    vector<Task> tasks;

    while (true) {
        cout << "============= Список дел =============";
        cout << "\n1 - Добавить\n";
        cout << "2 - Показать все\n";
        cout << "3 - Удалить\n";
        cout << "4 - Поиск\n";
        cout << "5 - Сортировать по приоритету\n";
        cout << "0 - Выход\n";
        cout << "Выберите действие: ";

        int choice;
        cin >> choice;

        if (!cin) {
            cin.clear();
            cin.ignore(10000, '\n');
            continue;
        }

        switch (choice) {
        case 1: addTask(tasks); break;
        case 2: showTasks(tasks); break;
        case 3: deleteTask(tasks); break;
        case 4: searchTasks(tasks); break;
        case 5: sortTasks(tasks); break;
        case 0: return 0;
        default: break;
        }
    }
}
