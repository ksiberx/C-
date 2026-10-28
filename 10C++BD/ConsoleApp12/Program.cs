using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpanishChampionship;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var options = new DbContextOptionsBuilder<ChampionshipContext>()
    .UseSqlServer(configuration.GetConnectionString("Default"))
    .Options;

await using var db = new ChampionshipContext(options);
await db.Database.EnsureCreatedAsync();

if (!await db.Teams.AnyAsync())
{
    var real = new Team { Name = "Real Madrid" };
    var barca = new Team { Name = "Barcelona" };
    var atletico = new Team { Name = "Atletico Madrid" };
    db.Teams.AddRange(real, barca, atletico);
    await db.SaveChangesAsync();

    db.Players.AddRange(
        new Player { Name = "Игрок Реала 1", TeamId = real.Id },
        new Player { Name = "Игрок Реала 2", TeamId = real.Id },
        new Player { Name = "Игрок Барселоны 1", TeamId = barca.Id },
        new Player { Name = "Игрок Атлетико 1", TeamId = atletico.Id });
    db.Matches.AddRange(
        new Match { HomeTeamId = real.Id, AwayTeamId = barca.Id, MatchDate = DateTime.Today.AddDays(-7) },
        new Match { HomeTeamId = atletico.Id, AwayTeamId = real.Id, MatchDate = DateTime.Today.AddDays(-3) },
        new Match { HomeTeamId = barca.Id, AwayTeamId = atletico.Id, MatchDate = DateTime.Today });
    await db.SaveChangesAsync();
}

var service = new ChampionshipService(db);
Console.WriteLine("ЧЕМПИОНАТ ИСПАНИИ");
Console.WriteLine("1 — случайные результаты матчей");
Console.WriteLine("2 — топ-3 бомбардиров чемпионата");
Console.WriteLine("3 — топ-3 команд по забитым голам");
Console.WriteLine("4 — топ-3 команд по очкам");
Console.Write("Выберите действие: ");
var choice = Console.ReadLine();

try
{
    if (choice == "1")
    {
        await service.FillMatchesRandomlyAsync();
        Console.WriteLine("Результаты матчей заполнены.");
    }
    else if (choice == "2")
    {
        var scorers = await service.TopScorersAsync();
        foreach (var s in scorers) Console.WriteLine($"{s.Player} ({s.Team}) — {s.Goals} гол.");
    }
    else if (choice == "3")
    {
        var teams = await service.TopTeamsByGoalsAsync();
        foreach (var t in teams) Console.WriteLine($"{t.Team}: забито {t.GoalsFor}, пропущено {t.GoalsAgainst}");
    }
    else if (choice == "4")
    {
        var teams = await service.TopTeamsByPointsAsync();
        foreach (var t in teams) Console.WriteLine($"{t.Team}: {t.Points} очк.");
    }
    else Console.WriteLine("Нет такого пункта меню.");
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка: " + ex.Message);
    Console.WriteLine("Проверь, что SQL-скрипты выполнены и строка подключения правильная.");
}
