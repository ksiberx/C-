using Games.Data;
using Games.Models;
using Microsoft.EntityFrameworkCore;

using var db = new GameContext();

// Создаём базу и применяем миграции
db.Database.Migrate();

// Добавляем примеры, если таблица пока пустая
if (!db.Games.Any())
{
    db.Games.AddRange(
        new Game
        {
            Name = "The Witcher 3",
            Studio = "CD Projekt Red",
            Genre = "RPG",
            ReleaseDate = new DateTime(2015, 5, 19),
            Mode = "Однопользовательский",
            SoldCopies = 50000000
        },
        new Game
        {
            Name = "Minecraft",
            Studio = "Mojang",
            Genre = "Песочница",
            ReleaseDate = new DateTime(2011, 11, 18),
            Mode = "Однопользовательский и многопользовательский",
            SoldCopies = 300000000
        },
        new Game
        {
            Name = "Counter-Strike 2",
            Studio = "Valve",
            Genre = "Шутер",
            ReleaseDate = new DateTime(2023, 9, 27),
            Mode = "Многопользовательский",
            SoldCopies = 0
        }
    );

    db.SaveChanges();
}

// Выводим данные из базы
foreach (Game game in db.Games.AsNoTracking())
{
    Console.WriteLine($"Название: {game.Name}");
    Console.WriteLine($"Студия: {game.Studio}");
    Console.WriteLine($"Жанр: {game.Genre}");
    Console.WriteLine($"Дата выхода: {game.ReleaseDate:dd.MM.yyyy}");
    Console.WriteLine($"Режим: {game.Mode}");
    Console.WriteLine($"Продано копий: {game.SoldCopies}");
    Console.WriteLine(new string('-', 35));
}

Console.WriteLine("Нажмите любую клавишу для выхода...");
Console.ReadKey();
