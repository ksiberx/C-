USE SpanishChampionship;
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Teams)
BEGIN
 INSERT dbo.Teams(TeamName,City) VALUES
 (N'Real Madrid',N'Madrid'),(N'Barcelona',N'Barcelona'),(N'Atlético Madrid',N'Madrid'),(N'Sevilla',N'Sevilla');
 INSERT dbo.Players(TeamId,FullName,Country,ShirtNumber,Position) VALUES
 (1,N'Thịbaut Courtois',N'Бельгия',1,N'Вратарь'),(1,N'Jude Bellingham',N'Англия',5,N'Полузащитник'),(1,N'Vinícius Júnior',N'Бразилия',7,N'Нападающий'),
 (2,N'Marc-André ter Stegen',N'Германия',1,N'Вратарь'),(2,N'Pedri',N'Испания',8,N'Полузащитник'),(2,N'Robert Lewandowski',N'Польша',9,N'Нападающий'),
 (3,N'Jan Oblak',N'Словения',13,N'Вратарь'),(3,N'Antoine Griezmann',N'Франция',7,N'Нападающий'),(4,N'Youssef En-Nesyri',N'Марокко',15,N'Нападающий');
 INSERT dbo.Matches(Team1Id,Team2Id,GoalsTeam1,GoalsTeam2,MatchDate) VALUES
 (1,2,2,1,'2026-09-20'),(3,4,1,1,'2026-09-21'),(2,3,0,2,'2026-09-27');
 INSERT dbo.Goals(MatchId,PlayerId,GoalMinute) VALUES (1,2,35),(1,3,72),(1,6,81),(2,8,44),(3,8,60),(3,8,88);
END
GO
