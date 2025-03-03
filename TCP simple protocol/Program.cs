using System.Net;
using System.Net.Sockets;

namespace TCP_simple_protocol
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TCP Server");

            int port = 7;
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => ClientHandler.HandleClient(client));
            }
            listener.Stop();
        }
    }
}
