using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TCP_simple_protocol.TCP_simple_protocol;

namespace TCP_simple_protocol
{
    public class TcpServerJson
    {
        public void JsonTcpServerStart()
        {
            Console.WriteLine("JSON Server started...");
            int port = 8;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Task.Run(() => new JsonClientHandler(client).HandleJsonClientAsync());
            }
        }
    }
}
