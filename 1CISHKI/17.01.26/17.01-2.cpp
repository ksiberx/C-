#include <iostream>
#include <string>

enum class Day {
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY
};

std::string getDayName(Day day) {
    switch(day) {
        case Day::MONDAY: return "Понедельник";
        case Day::TUESDAY: return "Вторник";
        case Day::WEDNESDAY: return "Среда";
        case Day::THURSDAY: return "Четверг";
        case Day::FRIDAY: return "Пятница";
        case Day::SATURDAY: return "Суббота";
        case Day::SUNDAY: return "Воскресенье";
        default: return "Неизвестный день";
    }
}

bool isWeekend(Day day) {
    return (day == Day::SATURDAY || day == Day::SUNDAY);
}

int main() {
    setlocale(LC_ALL, "ru");
    Day today = Day::SATURDAY;
    
    std::cout << "Сегодня: " << getDayName(today) << std::endl;
    std::cout << "Это выходной? " << (isWeekend(today) ? "Да" : "Нет") << std::endl;
    
    return 0;
}
