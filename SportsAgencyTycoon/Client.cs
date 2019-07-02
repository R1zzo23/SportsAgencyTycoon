using System;
using System.Text.RegularExpressions;

namespace SportsAgencyTycoon
{
    public class Client
    {
        public int Id;
        public string First;
        public string Last;
        public string FullName;
        public int Age;
        public int CurrentSkill;
        public int PotentialSkill;
        public int Popularity;
        public PopularityDescription PopularityDescription;
        public string PopularityString;
        public int AgencyHappiness;
        public HappinessDescription AgencyHappinessDescription;
        public string AgencyHappinessString;
        public int TeamHappiness;
        public HappinessDescription TeamHappinessDescription;
        public string TeamHappinessString;
        public Months BirthMonth;
        public int BirthWeek;
        public Date Birthday;
        public Sports Sport;
        public Contract Contract;

        public Client(Random rnd, int id, string firstName, string lastName, int age, int currentSkill, int potentialSkill, int popularity, int agencyHappiness, int teamHappiness, Sports sport, Months birthMonth, int birthWeek)
        {
            Id = id;

            First = firstName;
            Last = lastName;
            FullName = firstName + " " + lastName;
            Age = age;

            CurrentSkill = currentSkill;
            PotentialSkill = potentialSkill;
            if (PotentialSkill < CurrentSkill) PotentialSkill = CurrentSkill;

            Popularity = DeterminePopularity(rnd, CurrentSkill, PotentialSkill, Age);
            PopularityDescription = DescribePopularity(Popularity);
            PopularityString = EnumToString(PopularityDescription.ToString());

            TeamHappiness = DetermineTeamHappiness(rnd);
            TeamHappinessDescription = DescribeHappiness(TeamHappiness);
            TeamHappinessString = EnumToString(TeamHappinessDescription.ToString());

            AgencyHappiness = DetermineAgencyHappiness(rnd, TeamHappiness);
            AgencyHappinessDescription = DescribeHappiness(AgencyHappiness);
            AgencyHappinessString = EnumToString(AgencyHappinessDescription.ToString());

            Sport = sport;

            BirthMonth = birthMonth;
            BirthWeek = birthWeek;
            Birthday = new Date((int)birthMonth, birthMonth, birthWeek);

        }

        public int DeterminePopularity(Random rnd, int currentSkill, int potentialSkill, int age)
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
        //and team's titleContender variable 
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