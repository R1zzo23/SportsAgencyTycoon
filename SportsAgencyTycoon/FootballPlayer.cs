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
        public int ChunkPlays;

        //individual player stats
        public int PassingTDs = 0;
        public int PassingYards = 0;
        public int Interceptions = 0;
        public int Receptions = 0;
        public int ReceivingYards = 0;
        public int ReceivingYardsThisWeek = 0;
        public int ReceivingTDs = 0;
        public int Carries = 0;
        public int RushingYards = 0;
        public double YardsPerCarry = 0.0;
        public int RushingTDs = 0;
        public int Fumbles = 0;
        public int Tackles = 0;
        public int DefensiveInterceptions = 0;
        public int TacklesForLoss = 0;
        public int Sacks = 0;
        public int FGAttempts = 0;
        public int FGMakes = 0;
        public int XPAttempts = 0;
        public int XPMakes = 0;
        public int Punts = 0;
        public int NetPuntYards = 0;
        public double NetPuntAverage = 0;
        public int SacksAllowed = 0;
        public int PassesDefended = 0;
        public int PancakeBlocks = 0;
        public double MVPScore = 0;
        public double OPOYScore = 0;
        public double DPOYScore = 0;
        public List<Award> Awards = new List<Award>();

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
