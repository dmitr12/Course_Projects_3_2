//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace App
//{
//    class ListenerSinch
//    {
//        HC256 hc256;
//        byte[] hc256key=new byte[32];
//        byte[] hc256iv=new byte[32];
//        byte[] position;
//        Socket socket;
//        Socket clientSocket;
//        private byte[] buffer;
//        byte[] clientPublicKey;
//        byte[] clientIV;
//        string ipClient = "";
//        Task task;

//        public ListenerSinch(HC256 hc256, byte[] clientPublicKey, byte[] clientIV)
//        {
//            this.hc256 = hc256;
//            this.clientPublicKey = clientPublicKey;
//        }

//        public void StartServer()
//        {
//            task = Task.Factory.StartNew(() =>
//            {
//                try
//                {
//                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                    socket.Bind(new IPEndPoint(IPAddress.Any, 8891));
//                    socket.Listen(1);
//                    socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.Message);
//                }
//            });
//        }

//        public void StopServer()
//        {
//            socket.Close();
//        }

//        private void AcceptCallback(IAsyncResult ar)
//        {
//            try
//            {
//                clientSocket = socket.EndAccept(ar);
//                ipClient = clientSocket.RemoteEndPoint.ToString().Substring(0, clientSocket.RemoteEndPoint.ToString().IndexOf(':'));
//                buffer = new byte[clientSocket.ReceiveBufferSize];
//                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
//            }
//            catch (ObjectDisposedException) { }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.GetType().ToString());
//            }
//        }

//        private void ReceiveCallback(IAsyncResult ar)
//        {
//            try
//            {
//                int j = 0;
//                DH dh = new DH();
//                byte[] decryptedBytes = dh.Decrypt(clientPublicKey, buffer, clientIV);
//                for (int i = 0; i < 32; i++)
//                    hc256key[i] = decryptedBytes[i];
//                for (int i = 32; i < 64; i++)
//                {
//                    hc256iv[j] = decryptedBytes[i];
//                    j++;
//                }
//                j = 0;
//                position = new byte[decryptedBytes.Length - 64];
//                for(int i = 64; i < decryptedBytes.Length; i++)
//                {
//                    position[j] = decryptedBytes[i];
//                    j++;
//                }
//                BigInteger step = new BigInteger(0);
//                for (int i = 0; i < position.Length; i++)
//                {
//                    step += new BigInteger(position[i]) << (8 * i);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                if (clientSocket != null)
//                {
//                    clientSocket.Dispose();
//                }
//                if (socket != null)
//                {
//                    socket.Dispose();
//                }
//            }
//        }
//    }
//}
