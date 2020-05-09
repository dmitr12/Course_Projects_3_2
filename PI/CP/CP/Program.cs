using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
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
            for (uint i = 0; i < key.Length; i++)
            {
                key[i] = 1846593+i+3;
                iv[i] = 2759372+i+3;
            }
            key[0] = 37593674;
            key[1] = 46253648;
            key[2] = uint.MaxValue - 1537482;
            key[4] = 63528423;
            WriteLine("Your key: ");
            string ke = "";
            for(int i = 0; i < 8; i++)
            {
                string str = Convert.ToString(key[i], 16);
                while (str.Length != 8)
                    str = '0' + str;
                ke += str;
                WriteLine(str);
            }
            WriteLine("Key: " + ke);
            pr.InitializationProcess(key, iv);
            while (true)
            {
                WriteLine("Введите текст");
                string s = ReadLine();
                byte[] bt = Encoding.Unicode.GetBytes(s);
                List<uint> keyStream = pr.GenerateKeyStream(Encoding.Unicode.GetBytes(s));
                WriteLine("KeyStream:");
                foreach (var k in keyStream)
                    WriteLine(Convert.ToString(k,16));
                List<byte> word = new List<byte>();
                word.AddRange(Encoding.Unicode.GetBytes(s));
                List<byte> encrypted = pr.Encrypt(word, keyStream);
                List<byte> decrypted = pr.Decrypt(encrypted, keyStream);
                using (StreamWriter sw = new StreamWriter(@"test.txt", true, Encoding.Unicode))
                    sw.WriteLine(Encoding.Unicode.GetString(encrypted.ToArray()));
            }
            
        }
    }
}
