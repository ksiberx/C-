IF DB_ID(N'SpanishChampionship') IS NULL
    CREATE DATABASE SpanishChampionship;
GO
USE SpanishChampionship;
GO

IF OBJECT_ID('dbo.Goals','U') IS NOT NULL DROP TABLE dbo.Goals;
IF OBJECT_ID('dbo.Matches','U') IS NOT NULL DROP TABLE dbo.Matches;
IF OBJECT_ID('dbo.Players','U') IS NOT NULL DROP TABLE dbo.Players;
IF OBJECT_ID('dbo.Teams','U') IS NOT NULL DROP TABLE dbo.Teams;
GO

CREATE TABLE dbo.Teams (
    TeamId INT IDENTITY(1,1) PRIMARY KEY,
    TeamName NVARCHAR(100) NOT NULL UNIQUE,
    City NVARCHAR(100) NULL
);

CREATE TABLE dbo.Players (
    PlayerId INT IDENTITY(1,1) PRIMARY KEY,
    TeamId INT NOT NULL,
    FullName NVARCHAR(150) NOT NULL,
    Country NVARCHAR(80) NOT NULL,
    ShirtNumber INT NOT NULL CHECK (ShirtNumber BETWEEN 1 AND 99),
    Position NVARCHAR(40) NOT NULL,
    CONSTRAINT FK_Players_Teams FOREIGN KEY (TeamId) REFERENCES dbo.Teams(TeamId),
    CONSTRAINT UQ_Player_Team_Number UNIQUE (TeamId, ShirtNumber)
);

CREATE TABLE dbo.Matches (
    MatchId INT IDENTITY(1,1) PRIMARY KEY,
    Team1Id INT NOT NULL,
    Team2Id INT NOT NULL,
    GoalsTeam1 INT NOT NULL CHECK (GoalsTeam1 >= 0),
    GoalsTeam2 INT NOT NULL CHECK (GoalsTeam2 >= 0),
    MatchDate DATE NOT NULL,
    CONSTRAINT FK_Matches_Team1 FOREIGN KEY (Team1Id) REFERENCES dbo.Teams(TeamId),
    CONSTRAINT FK_Matches_Team2 FOREIGN KEY (Team2Id) REFERENCES dbo.Teams(TeamId),
    CONSTRAINT CK_Matches_DifferentTeams CHECK (Team1Id <> Team2Id),
    CONSTRAINT UQ_Matches_Teams_Date UNIQUE (Team1Id, Team2Id, MatchDate)
);

CREATE TABLE dbo.Goals (
    GoalId INT IDENTITY(1,1) PRIMARY KEY,
    MatchId INT NOT NULL,
    PlayerId INT NOT NULL,
    GoalMinute INT NULL CHECK (GoalMinute BETWEEN 1 AND 130),
    CONSTRAINT FK_Goals_Matches FOREIGN KEY (MatchId) REFERENCES dbo.Matches(MatchId) ON DELETE CASCADE,
    CONSTRAINT FK_Goals_Players FOREIGN KEY (PlayerId) REFERENCES dbo.Players(PlayerId)
);
GO

CREATE VIEW dbo.TeamGoalDifference AS
SELECT t.TeamId, t.TeamName,
       COALESCE(SUM(CASE WHEN m.Team1Id=t.TeamId THEN m.GoalsTeam1 WHEN m.Team2Id=t.TeamId THEN m.GoalsTeam2 ELSE 0 END),0) AS GoalsScored,
       COALESCE(SUM(CASE WHEN m.Team1Id=t.TeamId THEN m.GoalsTeam2 WHEN m.Team2Id=t.TeamId THEN m.GoalsTeam1 ELSE 0 END),0) AS GoalsConceded,
       COALESCE(SUM(CASE WHEN m.Team1Id=t.TeamId THEN m.GoalsTeam1-m.GoalsTeam2 WHEN m.Team2Id=t.TeamId THEN m.GoalsTeam2-m.GoalsTeam1 ELSE 0 END),0) AS GoalDifference
FROM dbo.Teams t
LEFT JOIN dbo.Matches m ON m.Team1Id=t.TeamId OR m.Team2Id=t.TeamId
GROUP BY t.TeamId,t.TeamName;
GO
