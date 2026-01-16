#include <iostream>
#include <algorithm>

enum class SortOrder {
    ASCENDING,   // по возрастанию
    DESCENDING   // по убыванию
};

void sortArray(int arr[], int size, SortOrder order) {
    if (order == SortOrder::ASCENDING) {
        std::sort(arr, arr + size);
    } else {
        std::sort(arr, arr + size, std::greater<int>());
    }
}

void printArray(int arr[], int size) {
    for(int i = 0; i < size; i++) {
        std::cout << arr[i] << " ";
    }
    std::cout << std::endl;
}

int main() {
    setlocale(LC_ALL, "ru");
    int numbers[] = {5, 2, 8, 1, 9, 3};
    int size = 6;
    
    std::cout << "Исходный массив: ";
    printArray(numbers, size);
    
    // Сортировка по возрастанию
    sortArray(numbers, size, SortOrder::ASCENDING);
    std::cout << "По возрастанию: ";
    printArray(numbers, size);
    
    // Сортировка по убыванию
    sortArray(numbers, size, SortOrder::DESCENDING);
    std::cout << "По убыванию: ";
    printArray(numbers, size);
    
    return 0;
}
