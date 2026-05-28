using System;
using System.Collections.Generic;

abstract class Product
{
    public string Name;
    public int Quantity;
    public Product(string name, int qty) { Name = name; Quantity = qty; }
}

class Chemistry : Product
{
    public Chemistry(string name, int qty) : base(name, qty) { }
}

class Food : Product
{
    public Food(string name, int qty) : base(name, qty) { }
}

class ProductManager
{
    List<Product> stock = new List<Product>();
    List<Product> sold = new List<Product>();
    
    public void Add(Product p) { stock.Add(p); Console.WriteLine($"+ {p.Name} ({p.Quantity} шт)"); }
    
    public void Sell(Product p, int qty)
    {
        if (p.Quantity >= qty)
        {
            p.Quantity -= qty;
            sold.Add(p);
            Console.WriteLine($"- Продано: {p.Name} {qty} шт");
        }
    }
    
    public void WriteOff(Product p, int qty)
    {
        if (p.Quantity >= qty)
        {
            p.Quantity -= qty;
            Console.WriteLine($"- Списано: {p.Name} {qty} шт");
        }
    }
    
    public void Transfer(Product p, int qty, string to)
    {
        if (p.Quantity >= qty)
        {
            p.Quantity -= qty;
            Console.WriteLine($"- Передано: {p.Name} {qty} шт -> {to}");
        }
    }
    
    public void Report()
    {
        Console.WriteLine($"\nОстаток на складе: {stock.Count} товаров");
    }
}

class Program
{
    static void Main()
    {
        var manager = new ProductManager();
        
        var milk = new Food("Молоко", 100);
        var bleach = new Chemistry("Белизна", 50);
        
        manager.Add(milk);
        manager.Add(bleach);
        
        manager.Sell(milk, 30);
        manager.Transfer(bleach, 20, "Магазин");
        manager.WriteOff(milk, 5);
        manager.Report();
        
        Console.ReadKey();
    }
}
