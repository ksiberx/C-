using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    struct Article
    {
        public string code;
        public string name;
        public double price;
    }

    struct RequestItem
    {
        public Article product;
        public int quantity;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Article tovar;
            tovar.code = "01";
            tovar.name = "Milk";
            tovar.price = 90.00;

            RequestItem item;
            item.product = tovar;
            item.quantity = 3;

            Console.WriteLine("Заказ");
            Console.WriteLine("Код товара: " + item.product.code);
            Console.WriteLine("Товар: " + item.product.name);
            Console.WriteLine("Цена: " + item.product.price);
            Console.WriteLine("Количество: " + item.quantity);
            Console.WriteLine("Итого: " + item.product.price * item.quantity);
        }
    }
}
