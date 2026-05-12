#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

struct Student {
    string name;
    vector<int> grades;
    
    double average() const {
        if (grades.empty()) return 0.0;
        double sum = 0.0;
        for (int g : grades) {
            sum += g;
        }
        return sum / grades.size();
    }
};

vector<Student> inputStudents() {
    vector<Student> students;
    int count;
    
    cout << "Введите количество студентов: ";
    cin >> count;
    cin.ignore(); 
    
    for (int i = 0; i < count; i++) {
        Student s;
        
        cout << "Имя студента " << (i + 1) << ": ";
        getline(cin, s.name);
        
        int n;
        cout << "Количество оценок: ";
        cin >> n;
        
        if (n <= 0) {
            cout << "Ошибка! Количество оценок должно быть больше нуля." << endl;
            i--;  
            cin.clear();
            cin.ignore(10000, '\n');
            continue;
        }
        
        cout << "Введите оценки через пробел: ";
        for (int j = 0; j < n; j++) {
            int grade;
            cin >> grade;
            s.grades.push_back(grade);
        }
        cin.ignore();
        
        students.push_back(s);
    }
    
    return students;
}

void printTop3(const vector<Student>& students) {
    if (students.empty()) {
        cout << "Нет студентов для вывода." << endl;
        return;
    }
    
    vector<Student> sorted = students;

    sort(sorted.begin(), sorted.end(), [](const Student& a, const Student& b) {
        return a.average() > b.average();
    });
    
    int topCount = 3;
    if (sorted.size() < 3) topCount = sorted.size();
    
    cout << "\n=== ТОП-" << topCount << " студентов ===" << endl;
    for (int i = 0; i < topCount; i++) {
        cout << i + 1 << ". " << sorted[i].name 
             << " - Средний балл: " << sorted[i].average() << endl;
    }
}

void printAllSorted(vector<Student>& students) {
    if (students.empty()) {
        cout << "Нет студентов для вывода." << endl;
        return;
    }
    
    sort(students.begin(), students.end(), [](const Student& a, const Student& b) {
        return a.average() > b.average();
    });
    
    cout << "\nВсе студенты (по убыванию среднего балла)" << endl;
    for (size_t i = 0; i < students.size(); i++) {
        cout << i + 1 << ". " << students[i].name 
             << " - Средний балл: " << students[i].average() << endl;
    }
}

int main() {
    setlocale(LC_ALL, "ru");
    
    vector<Student> students = inputStudents();
    
    if (students.empty()) {
        cout << "Список студентов пуст. Программа завершена." << endl;
        return 0;
    }
    
    printTop3(students);
    
    printAllSorted(students);
    
    return 0;
}
