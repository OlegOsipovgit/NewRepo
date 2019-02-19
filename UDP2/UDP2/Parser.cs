using System;
using System.Collections.Generic;
using System.Text;

namespace UDP2
{
    public static class Parser
    {
        static byte[] myip;
        static byte[] destinationip;
        static byte[] cipher;
        static byte[] counter;
        static byte[] length;
        static byte[] frequency1;
        static byte[] frequency2;
        static byte[] amplification;
        static byte[] spectrum;
        static byte[] currentpocket;
              
        
        public static byte[] CreateSendingDatagram(byte myip, byte destinationip, byte cipher, byte counter, byte length, byte frequency1, byte frequency2, byte amplification)
        {
            byte[] datagram = { myip, destinationip, cipher, counter, length, frequency1, frequency2, amplification };
            return datagram;
        }
        public static byte[] ChooseDatagramElement(byte[] datagram, string nameofelement, string datagramtype)
        {
            if (datagramtype.Equals("query")) {
                switch (nameofelement)
                {
                    case "myip":
                        Array.Copy(datagram, 0, myip, 0, 1);
                        return myip;
                        break;
                    case "destinationip":
                        Array.Copy(datagram, 1, destinationip, 0, 1);
                        return destinationip;
                        break;
                    case "cipher":
                        Array.Copy(datagram, 2, cipher, 0, 1);
                        return cipher;
                        break;
                    case "counter":
                        Array.Copy(datagram, 3, counter, 0, 1);
                        return cipher;
                        break;
                    case "length":
                        Array.Copy(datagram, 4, length, 0, 2);
                        return cipher;
                        break;
                    case "frequency1":
                        Array.Copy(datagram, 6, frequency1, 0, 1);
                        return frequency1;
                    case "frequency2":
                        Array.Copy(datagram, 7, frequency2, 0, 1);
                        return frequency2;
                        break;
                    case "amplification":
                        Array.Copy(datagram, 8, amplification, 0, 1);
                        return amplification;
                        break;
                    default:
                        Console.WriteLine("Введите правильное название элемента");
                        return spectrum;
                        break;
                }
            }
         
        
            if (datagramtype.Equals("responce"))
            {
                switch (nameofelement)
                {
                    case "myip":
                        Array.Copy(datagram, 0, myip, 0, 1);
                        return myip;
                        break;
                    case "destinationip":
                        Array.Copy(datagram, 1, destinationip, 0, 1);
                        return destinationip;
                        break;
                    case "cipher":
                        Array.Copy(datagram, 2, cipher, 0, 1);
                        return cipher;
                        break;
                    case "counter":
                        Array.Copy(datagram, 3, counter, 0, 1);
                        return cipher;
                        break;
                    case "length":
                        Array.Copy(datagram, 4, length, 0, 2);
                        return cipher;
                        break;
                    case "errorcode":
                        Array.Copy(datagram, 6, frequency1, 0, 1);
                        return frequency1;
                    case "pocketsnumber":
                        Array.Copy(datagram, 7, frequency2, 0, 1);
                        return frequency2;
                        break;
                    case "currentpocket":
                        Array.Copy(datagram, 8, currentpocket, 0, 1);
                        return currentpocket;
                        break;
                    case "frequency1":
                        Array.Copy(datagram, 9, frequency1, 0, 1);
                        return frequency1;
                    case "frequency2":
                        Array.Copy(datagram, 10, frequency2, 0, 1);
                        return frequency2;
                        break;
                    case "amplification":
                        Array.Copy(datagram, 11, amplification, 0, 1);
                        return amplification;
                        break;
                    case "spectrum":
                        Array.Copy(datagram, 12, spectrum, 0, 8203);
                        return spectrum;
                        break;
                    default:
                        Console.WriteLine("Введите правильное название элемента");
                        return spectrum;
                        break;
                }
            }
            return null;
        }

        
    }
}
//датаграмма, что в ней записывается битами изначально, а что переводиться из других типов