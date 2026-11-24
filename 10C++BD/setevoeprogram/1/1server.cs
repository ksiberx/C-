
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
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();

            Console.WriteLine("Сервер запущен. Ожидание клиента...");

            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            Console.WriteLine($"В {DateTime.Now:HH:mm} от {ip} получена строка: {message}");

            string response = "Привет, клиент!";
            byte[] data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);

            client.Close();
            server.Stop();

            Console.WriteLine("Сервер завершил работу.");
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Ошибка сокета: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
