#include <iostream>
#include <string>
using namespace std;

class Student {
private:
    string fullName;
    string birthDate;
    string phone;
    string city;
    string country;
    string university;
    string uniCity;
    string uniCountry;
    string groupNumber;

public:
    // Ввод данных
    void input() {
        cout << "Введите ФИО: ";
        getline(cin, fullName);
        
        cout << "Дата рождения: ";
        getline(cin, birthDate);
        
        cout << "Телефон: ";
        getline(cin, phone);
        
        cout << "Город: ";
        getline(cin, city);
        
        cout << "Страна: ";
        getline(cin, country);
        
        cout << "Учебное заведение: ";
        getline(cin, university);
        
        cout << "Город учебного заведения: ";
        getline(cin, uniCity);
        
        cout << "Страна учебного заведения: ";
        getline(cin, uniCountry);
        
        cout << "Номер группы: ";
        getline(cin, groupNumber);
    }

    // Вывод данных
    void display() {
        cout << "\n--- Данные студента ---\n";
        cout << "ФИО: " << fullName << endl;
        cout << "Дата рождения: " << birthDate << endl;
        cout << "Телефон: " << phone << endl;
        cout << "Город: " << city << endl;
        cout << "Страна: " << country << endl;
        cout << "Учебное заведение: " << university << endl;
        cout << "Город учебного заведения: " << uniCity << endl;
        cout << "Страна учебного заведения: " << uniCountry << endl;
        cout << "Группа: " << groupNumber << endl;
    }

    // Аксессоры (геттеры)
    string getFullName() { return fullName; }
    string getBirthDate() { return birthDate; }
    string getPhone() { return phone; }
    string getCity() { return city; }
    string getCountry() { return country; }
    string getUniversity() { return university; }
    string getUniCity() { return uniCity; }
    string getUniCountry() { return uniCountry; }
    string getGroupNumber() { return groupNumber; }

    // Сеттеры
    void setFullName(string name) { fullName = name; }
    void setGroupNumber(string group) { groupNumber = group; }
    // ... остальные сеттеры по аналогии
};

int main() {
    setlocale(LC_ALL, "ru");
    Student s1;
    s1.input();
    s1.display();
    
    // Использование аксессоров
    cout << "\nДоступ через геттеры:\n";
    cout << "Имя: " << s1.getFullName() << endl;
    cout << "Группа: " << s1.getGroupNumber() << endl;
    
    return 0;
}
