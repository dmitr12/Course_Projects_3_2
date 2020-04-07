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
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            WriteLine("\x89f4");
            HC256 pr = new HC256();
            uint[] key = new uint[8];
            uint[] iv = new uint[8];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = 0;
                iv[i] = 0;
            }
            pr.InitializationProcess(key, iv);
            while (true)
            {
                WriteLine("Введите текст");
                string s = ReadLine();
                List<uint> keyStream = pr.GenerateKeyStream((uint)Math.Ceiling((decimal)(s.Length * 16) / 32));
                WriteLine("KeyStream:");
                foreach (var k in keyStream)
                    WriteLine(k);
                List<byte> word = new List<byte>();
                word.AddRange(Encoding.Unicode.GetBytes(s));
                List<byte> encrypted = pr.Encrypt(word, keyStream);
                List<byte> decrypted = pr.Decrypt(encrypted, keyStream);
            }
            //byte[] bt=Encoding.Unicode.GetBytes("")
        }
    }
}
