using BeetleX;
using BeetleX.Clients;
using System;
using System.Net;
using System.Threading;

namespace TcpClientTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            TcpClient client = SocketFactory.CreateClient<TcpClient>("127.0.0.1", 18000);
            client.LocalEndPoint = new System.Net.IPEndPoint(IPAddress.Any, 9022);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            while (true)
            {
                Console.Write("Enter Name:");
                var line = Console.ReadLine();
                line = line.Trim();
                client.Stream.ToPipeStream().WriteLine(line);
                client.Stream.Flush();
                var reader = client.Receive();
                line = reader.ReadLine();
                Console.WriteLine($"{DateTime.Now} {line}");
            }

        }
    }
}
