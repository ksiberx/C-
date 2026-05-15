using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Magazine
    {
        public string name, description, phone, email;
        public int year;

        public void Input()
        {
            Console.Write("Название журнала: "); name = Console.ReadLine();
            Console.Write("Год основания: "); year = int.Parse(Console.ReadLine());
            Console.Write("Описание: "); description = Console.ReadLine();
            Console.Write("Телефон: "); phone = Console.ReadLine();
            Console.Write("Email: "); email = Console.ReadLine();
        }

        public void Output()
        {
            Console.WriteLine($"\nНазвание: {name}\nГод основания: {year}\nОписание: {description}\nТелефон: {phone}\nEmail: {email}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Magazine mag = new Magazine();
            mag.Input();
            mag.Output();
        }
    }
}
        
