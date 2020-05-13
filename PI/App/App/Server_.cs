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
        int port = 10355;
        TextBox txtB;
        Thread serverThread;

        public Server_(TextBox text)
        {
            this.txtB = text;
            
        }

        public void Start()
        {
            serverThread = new Thread(Listen);
            serverThread.Start();
        }

        public void Stop()
        {
            if (client != null)
            {
                client.Close();
                client.Dispose();
            }
            socket.Close();
            socket.Dispose();
            serverThread.Abort();
        }

        private void Listen()
        {
            while (true)
            {
                try
                {
                    client = null;
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, port));
                    socket.Listen(1);
                    client = socket.Accept();
                    byte[] buff = new byte[1024];
                    do
                    {
                        client.Receive(buff);
                        txtB.AppendText(Encoding.Unicode.GetString(buff));
                    }
                    while (client.Available > 0);
                    client.Close();
                    client.Dispose();
                    socket.Close();
                    socket.Dispose();
                }
                catch (ThreadAbortException) { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (client != null)
                    {
                        client.Close();
                        client.Dispose();
                    }
                    socket.Close();
                    socket.Dispose();
                }
            }      
        }
    }
}
