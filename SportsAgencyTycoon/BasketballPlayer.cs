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
        public double Points = 0.0;
        public double Rebounds = 0.0;
        public double Assists = 0.0;
        public double Steals = 0.0;
        public double Blocks = 0.0;
        public Skill Strength;

        public BasketballPlayer(Random rnd, int id, Sports sport, int age, Position position)
            : base(rnd, id, sport, age)
        {
            Position = position;
            PlayoffsMade = 0;
            SeasonsPlayed = 0;
            TitlesWon = 0;
            AllStarTeams = 0;
            AllNBATeams = 0;
            DetermineStrength(Position, rnd);
        }

        public void DetermineStrength(Position p, Random rnd)
        {
            int strengthNumber = rnd.Next(1, 101);
            if (p == Position.PG)
            {
                if (strengthNumber <= 3) Strength = Skill.Blocking;
                else if (strengthNumber <= 15) Strength = Skill.Rebounding;
                else if (strengthNumber <= 40) Strength = Skill.Stealing;
                else if (strengthNumber <= 70) Strength = Skill.Passing;
                else Strength = Skill.Scoring;
            }
            else if (p == Position.SG)
            {
                if (strengthNumber <= 5) Strength = Skill.Blocking;
                else if (strengthNumber <= 18) Strength = Skill.Rebounding;
                else if (strengthNumber <= 42) Strength = Skill.Stealing;
                else if (strengthNumber <= 67) Strength = Skill.Passing;
                else Strength = Skill.Scoring;
            }
            else if (p == Position.SF)
            {
                if (strengthNumber <= 8) Strength = Skill.Blocking;
                else if (strengthNumber <= 22) Strength = Skill.Rebounding;
                else if (strengthNumber <= 45) Strength = Skill.Stealing;
                else if (strengthNumber <= 65) Strength = Skill.Passing;
                else Strength = Skill.Scoring;
            }
            else if (p == Position.PF)
            {
                if (strengthNumber <= 20) Strength = Skill.Blocking;
                else if (strengthNumber <= 45) Strength = Skill.Rebounding;
                else if (strengthNumber <= 52) Strength = Skill.Stealing;
                else if (strengthNumber <= 64) Strength = Skill.Passing;
                else Strength = Skill.Scoring;
            }
            else if (p == Position.C)
            {
                if (strengthNumber <= 30) Strength = Skill.Blocking;
                else if (strengthNumber <= 65) Strength = Skill.Rebounding;
                else if (strengthNumber <= 72) Strength = Skill.Stealing;
                else if (strengthNumber <= 75) Strength = Skill.Passing;
                else Strength = Skill.Scoring;
            }
        }
    }
}
