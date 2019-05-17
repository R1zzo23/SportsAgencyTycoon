using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Player
    {
        public Sports Sport;
        public FirstName FirstName;
        public LastName LastName;
        public int SkillLevel;
        public int Age;
        public int CareerEarnings;
        public int WorldRanking;

        public Player(Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age)
        {
            Sport = sport;
            FirstName = firstName;
            LastName = lastName;
            SkillLevel = skillLevel;
            Age = age;
            CareerEarnings = 0;
        }
    }
}
