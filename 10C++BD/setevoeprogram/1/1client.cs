
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
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 5000);

            NetworkStream stream = client.GetStream();

            string message = "Привет, сервер!";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            Console.WriteLine($"В {DateTime.Now:HH:mm} от {ip} получена строка: {response}");

            client.Close();
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
