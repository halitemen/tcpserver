using System;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpInit.GetIntance().StartConsume();
            TcpInit.GetIntance().DataReceived += Program_DataReceived;
            Console.ReadLine();
        }

        private static void Program_DataReceived(object sender, byte[] e)
        {
            Console.WriteLine(sender);
        }
    }
}
