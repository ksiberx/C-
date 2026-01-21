class LibraryItem:
    def __init__(self, title, author, year):
        self._title = title
        self._author = author
        self._year = year
        self.__item_id = id(self)
        self._is_checked_out = False

    @property
    def title(self): return self._title

    @property
    def author(self): return self._author

    @property
    def year(self): return self._year

    @property
    def item_id(self): return self.__item_id

    @property
    def is_checked_out(self): return self._is_checked_out

    @is_checked_out.setter
    def is_checked_out(self, value):
        if not isinstance(value, bool):
            raise ValueError("Только True/False")
        self._is_checked_out = value


class Book(LibraryItem):
    def __init__(self, title, author, year, genre, page_count):
        super().__init__(title, author, year)
        self.genre = genre
        self.__page_count = page_count

    @property
    def page_count(self): return self.__page_count

    @page_count.setter
    def page_count(self, value):
        if value < 1: raise ValueError("Не менее 1 страницы")
        self.__page_count = value


class Magazine(LibraryItem):
    def __init__(self, title, author, year, issue_number):
        super().__init__(title, author, year)
        self._issue_number = issue_number


class DVD(LibraryItem):
    def __init__(self, title, author, year, duration, rating):
        super().__init__(title, author, year)
        self.__duration = duration
        self.__rating = rating

    @property
    def duration(self):
        return self.__duration

    @property
    def rating(self):
        return self.__rating

    @duration.setter
    def duration(self, value):
        if not 1 <= value <= 300:
            raise ValueError("От 1 до 300 минут")
        self.__duration = value

    @rating.setter
    def rating(self, value):
        valid = ["G", "PG", "PG-13", "R", "NC-17"]
        if value not in valid:
            raise ValueError(f"Рейтинг: {', '.join(valid)}")
        self.__rating = value


class Library:
    def __init__(self):
        self.__items = []

    def add_item(self, item): self.__items.append(item)

    def remove_item(self, item_id):
        self.__items = [i for i in self.__items if i.item_id != item_id]

    def search(self, term):
        term = term.lower()
        return [i for i in self.__items
                if term in i.title.lower() or term in i.author.lower()]

    def statistics(self):
        books = sum(isinstance(i, Book) for i in self.__items)
        mags = sum(isinstance(i, Magazine) for i in self.__items)
        dvds = sum(isinstance(i, DVD) for i in self.__items)
        return {"books": books, "magazines": mags, "dvds": dvds}

    def available_items(self):
        return [i for i in self.__items if not i.is_checked_out]
    
    
