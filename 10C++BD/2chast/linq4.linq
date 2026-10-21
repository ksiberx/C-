
-- Топ-3 стран по населению
CREATE OR ALTER PROCEDURE Top3Countries
AS
BEGIN
    SELECT TOP 3 Name, Population
    FROM Countries
    ORDER BY Population DESC;
END;
GO

-- Топ-3 столиц по населению
CREATE OR ALTER PROCEDURE Top3Capitals
AS
BEGIN
    SELECT TOP 3 Name, Population
    FROM Capitals
    ORDER BY Population DESC;
END;
GO

-- Самая населённая страна
CREATE OR ALTER PROCEDURE BiggestCountry
AS
BEGIN
    SELECT TOP 1 Name, Population
    FROM Countries
    ORDER BY Population DESC;
END;
GO

-- Самый населённый город
CREATE OR ALTER PROCEDURE BiggestCity
AS
BEGIN
    SELECT TOP 1 Name, Population
    FROM Cities
    ORDER BY Population DESC;
END;
GO
