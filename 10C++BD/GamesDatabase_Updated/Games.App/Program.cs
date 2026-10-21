using Games.Data;
using Games.Models;
using Microsoft.EntityFrameworkCore;

using var db = new GameContext();
db.Database.Migrate();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== БАЗА ДАННЫХ ИГР ===");
    Console.WriteLine("1. Показать все игры");
    Console.WriteLine("2. Поиск по названию игры");
    Console.WriteLine("3. Поиск по студии");
    Console.WriteLine("4. Поиск по студии и названию игры");
    Console.WriteLine("5. Поиск по жанру");
    Console.WriteLine("6. Поиск по году релиза");
    Console.WriteLine("7. Однопользовательские игры");
    Console.WriteLine("8. Многопользовательские игры");
    Console.WriteLine("9. Игра с максимальными продажами");
    Console.WriteLine("10. Игра с минимальными продажами");
    Console.WriteLine("11. Топ-3 самых продаваемых");
    Console.WriteLine("12. Топ-3 с наименьшими продажами");
    Console.WriteLine("13. Добавить игру");
    Console.WriteLine("14. Изменить игру");
    Console.WriteLine("15. Удалить игру");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");

    string choice = Console.ReadLine() ?? "";

    Console.Clear();
    switch (choice)
    {
        case "1":
            ShowGames(db.Games.AsNoTracking().ToList());
            break;
        case "2":
            Console.Write("Название игры: ");
            string name = ReadText();
            ShowGames(db.Games.AsNoTracking()
                .Where(g => g.Name.Contains(name)).ToList());
            break;
        case "3":
            Console.Write("Название студии: ");
            string studio = ReadText();
            ShowGames(db.Games.AsNoTracking()
                .Where(g => g.Studio.Contains(studio)).ToList());
            break;
        case "4":
            Console.Write("Название студии: ");
            string studioName = ReadText();
            Console.Write("Название игры: ");
            string gameName = ReadText();
            ShowGames(db.Games.AsNoTracking()
                .Where(g => g.Studio.Contains(studioName) &&
                            g.Name.Contains(gameName)).ToList());
            break;
        case "5":
            Console.Write("Жанр: ");
            string genre = ReadText();
            ShowGames(db.Games.AsNoTracking()
                .Where(g => g.Genre.Contains(genre)).ToList());
            break;
        case "6":
            Console.Write("Год релиза: ");
            if (int.TryParse(Console.ReadLine(), out int year))
                ShowGames(db.Games.AsNoTracking()
                    .Where(g => g.ReleaseDate.Year == year).ToList());
            else
                Console.WriteLine("Некорректный год.");
            break;
        case "7":
            ShowGames(db.Games.AsNoTracking()
                .Where(g => g.Mode.Contains("Однопользовательский")).ToList());
            break;
        case "8":
            ShowGames(db.Games.AsNoTracking()
                .Where(g => g.Mode.Contains("Многопользовательский")).ToList());
            break;
        case "9":
        {
            Game? game = db.Games.AsNoTracking()
                .OrderByDescending(g => g.SoldCopies).FirstOrDefault();
            ShowOne(game);
            break;
        }
        case "10":
        {
            Game? game = db.Games.AsNoTracking()
                .OrderBy(g => g.SoldCopies).FirstOrDefault();
            ShowOne(game);
            break;
        }
        case "11":
            ShowGames(db.Games.AsNoTracking()
                .OrderByDescending(g => g.SoldCopies).Take(3).ToList());
            break;
        case "12":
            ShowGames(db.Games.AsNoTracking()
                .OrderBy(g => g.SoldCopies).Take(3).ToList());
            break;
        case "13":
            AddGame(db);
            break;
        case "14":
            EditGame(db);
            break;
        case "15":
            DeleteGame(db);
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Такого пункта нет.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Нажмите любую клавишу для продолжения...");
    Console.ReadKey();
}

static string ReadText()
{
    return (Console.ReadLine() ?? "").Trim();
}

static void ShowGames(List<Game> games)
{
    if (games.Count == 0)
    {
        Console.WriteLine("Игры не найдены.");
        return;
    }

    foreach (Game game in games)
        PrintGame(game);
}

static void ShowOne(Game? game)
{
    if (game == null)
        Console.WriteLine("В базе пока нет игр.");
    else
        PrintGame(game);
}

