using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class SoccerPlayer : Player
    {
        //public Position Position;
        public int PlayoffsMade;
        public int SeasonsPlayed;
        public int TitlesWon;
        public int AllStarTeams;
        public int Goals = 0;
        public int Assists = 0;
        public int Shots = 0;
        public int Saves = 0;
        public int GoalsAllowed = 0;
        public int ShotsFaced = 0;
        public int CleanSheets = 0;
        public int GamesPlayed = 0;
        public double GAA = 0.0;
        public double SavePercentage = 0.0;
        public double MatchRating = 0.0;
        public double PlayerOfYearScore = 0.0;
        public List<Award> Awards = new List<Award>();

        public SoccerPlayer(Random rnd, int id, Sports sport, int age, Position position)
            : base(rnd, id, sport, age)
        {
            Position = position;
            PlayoffsMade = 0;
            SeasonsPlayed = 0;
            TitlesWon = 0;
            AllStarTeams = 0;
        }
    }
}
