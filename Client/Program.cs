using System;
using System.IO;
using System.IO.Pipes;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Клиент запущен");
            NamedPipeClientStream npcs = new NamedPipeClientStream("PipeDream");
            Console.WriteLine("Подключаюсь к серверу...");
            npcs.Connect();
            Console.WriteLine("Подключен!");
            StreamWriter sw = new StreamWriter(npcs);
            StreamReader sr = new StreamReader(npcs);
            sw.AutoFlush = true;

            while (true)
            {
                Console.Write("> ");
                sw.WriteLine(Console.ReadLine());
                Console.WriteLine($"< {sr.ReadLine()}");
            }
        }
    }
}