static void PrintGame(Game game)
{
    Console.WriteLine($"ID: {game.Id}");
    Console.WriteLine($"Название: {game.Name}");
    Console.WriteLine($"Студия: {game.Studio}");
    Console.WriteLine($"Жанр: {game.Genre}");
    Console.WriteLine($"Дата выхода: {game.ReleaseDate:dd.MM.yyyy}");
    Console.WriteLine($"Режим: {game.Mode}");
    Console.WriteLine($"Продано копий: {game.SoldCopies:N0}");
    Console.WriteLine(new string('-', 35));
}

static void AddGame(GameContext db)
{
    Console.Write("Название игры: ");
    string name = ReadText();
    Console.Write("Студия: ");
    string studio = ReadText();

    bool exists = db.Games.Any(g =>
        g.Name.ToLower() == name.ToLower() &&
        g.Studio.ToLower() == studio.ToLower());

    if (exists)
    {
        Console.WriteLine("Такая игра этой студии уже есть.");
        return;
    }

    Console.Write("Жанр: ");
    string genre = ReadText();
    Console.Write("Дата выхода (гггг-мм-дд): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime releaseDate))
    {
        Console.WriteLine("Некорректная дата.");
        return;
    }

    Console.Write("Режим (Однопользовательский / Многопользовательский / Однопользовательский и многопользовательский): ");
    string mode = ReadText();
    Console.Write("Количество проданных копий: ");
    if (!int.TryParse(Console.ReadLine(), out int soldCopies) || soldCopies < 0)
    {
        Console.WriteLine("Количество должно быть неотрицательным числом.");
        return;
    }

    db.Games.Add(new Game
    {
        Name = name,
        Studio = studio,
        Genre = genre,
        ReleaseDate = releaseDate,
        Mode = mode,
        SoldCopies = soldCopies
    });
    db.SaveChanges();
    Console.WriteLine("Игра добавлена.");
}

static Game? FindGame(GameContext db)
{
    Console.Write("Название игры: ");
    string name = ReadText();
    Console.Write("Студия: ");
    string studio = ReadText();

    return db.Games.FirstOrDefault(g =>
        g.Name.ToLower() == name.ToLower() &&
        g.Studio.ToLower() == studio.ToLower());
}

static void EditGame(GameContext db)
{
    Game? game = FindGame(db);
    if (game == null)
    {
        Console.WriteLine("Игра не найдена.");
        return;
    }

    Console.WriteLine("Оставьте поле пустым, если не хотите менять значение.");
    Console.Write($"Название [{game.Name}]: ");
    string value = ReadText();
    if (value != "") game.Name = value;

    Console.Write($"Студия [{game.Studio}]: ");
    value = ReadText();
    if (value != "") game.Studio = value;

    Console.Write($"Жанр [{game.Genre}]: ");
    value = ReadText();
    if (value != "") game.Genre = value;

    Console.Write($"Дата выхода [{game.ReleaseDate:yyyy-MM-dd}]: ");
    value = ReadText();
    if (value != "")
    {
        if (DateTime.TryParse(value, out DateTime date))
            game.ReleaseDate = date;
        else
        {
            Console.WriteLine("Некорректная дата. Изменения отменены.");
            return;
        }
    }

    Console.Write($"Режим [{game.Mode}]: ");
    value = ReadText();
    if (value != "") game.Mode = value;

    Console.Write($"Продано копий [{game.SoldCopies}]: ");
    value = ReadText();
    if (value != "")
    {
        if (int.TryParse(value, out int sold) && sold >= 0)
            game.SoldCopies = sold;
        else
        {
            Console.WriteLine("Некорректное количество. Изменения отменены.");
            return;
        }
    }

    db.SaveChanges();
    Console.WriteLine("Данные игры изменены.");
}

static void DeleteGame(GameContext db)
{
    Game? game = FindGame(db);
    if (game == null)
    {
        Console.WriteLine("Игра не найдена.");
        return;
    }

    PrintGame(game);
    Console.Write("Точно удалить игру? (да/нет): ");
    string answer = ReadText();

    if (answer.Equals("да", StringComparison.OrdinalIgnoreCase))
    {
        db.Games.Remove(game);
        db.SaveChanges();
        Console.WriteLine("Игра удалена.");
    }
    else
    {
        Console.WriteLine("Удаление отменено.");
    }
}
