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
        int bufSz;

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
                    socket.Bind(new IPEndPoint(IPAddress.Any, 10431));
                    socket.Listen(2);
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
                bufSz = clientSocket.Available;
                buffer = new byte[bufSz];
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
                if(buffer.Length==0)
                {
                    buffer = new byte[bufSz];
                    do
                    {
                        socket.Receive(buffer);
                    } while (clientSocket.Available > 0);
                }
                byte[] checkBytes = new byte[3];
                for (int i = 0; i < 3; i++)
                    checkBytes[i] = buffer[i];
                List<byte> bytes = new List<byte>();
                bytes.AddRange(buffer);
                bytes.RemoveRange(0, 3);
                if (Encoding.UTF8.GetString(checkBytes) == "msg")
                {
                    AppendTextBox(bytes);
                }
                if(Encoding.UTF8.GetString(checkBytes) == "key")
                {                  
                    GetOtherHC256Key(bytes.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AppendTextBox(List<byte> encryptedMsg)
        {
            List<byte> listDecrypted = other256.Encrypt(encryptedMsg);
            MethodInvoker invoker = new MethodInvoker(delegate
              {
                  txtB.Text +=Environment.NewLine+"New message: "+Environment.NewLine+Encoding.UTF8.GetString(listDecrypted.ToArray());
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
            if(other256.key==null || other256.iv == null)
            {
                other256.SetKey(key);
                other256.SetVector(vector);
                other256.InitializationProcess();
            }      
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
