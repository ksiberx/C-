"""
Часть 1: Создание классов
"""

class Student:
    """
    Класс, представляющий студента университета
    """
    
    def __init__(self, name: str, age: int, course: int, gpa: float, student_id: str):
        """
        Инициализация объекта Student с проверкой корректности данных
        
        Args:
            name: Имя студента
            age: Возраст студента
            course: Курс обучения (1-5)
            gpa: Средний балл (0-5)
            student_id: Номер студенческого билета
        """
        self.name = name
        self.age = age
        
        # Проверка корректности курса
        if 1 <= course <= 5:
            self.course = course
        else:
            raise ValueError("Курс должен быть от 1 до 5")
        
        # Проверка корректности GPA
        if 0 <= gpa <= 5:
            self.gpa = gpa
        else:
            raise ValueError("Средний балл должен быть от 0 до 5")
        
        self.student_id = student_id
    
    def increase_course(self) -> None:
        """
        Увеличивает курс студента на 1, но не более 5
        """
        if self.course < 5:
            self.course += 1
            print(f"{self.name} переведен на {self.course} курс")
        else:
            print(f"{self.name} уже на максимальном курсе ({self.course})")
    
    def update_gpa(self, new_gpa: float) -> None:
        """
        Обновляет средний балл студента с проверкой корректности
        
        Args:
            new_gpa: Новый средний балл (должен быть от 0 до 5)
        """
        if 0 <= new_gpa <= 5:
            self.gpa = new_gpa
            print(f"Средний балл {self.name} обновлен: {self.gpa}")
        else:
            print(f"Ошибка: средний балл {new_gpa} некорректен. Должен быть от 0 до 5")
    
    def display_info(self) -> None:
        """
        Выводит информацию о студенте в читаемом формате
        """
        print(f"Студент: {self.name}")
        print(f"  Возраст: {self.age}")
        print(f"  Курс: {self.course}")
        print(f"  Средний балл: {self.gpa}")
        print(f"  ID: {self.student_id}")
        print("-" * 30)


class Book:
    """
    Класс, представляющий книгу в библиотеке
    """
    
    def __init__(self, title: str, author: str, year: int, isbn: str, pages: int):
        """
        Инициализация объекта Book
        
        Args:
            title: Название книги
            author: Автор книги
            year: Год издания
            isbn: ISBN номер
            pages: Количество страниц
        """
        self.title = title
        self.author = author
        self.year = year
        self.isbn = isbn
        self.pages = pages
        self.is_borrowed = False  # Атрибут для отслеживания статуса книги
    
    def borrow_book(self) -> None:
        """
        Отмечает книгу как взятую (меняет статус доступности)
        """
        if not self.is_borrowed:
            self.is_borrowed = True
            print(f'Книга "{self.title}" успешно взята')
        else:
            print(f'Книга "{self.title}" уже взята, невозможно взять повторно')
    
    def return_book(self) -> None:
        """
        Отмечает книгу как возвращенную (меняет статус доступности)
        """
        if self.is_borrowed:
            self.is_borrowed = False
            print(f'Книга "{self.title}" успешно возвращена')
        else:
            print(f'Книга "{self.title}" уже доступна, не была взята')
    
    def display_info(self) -> None:
        """
        Выводит информацию о книге в читаемом формате
        """
        status = "Взята" if self.is_borrowed else "Доступна"
        print(f'Книга: "{self.title}"')
        print(f"  Автор: {self.author}")
        print(f"  Год издания: {self.year}")
        print(f"  ISBN: {self.isbn}")
        print(f"  Страниц: {self.pages}")
        print(f"  Статус: {status}")
        print("-" * 30)


"""
Часть 2: Создание объектов
"""

print("=" * 50)
print("СОЗДАНИЕ ОБЪЕКТОВ СТУДЕНТОВ")
print("=" * 50)

# Создание объектов Student
student1 = Student("Иван Петров", 19, 1, 4.2, "ST001")
student2 = Student("Мария Сидорова", 20, 2, 4.7, "ST002")

student1.display_info()
student2.display_info()

print("\n" + "=" * 50)
print("СОЗДАНИЕ ОБЪЕКТОВ КНИГ")
print("=" * 50)

# Создание объектов Book
book1 = Book("Преступление и наказание", "Фёдор Достоевский", 1866, "978-5-17-090879-1", 608)
book2 = Book("Мастер и Маргарита", "Михаил Булгаков", 1967, "978-5-389-06204-3", 480)

