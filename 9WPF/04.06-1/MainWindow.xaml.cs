using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

namespace OrdersApp
{
    public partial class MainWindow : Window
    {
        private string filePath = "orders.xml";

        public MainWindow()
        {
            InitializeComponent();
        }

        // 1. СОЗДАНИЕ XML через XmlTextWriter
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            using (XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Заказы");

                // Заказ 1
                writer.WriteStartElement("Заказ");
                writer.WriteAttributeString("Номер", "1");
                writer.WriteElementString("Клиент", "Иван Петров");
                writer.WriteElementString("Дата", "25.05.2026");

                writer.WriteStartElement("Товары");
                writer.WriteStartElement("Товар");
                writer.WriteElementString("Название", "Ноутбук");
                writer.WriteElementString("Количество", "1");
                writer.WriteElementString("Цена", "50000");
                writer.WriteEndElement();

                writer.WriteStartElement("Товар");
                writer.WriteElementString("Название", "Мышь");
                writer.WriteElementString("Количество", "2");
                writer.WriteElementString("Цена", "1500");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();

                // Заказ 2
                writer.WriteStartElement("Заказ");
                writer.WriteAttributeString("Номер", "2");
                writer.WriteElementString("Клиент", "Мария Смирнова");
                writer.WriteElementString("Дата", "26.05.2026");

                writer.WriteStartElement("Товары");
                writer.WriteStartElement("Товар");
                writer.WriteElementString("Название", "Клавиатура");
                writer.WriteElementString("Количество", "1");
                writer.WriteElementString("Цена", "3000");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            txtOutput.Text = "XML файл создан!\n\n" + File.ReadAllText(filePath);
        }

        // 2. ЧТЕНИЕ через XmlDocument
        private void BtnReadDoc_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(filePath)) { txtOutput.Text = "Сначала создай XML!"; return; }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            string result = "ЧТЕНИЕ ЧЕРЕЗ XmlDocument";

            foreach (XmlNode order in doc.SelectNodes("/Заказы/Заказ"))
            {
                string num = order.Attributes["Номер"].Value;
                string client = order["Клиент"].InnerText;
                string date = order["Дата"].InnerText;

                result += $"Заказ №{num} | Клиент: {client} | Дата: {date}\n";
                result += "Товары:\n";

                foreach (XmlNode product in order.SelectNodes("Товары/Товар"))
                {
                    string name = product["Название"].InnerText;
                    string qty = product["Количество"].InnerText;
                    string price = product["Цена"].InnerText;
                    result += $"  - {name} | {qty} шт. | {price} руб.\n";
                }
                result += "\n";
            }

            txtOutput.Text = result;
        }

        // 3. ЧТЕНИЕ через XmlTextReader
        private void BtnReadReader_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(filePath)) { txtOutput.Text = "Сначала создай XML!"; return; }

            string result = "ЧТЕНИЕ ЧЕРЕЗ XmlTextReader";
            string currentOrder = "";
            string currentProduct = "";

            using (XmlTextReader reader = new XmlTextReader(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Заказ")
                        {
                            currentOrder = $"Заказ №{reader.GetAttribute("Номер")}: ";
                        }
                        else if (reader.Name == "Клиент")
                        {
                            currentOrder += $"Клиент: {reader.ReadString()} | ";
                        }
                        else if (reader.Name == "Дата")
                        {
                            currentOrder += $"Дата: {reader.ReadString()}";
                            result += currentOrder + "\nТовары:\n";
                        }
                        else if (reader.Name == "Товар")
                        {
                            currentProduct = "  - ";
                        }
                        else if (reader.Name == "Название" && currentProduct != "")
                        {
                            currentProduct += $"{reader.ReadString()} | ";
                        }
                        else if (reader.Name == "Количество")
                        {
                            currentProduct += $"{reader.ReadString()} шт. | ";
                        }
                        else if (reader.Name == "Цена")
                        {
                            currentProduct += $"{reader.ReadString()} руб.";
                            result += currentProduct + "\n";
                            currentProduct = "";
                        }
                    }
                }
            }

            txtOutput.Text = result;
        }
    }
}
