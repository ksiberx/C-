<Query Kind="Program" />

void Main()
{
// 1 задание
    List<Country> countries = new List<Country>
    {
        new Country("Россия", "Москва", 146000000, 17098246, "Европа"),
        new Country("Франция", "Париж", 68000000, 551695, "Европа"),
        new Country("Германия", "Берлин", 84000000, 357022, "Европа"),
        new Country("Китай", "Пекин", 1410000000, 9596961, "Азия"),
        new Country("Индия", "Нью-Дели", 1430000000, 3287263, "Азия"),
        new Country("Япония", "Токио", 124000000, 377975, "Азия"),
        new Country("Египет", "Каир", 112000000, 1002450, "Африка"),
        new Country("Алжир", "Алжир", 46000000, 2381741, "Африка"),
        new Country("США", "Вашингтон", 335000000, 9833517, "Северная Америка"),
        new Country("Бразилия", "Бразилиа", 216000000, 8515767, "Южная Америка")
    };

    countries.Dump("Все страны");
// 2 задание	

// Вся информация о странах
(from c in countries
 select c).Dump();

// Названия стран
(from c in countries
 select c.Name).Dump();

// Названия столиц
(from c in countries
 select c.Capital).Dump();

// Европейские страны
(from c in countries
 where c.Continent == "Европа"
 select c.Name).Dump();

// Страны с площадью больше 1000000 км
(from c in countries
 where c.Area > 1000000
 select c.Name).Dump();

// 3 задание

// Названия содержат буквы a и u
(from c in countries
 where c.Name.ToLower().Contains("a")
    && c.Name.ToLower().Contains("u")
 select c.Name).Dump();

// Названия начинаются с буквы а
(from c in countries
 where c.Name.ToLower().StartsWith("а")
 select c.Name).Dump();

// Площадь от 300000 до 1000000 км
(from c in countries
 where c.Area >= 300000 && c.Area <= 1000000
 select c.Name).Dump();

// Население больше 100 миллионов
(from c in countries
 where c.Population > 100000000
 select c.Name).Dump();

// 4 задание

// Топ-5 стран по площади
(from c in countries
 orderby c.Area descending
 select c).Take(5).Dump();

// Топ-5 стран по населению
(from c in countries
 orderby c.Population descending
 select c).Take(5).Dump();

// Страна с самой большой площадью
countries.OrderByDescending(c => c.Area)
    .First().Dump();

// Страна с самым большим населением
countries.OrderByDescending(c => c.Population)
    .First().Dump();

// Страна с самой маленькой площадью в Африке
(from c in countries
 where c.Continent == "Африка"
 orderby c.Area
 select c).First().Dump();

// Средняя площадь стран Азии
(from c in countries
 where c.Continent == "Азия"
 select c.Area).Average().Dump();
	
}


public class Country
{
    public string Name;
    public string Capital;
    public long Population;
    public double Area;
    public string Continent;

    public Country(string name, string capital, long population,
        double area, string continent)
    {
        Name = name;
        Capital = capital;
        Population = population;
        Area = area;
        Continent = continent;
    }
}
