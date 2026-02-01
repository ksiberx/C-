#include <iostream>
#include <windows.h>
#include "Task.h"

using namespace std;

int main() {

    setlocale(LC_ALL, "Ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    vector<Task> tasks; // список задач
    loadTasks(tasks);

    while (true) {
        cout << "=============== Список дел ===============";
        cout << "\n1 - Добавить\n";
        cout << "2 - Показать все\n";
        cout << "3 - Сортировать по приоритету\n";
        cout << "4 - Поиск\n";
        cout << "5 - Удалить\n";
        cout << "6 - Сохранение\n";
        cout << "0 - Выход\n";
        cout << "Выберите действие: ";

        int choice;
        cin >> choice; // выбор пункта меню

        // проверка на ошибочный ввод
        if (!cin) {
            cin.clear();
            cin.ignore(10000, '\n');
            continue;
        }

        switch (choice) {
        case 1:
            addTask(tasks);
            break;

        case 2:
            showTasks(tasks);
            break;

        case 3:
            sortTasks(tasks);
            break;

        case 4:
            searchTasks(tasks);
            break;

        case 5:
            deleteTask(tasks);
            break;

        case 6:
            saveTasks(tasks); // сохранение

        case 0:
            return 0; // выход из программы

        default:
            cout << "Неверный пункт меню\n";
            break;
        }
    }
}
