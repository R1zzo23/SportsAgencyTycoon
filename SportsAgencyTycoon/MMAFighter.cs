using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class MMAFighter : Player
    {
        public int Wins;
        public int Losses;
        public bool KnockedOut;

        public MMAFighter(int id, Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age, Months birthMonth, int birthWeek)
            : base(id, sport, firstName, lastName, skillLevel, age, birthMonth, birthWeek)
        {
            Wins = 0;
            Losses = 0;
            KnockedOut = false;
        }
    }
}
