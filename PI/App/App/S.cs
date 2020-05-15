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
    class S:IDisposable
    {
        Socket socket;
        Socket clientSocket;
        private byte[] buffer;
        int port;
        TextBox txtB;
        Task task;
        Form form;

        public S(TextBox txtB, Form form, int port)
        {
            this.txtB = txtB;
            this.form = form;
            this.port = port;
        }

        public void StartServer()
        {
            task = Task.Factory.StartNew(() =>
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, port));
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
                string str = Encoding.Unicode.GetString(buffer);
                AppendTextBox(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AppendTextBox(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
              {
                  txtB.Text +=Environment.NewLine+"New message: "+Environment.NewLine+text;
              });
            
            form.Invoke(invoker);
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
