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
    class TestServ
    {
        Task task;
        Form form;
        TextBox txtB;

        public TestServ(Form form, TextBox txtB)
        {
            this.form = form;
            this.txtB = txtB;
        }

        public void Start()
        {
            task = Task.Factory.StartNew(() =>
            {
                try
                {
                    Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Socket client;
                    tcpListener.Bind(new IPEndPoint(IPAddress.Any, 10001));
                    tcpListener.Listen(1);
                    while (true)
                    {
                        client = tcpListener.Accept();
                        DateTime dt = DateTime.Now;
                        while (client.Available == 0)
                        {
                        }
                        byte[] bts = new byte[client.Available];
                        client.Receive(bts, 0, bts.Length, SocketFlags.None);
                        MethodInvoker invoker = new MethodInvoker(delegate
                        {
                            txtB.Text += Environment.NewLine + "New message: " + Environment.NewLine + Encoding.UTF8.GetString(bts);
                        });
                        form.Invoke(invoker);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public void Stop()
        {
            task.Dispose();
        }
    }
}
