using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Player
    {
        public int Id;
        public Sports Sport;
        public Team Team;
        public Contract Contract;

        //player attributes to determine which teams or contracts a player prefers
        public int Greed;
        public int Lifestyle;
        public int Loyalty;

        //name variables
        public FirstName FirstName;
        public LastName LastName;
        public string FullName;

        //player skills
        public int CurrentSkill;
        public int PotentialSkill;

        //age and birthday
        public int Age;
        public Months BirthMonth;
        public int BirthWeek;
        public Date Birthday;

        //popularity by numbers and descriptions
        public int Popularity;
        public PopularityDescription PopularityDescription;
        public string PopularityString;

        //agencyHappiness by numbers and descriptions
        public int AgencyHappiness;
        public HappinessDescription AgencyHappinessDescription;
        public string AgencyHappinessString;

        //teamHappines by numbers and description
        public int TeamHappiness;
        public HappinessDescription TeamHappinessDescription;
        public string TeamHappinessString;

        public int CareerEarnings;
        public int WorldRanking;
        

        public Player(Random rnd, int id, Sports sport, FirstName firstName, LastName lastName, int currentSkill, int age, Months birthMonth, int birthWeek)
        {
            Id = id;
            Sport = sport;

            FirstName = firstName;
            LastName = lastName;
            FullName = firstName.ToString() + " " + lastName.ToString();

            CurrentSkill = currentSkill;
            PotentialSkill = AssignPotential(rnd, age, currentSkill);

            Age = age;
            BirthMonth = birthMonth;
            if (birthWeek == 5)
            {
                if (((int)BirthMonth + 1) % 3 == 0) BirthWeek = 5;
                else BirthWeek = 4;
            }
            else BirthWeek = birthWeek;
            Birthday = CreateBirthday(birthMonth, birthWeek);

            Popularity = DeterminePopularity(CurrentSkill, PotentialSkill, Age);
            PopularityDescription = DescribePopularity(Popularity);
            PopularityString = EnumToString(PopularityDescription.ToString());

            TeamHappiness = DetermineTeamHappiness(rnd);
            TeamHappinessDescription = DescribeHappiness(TeamHappiness);
            TeamHappinessString = EnumToString(TeamHappinessDescription.ToString());

            AgencyHappiness = DetermineAgencyHappiness(rnd, TeamHappiness);
            AgencyHappinessDescription = DescribeHappiness(AgencyHappiness);
            AgencyHappinessString = EnumToString(AgencyHappinessDescription.ToString());

            CareerEarnings = 0;
        }

        public int AssignPotential(Random rnd, int age, int currentSkill)
        {
            int potentialSkill = 0;
            int potential = 0;

            if (age <= 20) potential = rnd.Next(15, 41);
            else if (age <= 25) potential = rnd.Next(10, 36);
            else if (age <= 30) potential = rnd.Next(5, 26);
            else if (age <= 35) potential = rnd.Next(0, 7);

            potentialSkill = currentSkill + potential;
            if (potentialSkill > 100) potentialSkill = 100;

            return potentialSkill;
        }

        public Date CreateBirthday(Months month, int week)
        {
            Date birthday = new Date((int)month, month, week);
            return birthday;
        }

        public int DeterminePopularity(int currentSkill, int potentialSkill, int age)
        {
            int popularity = 0;

            popularity = currentSkill + (potentialSkill - currentSkill);
            if (age <= 21) popularity += 20;
            else if (age <= 24) popularity += 15;
            else if (age <= 27) popularity += 10;
            else if (age <= 29) popularity += 5;

            return popularity;
        }

        public PopularityDescription DescribePopularity(int pop)
        {
            PopularityDescription description;

            if (pop <= 25) description = PopularityDescription.Unknown;
            else if (pop <= 35) description = PopularityDescription.RelativeUnknown;
            else if (pop <= 45) description = PopularityDescription.Neutral;
            else if (pop <= 55) description = PopularityDescription.LocalFavorite;
            else if (pop <= 70) description = PopularityDescription.VeryPopular;
            else if (pop <= 85) description = PopularityDescription.ExtremelyPopular;
            else description = PopularityDescription.Superstar;

            return description;
        }

        //want to rewrite this to use players position on depth chart
        //and team's titleContender and marketValue variables
        public int DetermineTeamHappiness(Random rnd)
        {
            int happiness = 0;

            happiness = rnd.Next(0, 100);

            return happiness;
        }

        //want to rewrite this to use TeamHappiness, Contract status
        //and a little randomness
        public int DetermineAgencyHappiness(Random rnd, int teamHappiness)
        {
            int happiness = 0;

            int random = rnd.Next(0, 100);
            happiness = (random + teamHappiness) / 2;

            return happiness;
        }

        public HappinessDescription DescribeHappiness(int happy)
        {
            HappinessDescription description;

            if (happy <= 20) description = HappinessDescription.Disgruntled;
            else if (happy <= 40) description = HappinessDescription.Displeased;
            else if (happy <= 60) description = HappinessDescription.Neutral;
            else if (happy <= 80) description = HappinessDescription.Happy;
            else description = HappinessDescription.Ecstatic;

            return description;
        }

        public string EnumToString(string s)
        {
            s = Regex.Replace(s, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
            return s;
        }
    }
}
