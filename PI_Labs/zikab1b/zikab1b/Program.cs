using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace zikab1b
{
    class Program
    {
        //xy+kn=1
        static void Main(string[] args)
        {
            while (true)
            {
                WriteLine("Введите a:");
                int a = Convert.ToInt32(ReadLine());
                WriteLine("Введите модуль:");
                int n = Convert.ToInt32(ReadLine());
                foo(a, n);
            }      
        }
        static void foo(int a,int m)
        {
            int x, y;
            int g = gfc(a, m, out x, out y);
            if (g != 1)
            {
                WriteLine($"НОД({a},{m})!=1");
            }
            else
            {
                WriteLine("\nРезультат:");
                WriteLine((x % m + m) % m);
            }
        }
        static int gfc(int a,int b,out int x, out int  y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            WriteLine($"{b}={a}*{b/a}+{b%a} --- {b%a}={b}-({a}*{b / a})");
            int d = gfc(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            if (x == 0 || y == 0)
                Write("1=");
            else
                Write($"{b}*{y}+({a}*{x})=");
            return d;

        }
    }
}
