using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{

    public partial class Form1 : Form
    {
        HC256 hc256;
        HC256 other256;

        CheckClass chCl = new CheckClass();

        S serv = null;
        Client_ client = null;
        ListenerKeys lk;
        DH dh = new DH();
        byte[] otherPublicKey = new byte[140];
        byte[] otherIV = new byte[16];

        public Form1()
        {
            hc256 = new HC256();
            other256 = new HC256();
            hc256.GenerateKey();
            Thread.Sleep(1000);
            hc256.GenerateVector();
            hc256.InitializationProcess();
            InitializeComponent();
            getMsgText.ReadOnly = true;
            exchangeKeysTxtB.ReadOnly = true;
            lk = new ListenerKeys(ref otherPublicKey, ref otherIV, this, exchangeKeysTxtB);
            lk.StartServer();

            //server = new Server_(msgTxtFromTcp);
            //server.Start();
        }

        //private void encryptBtn_Click(object sender, EventArgs e)
        //{
        //    if (forEncryptTxt.Text.Length == 0)
        //        MessageBox.Show("Введите текст для шифрования");
        //    else
        //    {
        //        List<byte> afterEncryptBytes = serverhc256.Encrypt(Encoding.Unicode.GetBytes(forEncryptTxt.Text).ToList());
        //        using(FileStream fs=new FileStream("encrypt.txt", FileMode.Create))
        //        {
        //            fs.Write(afterEncryptBytes.ToArray(), 0, afterEncryptBytes.ToArray().Length);
        //        }
        //        using (FileStream fs = File.OpenRead("encrypt.txt"))
        //        {
        //            byte[] bts = new byte[fs.Length];
        //            fs.Read(bts, 0, bts.Length);
        //            afterEncryptTxt.Text = Encoding.Unicode.GetString(bts);
        //            List<byte> realTxtbt = clienthc256.Encrypt(bts.ToList());
        //            using(StreamWriter sw=new StreamWriter("decrypt.txt", false, Encoding.Unicode))
        //            {
        //                sw.Write(Encoding.Unicode.GetString(realTxtbt.ToArray()));
        //            }
        //            decryptTxt.Text = Encoding.Unicode.GetString(realTxtbt.ToArray());
        //        }
        //    }
        //}

        private void radioGet_CheckedChanged(object sender, EventArgs e)
        {
            sendMsgBtn.Enabled = false;
            if (radioGet.Checked)
            {
                serv = new S(getMsgText, this, ref other256, ref dh, otherPublicKey, otherIV);
                serv.StartServer();
            }
        }

        private void radioSend_CheckedChanged(object sender, EventArgs e)
        {
            sendMsgBtn.Enabled = true;
            if (radioSend.Checked)
            {
                serv.Dispose();
                serv = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!radioSend.Checked)
            {
                serv.Dispose();
                serv = null;
            }
        }

        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            if (hostIp.Text.Length == 0 && sendMsgText.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля хоста и сообщения");
            }
            else
            {
                client = new Client_(hostIp.Text, 10431);              
                List<byte> sendBytes = new List<byte>();
                sendBytes.AddRange(Encoding.UTF8.GetBytes("msg"));
                List<byte> listBts = new List<byte>();
                listBts.AddRange(Encoding.UTF8.GetBytes(sendMsgText.Text));
                sendBytes.AddRange(hc256.Encrypt(listBts));
                client.Send(sendBytes.ToArray());
                client.Close();
            }
        }

        private void exchangeKeysBtn_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] msg = new byte[156];
                int j = 0;
                for (int i = 0; i < dh.PublicKey.Length; i++)
                    msg[i] = dh.PublicKey[i];
                for (int i = 140; i < 140 + dh.IV.Length; i++)
                {
                    msg[i] = dh.IV[j];
                    j++;
                }
                exchangeKeysBtn.Enabled = false;
                client = new Client_(hostIp.Text, 8890);
                lk.Dispose();
                lk = null;
                client.Send(msg);
                client.Close();
                exchangeKeysTxtB.Text += Environment.NewLine + "Ключи успешно отосланы " + hostIp.Text + Environment.NewLine;
                lk = new ListenerKeys(ref otherPublicKey, ref otherIV, this, exchangeKeysTxtB);
                lk.StartServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (lk == null)
                {
                    lk = new ListenerKeys(ref otherPublicKey, ref otherIV, this, exchangeKeysTxtB);
                    lk.StartServer();
                }
                exchangeKeysBtn.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My hc256 key: "+hc256.key[5]);
            MessageBox.Show("Other hc256key: " + other256.key[5]);
           
        }

        private void btnSynch_Click(object sender, EventArgs e)
        {
            try
            {
                client = new Client_(hostIp.Text, 10431);
                List<byte> keyBytes = new List<byte>();
                List<byte> fBt = new List<byte>();
                fBt.AddRange(Encoding.UTF8.GetBytes("key"));
                keyBytes.AddRange(hc256.GetBt(hc256.Key, 256));
                keyBytes.AddRange(hc256.GetBt(hc256.Vector, 256));
                keyBytes.AddRange(hc256.startEncrypt.ToByteArray());
                byte[] encryptKeyBytes = dh.Encrypt(otherPublicKey, keyBytes.ToArray());
                fBt.AddRange(encryptKeyBytes);
                client.Send(fBt.ToArray());
                client.Close();
            }      
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(hc256.Key.ToString());
            MessageBox.Show(hc256.Vector.ToString());
        }
    }
}
