using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class BasketballPlayer : Player
    {
        //public Position Position;
        public int PlayoffsMade;
        public int SeasonsPlayed;
        public int TitlesWon;
        public int AllStarTeams;
        public int AllNBATeams;

        public BasketballPlayer(Random rnd, int id, Sports sport, int age, Position position)
            : base(rnd, id, sport, age)
        {
            Position = position;
            PlayoffsMade = 0;
            SeasonsPlayed = 0;
            TitlesWon = 0;
            AllStarTeams = 0;
            AllNBATeams = 0;
        }
    }
}
