
-- Добавление страны
INSERT INTO Countries (Name, Population, Area, Continent)
VALUES (N'Россия', 146000000, 17098246, N'Европа');

-- Добавление столицы
INSERT INTO Capitals (CountryId, Name, Population)
VALUES (1, N'Москва', 13000000);

-- Добавление крупного города
INSERT INTO Cities (CountryId, Name, Population)
VALUES (1, N'Санкт-Петербург', 5600000);

-- Добавление ещё одного города
INSERT INTO Cities (CountryId, Name, Population)
VALUES (1, N'Новосибирск', 1600000);
