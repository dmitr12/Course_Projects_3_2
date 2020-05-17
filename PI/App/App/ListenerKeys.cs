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
    class ListenerKeys : IDisposable
    {
        Socket socket;
        Socket clientSocket;
        private byte[] buffer;
        byte[] publicKey;
        byte[] iv;
        string ipClient = "";
        TextBox txtB;
        Task task;
        Form form;

        public ListenerKeys(ref byte[] publicKey, ref byte[] iv,Form form, TextBox txtB)
        {
            this.publicKey = publicKey;
            this.iv = iv;
            this.form = form;
            this.txtB = txtB;
        }

        public void StartServer()
        {
            task = Task.Factory.StartNew(() =>
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, 8890));
                    socket.Listen(1);
                    socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public void StopServer()
        {
            socket.Close();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket = socket.EndAccept(ar);
                ipClient = clientSocket.RemoteEndPoint.ToString().Substring(0, clientSocket.RemoteEndPoint.ToString().IndexOf(':'));
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int j = 0;
                for (int i = 0; i < 140; i++)
                    publicKey[i] = buffer[i];
                for(int i=140;i<156;i++)
                {
                    iv[j] = buffer[i];
                    j++;
                }
                MethodInvoker invoker = new MethodInvoker(delegate
                {
                    txtB.Text += Environment.NewLine + ipClient + " прислал свои открытые ключи"+Environment.NewLine;
                });
                form.Invoke(invoker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (clientSocket != null)
                {
                    clientSocket.Dispose();
                }
                if (socket != null)
                {
                    socket.Dispose();
                }
            }
        }
    }
}
