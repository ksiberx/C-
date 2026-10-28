# ShopApp — итоговое задание по ADO.NET

## Запуск
1. Откройте `Database.sql` в SSMS / Visual Studio (SQL Server Object Explorer) и выполните целиком — создастся БД `ElectronicsShop` с тестовыми данными.
2. Откройте `ShopApp.csproj` в Visual Studio 2019/2022 (нужен .NET Framework 4.8 и LocalDB).
   Если у вас SQL Express — в `App.config` замените `(localdb)\MSSQLLocalDB` на `.\SQLEXPRESS`.
3. Восстановите NuGet-пакеты (EntityFramework 6.4.4, Dapper) и запустите проект (F5).
4. В меню выбирайте пункты 1–5 и делайте скриншоты вывода.

## Почему .NET Framework 4.8
LINQ to SQL (`System.Data.Linq`) официально входит только в .NET Framework, а Задание 3 требует именно его.

## Файлы
- `Database.sql` — создание БД и данные
- `App.config` — строка подключения и настройки EF
- `DbHelper.cs` — фабрика провайдеров (Задание 1)
- `Task1Connected.cs` … `Task5Dapper.cs` — задания 1–5
- `Program.cs` — меню
