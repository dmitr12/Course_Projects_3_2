using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
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
        DH dh = new DH();
        TestServ testServ;

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
            testServ = new TestServ(this, getMsgText,hc256, ref dh, requestSecretKeyBtn, exchangeKeysTxtB);
            testServ.Start();
        }

        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            if (hostIp.Text.Length == 0 && sendMsgText.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля хоста и сообщения");
            }
            else
            {
                Client_ client = new Client_(hostIp.Text, 10001);              
                List<byte> sendBytes = new List<byte>();
                sendBytes.AddRange(Encoding.UTF8.GetBytes("msg"));
                sendBytes.AddRange(hc256.Step.ToByteArray());
                sendBytes.AddRange(Encoding.UTF8.GetBytes("msg"));
                List<byte> listBts = new List<byte>();
                listBts.AddRange(Encoding.UTF8.GetBytes(sendMsgText.Text));
                sendBytes.AddRange(hc256.Encrypt(listBts));
                client.Send(sendBytes.ToArray());
                client.Close();
                Thread.Sleep(2000);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Key: " + hc256.Key.ToString());
            MessageBox.Show("Vector: " + hc256.Vector.ToString());

        }
        private void requestSecretKeyBtn_Click(object sender, EventArgs e)
        {
            if (hostIp.Text.Length == 0)
                MessageBox.Show("Заполните поле адреса");
            else
            {
                Client_ client = new Client_(hostIp.Text, 10001);
                List<byte> sendBytes = new List<byte>();
                sendBytes.AddRange(Encoding.UTF8.GetBytes("rsq"));
                sendBytes.AddRange(dh.PublicKey);
                client.Send(sendBytes.ToArray());
                client.Close();
                Thread.Sleep(1000);
            }
        }
    }
}
