using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Utils
{
    public static class Counter
    {
        public static int GetCountSeats(int startRow, int endRow, int countSeatsInRow)
            =>((endRow-startRow+1)*countSeatsInRow);
    }
}