using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class Server_
    {
        Socket socket;
        Socket client;
        int port=10408;
        TextBox text;
        Thread listener;
        public Server_(TextBox text)
        {
            this.text = text;
        }

        public void Start()
        {
            listener = new Thread(Listen);
            listener.Start();
        }

        public void Stop()
        {
            socket.Close();
            listener.Abort();
        }

        public void Listen()
        {
            while (true)
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, port));
                    socket.Listen(1);
                    client = socket.Accept();
                    byte[] buff = new byte[1024];
                    text.Text += "New message: "+Environment.NewLine;
                    do
                    {
                        client.Receive(buff);
                        text.AppendText(Encoding.UTF8.GetString(buff));
                    }
                    while (client.Available > 0);
                    text.Text += Environment.NewLine+Environment.NewLine;
                    client.Close();
                    socket.Close();
                }
                catch (ThreadAbortException)
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: " + ex.Message);
                }
            }         
        }
    }
}
