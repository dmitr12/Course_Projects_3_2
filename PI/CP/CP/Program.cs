using System;
using System.Collections.Generic;
using static System.Console;

namespace CP
{
    class Program
    {
        public static double GetDecFromBin(string strBin)
        {
            double dec = 0;
            for (int i = 0; i < strBin.Length; i++)
                if (strBin[i] == '1')
                    dec += Math.Pow(2, strBin.Length - 1 - i);
            return dec;
        }

        //В КАЧЕСТВЕ ПРИМЕРА
        public static void GenerateArr(ref uint[] arr)
        {
            Random random = new Random();
            for (int i = 0; i < 1024; i++)
                arr[i] = (uint)GetDecFromBin(GetTest32Bit());
        }

        //В КАЧЕСТВЕ ПРИМЕРА
        public static string GetTest256BitKey()
        {
            Random rand = new Random();
            string binKey = "";
            for (int i = 0; i < 255; i++)
                binKey = rand.Next(0, 2).ToString() + binKey;
            binKey = "1" + binKey;
            return binKey;
        }

        public static string GetTestZero256BitKey()
        {
            string binKey = "";
            for (int i = 0; i < 256; i++)
                binKey = "0" + binKey;
            return binKey;
        }

        //В КАЧЕСТВЕ ПРИМЕРА
        public static string GetTest32Bit()
        {
            Random rand = new Random();
            string binKey = "";
            for (int i = 0; i < 32; i++)
                binKey = rand.Next(0, 2).ToString() + binKey;
            return binKey;
        }
        static void Main(string[] args)
        {
            uint[] P = new uint[1024];
            uint[] Q = new uint[1024];
            GenerateArr(ref P);
            GenerateArr(ref Q);   
            HC256 hc256 = new HC256(P,Q);
            WriteLine("Генерация P и Q прошла успешно"+" ");
            string binTestKey = GetTest256BitKey();
            string binTestVector = GetTest256BitKey();
            WriteLine("Генерация ключа и вектора прошла успешно" + " ");
            WriteLine("Процесс инициализации...");
            hc256.InitializaionProcess(binTestKey, binTestVector);
            WriteLine("Инициализация завершена");
            List<uint> keyStream = hc256.KeyStreamGenaretion(16);
            foreach (var item in keyStream)
                WriteLine($"{Convert.ToString(item,16)}");
            
            //string binTestKey = hc256.GetTest256BitKey();dfgklkoikgtfrdeswaqaesrdtfgiokpii
            //WriteLine($"{binTestKey}-{binTestKey.Length}");
            //foreach (uint s in hc256.GetNBlocksFromYBitWord(binTestKey, 8, 256))
            //    WriteLine($"{s}-{hc256.GetBinNWord(s,32)}-{hc256.GetBinNWord(s, 32).Length}");
        }
    }
}
