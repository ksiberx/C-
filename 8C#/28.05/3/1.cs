using System;
using System.Collections.Generic;

abstract class Figure
{
    public abstract double Area();
    public abstract double Perimeter();
}

class Square : Figure
{
    double a;
    public Square(double a) { this.a = a; }
    public override double Area() { return a * a; }
    public override double Perimeter() { return 4 * a; }
}

class Circle : Figure
{
    double r;
    public Circle(double r) { this.r = r; }
    public override double Area() { return Math.PI * r * r; }
    public override double Perimeter() { return 2 * Math.PI * r; }
}

class Triangle : Figure
{
    double a, b, c;
    public Triangle(double a, double b, double c) { this.a = a; this.b = b; this.c = c; }
    public override double Area()
    {
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
    public override double Perimeter() { return a + b + c; }
}

class Rectangle : Figure
{
    double w, h;
    public Rectangle(double w, double h) { this.w = w; this.h = h; }
    public override double Area() { return w * h; }
    public override double Perimeter() { return 2 * (w + h); }
}

class CompositeFigure : Figure
{
    List<Figure> figs = new List<Figure>();
    public void Add(Figure f) { figs.Add(f); }
    public override double Area()
    {
        double sum = 0;
        foreach (var f in figs) sum += f.Area();
        return sum;
    }
    public override double Perimeter()
    {
        double sum = 0;
        foreach (var f in figs) sum += f.Perimeter();
        return sum;
    }
}

class Program
{
    static void Main()
    {
        var house = new CompositeFigure();
        house.Add(new Square(5));
        house.Add(new Triangle(3, 4, 5));
        
        Console.WriteLine($"Площадь дома: {house.Area()}");
        Console.WriteLine($"Периметр дома: {house.Perimeter()}");
        Console.ReadKey();
    }
}
