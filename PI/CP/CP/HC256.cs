using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestHC
{
    class HC256
    {
        uint[] P = new uint[1024];
        uint[] Q = new uint[1024];
        uint[] W = new uint[2560];

        public uint CycleSdvigRight(uint x, int n) => ((x >> n) ^ (x << (32 - n)));

        public uint Minus(uint x, uint y) => ((x - y) % 1024);

        public uint f1(uint x) => (CycleSdvigRight(x, 7) ^ CycleSdvigRight(x, 18) ^ (x >> 3));

        public uint f2(uint x) => (CycleSdvigRight(x, 17) ^ CycleSdvigRight(x, 19) ^ (x >> 10));

        public uint g1(uint x, uint y) => ((CycleSdvigRight(x, 10) ^ CycleSdvigRight(y, 23)) + Q[(x ^ y) % 1024]);

        public uint g2(uint x, uint y) => ((CycleSdvigRight(x, 10) ^ CycleSdvigRight(y, 23)) + P[(x ^ y) % 1024]);

        public uint h1(uint x)
        {
            byte a = (byte)x;
            byte b = (byte)(x >> 8);
            byte c = (byte)(x >> 16);
            byte d = (byte)(x >> 24);
            return Q[a] + Q[256 + b] + Q[512 + c] + Q[768 + d];
        }

        public uint h2(uint x)
        {
            byte a = (byte)x;
            byte b = (byte)(x >> 8);
            byte c = (byte)(x >> 16);
            byte d = (byte)(x >> 24);
            return P[a] + P[256 + b] + P[512 + c] + P[768 + d];
        }

        public void feedback_1(ref uint u, uint v, uint b, uint c)
        {
            uint tem0, tem1, tem2;
            tem0 = CycleSdvigRight(v, 23);
            tem1 = CycleSdvigRight(c, 10);
            tem2 = (v ^ c) & 1023;
            u += b + (tem0 ^ tem1) + Q[tem2];
        }

        public void feedback_2(ref uint u, uint v, uint b, uint c)
        {
            uint tem0, tem1, tem2;
            tem0 = CycleSdvigRight(v, 23);
            tem1 = CycleSdvigRight(c, 10);
            tem2 = (v ^ c) & 1023;
            u += b + (tem0 ^ tem1) + P[tem2];
        }

        public void InitializationProcess(uint[] key, uint[] iv)
        {
            //1)
            for (int i = 0; i <= 2559; i++)
            {
                if (i >= 0 && i <= 7)
                    W[i] = key[i];
                else if (i >= 8 && i <= 15)
                    W[i] = iv[i - 8];
                else
                    W[i] = f2(W[i - 2]) + W[i - 7] + f1(W[i - 15]) + W[i - 16] + (uint)i;
            }
            //2)
            for (int i = 0; i <= 1023; i++)
            {
                P[i] = W[i + 512];
                Q[i] = W[i + 1536];
            }
            //3)
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                    feedback_1(ref P[j], P[j + 1], P[(j - 10) & 1023], P[(j - 3) & 1023]);
                for (int j = 10; j < 1023; j++)
                    feedback_1(ref P[j], P[j + 1], P[j - 10], P[j - 3]);
                feedback_1(ref P[1023], P[0], P[1013], P[1020]);
                for (int j = 0; j < 10; j++)
                    feedback_2(ref Q[j], Q[j + 1], Q[(j - 10) & 1023], Q[(j - 3) & 1023]);
                for (int j = 10; j < 1023; j++)
                    feedback_2(ref Q[j], Q[j + 1], Q[j - 10], Q[j - 3]);
                feedback_2(ref Q[1023], Q[0], Q[1013], Q[1020]);
            }
        }
        public List<uint> GenerateKeyStream(byte[] bytes)
        {
            int counter= bytes.Length % 4 == 0 ? bytes.Length / 4 : (bytes.Length / 4) + 1;
            List<uint> keStream = new List<uint>();
            uint j;
            for (uint i = 0; i < counter; i++)
            {
                j = i % 1024;
                if ((i % 2048) < 1024)
                {
                    P[j] = P[j] + P[Minus(j, 10)] + g1(P[Minus(j, 3)], P[Minus(j, 1023)]);
                    keStream.Add(h1(P[Minus(j, 12)]) ^ P[j]);
                }
                else
                {
                    Q[j] = Q[j] + Q[Minus(j, 10)] + g2(Q[Minus(j, 3)], Q[Minus(j, 1023)]);
                    keStream.Add(h2(Q[Minus(j, 12)]) ^ Q[j]);
                }
            }
            return keStream;
        }

        public List<byte> GetBytesFromKeyStream(List<uint> keyStream)
        {
            List<byte> bytes = new List<byte>();
            foreach (uint item in keyStream)
            {
                bytes.AddRange(new byte[]
                {
                    (byte)(item >> 24), (byte)(item >> 16),
                    (byte)(item >> 8), (byte)item
                });
            }
            return bytes;
        }

        public List<byte> XorBytes(List<byte> bytes1, List<byte> bytes2)
        {
            List<byte> xoredBytes = new List<byte>();
            //if (bytes1.Count < bytes2.Count)
            //    while (bytes1.Count != bytes2.Count)
            //        bytes1.Add(0);
            //else
            //    while (bytes2.Count != bytes1.Count)
            //        bytes2.Add(0);
            int counter = bytes1.Count < bytes2.Count ? bytes1.Count : bytes2.Count;
            for (int i = 0; i < counter; i++)
                xoredBytes.Add((byte)(bytes1[i] ^ bytes2[i]));
            return xoredBytes;
        }

        public List<byte> Encrypt(List<byte> bytesTxt, List<uint> keyStream)
        {
            List<byte> xoredBytes = XorBytes(bytesTxt, GetBytesFromKeyStream(keyStream));
            string txt = Encoding.Unicode.GetString(xoredBytes.ToArray());
            string path = @"test.txt";
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
                sw.WriteLine(txt);
            Console.WriteLine($"Текст: {txt}");
            return xoredBytes;
        }

        public List<byte> Decrypt(List<byte> bytesTxt, List<uint> keyStream)
            => Encrypt(bytesTxt, keyStream);

    }
}
