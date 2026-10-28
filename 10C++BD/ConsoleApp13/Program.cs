using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
string connectionString = configuration["ConnectionString"] ?? throw new Exception("Не задана строка подключения.");

while (true)
{
    Console.Clear();
    Console.WriteLine("=== ЧЕМПИОНАТ ИСПАНИИ ПО ФУТБОЛУ ===");
    Console.WriteLine("1. Разница забитых и пропущенных голов");
    Console.WriteLine("2. Полная информация о матче");
    Console.WriteLine("3. Матчи за конкретную дату");
    Console.WriteLine("4. Все матчи команды");
    Console.WriteLine("5. Авторы голов за конкретную дату");
    Console.WriteLine("6. Добавить матч");
    Console.WriteLine("7. Изменить матч");
    Console.WriteLine("8. Удалить матч");
    Console.WriteLine("9. Показать команды и игроков");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");
    string? choice = Console.ReadLine();
    try
    {
        switch (choice)
        {
            case "1": ShowTeamStats(); break;
            case "2": ShowMatchDetails(); break;
            case "3": ShowMatchesByDate(); break;
            case "4": ShowTeamMatches(); break;
            case "5": ShowScorersByDate(); break;
            case "6": AddMatch(); break;
            case "7": EditMatch(); break;
            case "8": DeleteMatch(); break;
            case "9": ShowTeamsAndPlayers(); break;
            case "0": return;
            default: Console.WriteLine("Нет такого пункта меню."); break;
        }
    }
    catch (SqlException ex) { Console.WriteLine("Ошибка базы данных: " + ex.Message); }
    catch (Exception ex) { Console.WriteLine("Ошибка: " + ex.Message); }
    Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
    Console.ReadLine();
}

SqlConnection OpenConnection()
{
    var connection = new SqlConnection(connectionString);
    connection.Open();
    return connection;
}

void ShowTeamStats()
{
    using var c = OpenConnection();
    using var cmd = new SqlCommand("SELECT TeamName, GoalsScored, GoalsConceded, GoalDifference FROM dbo.TeamGoalDifference ORDER BY GoalDifference DESC, TeamName", c);
    using var r = cmd.ExecuteReader();
    Console.WriteLine("\nКоманда | Забито | Пропущено | Разница");
    while (r.Read()) Console.WriteLine($"{r[0]} | {r[1]} | {r[2]} | {r[3]}");
}