book1.display_info()
book2.display_info()


"""
Часть 3: Тестирование методов
"""

print("\n" + "=" * 50)
print("ТЕСТИРОВАНИЕ МЕТОДОВ СТУДЕНТОВ")
print("=" * 50)

# Тестирование методов Student
print("1. Увеличение курса student1:")
student1.increase_course()

print("\n2. Обновление среднего балла student1 на 4.5:")
student1.update_gpa(4.5)

print("\n3. Обновление среднего балла student2 на 4.9:")
student2.update_gpa(4.9)

print("\n4. Попытка установить некорректный GPA (6.0):")
student1.update_gpa(6.0)

print("\nИнформация после изменений:")
student1.display_info()
student2.display_info()

print("\n" + "=" * 50)
print("ТЕСТИРОВАНИЕ МЕТОДОВ КНИГ")
print("=" * 50)

# Тестирование методов Book
print("1. Взятие book1:")
book1.borrow_book()

print("\n2. Возврат book1:")
book1.return_book()

print("\n3. Двойное взятие book2:")
book2.borrow_book()
book2.borrow_book()  # Попытка взять уже взятую книгу

print("\n4. Возврат book2:")
book2.return_book()

print("\nИнформация о книгах после операций:")
book1.display_info()
book2.display_info()


"""
Часть 4: Дополнительный класс
"""

print("\n" + "=" * 50)
print("ДОПОЛНИТЕЛЬНЫЙ КЛАСС: CAR (АВТОМОБИЛЬ)")
print("=" * 50)

class Car:
    """
    Класс, представляющий автомобиль
    """
    
    def __init__(self, brand: str, model: str, year: int, vin: str, price: float):
        """
        Инициализация объекта Car
        
        Args:
            brand: Марка автомобиля
            model: Модель автомобиля
            year: Год выпуска
            vin: VIN номер
            price: Цена автомобиля
        """
        self.brand = brand
        self.model = model
        
        # Проверка корректности года
        if year > 1885:  # Первый автомобиль был создан в 1885
            self.year = year
        else:
            raise ValueError("Год выпуска некорректен")
        
        self.vin = vin
        
        # Проверка корректности цены
        if price >= 0:
            self.price = price
        else:
            raise ValueError("Цена не может быть отрицательной")
        
        self.is_sold = False  # Статус продажи
    
    def sell_car(self) -> None:
        """
        Отмечает автомобиль как проданный
        """
        if not self.is_sold:
            self.is_sold = True
            print(f'Автомобиль {self.brand} {self.model} продан')
        else:
            print(f'Автомобиль {self.brand} {self.model} уже продан')
    
    def apply_discount(self, discount_percent: float) -> None:
        """
        Применяет скидку к цене автомобиля
        
        Args:
            discount_percent: Процент скидки (от 0 до 100)
        """
        if 0 <= discount_percent <= 100:
            discount = self.price * discount_percent / 100
            self.price -= discount
            print(f'Применена скидка {discount_percent}% к {self.brand} {self.model}')
            print(f'Новая цена: {self.price:.2f}')
        else:
            print(f'Ошибка: скидка {discount_percent}% некорректна')
    
    def display_info(self) -> None:
        """
        Выводит информацию об автомобиле
        """
        status = "Продан" if self.is_sold else "В продаже"
        print(f"Автомобиль: {self.brand} {self.model}")
        print(f"  Год выпуска: {self.year}")
        print(f"  VIN: {self.vin}")
        print(f"  Цена: {self.price:.2f}")
        print(f"  Статус: {status}")
        print("-" * 30)

# Создание объектов Car
car1 = Car("Toyota", "Camry", 2022, "JTDKBRFU9J1234567", 25000.00)
car2 = Car("BMW", "X5", 2023, "WBXFU9J1234567890", 65000.00)

print("Созданные автомобили:")
car1.display_info()
car2.display_info()

# Тестирование методов Car
print("\nТестирование методов Car:")
print("1. Продажа car1:")
car1.sell_car()

print("\n2. Применение скидки 10% к car2:")
car2.apply_discount(10)

print("\n3. Попытка повторной продажи car1:")
car1.sell_car()

print("\n4. Попытка применить некорректную скидку (150%):")
car2.apply_discount(150)

print("\nИнформация об автомобилях после операций:")
car1.display_info()
car2.display_info()
