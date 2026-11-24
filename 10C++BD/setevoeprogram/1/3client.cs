
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        try
        {
            Console.WriteLine("Что запросить? (date/time)");
            string request = Console.ReadLine();

            TcpClient client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 5000);

            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(request);
            await stream.WriteAsync(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            Console.WriteLine($"От сервера {ip} получено: {response}");

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
