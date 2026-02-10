#include <iostream>
#include <fstream>
#include <string>
using namespace std;

class Point3D {
private:
    double x, y, z;

public:
    // Конструктор
    Point3D(double x = 0, double y = 0, double z = 0) : x(x), y(y), z(z) {}

    // Ввод данных
    void input() {
        cout << "Введите координату x: ";
        cin >> x;
        cout << "Введите координату y: ";
        cin >> y;
        cout << "Введите координату z: ";
        cin >> z;
    }

    // Вывод данных
    void display() {
        cout << "Точка: (" << x << ", " << y << ", " << z << ")" << endl;
    }

    // Аксессоры (геттеры)
    double getX() { return x; }
    double getY() { return y; }
    double getZ() { return z; }

    // Сеттеры
    void setX(double newX) { x = newX; }
    void setY(double newY) { y = newY; }
    void setZ(double newZ) { z = newZ; }

    // Сохранение в файл
    void saveToFile(string filename) {
        ofstream file(filename);
        if (file.is_open()) {
            file << x << " " << y << " " << z;
            file.close();
            cout << "Данные сохранены в файл: " << filename << endl;
        } else {
            cout << "Ошибка открытия файла!" << endl;
        }
    }

    // Загрузка из файла
    void loadFromFile(string filename) {
        ifstream file(filename);
        if (file.is_open()) {
            file >> x >> y >> z;
            file.close();
            cout << "Данные загружены из файла: " << filename << endl;
        } else {
            cout << "Ошибка открытия файла!" << endl;
        }
    }
};

int main() {
    setlocale(LC_ALL, "ru");
    Point3D p1;
    
    // Ввод и вывод
    p1.input();
    p1.display();
    
    // Использование аксессоров
    cout << "\nКоордината X через геттер: " << p1.getX() << endl;
    
    // Изменение через сеттер
    p1.setY(10.5);
    cout << "После изменения Y: ";
    p1.display();
    
    // Работа с файлами
    p1.saveToFile("point.txt");
    
    Point3D p2;
    p2.loadFromFile("point.txt");
    cout << "Загруженная точка: ";
    p2.display();
    
    return 0;
}
