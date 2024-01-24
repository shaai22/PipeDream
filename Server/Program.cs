using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Server
{
    internal class Program
    {
        public static int tr = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Сервер запущен");
            Thread firstThread = new Thread(client);
            firstThread.Start();
        }

        public static void client()
        {
            tr++;
            Console.WriteLine($"Поток {tr}");
            NamedPipeServerStream npss = new NamedPipeServerStream("PipeDream", PipeDirection.InOut, 10);
            Console.WriteLine("Ожидаю подключение...");
            npss.WaitForConnection();

            Thread nextThread = new Thread(client);
            nextThread.Start();

            Console.WriteLine("Клиент подключен");
            StreamReader sr = new StreamReader(npss);
            StreamWriter sw = new StreamWriter(npss);
            sw.AutoFlush = true;

            while (true)
            {
                string message = sr.ReadLine();
                Console.WriteLine($"{tr-1}> {message}");
                sw.WriteLine($"Клиент: {message}");
            }
        }
    }
}
