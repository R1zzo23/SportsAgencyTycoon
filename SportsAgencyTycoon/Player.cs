using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Player
    {
        public int Id;
        public Sports Sport;
        public FirstName FirstName;
        public LastName LastName;
        public string FullName;
        public int SkillLevel;
        public int PotentialSkill;
        public int Age;
        public Months BirthMonth;
        public int BirthWeek;
        public int CareerEarnings;
        public int WorldRanking;
        public Date Birthday;

        public Player(int id, Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age, Months birthMonth, int birthWeek)
        {
            Id = id;
            Sport = sport;
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName.ToString() + " " + lastName.ToString();
            SkillLevel = skillLevel;
            PotentialSkill = AssignPotential(age, skillLevel);
            Age = age;
            BirthMonth = birthMonth;
            if (birthWeek == 5)
            {
                if (((int)BirthMonth + 1) % 3 == 0) BirthWeek = 5;
                else BirthWeek = 4;
            }
            else BirthWeek = birthWeek;
            Birthday = CreateBirthday(birthMonth, birthWeek);
            CareerEarnings = 0;
        }

        public int AssignPotential(int age, int skillLevel)
        {
            Random rnd = new Random();
            int potentialSkill = 0;
            int potential = 0;

            if (age <= 20) potential = rnd.Next(15, 41);
            else if (age <= 25) potential = rnd.Next(10, 36);
            else if (age <= 30) potential = rnd.Next(5, 26);
            else if (age <= 35) potential = rnd.Next(0, 7);

            potentialSkill = skillLevel + potential;
            if (potentialSkill > 100) potentialSkill = 100;

            return potentialSkill;
        }

        public Date CreateBirthday(Months month, int week)
        {
            Date birthday = new Date((int)month, month, week);
            return birthday;
        }
    }
}
