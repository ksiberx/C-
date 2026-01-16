#include <iostream>
#include <chrono>
#include <ctime>

enum class LogLevel {
    INFO,
    WARNING,
    ERROR
};

void logMessage(LogLevel level, const std::string& message) {
    // Получаем текущее время
    auto now = std::chrono::system_clock::now();
    std::time_t time = std::chrono::system_clock::to_time_t(now);
    
    std::cout << std::ctime(&time);
    
    switch(level) {
        case LogLevel::INFO:
            std::cout << "[INFO] ";
            break;
        case LogLevel::WARNING:
            std::cout << "[WARNING] ";
            break;
        case LogLevel::ERROR:
            std::cout << "[ERROR] ";
            break;
    }
    
    std::cout << message << std::endl;
}

int main() {
    setlocale(LC_ALL, "ru");
    logMessage(LogLevel::INFO, "Приложение запущено");
    logMessage(LogLevel::WARNING, "Мало свободной памяти");
    logMessage(LogLevel::ERROR, "Файл не найден");
    
    return 0;
}
