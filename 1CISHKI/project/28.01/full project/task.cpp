#include "Task.h" // Подключаем заголовок
#include <iostream>
#include <fstream> // Для сохранения
#include <sstream> 

using namespace std;

// Добавление новой задачи
void addTask(vector<Task>& tasks) {
    Task t; // создаем временную задачу
    cin.ignore(10000, '\n'); 

    cout << "\nНазвание: ";
    getline(cin, t.title); // ввод

    cout << "Описание: ";
    getline(cin, t.description); // ввод

    cout << "Приоритет (1-3): ";
    cin >> t.priority; // ввод

    cout << "Дата (д м г): ";
    cin >> t.day >> t.month >> t.year; // ввод

    cout << "Время (ч м): ";
    cin >> t.hour >> t.minute; // ввод

    tasks.push_back(t); // добавление в список
    cout << "---------------------- Успешно добавлено! ----------------------\n\n";
}
// Вывод всех задач
void showTasks(vector<Task>& tasks) {
    cout << "\n---------------------- Весь список ----------------------\n";
    for (int i = 0; i < tasks.size(); i++) {
        cout << "[" << i << "] " << tasks[i].title << endl;
        cout << "   Описание: " << tasks[i].description << endl;
        cout << "   Приоритет: " << tasks[i].priority << endl;
        cout << "   Дата: " << tasks[i].day << "." << tasks[i].month << "." << tasks[i].year
            << " " << tasks[i].hour << ":" << tasks[i].minute << endl;
    }
    cout << "---------------------------------------------------------\n\n"; // Линия в конце всего списка
}
// Удаление задач
void deleteTask(vector<Task>& tasks) {
    int id;
    cout << "Номер: ";
    cin >> id;
    cout << "---------------------- Успешно удалено! ----------------------\n\n";
    if (id >= 0 && id < tasks.size()) {
        tasks.erase(tasks.begin() + id);
    }
}
// Поиск задач
void searchTasks(vector<Task>& tasks) {
    string key;

    cin.ignore(10000, '\n');
    cout << "Введите слово: ";
    getline(cin, key);
    cout << "Найдено: \n";
    for (int i = 0; i < tasks.size(); i++) {
        if (tasks[i].title.find(key) != -1 ||
            tasks[i].description.find(key) != -1) {

            cout << "[" << i << "] " << tasks[i].title << endl;
        }
    }
}
// Сортировка по приоритету
void sortTasks(vector<Task>& tasks) {
    cout << "---------------------- Успешно! ----------------------\n\n";
    for (int i = 0; i < tasks.size(); i++) {
        for (int j = i + 1; j < tasks.size(); j++) {
            if (tasks[i].priority < tasks[j].priority) {
                Task temp = tasks[i];
                tasks[i] = tasks[j];
                tasks[j] = temp;
            }
        }
    }
}
// Сохранение задач
void saveTasks(vector<Task>& tasks) {
    cout << "Сохраняемся...";
    ofstream file("tasks1.txt");

    for (int i = 0; i < tasks.size(); i++) {
        file << "Название: " << tasks[i].title << endl;
        file << "Описание: " << tasks[i].description << endl;
        file << "Приоритет: " << tasks[i].priority << endl;
        file << "Дата: "
            << tasks[i].day << " "
            << tasks[i].month << " "
            << tasks[i].year << endl;
        file << "Время: "
            << tasks[i].hour << " "
            << tasks[i].minute << endl;
        file << "-----" << endl;
    }

    file.close();
}


void loadTasks(vector<Task>& tasks) {
    ifstream file("tasks1.txt");
    Task t;
    string line;

    while (getline(file, line)) {

        // Название
        if (line.find("Название: ") != 0)
            break;
        t.title = line.substr(10);

        // Описание
        getline(file, line);
        t.description = line.substr(11);

        // Приоритет
        getline(file, line);
        stringstream(line.substr(11)) >> t.priority;

        // Дата
        getline(file, line);
        stringstream(line.substr(6)) >> t.day >> t.month >> t.year;

        // Время
        getline(file, line);
        stringstream(line.substr(7)) >> t.hour >> t.minute;

        // ---
        getline(file, line);

        tasks.push_back(t);
    }

    file.close();
}

