using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace TestHC
{
    class Program
    {
        public static string GetBinNWord(uint x, int n)
        {
            string bin = "";
            while (x != 1)
            {
                bin = (x % 2).ToString() + bin;
                x /= 2;
            }
            bin = "1" + bin;
            while (bin.Length != n)
                bin = "0" + bin;
            return bin;
        }

        public static byte[] GetBytesFromKeyStream(uint keyStream)
        {
            return new byte[]
            {
                (byte)(keyStream >> 24), (byte)(keyStream >> 16),
                (byte)(keyStream >> 8), (byte)keyStream
            };
        }

        static void Main(string[] args)
        {
            //example
            HC256 pr = new HC256();
            uint[] key = new uint[8];
            uint[] iv = new uint[8];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = 0;
                iv[i] = 0;
            }
            pr.InitializationProcess(key, iv);
            List<uint> keyStream = pr.GenerateKeyStream(16);
            List<byte> word = new List<byte>();
            word.AddRange(Encoding.Unicode.GetBytes("Привет, how are ты?)"));
            List<byte> encrypted = pr.Encrypt( word , keyStream);
            List<byte> decrypted = pr.Decrypt(encrypted, keyStream);
            encrypted = pr.Encrypt(decrypted, keyStream);
            decrypted = pr.Decrypt(encrypted, keyStream);
            //foreach (var t in keyStream)
            //{
            //    WriteLine("32-bit:");
            //    WriteLine(t + "\n/////////////////////////////////////////");
            //    foreach (var b in GetBytesFromKeyStream(t))
            //        WriteLine(b);

            //}
            //WriteLine("\n\nKeystreambytes:");
            //foreach (var t in pr.GetBytesFromKeyStream(keyStream))
            //    WriteLine(t);
        }
    }
}
