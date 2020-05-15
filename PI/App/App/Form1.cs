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
        HC256 serverhc256;
        HC256 clienthc256;

        S serv=null;
        Client_ client = null;
        ListenerKeys lk;
        DH dh = new DH();
        byte[] otherPublicKey = new byte[140];
        byte[] otherIV = new byte[16];

        public Form1()
        {
            serverhc256 = new HC256();
            clienthc256 = new HC256();
            serverhc256.GenerateKey();
            serverhc256.GenerateVector();
            serverhc256.InitializationProcess();
            clienthc256.SetKey(serverhc256.Key);
            clienthc256.SetVector(serverhc256.Vector);
            clienthc256.InitializationProcess();
            InitializeComponent();
            getMsgText.ReadOnly = true;
            exchangeKeysTxtB.ReadOnly = true;
            lk = new ListenerKeys(ref otherPublicKey, ref otherIV, this, exchangeKeysTxtB, 8890);
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
                serv = new S(getMsgText, this, 8889);
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
            if(hostIp.Text.Length==0 && sendMsgText.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля хоста и сообщеия");
            }
            else
            {
                client = new Client_(hostIp.Text, 8889);
                client.Send(Encoding.Unicode.GetBytes(sendMsgText.Text));
                client.Close();
            }     
        }

        private void getMsgText_TextChanged(object sender, EventArgs e)
        {
            if (radioGet.Checked)
            {
                if (serv != null)
                {
                    serv.Dispose();
                    serv.StartServer();
                }
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
                client = new Client_(hostIp.Text, 8890);
                lk.Dispose();
                lk = null;
                client.Send(msg);
                client.Close();
                exchangeKeysTxtB.Text += Environment.NewLine + "Ключи успешно отосланы " + hostIp.Text + Environment.NewLine;
                lk = new ListenerKeys(ref otherPublicKey, ref otherIV, this, exchangeKeysTxtB, 8890);
                lk.StartServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Close();
                if(lk==null)
                {
                    lk = new ListenerKeys(ref otherPublicKey, ref otherIV, this, exchangeKeysTxtB, 8890);
                    lk.StartServer();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My publicKey: " + dh.PublicKey[139]);
            MessageBox.Show("My Iv: " + dh.IV[15]);
            MessageBox.Show("My otherPublicKey: " + otherPublicKey[139]);
            MessageBox.Show("My otherIV: " + otherIV[15]);
        }
    }
}
