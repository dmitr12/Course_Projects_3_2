using System;
using System.Collections.Generic;
using System.Text;

namespace CP
{
    public class HC256
    {
        uint[] P;
        uint[] Q;
        uint[] W = new uint[2560];
        ulong key256Bit, vector256Bit;

        public HC256(uint[] P, uint[] Q)
        {
            this.P = P;
            this.Q = Q;
            //P = new uint[1024];
            //Q = new uint[1024];
        }

        public uint Plus(uint x, uint y) => (uint)((x + y) % Math.Pow(2, 32));

        public uint Minus(uint x, uint y) => (x - y) % 1024;

        public uint Xor(uint x, uint y) => x ^ y;

        //public ulong Concatanation(IEnumerable<string> arr)
        //{
        //    string result = "";
        //    foreach (string item in arr)
        //        result += arr = item;

        //}

        public uint SdvigLeft(uint x, int n) => x << n;

        public uint SdvigRight(uint x, int n) => x >> n;

        public uint CycleSdvigRight(uint x, int n)=> Xor(SdvigRight(x, n), SdvigLeft(x, 32 - n));

        public uint FuncF1(uint x) => Xor(Xor(CycleSdvigRight(x,7), CycleSdvigRight(x, 18)), SdvigRight(x, 3));

        public uint FuncF2(uint x) => Xor(Xor(CycleSdvigRight(x, 17), CycleSdvigRight(x, 19)), SdvigRight(x, 10));

        public uint FuncG1(uint x, uint y)
            => Plus(Xor(CycleSdvigRight(x,10), CycleSdvigRight(y,23)), Q[(Xor(x, y)) % 1024]);

        public uint FuncG2(uint x, uint y)
            => Plus(Xor(CycleSdvigRight(x, 10), CycleSdvigRight(y, 23)), P[(Xor(x, y)) % 1024]);

        public uint FuncH1(uint x)
        {
            List<uint> listX = GetNBlocksFromYBitWord(x,4,32);
            uint operand1 = Plus(Q[listX[3]], Q[Plus(256, listX[2])]);
            uint operand2 = Plus(Q[Plus(512, listX[1])], Q[Plus(768, listX[0])]);
            return Plus(operand1, operand2);
        }

        public uint FuncH2(uint x)
        {
            List<uint> listX = GetNBlocksFromYBitWord(x,4,32);
            uint operand1 = Plus(P[listX[3]], P[Plus(256, listX[2])]);
            uint operand2 = Plus(P[Plus(512, listX[1])], P[Plus(768, listX[0])]);
            return Plus(operand1, operand2);
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //ИЗМЕНИТЬ МЕТОД, НЕ ТАК РАБОТАЕТ ПРИ n=8 и y=256
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public List<uint> GetNBlocksFromYBitWord(uint x, int n, int y)
        {
            return GetNBlocksFromYBitWord(GetBinNWord(x, y), n, y);
        }

        public List<uint> GetNBlocksFromYBitWord(string binW, int n, int y)
        {
            string binBlock = "";
            List<string> binBlocks = new List<string>();
            List<uint> result = new List<uint>();
            for (int i = 0; i < y; i++)
            {
                binBlock += binW[i];
                if ((i + 1) % (y / n) == 0)
                {
                    binBlocks.Add(binBlock);
                    binBlock = "";
                }
            }
            foreach (string bt in binBlocks)
                result.Add((uint)GetDecFromBin(bt));
            return result;
        }

        public double GetDecFromBin(string strBin)
        {
            double dec = 0;
            for (int i = 0; i < strBin.Length; i++)
                if (strBin[i] == '1')
                    dec += Math.Pow(2, strBin.Length - 1 - i);
            return dec;
        }

        public string GetBinNWord(uint x, int n)
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

        //Процесс инициализации
        public void InitializaionProcess(string key256Bit, string vector256Bit)
        {
            List<uint> list32BitBlocksKey = GetNBlocksFromYBitWord(key256Bit, 8, 256);
            List<uint> list32BitBlocksVector = GetNBlocksFromYBitWord(vector256Bit, 8, 256);
            //1) Составление массива W
            for(int i=0;i<=2559;i++)
            {
                if (i >= 0 && i <= 7)
                    W[i] = list32BitBlocksKey[i];
                else if (i >= 8 && i <= 15)
                    W[i] = list32BitBlocksVector[i - 8];
                else
                    W[i] = Plus(Plus(Plus(FuncF2(W[i - 2]), W[i - 7]), Plus(FuncF1(W[i - 15]), W[i - 16])), (uint)i);
            }
            //2) Обновление таблиц P и Q с помощью массива W
            for(int i = 0; i <= 1023; i++)
            {
                P[i] = W[i + 512];
                Q[i] = W[i + 1535];
            }
            //3) 3. Run the cipher (the keystream generation algorithm) 4096
            //steps without generating output.
            KeyStreamGenaretion();
            
        }

        public List<uint> KeyStreamGenaretion(uint countOfKeyStream32BitBlocks=128)
        {
            uint j;
            List<uint> sKey = new List<uint>();
            for(int i=0;i< countOfKeyStream32BitBlocks; i++)
            {
                j =(uint)i % 1024;
                if ((i % 2048) < 1024)
                {
                    P[j] = Plus(Plus(P[j], P[Minus(j, 10)]), FuncG1(P[Minus(j, 3)], P[Minus(j, 1023)]));
                    sKey.Add(Xor(FuncH1(P[Minus(j,12)]), P[j]));
                }
                else
                {
                    Q[j] = Plus(Plus(Q[j], Q[Minus(j, 10)]), FuncG2(Q[Minus(j, 3)], Q[Minus(j, 1023)]));
                    sKey.Add(Xor(FuncH2(Q[Minus(j, 12)]), Q[j]));
                }
            }
            return sKey;
        }

        public string encrypt(string txt, List<uint> keyStream)
        {
            return null;
        }

        //Ненужное

        //public void GetMassW()
        //{
        //    for(int i=0;i<W.Length;i++)
        //        Console.WriteLine($"{i}) r");
        //}

    }
}