void ShowMatchDetails()
{
    int id = ReadInt("ID матча: ");
    using var c = OpenConnection();
    using var cmd = new SqlCommand(@"SELECT m.MatchId,t1.TeamName,t2.TeamName,m.GoalsTeam1,m.GoalsTeam2,m.MatchDate
        FROM dbo.Matches m JOIN dbo.Teams t1 ON t1.TeamId=m.Team1Id JOIN dbo.Teams t2 ON t2.TeamId=m.Team2Id WHERE m.MatchId=@id", c);
    cmd.Parameters.AddWithValue("@id", id);
    using var r = cmd.ExecuteReader();
    if (!r.Read()) { Console.WriteLine("Матч не найден."); return; }
    Console.WriteLine($"Матч №{r[0]}: {r[1]} {r[3]} : {r[4]} {r[2]}, дата: {((DateTime)r[5]):dd.MM.yyyy}");
    r.Close();
    using var goals = new SqlCommand(@"SELECT p.FullName, t.TeamName, g.GoalMinute FROM dbo.Goals g
        JOIN dbo.Players p ON p.PlayerId=g.PlayerId JOIN dbo.Teams t ON t.TeamId=p.TeamId
        WHERE g.MatchId=@id ORDER BY g.GoalMinute", c);
    goals.Parameters.AddWithValue("@id", id);
    using var gr = goals.ExecuteReader();
    Console.WriteLine("Авторы голов:");
    bool any = false;
    while (gr.Read()) { any = true; Console.WriteLine($"- {gr[0]} ({gr[1]}), { (gr.IsDBNull(2) ? "минута не указана" : gr[2] + " мин.")}"); }
    if (!any) Console.WriteLine("Авторы голов пока не внесены.");
}

void ShowMatchesByDate()
{
    DateTime date = ReadDate("Дата (дд.мм.гггг): ");
    using var c = OpenConnection();
    using var cmd = new SqlCommand(@"SELECT m.MatchId,t1.TeamName,t2.TeamName,m.GoalsTeam1,m.GoalsTeam2,m.MatchDate FROM dbo.Matches m
        JOIN dbo.Teams t1 ON t1.TeamId=m.Team1Id JOIN dbo.Teams t2 ON t2.TeamId=m.Team2Id WHERE m.MatchDate=@date ORDER BY m.MatchId", c);
    cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
    PrintMatches(cmd);
}

void ShowTeamMatches()
{
    using var c = OpenConnection();
    int teamId = ChooseTeam(c);
    if (teamId == 0) return;
    using var cmd = new SqlCommand(@"SELECT m.MatchId,t1.TeamName,t2.TeamName,m.GoalsTeam1,m.GoalsTeam2,m.MatchDate FROM dbo.Matches m
        JOIN dbo.Teams t1 ON t1.TeamId=m.Team1Id JOIN dbo.Teams t2 ON t2.TeamId=m.Team2Id
        WHERE m.Team1Id=@team OR m.Team2Id=@team ORDER BY m.MatchDate", c);
    cmd.Parameters.AddWithValue("@team", teamId);
    PrintMatches(cmd);
}

void ShowScorersByDate()
{
    DateTime date = ReadDate("Дата (дд.мм.гггг): ");
    using var c = OpenConnection();
    using var cmd = new SqlCommand(@"SELECT DISTINCT p.FullName,t.TeamName,m.MatchDate FROM dbo.Goals g
        JOIN dbo.Matches m ON m.MatchId=g.MatchId JOIN dbo.Players p ON p.PlayerId=g.PlayerId
        JOIN dbo.Teams t ON t.TeamId=p.TeamId WHERE m.MatchDate=@date ORDER BY t.TeamName,p.FullName", c);
    cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
    using var r = cmd.ExecuteReader();
    bool any = false;
    while (r.Read()) { any = true; Console.WriteLine($"{r[0]} ({r[1]})"); }
    if (!any) Console.WriteLine("За эту дату авторы голов не найдены.");
}

void AddMatch()
{
    using var c = OpenConnection();
    Console.WriteLine("Выберите первую команду:"); int team1 = ChooseTeam(c);
    Console.WriteLine("Выберите вторую команду:"); int team2 = ChooseTeam(c);
    if (team1 == 0 || team2 == 0) return;
    if (team1 == team2) { Console.WriteLine("Команды должны различаться."); return; }
    int g1 = ReadNonNegative("Голы первой команды: ");
    int g2 = ReadNonNegative("Голы второй команды: ");
    DateTime date = ReadDate("Дата матча (дд.мм.гггг): ");
    using var duplicate = new SqlCommand(@"SELECT COUNT(*) FROM dbo.Matches WHERE MatchDate=@date AND ((Team1Id=@a AND Team2Id=@b) OR (Team1Id=@b AND Team2Id=@a))", c);
    duplicate.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
    duplicate.Parameters.AddWithValue("@a", team1); duplicate.Parameters.AddWithValue("@b", team2);
    if ((int)duplicate.ExecuteScalar()! > 0) { Console.WriteLine("Такой матч между этими командами на эту дату уже существует."); return; }
    using var cmd = new SqlCommand("INSERT dbo.Matches(Team1Id,Team2Id,GoalsTeam1,GoalsTeam2,MatchDate) VALUES(@a,@b,@g1,@g2,@date)", c);
    cmd.Parameters.AddWithValue("@a", team1); cmd.Parameters.AddWithValue("@b", team2);
    cmd.Parameters.AddWithValue("@g1", g1); cmd.Parameters.AddWithValue("@g2", g2);
    cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
    cmd.ExecuteNonQuery();
    Console.WriteLine("Матч добавлен.");
    Console.WriteLine("Авторов голов можно добавить в таблицу dbo.Goals, выбрав игроков из dbo.Players.");
}

void EditMatch()
{
    int id = ReadInt("ID матча для изменения: ");
    using var c = OpenConnection();
    using (var check = new SqlCommand("SELECT COUNT(*) FROM dbo.Matches WHERE MatchId=@id", c))
    { check.Parameters.AddWithValue("@id", id); if ((int)check.ExecuteScalar()! == 0) { Console.WriteLine("Матч не найден."); return; } }
    Console.WriteLine("Выберите новую первую команду:"); int team1 = ChooseTeam(c);
    Console.WriteLine("Выберите новую вторую команду:"); int team2 = ChooseTeam(c);
    if (team1 == 0 || team2 == 0 || team1 == team2) { Console.WriteLine("Выбраны некорректные команды."); return; }
    int g1 = ReadNonNegative("Новое число голов первой команды: ");
    int g2 = ReadNonNegative("Новое число голов второй команды: ");
    DateTime date = ReadDate("Новая дата (дд.мм.гггг): ");
    using var duplicate = new SqlCommand(@"SELECT COUNT(*) FROM dbo.Matches WHERE MatchId<>@id AND MatchDate=@date AND ((Team1Id=@a AND Team2Id=@b) OR (Team1Id=@b AND Team2Id=@a))", c);
    duplicate.Parameters.AddWithValue("@id", id); duplicate.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
    duplicate.Parameters.AddWithValue("@a", team1); duplicate.Parameters.AddWithValue("@b", team2);
    if ((int)duplicate.ExecuteScalar()! > 0) { Console.WriteLine("Другой матч с такими командами и датой уже существует."); return; }
    using var cmd = new SqlCommand("UPDATE dbo.Matches SET Team1Id=@a,Team2Id=@b,GoalsTeam1=@g1,GoalsTeam2=@g2,MatchDate=@date WHERE MatchId=@id", c);
    cmd.Parameters.AddWithValue("@id", id); cmd.Parameters.AddWithValue("@a", team1); cmd.Parameters.AddWithValue("@b", team2);
    cmd.Parameters.AddWithValue("@g1", g1); cmd.Parameters.AddWithValue("@g2", g2); cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
    cmd.ExecuteNonQuery();
    Console.WriteLine("Данные матча изменены.");
}

void DeleteMatch()
{
    using var c = OpenConnection();
    Console.WriteLine("Выберите первую команду матча:"); int team1 = ChooseTeam(c);
    Console.WriteLine("Выберите вторую команду матча:"); int team2 = ChooseTeam(c);
    if (team1 == 0 || team2 == 0 || team1 == team2) { Console.WriteLine("Некорректный выбор команд."); return; }
    DateTime date = ReadDate("Дата матча (дд.мм.гггг): ");
    using var find = new SqlCommand(@"SELECT m.MatchId,t1.TeamName,t2.TeamName,m.GoalsTeam1,m.GoalsTeam2 FROM dbo.Matches m
        JOIN dbo.Teams t1 ON t1.TeamId=m.Team1Id JOIN dbo.Teams t2 ON t2.TeamId=m.Team2Id
        WHERE m.MatchDate=@date AND ((m.Team1Id=@a AND m.Team2Id=@b) OR (m.Team1Id=@b AND m.Team2Id=@a))", c);
    find.Parameters.Add("@date", SqlDbType.Date).Value = date.Date; find.Parameters.AddWithValue("@a", team1); find.Parameters.AddWithValue("@b", team2);
    int id; using (var r = find.ExecuteReader())
    { if (!r.Read()) { Console.WriteLine("Матч не найден."); return; } id = r.GetInt32(0); Console.WriteLine($"Найден матч №{id}: {r[1]} {r[3]} : {r[4]} {r[2]}."); }
    Console.Write("Удалить этот матч? (д/н): ");
    if (!string.Equals(Console.ReadLine()?.Trim(), "д", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Удаление отменено."); return; }
    using var cmd = new SqlCommand("DELETE FROM dbo.Matches WHERE MatchId=@id", c); cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery();
    Console.WriteLine("Матч удалён. Связанные записи о голах также удалены.");
}

void ShowTeamsAndPlayers()
{
    using var c = OpenConnection();
    using var cmd = new SqlCommand(@"SELECT t.TeamName,p.FullName,p.Country,p.ShirtNumber,p.Position FROM dbo.Teams t
        LEFT JOIN dbo.Players p ON p.TeamId=t.TeamId ORDER BY t.TeamName,p.ShirtNumber", c);
    using var r = cmd.ExecuteReader();
    while (r.Read())
        Console.WriteLine(r.IsDBNull(1) ? $"{r[0]} — состав пока не заполнен" : $"{r[0]} | №{r[3]} {r[1]} | {r[2]} | {r[4]}");
}

int ChooseTeam(SqlConnection c)
{
    using var cmd = new SqlCommand("SELECT TeamId,TeamName FROM dbo.Teams ORDER BY TeamName", c);
    using var r = cmd.ExecuteReader();
    while (r.Read()) Console.WriteLine($"{r[0]}. {r[1]}");
    r.Close();
    return ReadInt("Введите номер команды (0 — отмена): ");
}

void PrintMatches(SqlCommand cmd)
{
    using var r = cmd.ExecuteReader(); bool any = false;
    while (r.Read()) { any = true; Console.WriteLine($"ID {r[0]} | {((DateTime)r[5]):dd.MM.yyyy} | {r[1]} {r[3]} : {r[4]} {r[2]}"); }
    if (!any) Console.WriteLine("Матчи не найдены.");
}

int ReadInt(string prompt)
{
    while (true) { Console.Write(prompt); if (int.TryParse(Console.ReadLine(), out int value) && value >= 0) return value; Console.WriteLine("Введите целое неотрицательное число."); }
}
int ReadNonNegative(string prompt) => ReadInt(prompt);
DateTime ReadDate(string prompt)
{
    while (true) { Console.Write(prompt); if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), System.Globalization.DateTimeStyles.None, out DateTime value)) return value.Date; Console.WriteLine("Введите дату в формате дд.мм.гггг."); }
}
