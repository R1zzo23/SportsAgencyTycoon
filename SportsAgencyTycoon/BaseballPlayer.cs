using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class BaseballPlayer : Player
    {
        //public Position Position;
        public int PlayoffsMade;
        public int SeasonsPlayed;
        public int TitlesWon;
        public int AllStarTeams;
        public double Average;
        public int HomeRuns;
        public int RBI;
        public int Wins;
        public int Losses;
        public int Saves;
        public double ERA;

        public BaseballPlayer(Random rnd, int id, Sports sport, int age, Position position)
            : base(rnd, id, sport, age)
        {
            Position = position;
            PlayoffsMade = 0;
            SeasonsPlayed = 0;
            TitlesWon = 0;
            AllStarTeams = 0;
            Average = 0.0;
            HomeRuns = 0;
            RBI = 0;
            Wins = 0;
            Losses = 0;
            Saves = 0;
            ERA = 0.0;
        }
    }
}
