
-- Изменение страны
UPDATE Countries
SET Population = 147000000
WHERE Id = 1;

-- Изменение столицы
UPDATE Capitals
SET Population = 13100000
WHERE CountryId = 1;

-- Изменение города
UPDATE Cities
SET Population = 5700000
WHERE Name = N'Санкт-Петербург'
  AND CountryId = 1;
