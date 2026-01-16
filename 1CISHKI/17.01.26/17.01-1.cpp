#include <iostream>
#include <string>

enum class ErrorCode {
    SUCCESS,
    FILE_NOT_FOUND,
    ACCESS_DENIED,
    OUT_OF_MEMORY
};

ErrorCode checkFileOperation(bool fileExists, bool hasAccess, bool hasMemory) {
    if (!fileExists) return ErrorCode::FILE_NOT_FOUND;
    if (!hasAccess) return ErrorCode::ACCESS_DENIED;
    if (!hasMemory) return ErrorCode::OUT_OF_MEMORY;
    return ErrorCode::SUCCESS;
}

int main() {
    setlocale(LC_ALL, "ru");
    ErrorCode result = checkFileOperation(true, true, true);
    
    if (result == ErrorCode::SUCCESS) {
        std::cout << "Операция выполнена успешно!" << std::endl;
    }
    
    return 0;
}
