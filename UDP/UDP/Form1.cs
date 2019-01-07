using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace UDP
{
    public partial class Form1 : Form
    {
        int enter = 0;
        public void method()
        {
            try
            {
                MessageBox.Show("Введите удаленный IP-адрес в поле и нажмите enter");
                if (enter!=1) {
                    System.Threading.Thread.Sleep(50000);
                }
                while (enter != 1) { continue; }
                Client.remoteIPAddress = IPAddress.Parse(textBox1.Text.ToString());//"127.0.0.1");
                Client.endPoint = new IPEndPoint(Client.remoteIPAddress, Client.remotePort);
                textBox1.Clear();
                enter = 0;

                MessageBox.Show("Введите путь к файлу и его имя в поле и нажмите enter");
                while (enter != 1) { continue; }
                Client.fs = new FileStream(textBox1.Text.ToString(), FileMode.Open, FileAccess.Read);
                textBox1.Clear();
                enter = 0;

                if (Client.fs.Length > 8192)
                {
                    MessageBox.Show("Файл должен весить меньше 8кБ");
                    Client.sender.Close();
                    Client.fs.Close();
                    return;
                }

                Client.SendFileInfo();
                MessageBox.Show("Отправка файла размером " + Client.fs1.Length + " байт");

                Thread.Sleep(2000);

                Client.SendFile();
                MessageBox.Show("Файл успешно отправлен.");
                Console.ReadLine();
            }
            catch (Exception eR)
            {
                Console.WriteLine(eR.ToString());
            }

        }
        public Form1()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Client.GetFileDetails();
            MessageBox.Show("----Информация о файле получена!" + "Получен файл типа ." + Client.fileDet.FILETYPE +
                    " имеющий размер " + Client.fileDet.FILESIZE.ToString() + " байт");
            MessageBox.Show("-------Открытие файла------");
            Client.ReceiveFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            method();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {                                 
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            enter = 1;
        }
    }
}
