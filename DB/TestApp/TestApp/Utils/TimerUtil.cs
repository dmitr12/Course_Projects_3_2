using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Utils
{
    public class TimerUtil
    {
        public static string GetTimeString(int timeMinutes)
        {
            int hourses = timeMinutes / 60;
            int minutes = timeMinutes - 60 * hourses;
            return $"{hourses}:{minutes}";
        }
    }
}