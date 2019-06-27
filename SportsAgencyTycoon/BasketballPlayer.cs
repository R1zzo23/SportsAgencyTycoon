using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class BasketballPlayer : Player
    {
        public Position Position;
        public int PlayoffsMade;
        public int SeasonsPlayed;
        public int TitlesWon;
        public int AllStarTeams;
        public int AllNBATeams;

        public BasketballPlayer(int id, Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age, Months birthMonth, int birthWeek, Position position)
            : base(id, sport, firstName, lastName, skillLevel, age, birthMonth, birthWeek)
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
