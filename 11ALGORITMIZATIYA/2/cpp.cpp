
#include <iostream>
#include <cmath>
#include <string>
#include <algorithm>
#include <random>
using namespace std;

// Задача 1. Копилка

class PiggyBank {
private:
    int money = 0;

public:
    void put(int amount) {
        if (amount > 0) money += amount;
    }

    void take(int amount) {
        if (amount > 0 && amount <= money)
            money -= amount;
        else
            cout << "Недостаточно денег!\n";
    }

    void show() {
        cout << "В копилке: " << money << endl;
    }
};

// Задача 2. Светофор

class TrafficLight {
private:
    string color = "red";

public:
    void next() {
        if (color == "red")
            color = "green";
        else if (color == "green")
            color = "yellow";
        else
            color = "red";
    }

    void show() {
        cout << "Цвет: " << color << endl;
    }
};

// Задача 3. Точка на плоскости

class Point {
private:
    double x, y;

public:
    Point(double x, double y) {
        this->x = x;
        this->y = y;
    }

    void move(double dx, double dy) {
        x += dx;
        y += dy;
    }

    double distanceTo(Point other) {
        return sqrt(pow(other.x - x, 2) +
                    pow(other.y - y, 2));
    }

    void show() {
        cout << "(" << x << ", " << y << ")\n";
    }
};

// Задача 4. Термометр

class Thermometer {
private:
    double temperature;

public:
    Thermometer(double t = 0) {
        setCelsius(t);
    }

    void setCelsius(double t) {
        if (t < -273.15)
            throw invalid_argument("Ниже абсолютного нуля!");
        temperature = t;
    }

    double getCelsius() const {
        return temperature;
    }

    double getFahrenheit() const {
        return temperature * 9.0 / 5.0 + 32;
    }
};

// Задача 5. Тамагочи

class Pet {
private:
    string name;
    int hunger;
    int happiness;

public:
    Pet(string n) {
        name = n;
        hunger = 50;
        happiness = 50;
    }

    void feed() {
        hunger = max(0, hunger - 20);
        happiness = min(100, happiness + 5);
    }

    void play() {
        happiness = min(100, happiness + 15);
        hunger = min(100, hunger + 10);
    }

    void tick() {
        hunger = min(100, hunger + 5);
        happiness = max(0, happiness - 3);
    }

    string mood() const {
        if (happiness > 70) return "счастлив";
        if (happiness >= 40) return "нормально";
        return "грустен";
    }

    void show() {
        cout << name
             << " | Голод: " << hunger
             << " | Счастье: " << happiness
             << " | Настроение: " << mood() << endl;
    }
};

// Главная функция

int main() {
    cout << "--- Задача 1 ---\n";
    PiggyBank bank;
    bank.put(100);
    bank.put(50);
    bank.take(200);
    bank.show();

    cout << "\n--- Задача 2 ---\n";
    TrafficLight light;
    for (int i = 0; i < 5; i++) {
        light.next();
        light.show();
    }

    cout << "\n--- Задача 3 ---\n";
    Point p1(2, 3);
    Point p2(5, 7);
    p1.move(1, 2);
    p1.show();
    p2.show();
    cout << "Расстояние: " << p1.distanceTo(p2) << endl;

    cout << "\n--- Задача 4 ---\n";
    Thermometer thermometer(25);
    cout << "Цельсий: " << thermometer.getCelsius() << endl;
    cout << "Фаренгейт: " << thermometer.getFahrenheit() << endl;

    cout << "\n--- Задача 5 ---\n";
    Pet pet("Бобик");

    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> dist(0, 2);

    for (int i = 1; i <= 10; i++) {
        int action = dist(gen);

        cout << "Шаг " << i << ": ";

        if (action == 0) {
            cout << "Кормим\n";
            pet.feed();
        }
        else if (action == 1) {
            cout << "Играем\n";
            pet.play();
        }
        else {
            cout << "Ждём\n";
            pet.tick();
        }

        pet.show();
    }

    return 0;
}
