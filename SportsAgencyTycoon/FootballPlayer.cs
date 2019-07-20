using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class FootballPlayer : Player
    {
        //public Position Position;
        public int PlayoffsMade;
        public int SeasonsPlayed;
        public int TitlesWon;
        public int ProBowlTeams;
        public int AllProSelections;

        public FootballPlayer(Random rnd, int id, Sports sport, int age, Position position)
            : base(rnd, id, sport, age)
        {
            Position = position;
            PlayoffsMade = 0;
            SeasonsPlayed = 0;
            TitlesWon = 0;
            ProBowlTeams = 0;
            AllProSelections = 0;
        }
    }
}
