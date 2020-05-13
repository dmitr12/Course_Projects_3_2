using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class Server
    {
        Socket socket;
        static int port = 10355;
        TextBox textB;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        Socket client = null;
        public bool isOkey = true;

        public Server(TextBox textB)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.textB = textB;
                socket.Bind(endPoint);
                socket.Listen(1);
                var compl = new SocketAsyncEventArgs();
                compl.Completed += Compl_Completed;
                socket.AcceptAsync(compl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Compl_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                var compl = (SocketAsyncEventArgs)e;
                MessageBox.Show("Сервер: Успешное подключение");
                if (compl.SocketError == SocketError.Success)
                {
                    client = compl.AcceptSocket;
                }
                if (client != null)
                {
                    byte[] buff = new byte[1024];
                    string gettext = string.Empty;
                    do
                    {
                        client.Receive(buff);
                        gettext += Encoding.Unicode.GetString(buff);
                    } while (client.Available > 0);

                    textB.Invoke(new Action(() =>
                    {
                        textB.AppendText(gettext);
                    }));
                    isOkey = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CloseSocket()
        {
            try
            {
                socket.Close();
                socket.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
