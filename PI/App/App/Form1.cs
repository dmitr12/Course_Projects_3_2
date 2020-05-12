using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{

    public partial class Form1 : Form
    {
        delegate void Listen(TextBox txtB);

        HC256 serverhc256;
        HC256 clienthc256;
        Client_ client;
        Server_ server;

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

        private void sendTcpBtn_Click(object sender, EventArgs e)
        {
            if(ipTxt.Text.Length==0 || msgSendTcpTxt.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля айпи хоста и введите сообщение");
            }
            else
            {
                client = new Client_(ipTxt.Text);
                client.Send(msgSendTcpTxt.Text);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //server.Stop();
        }
    }
}
