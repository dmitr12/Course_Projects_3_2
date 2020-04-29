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
            return $"{GVPOD(hourses.ToString())}:{GVPOD(minutes.ToString())}";
        }

        public static string GetDateTimeString(DateTime dt)
        {
            string res = $"{GVPOD(dt.Day.ToString())}.{GVPOD(dt.Month.ToString())}.{dt.Year} " +
                $"{GVPOD(dt.Hour.ToString())}:{GVPOD(dt.Minute.ToString())}";
            return res;
        }

        public static string GetTimeFormat(DateTime dt)
        {
            string format = $"{dt.Year}-{GVPOD(dt.Month.ToString())}" +
                $"-{GVPOD(dt.Day.ToString())}T{GVPOD(dt.Hour.ToString())}:{GVPOD(dt.Minute.ToString())}";
            return format;
        }

        static string GVPOD(string part)
        {
            return part.Length == 1 ? "0" + part : part;
        }
    }
}