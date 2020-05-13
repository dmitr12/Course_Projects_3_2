using CP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
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

        public static uint FromBinToDec(string bin)
        {
            uint res = 0;
            int ln = bin.Length - 1;
            for (int i = ln; i >=0; i--)
                if (bin[i] == '1')
                    res += (uint)Math.Pow(2, ln-i); 
            return res;
        }

        public static byte[] GetBt(BigInteger bi, int bitLength)
        {
            int counter = bitLength / 8;
            byte[] bts = new byte[counter];
            for(int i=1;i<=counter;i++)
            {
                bts[i - 1] = (byte)(bi >> (bitLength - 8 * i) & byte.MaxValue);
            }
            return bts;
        }

        public static uint[] GetArr32Bit(BigInteger bi, int bitLength)
        {
            int counter = bitLength / 32;
            uint[] bts = new uint[counter];
            for (int i = 1; i <= counter; i++)
            {
                bts[i - 1] = (uint)(bi >> (bitLength - 32 * i) & uint.MaxValue);
            }
            return bts;
        }

        static void Main(string[] args)
        {

            string text = "Hell!";
            string dc = null;
            BigInteger secretKey256Bit = new RandomBigInteger().NextBigInteger(256);
            WriteLine("Отправленный секретный ключ:\n " + secretKey256Bit);
            byte[] secretKeyBytes = GetBt(secretKey256Bit, 256);
            byte[] getedSecretKeyBytes = null;
            using (var bob = new DH())
            {
                using (var alice = new DH())
                {
                    // Bob uses Alice's public key to encrypt his message.
                    byte[] secretMessage = bob.Encrypt(alice.PublicKey, secretKeyBytes);

                    // Alice uses Bob's public key and IV to decrypt the secret message.
                    getedSecretKeyBytes = alice.Decrypt(bob.PublicKey, secretMessage, bob.IV);
                }
            }
            Array.Reverse(getedSecretKeyBytes);
            BigInteger zz = new BigInteger(0);
            for (int i = 0; i < getedSecretKeyBytes.Length; i++)
            {
                zz += new BigInteger(getedSecretKeyBytes[i]) << (8 * i);
            }
            WriteLine("Полученный секретный ключ:\n " + zz);
            //BigInteger key = new RandomBigInteger().NextBigInteger(256);
            //WriteLine(key);
            //byte[] bts = GetBt(key, 256);

            //for(int i=0;i<bts.Length;i++)
            //{
            //    WriteLine(bts[i]);
            //}
            //BigInteger zz = new BigInteger(0);
            //Array.Reverse(bts);
            //for (int i = 0; i < bts.Length; i++)
            //{
            //    zz += new BigInteger(bts[i]) << (8 * i);
            //}
            //WriteLine("Length array: " + bts.Length);
            //WriteLine();
            //WriteLine(zz);
            //if (zz == key)
            //    WriteLine("Okey))");
            //else
            //    WriteLine("Oh no((");
            //HC256 pr = new HC256();
            //WriteLine(key256Bit);
            //BigInteger vector256Bit = new BigInteger(0);
            //uint[] key = GetArr32Bit(key256Bit, 256);
            //uint[] iv = GetArr32Bit(vector256Bit,256);
            //WriteLine(uint.MaxValue);
            //WriteLine("Key:");
            //for(int i = 0; i < key.Length; i++)
            //{
            //    WriteLine(key[i]);
            //}
            //WriteLine("Vector:");
            //for (int i = 0; i < key.Length; i++)
            //{
            //    WriteLine(iv[i]);
            //}
            //pr.InitializationProcess(key, iv);
            //while (true)
            //{
            //    WriteLine("Введите текст");
            //    string s = ReadLine();
            //    byte[] bt = Encoding.Unicode.GetBytes(s);
            //    List<uint> keyStream = pr.GenerateKeyStream(Encoding.Unicode.GetBytes(s));
            //    WriteLine("KeyStream:");
            //    foreach (var k in keyStream)
            //        WriteLine(Convert.ToString(k,16));
            //    List<byte> word = new List<byte>();
            //    word.AddRange(Encoding.Unicode.GetBytes(s));
            //    List<byte> encrypted = pr.Encrypt(word, keyStream);
            //    List<byte> decrypted = pr.Decrypt(encrypted, keyStream);
            //}       
        }
    }
    class RandomBigInteger : Random
    {
        public RandomBigInteger() : base()
        {
        }

        public RandomBigInteger(int Seed) : base(Seed)
        {
        }

        /// <summary>
        /// Generates a random positive BigInteger between 0 and 2^bitLength (non-inclusive).
        /// </summary>
        /// <param name="bitLength">The number of random bits to generate.</param>
        /// <returns>A random positive BigInteger between 0 and 2^bitLength (non-inclusive).</returns>
        public BigInteger NextBigInteger(int bitLength)
        {
            if (bitLength < 1) return BigInteger.Zero;

            int bytes = bitLength / 8;
            int bits = bitLength % 8;

            // Generates enough random bytes to cover our bits.
            byte[] bs = new byte[bytes + 1];
            NextBytes(bs);

            // Mask out the unnecessary bits.
            byte mask = (byte)(0xFF >> (8 - bits));
            bs[bs.Length - 1] &= mask;

            return new BigInteger(bs);
        }

        /// <summary>
        /// Generates a random BigInteger between start and end (non-inclusive).
        /// </summary>
        /// <param name="start">The lower bound.</param>
        /// <param name="end">The upper bound (non-inclusive).</param>
        /// <returns>A random BigInteger between start and end (non-inclusive)</returns>
        public BigInteger NextBigInteger(BigInteger start, BigInteger end)
        {
            if (start == end) return start;

            BigInteger res = end;

            // Swap start and end if given in reverse order.
            if (start > end)
            {
                end = start;
                start = res;
                res = end - start;
            }
            else
                // The distance between start and end to generate a random BigIntger between 0 and (end-start) (non-inclusive).
                res -= start;

            byte[] bs = res.ToByteArray();

            // Count the number of bits necessary for res.
            int bits = 8;
            byte mask = 0x7F;
            while ((bs[bs.Length - 1] & mask) == bs[bs.Length - 1])
            {
                bits--;
                mask >>= 1;
            }
            bits += 8 * bs.Length;

            // Generate a random BigInteger that is the first power of 2 larger than res, 
            // then scale the range down to the size of res,
            // finally add start back on to shift back to the desired range and return.
            return ((NextBigInteger(bits + 1) * res) / BigInteger.Pow(2, bits + 1)) + start;
        }
    }
}
