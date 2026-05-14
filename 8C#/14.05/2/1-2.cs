/*
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
    internal class Program
    {
        static void Main(string[] args)
        {
            Article tovar;
            tovar.code = "01";
            tovar.name = "Milk";
            tovar.price = 90.00;

            Console.WriteLine("Код: " + tovar.code);
            Console.WriteLine("Товар: " + tovar.name);
            Console.WriteLine("Цена: " + tovar.price);
        }
    }
}
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2 { 
    struct Client
    {
        public string code;
        public string SNP;
        public string street;
        public int number;
        public int order;
        public int summaorder;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Client programm;
            programm.code = "01";
            programm.SNP = "Mukhtarov Ivan Pavlovich";
            programm.street = "Pushnika 52, kv 42";
            programm.number = 890692491;
            programm.order = 5;
            programm.summaorder = 105;

            Console.WriteLine("Код: " + programm.code);
            Console.WriteLine("ФИО: " + programm.SNP);
            Console.WriteLine("Адрес: " + programm.street);
            Console.WriteLine("Номер: " + programm.number);
            Console.WriteLine("Кол-во заказов: " + programm.order);
            Console.WriteLine("Общая сумма заказов клиента: " + programm.summaorder);

        }
    }
}
