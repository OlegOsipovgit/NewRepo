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
        Client client = new Client();
        public Form1()
        {
            InitializeComponent();           
        }

        public void button1_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("----Информация о файле получена!" + "Получен файл типа ." + Client.fileDet.FILETYPE +
                    " имеющий размер " + Client.fileDet.FILESIZE.ToString() + " байт");
            MessageBox.Show("-------Открытие файла------");
         }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Введите IP-адрес адресата в поле и нажмите enter");
            
            client.ReceiveFile();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    client.remoteIPAddress = IPAddress.Parse(textBox1.Text.ToString());
                    client.endPoint = new IPEndPoint(client.remoteIPAddress, Client.remotePort);

                    MessageBox.Show("Введите путь к файлу и его имя в поле и нажмите enter");
                }

            }
            catch (Exception eR)
            {
                Console.WriteLine(eR.ToString());
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Client.fs = new FileStream(textBox1.Text.ToString(), FileMode.Open, FileAccess.Read);

                if (Client.fs.Length > 8192)
                {
                    MessageBox.Show("Файл должен весить меньше 8кБ");
                    Client.sender.Close();
                    Client.fs.Close();
                    return;
                }

                client.SendFileInfo();
                MessageBox.Show("Отправка файла размером " + Client.fs1.Length + " байт");

                Thread.Sleep(2000);

                client.SendFile();
                MessageBox.Show("Файл успешно отправлен.");
                Console.ReadLine();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
