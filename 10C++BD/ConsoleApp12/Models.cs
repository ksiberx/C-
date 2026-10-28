namespace SpanishChampionship;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Player> Players { get; set; } = new();
    public List<Match> HomeMatches { get; set; } = new();
    public List<Match> AwayMatches { get; set; } = new();
}

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int TeamId { get; set; }
    public Team? Team { get; set; }
}

public class Match
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public Team? HomeTeam { get; set; }
    public int AwayTeamId { get; set; }
    public Team? AwayTeam { get; set; }
    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }
    public DateTime MatchDate { get; set; } = DateTime.Today;
}

public class Goal
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    public int MatchId { get; set; }
    public Match? Match { get; set; }
    public int Minute { get; set; }
}
