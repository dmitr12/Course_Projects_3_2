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
        HC256 hc256;
        DH dh;
        Button btn;
        TextBox infoK;  

        public TestServ(Form form, TextBox txtB,HC256 hc256, ref DH dh, Button requestKey, TextBox infoKey)
        {
            this.form = form;
            this.txtB = txtB;
            this.hc256 = hc256;
            other256 = new HC256();
            this.dh = dh;
            btn = requestKey;
            infoK = infoKey;
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
                        if(Encoding.UTF8.GetString(checkBytes) == "rsq")
                        {                          
                            RequestForSecretKey(bytes.ToArray(), client.RemoteEndPoint.ToString().Substring(0, client.RemoteEndPoint.ToString().IndexOf(":")));
                        }
                        if(Encoding.UTF8.GetString(checkBytes) == "gsq")
                        {
                            ResponseSecretKey(bytes.ToArray(), client.RemoteEndPoint.ToString().Substring(0, client.RemoteEndPoint.ToString().IndexOf(":")));
                        }
                        if (Encoding.UTF8.GetString(checkBytes) == "msg")
                        {
                            int i = 0;
                            BigInteger st = new BigInteger(0);
                            while (true)
                            {
                                st += new BigInteger(bytes[i]) << (8 * i);
                                if(Encoding.UTF8.GetString(new byte[] { bytes[i+1], bytes[i + 2], bytes[i + 3] }) == "msg")
                                {
                                    bytes.RemoveRange(0, i + 4);
                                    break;
                                }
                                i++;
                            }
                            AppendTextBox(bytes, st, client.RemoteEndPoint.ToString().Substring(0, client.RemoteEndPoint.ToString().IndexOf(":")));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public void RequestForSecretKey(byte[] bts, string ip)
        {
            Client_ client = new Client_(ip, 10001);
            try
            {
                List<byte> keyBytes = new List<byte>();
                List<byte> fBt = new List<byte>();
                fBt.AddRange(Encoding.UTF8.GetBytes("gsq"));
                fBt.AddRange(dh.PublicKey);
                fBt.AddRange(dh.IV);
                keyBytes.AddRange(hc256.GetBt(hc256.Key, 256));
                keyBytes.AddRange(hc256.GetBt(hc256.Vector, 256));
                byte[] encryptKeyBytes = dh.Encrypt(bts, keyBytes.ToArray());
                fBt.AddRange(encryptKeyBytes);
                client.Send(fBt.ToArray());
                client.Close();
                MessageBox.Show("Пользователю " + ip + " послан секретный ключ, запросите его секретный ключ для дальнейшего успешного обмена");
            }
            catch(Exception ex)
            {
                client.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void ResponseSecretKey(byte[] bts, string ip)
        {
            BigInteger key = new BigInteger(0);
            BigInteger vector = new BigInteger(0);
            byte[] othPublicKey = new byte[140];
            byte[] othIV = new byte[16];
            for (int i = 0; i < 140; i++)
                othPublicKey[i] = bts[i];
            for (int i = 140; i < 156; i++)
                othIV[i - 140] = bts[i];
            List<byte> listB = new List<byte>();
            listB.AddRange(bts);
            listB.RemoveRange(0, 156);
            byte[] encryptMsg = listB.ToArray();
            byte[] decrypted = dh.Decrypt(othPublicKey, encryptMsg, othIV);
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
            if (other256.Key != key || other256.Vector != vector)
            {
                other256.Step = 0;
                other256.SetKey(key);
                other256.SetVector(vector);
                other256.InitializationProcess();
            }
            MethodInvoker invoker = new MethodInvoker(delegate
            {
                infoK.Text += Environment.NewLine + "Пользователь " + ip + " прислал секретный ключ: " + key;
                infoK.Text += Environment.NewLine + "Пользователь " + ip + " прислал секретный вектор: " + vector+Environment.NewLine;
            });
            form.Invoke(invoker);
        }

        private void AppendTextBox(List<byte> encryptedMsg, BigInteger step, string ip)
        {
            if (infoK.Text.Length == 0)
            {
                MessageBox.Show("Вам пришло сообщение от " + ip + ", но у вас нет секретного ключа");
            }
            else
            {
                other256.Synchronization(step);
                List<byte> listDecrypted = other256.Encrypt(encryptedMsg);
                MethodInvoker invoker = new MethodInvoker(delegate
                {
                    txtB.Text += Environment.NewLine + $"New message from {ip} : " + Environment.NewLine + Encoding.UTF8.GetString(listDecrypted.ToArray());
                });
                form.Invoke(invoker);
            }     
        }
    }
}
