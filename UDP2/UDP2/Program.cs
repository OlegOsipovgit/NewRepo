using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP2
{
    class Program
    {            
        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("Введите порт для прослушивания: "); // локальный порт
            UDP.localPort = Int32.Parse(Console.ReadLine());
            Console.Write("Введите удаленный адрес для подключения: ");
            UDP.remoteAddress = Console.ReadLine(); // адрес, к которому мы подключаемся
            Console.Write("Введите порт для подключения: ");
            UDP.remotePort = Int32.Parse(Console.ReadLine()); // порт, к которому мы подключаемся

            UDP udp = new UDP();
            Thread receiveThread = new Thread(new ThreadStart(udp.ReceiveMessage));
            receiveThread.Start();
            udp.SendMessage(); // отправляем сообщение
            
        }
     }
}