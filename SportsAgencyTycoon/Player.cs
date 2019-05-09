using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Player
    {
        public FirstName FirstName;
        public LastName LastName;
        public int SkillLevel;
        public int Age;

        public Player(FirstName firstName, LastName lastName, int skillLevel, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            SkillLevel = skillLevel;
            Age = age;
        }
    }
}
