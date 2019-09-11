using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Award
    {
        public int Year;
        public string AwardName;

        public Award(int year, string name)
        {
            Year = year;
            AwardName = name;
        }
    }
}
