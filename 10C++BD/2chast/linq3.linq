
-- Удаление города
DELETE FROM Cities
WHERE Name = N'Новосибирск'
  AND CountryId = 1;

-- Удаление столицы
DELETE FROM Capitals
WHERE CountryId = 1;

-- Удаление страны вместе с оставшимися городами
DELETE FROM Countries
WHERE Id = 1;
