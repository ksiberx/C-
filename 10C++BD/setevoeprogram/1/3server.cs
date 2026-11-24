
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
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();

            Console.WriteLine("Асинхронный сервер запущен. Ожидание клиента...");

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Клиент подключился.");

                try
                {
                    NetworkStream stream = client.GetStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string response;

                    if (request == "date")
                    {
                        response = DateTime.Now.ToShortDateString();
                    }
                    else if (request == "time")
                    {
                        response = DateTime.Now.ToShortTimeString();
                    }
                    else
                    {
                        response = "Неверный запрос!";
                    }

                    byte[] data = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(data, 0, data.Length);

                    Console.WriteLine("Отправлено клиенту: " + response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при работе с клиентом: " + ex.Message);
                }
                finally
                {
                    client.Close();
                }
            }
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
