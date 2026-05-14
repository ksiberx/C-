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

    struct Client
    {
        public string code;
        public string SNP;
        public string street;
        public int number;
        public int order;
        public int summaorder;
    }

    struct RequestItem
    {
        public Article product;
        public int quantity;
    }

    struct Request
    {
        public string orderCode;
        public Client client;
        public DateTime orderDate;
        public RequestItem[] items;

        public double TotalSum
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    sum += items[i].product.price * items[i].quantity;
                }
                return sum;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Article tovar1;
            tovar1.code = "01";
            tovar1.name = "Milk";
            tovar1.price = 90.00;

            Article tovar2;
            tovar2.code = "02";
            tovar2.name = "Bread";
            tovar2.price = 35.00;

            Client client;
            client.code = "01";
            client.SNP = "Mukhtarov Ivan Pavlovich";
            client.street = "Pushnika 52, kv 42";
            client.number = 890692491;
            client.order = 5;
            client.summaorder = 105;

            RequestItem[] items = new RequestItem[2];
            items[0].product = tovar1;
            items[0].quantity = 2;
            items[1].product = tovar2;
            items[1].quantity = 3;

            Request request;
            request.orderCode = "ORD-001";
            request.client = client;
            request.orderDate = DateTime.Now;
            request.items = items;

            Console.WriteLine("Информация о заказе");
            Console.WriteLine("Код заказа: " + request.orderCode);
            Console.WriteLine("Клиент: " + request.client.SNP);
            Console.WriteLine("Дата заказа: " + request.orderDate);
            Console.WriteLine("\nТовары в заказе:");
            for (int i = 0; i < request.items.Length; i++)
            {
                Console.WriteLine("  - " + request.items[i].product.name + 
                                  " x" + request.items[i].quantity + 
                                  " = " + (request.items[i].product.price * request.items[i].quantity));
            }
            Console.WriteLine("Общая сумма заказа: " + request.TotalSum);
        }
    }
}
