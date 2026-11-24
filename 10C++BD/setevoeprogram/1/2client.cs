using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        try // Пробуем выполнить код
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            string message = "Привет, сервер!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            Console.WriteLine($"В {DateTime.Now:HH:mm} от {ip} получена строка: {response}");

            client.Close();
        }
        catch (Exception ex) // Если возникнет ошибка
        {
            Console.WriteLine("Ошибка подключения или передачи: " + ex.Message);
        }
    }
}
