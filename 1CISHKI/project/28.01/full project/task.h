#pragma once
#ifndef TASK_H // Защита от повторного подключения файла
#define TASK_H

#include <string>
#include <vector>

using namespace std;
// Структура одной задачи
struct Task {
    string title; // название задачи
    string description; // описание
    int priority; // приоритет (1-3)
    int day, month, year; // дата
    int hour, minute; // время
};

void addTask(vector<Task>& tasks); // добавить задачу
void showTasks(vector<Task>& tasks); // показать все задачи
void deleteTask(vector<Task>& tasks); // удалить задачу
void searchTasks(vector<Task>& tasks); // поиск задачи
void sortTasks(vector<Task>& tasks); // сортировка по приоритету
void saveTasks(vector<Task>& tasks); // сохранение
void loadTasks(vector<Task>& tasks); 
#endif

