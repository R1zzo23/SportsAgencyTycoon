using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Golfer : Player
    {
        public int TournamentWins;
        public int TopTenFinishes;

        public Golfer(Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age) 
            : base(sport, firstName, lastName, skillLevel, age)
        {
            TournamentWins = 0;
            TopTenFinishes = 0;
        }
    }
}
