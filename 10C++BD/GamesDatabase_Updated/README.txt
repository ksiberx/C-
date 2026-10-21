БАЗА ДАННЫХ ИГР — EF CORE 8

Состав:
- Games.Models — класс-модель Game
- Games.Data — GameContext и подключение к SQL Server LocalDB
- Games.App — консольная проверка

Требования:
- Visual Studio 2022 с .NET 8
- Компонент SQL Server Express LocalDB
- Интернет при первом восстановлении NuGet-пакетов

Как запустить:
1. Откройте GamesDatabase.sln в Visual Studio.
2. Восстановите NuGet-пакеты.
3. В меню Сборка выберите Собрать решение.
4. В Visual Studio откройте Инструменты -> Диспетчер пакетов NuGet -> Консоль диспетчера пакетов.
5. Выполните команды по очереди:

   Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.11
   Add-Migration InitialCreate -Project Games.Data -StartupProject Games.App
   Update-Database -Project Games.Data -StartupProject Games.App

6. Запустите проект Games.App.

Важно про задание 4:
В этом исходном варианте модель уже содержит Mode и SoldCopies, поэтому InitialCreate создаст сразу полную таблицу. Если преподавателю нужны именно две последовательные миграции, сначала удалите свойства Mode и SoldCopies из Game.cs, создайте InitialCreate и выполните Update-Database. Затем верните эти два свойства, выполните:
   Add-Migration AddGameModeAndSoldCopies -Project Games.Data -StartupProject Games.App
   Update-Database -Project Games.Data -StartupProject Games.App

Подключение:
(localdb)\MSSQLLocalDB;Database=GamesDb;Trusted_Connection=True;TrustServerCertificate=True;

Если у вас SQL Server Express вместо LocalDB, поменяйте строку подключения в GameContext.cs, например на:
Server=.\SQLEXPRESS;Database=GamesDb;Trusted_Connection=True;TrustServerCertificate=True;TrustServerCertificate=True;
