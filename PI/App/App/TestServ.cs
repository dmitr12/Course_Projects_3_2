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
    class TestServ
    {
        Task task;
        Form form;
        TextBox txtB;
        HC256 other256;
        DH dh;
        byte[] otherPublicKey;
        byte[] otherIV;

        public TestServ(Form form, TextBox txtB, /*ref HC256 other256,*/ ref DH dh, ref byte[] otherPublicKey, ref byte[] otherIV)
        {
            this.form = form;
            this.txtB = txtB;
            //this.other256 = other256;
            other256 = new HC256();
            this.dh = dh;
            this.otherPublicKey = otherPublicKey;
            this.otherIV = otherIV;
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
                        byte[] checkBytes = new byte[3];
                        for (int i = 0; i < 3; i++)
                            checkBytes[i] = bts[i];
                        List<byte> bytes = new List<byte>();
                        bytes.AddRange(bts);
                        bytes.RemoveRange(0, 3);
                        if (Encoding.UTF8.GetString(checkBytes) == "msg")
                        {
                            AppendTextBox(bytes);
                        }
                        if (Encoding.UTF8.GetString(checkBytes) == "key")
                        {
                            GetOtherHC256Key(bytes.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        private void AppendTextBox(List<byte> encryptedMsg)
        {
            List<byte> listDecrypted = other256.Encrypt(encryptedMsg);
            MethodInvoker invoker = new MethodInvoker(delegate
            {
                txtB.Text += Environment.NewLine + "New message: " + Environment.NewLine + Encoding.UTF8.GetString(listDecrypted.ToArray());
            });
            form.Invoke(invoker);
        }

        public void GetOtherHC256Key(byte[] bt)
        {
            BigInteger key = new BigInteger(0);
            BigInteger vector = new BigInteger(0);
            BigInteger step = new BigInteger(0);
            //Добавляю
            byte[] oldKey = dh.PublicKey;
            byte[] oldIV = dh.IV;
            //
            byte[] decrypted = dh.Decrypt(otherPublicKey, bt, otherIV);
            //Добавляю
            dh.PublicKey=oldKey;
            dh.IV=oldIV;
            //
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

            //other256 = new HC256();

            if (other256.key == null || other256.iv == null)
            {
                other256.SetKey(key);
                other256.SetVector(vector);
                other256.InitializationProcess();
            }
            other256.Synchronization(step);
        }
    }
}
