using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Date
    {
        public int MonthNumber;
        public Months MonthName;
        public int Week;

        public Date(int monthNumber, Months monthName, int week)
        {
            MonthNumber = monthNumber;
            MonthName = monthName;
            Week = week;
        }
    }
}
