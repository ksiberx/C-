#include <iostream>
#include <string>
using namespace std;

struct VideoStore {
    string title;
    string director;
    string genre;
    double rating;
    double price;
};

void showFilm(VideoStore f) {
    cout << f.title << " | " << f.director << " | "
        << f.genre << " | " << f.rating << " | "
        << f.price << endl;
}

void addFilm(VideoStore films[], int& count) {
    cout << "\nДобавление фильма:\n";
    cout << "Название: ";
    getline(cin, films[count].title);
    cout << "Режиссер: ";
    getline(cin, films[count].director);
    cout << "Жанр: ";
    getline(cin, films[count].genre);
    cout << "Рейтинг: ";
    cin >> films[count].rating;
    cout << "Цена: ";
    cin >> films[count].price;
    count++;
    cin.ignore();
}

void showAll(VideoStore films[], int count) {
    if (count == 0) {
        cout << "Нет фильмов\n";
        return;
    }
    cout << "\nВсе фильмы (" << count << "):\n";
    for (int i = 0; i < count; i++) {
        cout << i + 1 << ". ";
        showFilm(films[i]);
    }
}

void searchTitle(VideoStore films[], int count) {
    string s;
    bool found = false;
    cout << "Поиск по названию: ";
    getline(cin, s);
    for (int i = 0; i < count; i++) {
        if (films[i].title.find(s) != string::npos) {
            showFilm(films[i]);
            found = true;
        }
    }
    if (!found) cout << "Не найдено\n";
}

void searchGenre(VideoStore films[], int count) {
    string s;
    bool found = false;
    cout << "Поиск по жанру: ";
    getline(cin, s);
    for (int i = 0; i < count; i++) {
        if (films[i].genre.find(s) != string::npos) {
            showFilm(films[i]);
            found = true;
        }
    }
    if (!found) cout << "Не найдено\n";
}

void searchDirector(VideoStore films[], int count) {
    string s;
    bool found = false;
    cout << "Поиск по режиссеру: ";
    getline(cin, s);
    for (int i = 0; i < count; i++) {
        if (films[i].director.find(s) != string::npos) {
            showFilm(films[i]);
            found = true;
        }
    }
    if (!found) cout << "Не найдено\n";
}

void bestInGenre(VideoStore films[], int count) {
    string s;
    int bestIndex = -1;
    cout << "Жанр для поиска лучшего: ";
    getline(cin, s);
    for (int i = 0; i < count; i++) {
        if (films[i].genre.find(s) != string::npos) {
            if (bestIndex == -1 || films[i].rating > films[bestIndex].rating) {
                bestIndex = i;
            }
        }
    }
    if (bestIndex != -1) {
        cout << "Самый популярный: ";
        showFilm(films[bestIndex]);
    }
    else {
        cout << "Не найдено\n";
    }
}

int main() {
    setlocale(LC_ALL, "ru");
    VideoStore films[50];
    int count = 0;
    int choice;

    films[count++] = { "Матрица", "Вачовски", "Фантастика", 9.5, 500 };
    films[count++] = { "Криминальное чтиво", "Тарантино", "Криминал", 9.8, 600 };
    films[count++] = { "Побег из Шоушенка", "Дарабонт", "Драма", 9.7, 550 };
    films[count++] = { "Звездные войны", "Лукас", "Фантастика", 9.3, 700 };
    films[count++] = { "Крестный отец", "Коппола", "Криминал", 9.9, 800 };

    do {
        cout << "\n--- ВИДЕОМАГАЗИН ---\n";
        cout << "1. Показать все\n";
        cout << "2. Добавить\n";
        cout << "3. Поиск по названию\n";
        cout << "4. Поиск по жанру\n";
        cout << "5. Поиск по режиссеру\n";
        cout << "6. Самый популярный в жанре\n";
        cout << "0. Выход\n";
        cout << "Выбор: ";
        cin >> choice;
        cin.ignore();

        switch (choice) {
        case 1: showAll(films, count); break;
        case 2: addFilm(films, count); break;
        case 3: searchTitle(films, count); break;
        case 4: searchGenre(films, count); break;
        case 5: searchDirector(films, count); break;
        case 6: bestInGenre(films, count); break;
        case 0: cout << "Выход\n"; break;
        default: cout << "Ошибка\n";
        }
    } while (choice != 0);

    return 0;
}
