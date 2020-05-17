using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    class Cipher
    {
        public string TsezShifr(string str,int key, string alph)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char item in str)
                stringBuilder.Append(alph[(alph.IndexOf(item) + key) % alph.Length]);
            return stringBuilder.ToString();
        }

        public string TsezRash(string zash, int key, string alph)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char item in zash)
            {
                int i = alph.IndexOf(item) - key;
                if (i < 0)
                    while (i < 0)
                        i += alph.Length;
                stringBuilder.Append(alph[i%alph.Length]);
            }
            return stringBuilder.ToString();
        }
        
        public List<int> PortSifr(string str, string alph, out bool lastAdded)
        {
            List<int> list = new List<int>();
            if (str.Length % 2 != 0)
            {
                str += alph[0];
                lastAdded = true;
            }
            else lastAdded = false;
            for (int i=0;i<str.Length;i+=2)
            {
                list.Add(alph.IndexOf(str[i]) * alph.Length + (alph.IndexOf(str[i + 1]) + 1));
            }
            return list;
        }

        public string PortDeshifr(List<int> list, string alph, bool lastAdded)
        {
            int q, r;
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<list.Count;i++)
            {
                q = list[i] / alph.Length;
                r = list[i] - q*alph.Length;
                sb.Append(alph[q]);sb.Append(alph[r-1]);
            }
            if (lastAdded)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
