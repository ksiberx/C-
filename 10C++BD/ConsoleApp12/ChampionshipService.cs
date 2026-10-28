using Microsoft.EntityFrameworkCore;

namespace SpanishChampionship;

public record ScorerStat(string Player, string Team, int Goals);
public record TeamStat(string Team, int GoalsFor, int GoalsAgainst, int Points);

public class ChampionshipService
{
    private readonly ChampionshipContext _db;
    public ChampionshipService(ChampionshipContext db) => _db = db;

    // Задание 1: запуск хранимой процедуры SQL Server.
    public Task FillMatchesRandomlyAsync(CancellationToken token = default) =>
        _db.Database.ExecuteSqlRawAsync("EXEC dbo.FillMatchesRandomly", token);

    // Бомбардиры считаются по записям в таблице Goals.
    public async Task<List<ScorerStat>> TopScorersAsync(int? teamId = null, int take = 3)
    {
        var query = _db.Goals.AsNoTracking()
            .Where(g => teamId == null || g.Player!.TeamId == teamId)
            .GroupBy(g => new { g.PlayerId, g.Player!.Name, Team = g.Player.Team!.Name })
            .Select(g => new ScorerStat(g.Key.Name, g.Key.Team, g.Count()))
            .OrderByDescending(x => x.Goals)
            .ThenBy(x => x.Player);

        return await query.Take(take).ToListAsync();
    }

    public async Task<List<TeamStat>> GetTeamTableAsync()
    {
        var teams = await _db.Teams.AsNoTracking().ToListAsync();
        var matches = await _db.Matches.AsNoTracking()
            .Where(m => m.HomeGoals != null && m.AwayGoals != null).ToListAsync();

        return teams.Select(team =>
        {
            int scored = 0, conceded = 0, points = 0;
            foreach (var match in matches)
            {
                if (match.HomeTeamId == team.Id)
                {
                    scored += match.HomeGoals!.Value;
                    conceded += match.AwayGoals!.Value;
                    points += match.HomeGoals > match.AwayGoals ? 3 :
                              match.HomeGoals == match.AwayGoals ? 1 : 0;
                }
                else if (match.AwayTeamId == team.Id)
                {
                    scored += match.AwayGoals!.Value;
                    conceded += match.HomeGoals!.Value;
                    points += match.AwayGoals > match.HomeGoals ? 3 :
                              match.AwayGoals == match.HomeGoals ? 1 : 0;
                }
            }
            return new TeamStat(team.Name, scored, conceded, points);
        }).ToList();
    }

    public async Task<List<TeamStat>> TopTeamsByGoalsAsync(int take = 3) =>
        (await GetTeamTableAsync()).OrderByDescending(t => t.GoalsFor).ThenBy(t => t.Team).Take(take).ToList();

    public async Task<List<TeamStat>> BestDefenseAsync(int take = 3) =>
        (await GetTeamTableAsync()).OrderBy(t => t.GoalsAgainst).ThenBy(t => t.Team).Take(take).ToList();

    public async Task<List<TeamStat>> TopTeamsByPointsAsync(int take = 3) =>
        (await GetTeamTableAsync()).OrderByDescending(t => t.Points).ThenBy(t => t.Team).Take(take).ToList();

    public async Task<List<TeamStat>> LowestTeamsByPointsAsync(int take = 3) =>
        (await GetTeamTableAsync()).OrderBy(t => t.Points).ThenBy(t => t.Team).Take(take).ToList();
}
