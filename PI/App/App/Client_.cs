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
        Socket socket;
        string ip;
        int port;

        public Client_(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Connect()
        {
            socket.Connect(ip, port);
        }

        public void Send(byte[] bytesForSend)
        {
            Connect();
            socket.Send(bytesForSend);
            Close();
        }

        public void Close()
        {
            try
            {
                socket.Close();
                socket.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
