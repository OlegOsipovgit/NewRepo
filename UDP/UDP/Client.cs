using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;

namespace UDP
{
    public class Client
    {
        [Serializable]
        public class ReceiveFileDetails
        {
            public string FILETYPE = "";
            public long FILESIZE = 0;
        }

        public static ReceiveFileDetails fileDet;

        public static int localPort = 5002;
        public static UdpClient receivingUdpClient = new UdpClient(localPort);
        public static IPEndPoint RemoteIpEndPoint = null;

        public static FileStream fs;
        public static Byte[] receiveBytes = new Byte[0];


        public static void GetFileDetails()
        {
            try
            {
                
                receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
               
                XmlSerializer fileSerializer = new XmlSerializer(typeof(ReceiveFileDetails));
                MemoryStream stream1 = new MemoryStream();

                stream1.Write(receiveBytes, 0, receiveBytes.Length);
                stream1.Position = 0;

                fileDet = (ReceiveFileDetails)fileSerializer.Deserialize(stream1);
            }
            catch (Exception eR)
            {
                Console.WriteLine(eR.ToString());
            }
        }


        public static void ReceiveFile()
        {
            try
            {
                receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                fs = new FileStream("temp." + fileDet.FILETYPE, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Write(receiveBytes, 0, receiveBytes.Length);
                Process.Start(fs.Name);
            }
            catch (Exception eR)
            {
                Console.WriteLine(eR.ToString());
            }
            finally
            {
                fs.Close();
                receivingUdpClient.Close();
            }
        }

        [Serializable]
        public class SendFileDetails
        {
            public string FILETYPE = "";
            public long FILESIZE = 0;
        }

        public static SendFileDetails fileDet1 = new SendFileDetails();

        public static IPAddress remoteIPAddress;
        public const int remotePort = 5002;
        public static UdpClient sender = new UdpClient();
        public static IPEndPoint endPoint;

        public static FileStream fs1;

        public static void SendFileInfo()
        {

           fileDet1.FILETYPE = fs1.Name.Substring((int)fs1.Name.Length - 3, 3);

           fileDet1.FILESIZE = fs1.Length;

            XmlSerializer fileSerializer = new XmlSerializer(typeof(ReceiveFileDetails));
            MemoryStream stream = new MemoryStream();

           fileSerializer.Serialize(stream, fileDet1);

            stream.Position = 0;
            Byte[] bytes = new Byte[stream.Length];
            stream.Read(bytes, 0, Convert.ToInt32(stream.Length));

            sender.Send(bytes, bytes.Length, endPoint);
            stream.Close();

        }


        public static void SendFile()
        {
            Byte[] bytes = new Byte[fs1.Length];
            fs1.Read(bytes, 0, bytes.Length);

            try
            {
               sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception eR)
            {
                Console.WriteLine(eR.ToString());
            }
            finally
            {
                fs1.Close();
                sender.Close();
            }
        }

    }
}
