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
        bool isS = true;
        bool isR = false;
        HC256 serverhc256;
        HC256 clienthc256;
        Server_ server=null;
        Client_ client=null;

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
            //server = new Server_(msgTxtFromTcp);
            //server.Start();
        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {
            if (forEncryptTxt.Text.Length == 0)
                MessageBox.Show("Введите текст для шифрования");
            else
            {
                List<byte> afterEncryptBytes = serverhc256.Encrypt(Encoding.Unicode.GetBytes(forEncryptTxt.Text).ToList());
                using(FileStream fs=new FileStream("encrypt.txt", FileMode.Create))
                {
                    fs.Write(afterEncryptBytes.ToArray(), 0, afterEncryptBytes.ToArray().Length);
                }
                using (FileStream fs = File.OpenRead("encrypt.txt"))
                {
                    byte[] bts = new byte[fs.Length];
                    fs.Read(bts, 0, bts.Length);
                    afterEncryptTxt.Text = Encoding.Unicode.GetString(bts);
                    List<byte> realTxtbt = clienthc256.Encrypt(bts.ToList());
                    using(StreamWriter sw=new StreamWriter("decrypt.txt", false, Encoding.Unicode))
                    {
                        sw.Write(Encoding.Unicode.GetString(realTxtbt.ToArray()));
                    }
                    decryptTxt.Text = Encoding.Unicode.GetString(realTxtbt.ToArray());
                }
            }
        }

        private void radioGet_CheckedChanged(object sender, EventArgs e)
        {
            sendMsgBtn.Enabled = false;
            if (radioGet.Checked)
            {
                if (client != null)
                    client.Close();
                if (server == null)
                {
                    server = new Server_(getMsgText);
                    server.Start();
                }
               
            }
        }

        private void radioSend_CheckedChanged(object sender, EventArgs e)
        {
            sendMsgBtn.Enabled = true;
            if (radioSend.Checked)
            {
                if (server != null)
                {
                    server.Stop();
                    server = null;
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
                client.Close();
            if (server != null)
            {
                server.Stop();
            }
        }

        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            if (hostIp.Text.Length == 0 || sendMsgText.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля хоста и сообщения");
            }
            else
            {
                client = new Client_(hostIp.Text);
                client.Send(Encoding.Unicode.GetBytes(sendMsgText.Text));
                client = null;
            }
        }

        //private void radioGet_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        sendMsgBtn.Enabled = false;
        //        if (server != null)
        //        {
        //            server.CloseSocket();
        //            server = null;
        //        }
        //        server = new Server(getMsgText);
        //        Task.Factory.StartNew(() =>
        //        {
        //            while (server.isOkey) { }
        //            server.CloseSocket();
        //            server = new Server(getMsgText);
        //        });
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }        
        //}

        //private void radioSend_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        sendMsgBtn.Enabled = true;
        //        if (server != null)
        //        {
        //            server.CloseSocket();
        //            server = null;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }          
        //}

        //private void sendMsgBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (server != null)
        //        {
        //            server.CloseSocket();
        //            server = null;
        //        }
        //        if (client == null)
        //        {
        //            client = new Client(hostIp.Text);
        //            client.Connect();
        //        }
        //        client.Send(Encoding.Unicode.GetBytes(sendMsgText.Text));
        //        client.Close();
        //        client = null;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void radioSend_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        sendMsgBtn.Enabled = true;
        //        if (server != null)
        //        {
        //            server.CloseSocket();
        //            server = null;
        //        }
        //        isS = !isS;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void radioGet_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (radioGet.Checked)
        //        {
        //            sendMsgBtn.Enabled = false;
        //            if (server != null)
        //            {
        //                server.CloseSocket();
        //                server = null;
        //            }
        //            isR = !isR;
        //            server = new Server(getMsgText);
        //            Task.Factory.StartNew(() =>
        //            {
        //                while (server.isOkey) { }
        //                server.CloseSocket();
        //                server = new Server(getMsgText);
        //            });
        //        }           
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
