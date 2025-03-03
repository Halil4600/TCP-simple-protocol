using System;
using System.IO;
using System.Net.Sockets;

namespace TCP_simple_protocol
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient client)
        {
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream);
            using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            try
            {
                // Trin 1: Modtag kommando fra klienten (Random, Add, Subtract)
                string? message = reader.ReadLine();
                Console.WriteLine($"Received command: {message}");

                if (message == null)
                {
                    return;
                }

                // Trin 2: Send besked til klienten for at anmode om tal
                writer.WriteLine("2: Input numbers");

                // Trin 3: Modtag de to tal fra klienten
                string? numbers = reader.ReadLine();
                if (numbers == null)
                {
                    return;
                }

                Console.WriteLine($"Received numbers: {numbers}");
                string[] parts = numbers.Split(' ');

                if (parts.Length != 2 || !int.TryParse(parts[0], out int num1) || !int.TryParse(parts[1], out int num2))
                {
                    writer.WriteLine("Error: Invalid input");
                    return;
                }

                int result = 0;

                if (message == "Random")
                {
                    // Trin 4: Generer et tilfældigt tal i intervallet
                    Random rnd = new Random();
                    result = rnd.Next(num1, num2 + 1);
                }
                else if (message == "Add")
                {
                    // Trin 4: Beregn summen af de to tal
                    result = num1 + num2;
                }
                else if (message == "Subtract")
                {
                    // Trin 4: Beregn differensen (tal1 - tal2, rækkefølge er vigtig)
                    result = num1 - num2;
                }
                else
                {
                    writer.WriteLine("Error: Unknown command");
                    return;
                }

                // Trin 4: Send resultatet tilbage til klienten
                writer.WriteLine($"4: {result}");
                Console.WriteLine($"Sent result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }  
                client.Close(); // Luk forbindelsen til klienten           
        }
    }
}
