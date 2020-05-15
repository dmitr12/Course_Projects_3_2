using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //bool oneInstance;
            //Mutex mutex = new Mutex(true, "Application", out oneInstance);
            //if (oneInstance)
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            //}
            //else
            //    MessageBox.Show("Приложение уже запущено!");
        }
    }
}
