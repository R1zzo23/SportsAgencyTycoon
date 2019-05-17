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

        public MMAFighter(Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age)
            : base(sport, firstName, lastName, skillLevel, age)
        {
            Wins = 0;
            Losses = 0;
        }
    }
}
