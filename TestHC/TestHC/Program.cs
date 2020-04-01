using System;
using System.Collections.Generic;
using static System.Console;

namespace TestHC
{
    class Program
    {
        static void Main(string[] args)
        {
            //example
            HC256 pr = new HC256();
            uint[] key = new uint[8];
            uint[] iv = new uint[8];
            for(int i = 0; i < key.Length; i++)
            {
                key[i] = 0;
                iv[i] = 0;
            }
            pr.InitializationProcess(key, iv);

            List<uint> keyStream = pr.GenerateKeyStream(16);
            foreach (var t in keyStream)
                WriteLine(Convert.ToString(t, 16));
        }
    }
}
