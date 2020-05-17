using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
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
        byte[] otherPublicKey;
        byte[] otherIV;
        TextBox txtB;
        Task task;
        Form form;
        HC256 other256;
        DH dh;

        public S(TextBox txtB, Form form, ref HC256 other256, ref DH dh, byte[] otherPublicKey, byte[] otherIV)
        {
            this.txtB = txtB;
            this.form = form;
            this.other256 = other256;
            this.otherPublicKey = otherPublicKey;
            this.otherIV = otherIV;
            this.dh = dh;
        }

        public void StartServer()
        {
            task = Task.Factory.StartNew(() =>
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, 8889));
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
                byte[] checkBytes = new byte[3];
                for (int i = 0; i < 3; i++)
                    checkBytes[i] = buffer[i];
                List<byte> bytes = new List<byte>();
                bytes.AddRange(buffer);
                bytes.RemoveRange(0, 3);
                if (Encoding.UTF8.GetString(checkBytes) == "msg")
                {
                    string str = Encoding.UTF8.GetString(bytes.ToArray());
                    AppendTextBox(str);
                }
                else /*(Encoding.UTF8.GetString(checkBytes) == "key")*/
                {
                    List<byte> listB = new List<byte>();
                    int ind = 80;
                    listB.AddRange(buffer);
                    while (true)
                    {
                        if (Check(ind, listB))
                            break;
                        ind += 16;
                    }
                    listB.RemoveRange(ind, listB.Count - ind);
                    //byte[] bts = new byte[bytes.Count];
                    //for (int i = 0; i < bts.Length; i++)
                    //    bts[i] = bytes[i];
                    GetOtherHC256Key(listB.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool Check(int ind,List<byte> lst)
        {
            bool bl = true;
            for (int i = 0; i < 8; i++)
                if (lst[ind + i] != 0)
                    bl = false;
            return bl;
        }

        private void AppendTextBox(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
              {
                  txtB.Text +=Environment.NewLine+"New message: "+Environment.NewLine+text;
              });         
            form.Invoke(invoker);
            Dispose();
            StartServer();
        }

        public void GetOtherHC256Key(byte[] bt)
        {
      
            BigInteger key = new BigInteger(0);
            BigInteger vector = new BigInteger(0);
            BigInteger step = new BigInteger(0);
            byte[] decrypted = dh.Decrypt(otherPublicKey, bt, otherIV);
            byte[] checkKey = new byte[32];
            for (int i = 0; i < 32; i++)
                checkKey[i] = decrypted[i];
            Array.Reverse(checkKey);
            for (int i = 0; i < checkKey.Length; i++)
                key += new BigInteger(checkKey[i]) << (8 * i);
            byte[] checkVector = new byte[32];
            for (int i = 32; i < 64; i++)
                checkVector[i - 32] = decrypted[i];
            Array.Reverse(checkVector);
            for (int i = 0; i < checkVector.Length; i++)
                vector += new BigInteger(checkVector[i]) << (8 * i);
            for (int i = 64; i < decrypted.Length; i++)
                step += new BigInteger(decrypted[i]) << (8 * (i - 64));
            other256.SetKey(key);
            other256.SetVector(vector);
            other256.Synchronization(step);
            Dispose();
            StartServer();
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
