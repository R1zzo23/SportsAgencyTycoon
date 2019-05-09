using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Date
    {
        public int Year;
        public int MonthNumber;
        public Months MonthName;
        public int Week;

        public Date(int year, int monthNumber, Months monthName, int week)
        {
            Year = year;
            MonthNumber = monthNumber;
            MonthName = monthName;
            Week = week;
        }
    }
}
