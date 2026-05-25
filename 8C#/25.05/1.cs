using System;
using System.Collections.Generic;

// Задание 1
class Human
{
    public string name;
    public int age;
    
    public void Info()
    {
        Console.WriteLine("Имя: " + name + ", возраст: " + age);
    }
}

class Builder : Human
{
    public string instrument;
    
    public void Work()
    {
        Console.WriteLine(name + " работает строителем с инструментом " + instrument);
    }
}

class Sailor : Human
{
    public string ship;
    
    public void Swim()
    {
        Console.WriteLine(name + " плавает на корабле " + ship);
    }
}

class Pilot : Human
{
    public string plane;
    
    public void Fly()
    {
        Console.WriteLine(name + " управляет самолётом " + plane);
    }
}

// Задание 2
class Passport
{
    public string seria;
    public string name;
    public string country;
    
    public void Show()
    {
        Console.WriteLine("Паспорт: " + seria + " " + name + " " + country);
    }
}

class ForeignPassport : Passport
{
    public string number;
    public List<string> visas = new List<string>();
    
    public void AddVisa(string v)
    {
        visas.Add(v);
    }
    
    public void ShowAll()
    {
        Show();
        Console.WriteLine("Номер загран: " + number);
        Console.Write("Визы: ");
        foreach (string v in visas)
            Console.Write(v + " ");
        Console.WriteLine();
    }
}

// Задание 3
class Animal
{
    public string name;
    public string haract;
    
    public Animal(string n, string h)
    {
        name = n;
        haract = h;
    }
    
    public virtual void Print()
    {
        Console.WriteLine(name + " - " + haract);
    }
}

class Tiger : Animal
{
    public Tiger(string n, string h) : base(n, h) { }
}

class Crocodile : Animal
{
    public Crocodile(string n, string h) : base(n, h) { }
}

class Kangaroo : Animal
{
    public Kangaroo(string n, string h) : base(n, h) { }
}

// Задание 4
abstract class Figura
{
    public abstract double Ploshad();
}

class Pramougolnik : Figura
{
    double a, b;
    public Pramougolnik(double x, double y) { a = x; b = y; }
    public override double Ploshad() { return a * b; }
}

class Krug : Figura
{
    double r;
    public Krug(double x) { r = x; }
    public override double Ploshad() { return Math.PI * r * r; }
}

class Treugolnik : Figura
{
    double kat1, kat2;
    public Treugolnik(double x, double y) { kat1 = x; kat2 = y; }
    public override double Ploshad() { return (kat1 * kat2) / 2; }
}

class Trapezia : Figura
{
    double osn1, osn2, visota;
    public Trapezia(double a, double b, double h) { osn1 = a; osn2 = b; visota = h; }
    public override double Ploshad() { return ((osn1 + osn2) * visota) / 2; }
}

class Program
{
    static void Main()
    {
        // проверка 1 задания
        Builder b = new Builder();
        b.name = "Алексей";
        b.age = 28;
        b.instrument = "молоток";
        b.Info();
        b.Work();
        
        Sailor s = new Sailor();
        s.name = "Дмитрий";
        s.ship = "Алые паруса";
        s.Swim();
        
        // проверка 2
        ForeignPassport fp = new ForeignPassport();
        fp.seria = "1234";
        fp.name = "Иванов Иван";
        fp.country = "Россия";
        fp.number = "AB123456";
        fp.AddVisa("Шенген");
        fp.AddVisa("Великобритания");
        fp.ShowAll();
        
        // проверка 3
        Tiger t = new Tiger("Тигр", "полосатый, сильный");
        Crocodile c = new Crocodile("Крокодил", "зеленый, зубастый");
        Kangaroo k = new Kangaroo("Кенгуру", "прыгает высоко");
        t.Print();
        c.Print();
        k.Print();
        
        // проверка 4
        Figura[] mass = new Figura[4];
        mass[0] = new Pramougolnik(5, 3);
        mass[1] = new Krug(4);
        mass[2] = new Treugolnik(6, 8);
        mass[3] = new Trapezia(4, 6, 5);
        
        for (int i = 0; i < mass.Length; i++)
        {
            Console.WriteLine("Площадь: " + mass[i].Ploshad());
        }
    }
}
