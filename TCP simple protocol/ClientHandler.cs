using System;
using System.IO;
using System.Net.Sockets;

namespace TCP_simple_protocol
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient socket)
        {
            Console.WriteLine(socket.Client.RemoteEndPoint);
            using NetworkStream ns = socket.GetStream();
            using StreamReader reader = new StreamReader(ns);
            using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            bool isRunning = true;
            while (isRunning)
            {
                string message = reader.ReadLine();

                switch (message)
                {
                    case "Random":
                        writer.WriteLine("Input numbers");
                        writer.Flush();
                        writer.WriteLine(RNum(reader.ReadLine()));
                        writer.Flush();
                        break;
                    case "Add":
                        writer.WriteLine("Input numbers");
                        writer.Flush();
                        writer.WriteLine(ANum(reader.ReadLine()));
                        writer.Flush();
                        break;
                    case "Subtract":
                        writer.WriteLine("Input numbers");
                        writer.Flush();
                        writer.WriteLine(SNum(reader.ReadLine()));
                        writer.Flush();
                        break;
                    case "close":
                        writer.WriteLine("connection closed");
                        writer.Flush();
                        isRunning = false;
                        break;
                }
            }
        }

        public static int RNum(string message)
        {
            string[] numbers = message.Split(' ');
            if (numbers.Length != 2 || !int.TryParse(numbers[0], out int min) || !int.TryParse(numbers[1], out int max)) return -1;
            if (min > max) (min, max) = (max, min);
            return new Random().Next(min, max + 1);
        }

        public static int ANum(string message)
        {
            string[] numbers = message.Split(' ');
            int result = 0;
            foreach (string number in numbers) result += int.Parse(number);
            return result;
        }

        public static int SNum(string message)
        {
            string[] numbers = message.Split(' ');
            int result = int.Parse(numbers[0]);
            for (int i = 1; i < numbers.Length; i++) result -= int.Parse(numbers[i]);
            return result;
        }
    }
}
