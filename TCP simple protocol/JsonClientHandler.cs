using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCP_simple_protocol
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace TCP_simple_protocol
    {
        public class JsonClientHandler
        {
            private readonly TcpClient _client;
            public JsonClientHandler(TcpClient client) => _client = client;

            public async Task HandleJsonClientAsync()
            {
                Console.WriteLine(_client.Client.RemoteEndPoint);
                using NetworkStream ns = _client.GetStream();
                using StreamReader reader = new StreamReader(ns);
                using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

                string jsonRequest = await reader.ReadLineAsync();
                if (jsonRequest == null) return;

                var request = JsonSerializer.Deserialize<Request>(jsonRequest);
                Console.WriteLine($"Received JSON: {jsonRequest}");
                string result = null, error = null;

                try
                {
                    result = request.Method switch
                    {
                        "Random" => new Random().Next(request.Tal1, request.Tal2 + 1).ToString(),
                        "Add" => (request.Tal1 + request.Tal2).ToString(),
                        "Subtract" => (request.Tal1 - request.Tal2).ToString(),
                        "close" => "connection closed",
                        _ => throw new InvalidOperationException("Invalid command")
                    };
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }

                await writer.WriteLineAsync(JsonSerializer.Serialize(new { result}));
                Console.WriteLine($"Sent JSON: {{ result = {result}, error = {error} }}");
                _client.Close();
            }

            private class Request
            {
                public string Method { get; set; }
                public int Tal1 { get; set; }
                public int Tal2 { get; set; }
            }
        }
    }
}