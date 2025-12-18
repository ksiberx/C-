#include <iostream>
#include <algorithm>
using namespace std;

// Задание 1: Реверс строки
string reverseWithAlgorithm(string str) {
    reverse(str.begin(), str.end());
    return str;
}

string reverseWithLoop(const string& str) {
    string result;
    for (int i = str.length() - 1; i >= 0; --i) {
        result += str[i];
    }
    return result;
}

// Задание 2: Подсчет символов
struct CharCount {
    int letters = 0;
    int digits = 0;
    int spaces = 0;
    int others = 0;
};

CharCount countCharacters(const string& str) {
    CharCount count;

    for (char c : str) {
        if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ||
            (c >= 'А' && c <= 'я')) {
            count.letters++;
        }
        else if (c >= '0' && c <= '9') {
            count.digits++;
        }
        else if (c == ' ' || c == '\t' || c == '\n') {
            count.spaces++;
        }
        else {
            count.others++;
        }
    }

    return count;
}

// Задание 3: Удаление дубликатов
string removeDuplicates(const string& str) {
    string result;
    bool seen[256] = { false };

    for (char c : str) {
        if (!seen[static_cast<unsigned char>(c)]) {
            result += c;
            seen[static_cast<unsigned char>(c)] = true;
        }
    }

    return result;
}

// Задание 4: Проверка префикса и суффикса
bool startswith(const string& str, const string& prefix) {
    if (prefix.length() > str.length()) return false;

    for (size_t i = 0; i < prefix.length(); ++i) {
        if (str[i] != prefix[i]) return false;
    }
    return true;
}

bool endswith(const string& str, const string& suffix) {
    if (suffix.length() > str.length()) return false;

    size_t strPos = str.length() - suffix.length();
    for (size_t i = 0; i < suffix.length(); ++i) {
        if (str[strPos + i] != suffix[i]) return false;
    }
    return true;
}

// Задание 5: Форматирование имени
string formatName(const string& fullName) {
    string result;
    string word;
    string words[10];
    int wordCount = 0;

    // Разделение на слова
    for (char c : fullName) {
        if (c == ' ') {
            if (!word.empty()) {
                words[wordCount++] = word;
                word.clear();
            }
        }
        else {
            word += c;
        }
    }
    if (!word.empty()) {
        words[wordCount++] = word;
    }

    // Форматирование
    if (wordCount == 0) return "";

    result = words[0]; // Фамилия

    if (wordCount > 1) {
        result += " " + string(1, words[1][0]) + "."; // Инициал имени

        if (wordCount > 2) {
            result += string(1, words[2][0]) + "."; // Инициал отчества
        }
    }

    return result;
}

int main() {
    setlocale(LC_ALL, "ru");
    cout << "ТЕСТ ЗАДАНИЙ\n\n";

    // Тест задания 1
    string test1 = "Hello World!";
    cout << "Задание 1:\n";
    cout << "Исходная: " << test1 << "\n";
    cout << "С алгоритмом: " << reverseWithAlgorithm(test1) << "\n";
    cout << "С циклом: " << reverseWithLoop(test1) << "\n\n";

    // Тест задания 2
    string test2 = "Hello World 123!";
    CharCount counts = countCharacters(test2);
    cout << "Задание 2:\n";
    cout << "Строка: " << test2 << "\n";
    cout << "Букв: " << counts.letters << "\n";
    cout << "Цифр: " << counts.digits << "\n";
    cout << "Пробелов: " << counts.spaces << "\n";
    cout << "Других: " << counts.others << "\n\n";

    // Тест задания 3
    string test3 = "programming";
    cout << "Задание 3:\n";
    cout << "Исходная: " << test3 << "\n";
    cout << "Без дубликатов: " << removeDuplicates(test3) << "\n\n";

    // Тест задания 4
    string test4 = "document.pdf";
    cout << "Задание 4:\n";
    cout << "Строка: " << test4 << "\n";
    cout << "startswith('doc'): " << (startswith(test4, "doc") ? "true" : "false") << "\n";
    cout << "endswith('pdf'): " << (endswith(test4, "pdf") ? "true" : "false") << "\n";
    cout << "endswith('txt'): " << (endswith(test4, "txt") ? "true" : "false") << "\n\n";

    // Тест задания 5
    cout << "Задание 5:\n";
    cout << "Иванов Иван Петрович -> " << formatName("Иванов Иван Петрович") << "\n";
    cout << "Петров Алексей -> " << formatName("Петров Алексей") << "\n";
    cout << "Сидоров -> " << formatName("Сидоров") << "\n";

    return 0;
}
