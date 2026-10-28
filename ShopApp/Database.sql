-- Скрипт создания БД «Интернет-магазин электроники» и тестовых данных (SQL Server / LocalDB)
IF DB_ID(N'ElectronicsShop') IS NULL CREATE DATABASE ElectronicsShop; -- создаём БД, если её ещё нет
GO
USE ElectronicsShop; -- переключаемся на созданную БД
GO

IF OBJECT_ID(N'OrderItems') IS NOT NULL DROP TABLE OrderItems; -- удаляем таблицы в порядке, обратном зависимостям (чтобы скрипт можно было запускать повторно)
IF OBJECT_ID(N'Orders') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID(N'Products') IS NOT NULL DROP TABLE Products;
IF OBJECT_ID(N'Customers') IS NOT NULL DROP TABLE Customers;
IF OBJECT_ID(N'Categories') IS NOT NULL DROP TABLE Categories;
GO

CREATE TABLE Categories ( -- категории товаров
    Id   INT IDENTITY(1,1) PRIMARY KEY, -- первичный ключ с автонумерацией
    Name NVARCHAR(100) NOT NULL         -- название категории
);

CREATE TABLE Products ( -- товары
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    Name       NVARCHAR(200) NOT NULL,
    CategoryId INT NOT NULL REFERENCES Categories(Id), -- внешний ключ на категорию
    Price      DECIMAL(18,2) NOT NULL CHECK (Price >= 0), -- цена не может быть отрицательной
    Stock      INT NOT NULL CHECK (Stock >= 0)            -- остаток не может быть отрицательным
);

CREATE TABLE Customers ( -- клиенты
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    FullName         NVARCHAR(150) NOT NULL,
    Email            NVARCHAR(150) NOT NULL,
    RegistrationDate DATETIME NOT NULL
);

CREATE TABLE Orders ( -- заказы
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId  INT NOT NULL REFERENCES Customers(Id), -- кто сделал заказ
    OrderDate   DATETIME NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL
);

CREATE TABLE OrderItems ( -- позиции заказов
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    OrderId   INT NOT NULL REFERENCES Orders(Id),   -- к какому заказу относится
    ProductId INT NOT NULL REFERENCES Products(Id), -- какой товар
    Quantity  INT NOT NULL CHECK (Quantity > 0)     -- сколько штук
);
GO

INSERT INTO Categories (Name) VALUES (N'Смартфоны'), (N'Ноутбуки'), (N'Наушники'), (N'Телевизоры'), (N'Аксессуары'); -- 5 категорий 

INSERT INTO Products (Name, CategoryId, Price, Stock) VALUES -- 10 товаров (Id 1..10)
(N'iPhone 15 128GB',        1, 79990.00,  20),
(N'Samsung Galaxy S24',     1, 69990.00,  15),
(N'Xiaomi Redmi Note 13',   1, 19990.00,  40),
(N'ASUS VivoBook 15',       2, 54990.00,   8),
(N'Lenovo IdeaPad 3',       2, 42990.00,  12),
(N'Sony WH-1000XM5',        3, 29990.00,  10),
(N'JBL Tune 510BT',         3,  4990.00,  50),
(N'LG OLED55 C3',           4, 99990.00,   5),
(N'Samsung 43" Crystal UHD',4, 32990.00,   7),
(N'Кабель USB-C 1 м',       5,   590.00, 200);

INSERT INTO Customers (FullName, Email, RegistrationDate) VALUES -- 8 клиентов, даты считаются от текущей (скрипт всегда «свежий»)
(N'Иванов Иван Иванович',      'ivanov@mail.ru',   DATEADD(DAY, -400, GETDATE())),
(N'Петрова Анна Сергеевна',    'petrova@mail.ru',  DATEADD(DAY, -300, GETDATE())),
(N'Сидоров Пётр Алексеевич',   'sidorov@mail.ru',  DATEADD(DAY, -200, GETDATE())), -- без заказов, давно -> «спящий»
(N'Кузнецова Мария Олеговна',  'kuznet@mail.ru',   DATEADD(DAY, -150, GETDATE())), -- без заказов, давно -> «спящий»
(N'Смирнов Дмитрий Игоревич',  'smirnov@mail.ru',  DATEADD(DAY, -100, GETDATE())),
(N'Попова Елена Викторовна',   'popova@mail.ru',   DATEADD(DAY,  -20, GETDATE())), -- без заказов, но зарегистрирована недавно -> не «спящий»
(N'Волков Андрей Павлович',    'volkov@mail.ru',   DATEADD(DAY,  -10, GETDATE())),
(N'Морозова Ольга Николаевна', 'morozova@mail.ru', DATEADD(DAY, -500, GETDATE())); -- без заказов, давно -> «спящий»

INSERT INTO Orders (CustomerId, OrderDate, TotalAmount) VALUES -- 8 заказов, сумма пока 0 (посчитаем ниже)
(1, DATEADD(DAY,  -5, GETDATE()), 0),
(2, DATEADD(DAY, -12, GETDATE()), 0),
(1, DATEADD(DAY, -20, GETDATE()), 0),
(5, DATEADD(DAY, -25, GETDATE()), 0),
(7, DATEADD(DAY,  -3, GETDATE()), 0),
(2, DATEADD(DAY, -45, GETDATE()), 0), -- старше 30 дней: не попадёт в Топ-3 задания 3
(5, DATEADD(DAY, -60, GETDATE()), 0),
(1, DATEADD(DAY, -90, GETDATE()), 0);

INSERT INTO OrderItems (OrderId, ProductId, Quantity) VALUES -- 14 позиций заказов
(1, 1, 1), (1, 7, 2),
(2, 3, 3), (2, 10, 5),
(3, 7, 4), (3, 6, 1),
(4, 3, 2), (4, 2, 1),
(5, 7, 3), (5, 10, 10),
(6, 1, 1),
(7, 4, 1),
(8, 5, 2), (8, 7, 1);

UPDATE o SET TotalAmount = ( -- пересчитываем сумму каждого заказа = Σ(количество × цена)
    SELECT SUM(oi.Quantity * p.Price)
    FROM OrderItems oi JOIN Products p ON p.Id = oi.ProductId
    WHERE oi.OrderId = o.Id)
FROM Orders o;
GO
