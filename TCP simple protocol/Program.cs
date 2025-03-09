using System.Net;
using System.Net.Sockets;

namespace TCP_simple_protocol
{
    public class Program
    {
        public static void Main()
        {
            Task.Run(() => new TcpServer().TcpServerStart());
            Task.Run(() => new TcpServerJson().JsonTcpServerStart());
            Console.WriteLine("Servers are running. Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
