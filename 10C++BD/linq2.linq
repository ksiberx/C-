<Query Kind="Program" />


void Main()
{
    // Страны
    var countries = new List<Country>
    {
        new Country(1, "Россия", 146000000, 17098246, "Европа"),
        new Country(2, "Франция", 68000000, 551695, "Европа"),
        new Country(3, "Германия", 84000000, 357022, "Европа"),
        new Country(4, "Китай", 1410000000, 9596961, "Азия"),
        new Country(5, "Индия", 1430000000, 3287263, "Азия"),
        new Country(6, "Япония", 124000000, 377975, "Азия"),
        new Country(7, "Египет", 112000000, 1002450, "Африка"),
        new Country(8, "Алжир", 46000000, 2381741, "Африка"),
        new Country(9, "США", 335000000, 9833517, "Северная Америка"),
        new Country(10, "Бразилия", 216000000, 8515767, "Южная Америка"),
        new Country(11, "Украина", 37000000, 603628, "Европа")
    };

    // Столицы
    var capitals = new List<Capital>
    {
        new Capital(1, 1, "Moscow", 13000000),
        new Capital(2, 2, "Paris", 2100000),
        new Capital(3, 3, "Berlin", 3700000),
        new Capital(4, 4, "Beijing", 21500000),
        new Capital(5, 5, "New Delhi", 16700000),
        new Capital(6, 6, "Tokyo", 14000000),
        new Capital(7, 7, "Cairo", 10000000),
        new Capital(8, 8, "Algiers", 3900000),
        new Capital(9, 9, "Washington", 700000),
        new Capital(10, 10, "Brasilia", 3000000),
        new Capital(11, 11, "Kyiv", 3000000)
    };

    // Крупные города
    var cities = new List<City>
    {
        new City(1, 1, "Санкт-Петербург", 5600000),
        new City(2, 1, "Новосибирск", 1600000),
        new City(3, 1, "Екатеринбург", 1500000),
        new City(4, 2, "Марсель", 870000),
        new City(5, 2, "Лион", 520000),
        new City(6, 3, "Гамбург", 1900000),
        new City(7, 3, "Мюнхен", 1500000),
        new City(8, 4, "Шанхай", 24800000),
        new City(9, 4, "Гуанчжоу", 18800000),
        new City(10, 5, "Мумбаи", 12600000),
        new City(11, 5, "Бангалор", 8400000),
        new City(12, 6, "Осака", 2750000),
        new City(13, 7, "Александрия", 5200000),
        new City(14, 9, "Нью-Йорк", 8300000),
        new City(15, 9, "Лос-Анджелес", 3800000),
        new City(16, 10, "Сан-Паулу", 11400000),
        new City(17, 11, "Харьков", 1400000)
    };

    // Задание 2

    countries.Dump("Вся информация о странах");

    countries.Select(c => c.Name)
        .Dump("Названия стран");

    capitals.Select(c => c.Name)
        .Dump("Названия столиц");

    cities.Where(c => c.CountryId == 1)
        .Select(c => c.Name)
        .Dump("Крупные города России");

    capitals.Where(c => c.Population > 5000000)
        .Select(c => c.Name)
        .Dump("Столицы с населением больше 5 миллионов");

    countries.Where(c => c.Continent == "Европа")
        .Select(c => c.Name)
        .Dump("Европейские страны");

    countries.Where(c => c.Area > 1000000)
        .Select(c => c.Name)
        .Dump("Страны с площадью больше 1 миллиона");

    // Задание 3

    capitals.Where(c => c.Name.ToLower().Contains("a")
                     && c.Name.ToLower().Contains("p"))
        .Select(c => c.Name)
        .Dump("Столицы с буквами a и p");

    capitals.Where(c => c.Name.ToLower().StartsWith("k"))
        .Select(c => c.Name)
        .Dump("Столицы на букву k");

    countries.Where(c => c.Area >= 300000 && c.Area <= 1000000)
        .Select(c => c.Name)
        .Dump("Площадь от 300000 до 1000000");

    countries.Where(c => c.Population > 100000000)
        .Select(c => c.Name)
        .Dump("Население больше 100 миллионов");

    // Задание 4

    countries.OrderByDescending(c => c.Area)
        .Take(5).Dump("Топ-5 стран по площади");

    capitals.OrderByDescending(c => c.Population)
        .Take(5).Dump("Топ-5 столиц по населению");

    countries.OrderByDescending(c => c.Area)
        .First().Dump("Страна с самой большой площадью");

    capitals.OrderByDescending(c => c.Population)
        .First().Dump("Столица с самым большим населением");

    countries.Where(c => c.Continent == "Европа")
        .OrderBy(c => c.Area)
        .First().Dump("Самая маленькая страна Европы");

    countries.Where(c => c.Continent == "Европа")
        .Average(c => c.Area)
        .Dump("Средняя площадь стран Европы");

    cities.Where(c => c.CountryId == 1)
        .OrderByDescending(c => c.Population)
        .Take(3).Dump("Топ-3 города России");

    countries.Count().Dump("Количество стран");

    countries.GroupBy(c => c.Continent)
        .Select(g => new
        {
            ЧастьСвета = g.Key,
            КоличествоСтран = g.Count()
        })
        .OrderByDescending(g => g.КоличествоСтран)
        .First().Dump("Часть света с наибольшим количеством стран");

    countries.GroupBy(c => c.Continent)
        .Select(g => new
        {
            ЧастьСвета = g.Key,
            КоличествоСтран = g.Count()
        })
        .Dump("Количество стран по частям света");
}

public class Country
{
    public int Id;
    public string Name;
    public long Population;
    public double Area;
    public string Continent;

    public Country(int id, string name, long population,
        double area, string continent)
    {
        Id = id;
        Name = name;
        Population = population;
        Area = area;
        Continent = continent;
    }
}

public class Capital
{
    public int Id;
    public int CountryId;
    public string Name;
    public long Population;

    public Capital(int id, int countryId, string name, long population)
    {
        Id = id;
        CountryId = countryId;
        Name = name;
        Population = population;
    }
}

public class City
{
    public int Id;
    public int CountryId;
    public string Name;
    public long Population;

    public City(int id, int countryId, string name, long population)
    {
        Id = id;
        CountryId = countryId;
        Name = name;
        Population = population;
    }
}
