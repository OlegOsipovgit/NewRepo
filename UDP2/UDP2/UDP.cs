using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP2
{
    public class UDP:IUDP
    {
        public static string remoteAddress; // хост для отправки данных
        public static IPAddress remoteIPAddress;
        public static int remotePort; // порт для отправки данных
        public static int localPort; // локальный порт для прослушивания входящих подключений

        public void SendMessage()
        {
            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки сообщений
            try
            {
                while (true)
                {
                    //string message = Console.ReadLine(); // сообщение для отправки
                    byte[] data = Read();
                    sender.Send(data, data.Length, remoteAddress, remotePort); // отправка
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        public void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(); // UdpClient для получения данных
            remoteIPAddress = IPAddress.Parse(remoteAddress);
            receiver.Connect(remoteIPAddress, remotePort);
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    Write(data);
                    //string message = Encoding.Unicode.GetString(data);
                    //Console.WriteLine("Собеседник: {0}", message);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }
        public static void Write(byte[] data)
        {
            Console.WriteLine("Введите путь к файлу для записи:");
            string path = Console.ReadLine();

            // запись в файл
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                //byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(data, 0, data.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }
        public static byte[] Read ()
        {
            Console.WriteLine("Введите путь к файлу для его отправки:");
            string path = Console.ReadLine();
            // чтение из файла
            using (FileStream fstream = File.OpenRead(path))
            {
                // преобразуем строку в байты
                byte[] data = new byte[fstream.Length];
                // считываем данные
                fstream.Read(data, 0, data.Length);
                // декодируем байты в строку
                //string textFromFile = System.Text.Encoding.Default.GetString(array);
                //Console.WriteLine("Текст из файла: {0}", data);
                return data; 
            }    
        }
    }
}
