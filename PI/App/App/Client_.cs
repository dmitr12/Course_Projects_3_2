using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class Client_
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string ip;
        int port = 10408;
        public Client_(string ip)
        {
            this.ip = ip;
        }

        public void Send(string text)
        {
            try
            {
                byte[] buff = Encoding.UTF8.GetBytes(text);
                socket.Connect(ip, port);
                socket.Send(buff);
                socket.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка: " + ex.Message);
                if (socket.Connected)
                    socket.Close();
            }
        }
    }
}
