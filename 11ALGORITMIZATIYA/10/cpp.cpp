#include <iostream>
#include <string>
#include <stdexcept>

class Student {
private:
    std::string name_;
    int age_;
    double grade_;

public:
    Student(const std::string& name, int age, double grade) {
        setName(name);
        setAge(age);
        setGrade(grade);
    }

    // Геттеры
    std::string getName() const {
        return name_;
    }

    int getAge() const {
        return age_;
    }

    double getGrade() const {
        return grade_;
    }

    // Сеттеры с валидацией
    void setName(const std::string& name) {
        if (name.empty()) {
            throw std::invalid_argument("Имя студента не может быть пустым.");
        }
        name_ = name;
    }

    void setAge(int age) {
        if (age < 16 || age > 100) {
            throw std::invalid_argument("Возраст должен быть в диапазоне от 16 до 100 лет.");
        }
        age_ = age;
    }

    void setGrade(double grade) {
        if (grade < 0.0 || grade > 100.0) {
            throw std::invalid_argument("Оценка должна быть в диапазоне от 0 до 100.");
        }
        grade_ = grade;
    }

    // Вычисляемый метод
    bool isExcellent() const {
        return grade_ >= 90.0;
    }

    // Опциональные свойства (только для компилятора MSVC)
#ifdef _MSC_VER
    __declspec(property(get = getAge, put = setAge)) int Age;
    __declspec(property(get = getGrade)) double Grade; // только чтение
#endif
};

// Пример использования
int main() {
    setlocale(LC_ALL, "Russian");

    try {
        Student s("Иван Иванов", 20, 92.5);
        std::cout << "Студент: " << s.getName() << "\n";
        std::cout << "Отличник: " << (s.isExcellent() ? "Да" : "Нет") << "\n";

#ifdef _MSC_VER
        s.Age = 21;
        std::cout << "Новый возраст (MSVC property): " << s.Age << "\n";
        std::cout << "Оценка (MSVC property): " << s.Grade << "\n";
#endif

        // Попытка передать некорректные данные
        s.setAge(15); 
    } catch (const std::invalid_argument& e) {
        std::cerr << "Исключение: " << e.what() << "\n";
    }

    return 0;
}
