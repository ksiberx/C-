using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        try 
        {
            int port = 5000;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Сервер запущен. Ожидание клиента...");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клиент подключился.");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            Console.WriteLine($"В {DateTime.Now:HH:mm} от {ip} получена строка: {message}");

            string response = "Привет, клиент!";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);

            client.Close();
            server.Stop();
            Console.WriteLine("Соединение закрыто.");
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
