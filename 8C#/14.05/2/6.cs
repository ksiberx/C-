using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Shop
    {
        public string name, address, profile, phone, email;

        public void Input()
        {
            Console.Write("Название магазина: "); name = Console.ReadLine();
            Console.Write("Адрес: "); address = Console.ReadLine();
            Console.Write("Профиль магазина: "); profile = Console.ReadLine();
            Console.Write("Телефон: "); phone = Console.ReadLine();
            Console.Write("Email: "); email = Console.ReadLine();
        }

        public void Output()
        {
            Console.WriteLine($"\nНазвание: {name}\nАдрес: {address}\nПрофиль: {profile}\nТелефон: {phone}\nEmail: {email}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.Input();
            shop.Output();
        }
    }
}
