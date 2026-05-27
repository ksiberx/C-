using System;
using System.Collections.Generic;
using System.Threading;

// Абстрактный автомобиль
abstract class Car
{
    public string Name;
    public int Position = 0;
    public int Speed;
    public event Action<Car> Finished;
    protected Random rnd = new Random();

    public Car(string name) { Name = name; }
    public abstract void SetSpeed();
    
    public void Drive()
    {
        SetSpeed();
        Position += Speed;
        if (Position >= 100) Finished?.Invoke(this);
    }
}

// Типы авто
class SportsCar : Car { public SportsCar(string n) : base(n) { } public override void SetSpeed() { Speed = rnd.Next(15, 26); } }
class PassengerCar : Car { public PassengerCar(string n) : base(n) { } public override void SetSpeed() { Speed = rnd.Next(10, 21); } }
class Truck : Car { public Truck(string n) : base(n) { } public override void SetSpeed() { Speed = rnd.Next(5, 16); } }
class Bus : Car { public Bus(string n) : base(n) { } public override void SetSpeed() { Speed = rnd.Next(6, 13); } }

// Игра
class Game
{
    List<Car> cars = new List<Car>();
    bool finished = false;
    
    public void AddCar(Car c) { cars.Add(c); c.Finished += winner => { if (!finished) { finished = true; Console.WriteLine($"ПОБЕДИТЕЛЬ: {winner.Name}"); } }; }
    
    public delegate void StartRace();
    
    public void Race()
    {
        Console.WriteLine("ГОНКА СТАРТ!");
        while (!finished)
        {
            foreach (var c in cars)
            {
                c.Drive();
                Console.WriteLine($"{c.Name}: {c.Position} км/ч (скорость {c.Speed})");
                if (finished) break;
            }
            Console.WriteLine("---");
            Thread.Sleep(500);
        }
        Console.WriteLine("ИГРА ОКОНЧЕНА");
    }
}

// Главный класс
class Program
{
    static void Main()
    {
        Game game = new Game();
        game.AddCar(new SportsCar("Ferrari"));
        game.AddCar(new PassengerCar("Toyota"));
        game.AddCar(new Truck("KamAZ"));
        game.AddCar(new Bus("Bus"));
        
        game.Race();
        Console.ReadKey();
    }
}
