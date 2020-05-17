using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Console;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pl-PL");
            Cipher cipher = new Cipher();
            StringBuilder strB = new StringBuilder();
            string alph = "aąbcćdeęfghijklłmnńoóprsśtuwyzźż";
            Regex reg = new Regex(@"[aąbcćdeęfghijklłmnńoóprsśtuwyzźż]");
            using (StreamReader sr=new StreamReader("text.txt"))
            {
                bool forPort;
                string s = sr.ReadToEnd().ToLower();
                MatchCollection matches = reg.Matches(s);
                foreach (Match q in matches)
                    strB.Append(q.Value);
                WriteLine("Исходный текст:\n"+strB);
                string zash = cipher.TsezShifr(strB.ToString(), 28, alph);
                WriteLine("\nЗашифрованный текст:\n" + zash);
                string ish = cipher.TsezRash(zash, 28, alph);
                WriteLine("\nИсходный текст:\n" + ish);
                List<int> shifrPort = cipher.PortSifr(strB.ToString(),alph, out forPort);
                //foreach (int q in shifrPort)
                //{
                //    if (q < 10) Write("00"+q);
                //    else if (q < 100) Write("0"+q);
                //    else Write(q);
                //}
                foreach (int n in shifrPort)
                    WriteLine(n);
                string res = cipher.PortDeshifr(shifrPort, alph, forPort);
                WriteLine(res);
            }
        }
    }
}
