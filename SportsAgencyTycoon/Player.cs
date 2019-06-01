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
        public Months BirthMonth;
        public int BirthWeek;
        public int CareerEarnings;
        public int WorldRanking;

        public Player(Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age, Months birthMonth, int birthWeek)
        {
            Sport = sport;
            FirstName = firstName;
            LastName = lastName;
            SkillLevel = skillLevel;
            Age = age;
            BirthMonth = birthMonth;
            if (birthWeek == 5)
            {
                if (((int)BirthMonth + 1) % 3 == 0) BirthWeek = 5;
                else BirthWeek = 4;
            }
            CareerEarnings = 0;
        }
    }
}
