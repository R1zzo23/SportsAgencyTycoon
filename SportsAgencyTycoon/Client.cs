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

        public Client(int id, string firstName, string lastName, int age, int currentSkill, int potentialSkill, int popularity, int agencyHappiness, int teamHappiness, Sports sport, Months birthMonth, int birthWeek)
        {
            Id = id;
            First = firstName;
            Last = lastName;
            FullName = firstName + " " + lastName;
            Age = age;
            CurrentSkill = currentSkill;
            PotentialSkill = potentialSkill;
            if (PotentialSkill < CurrentSkill) PotentialSkill = CurrentSkill;
            Popularity = popularity;
            PopularityDescription = DescribePopularity(popularity);
            PopularityString = EnumToString(PopularityDescription.ToString());
            AgencyHappiness = agencyHappiness;
            AgencyHappinessDescription = DescribeHappiness(agencyHappiness);
            AgencyHappinessString = EnumToString(AgencyHappinessDescription.ToString());
            TeamHappiness = teamHappiness;
            TeamHappinessDescription = DescribeHappiness(teamHappiness);
            TeamHappinessString = EnumToString(TeamHappinessDescription.ToString());
            Sport = sport;
            BirthMonth = birthMonth;
            BirthWeek = birthWeek;
            Birthday = new Date((int)birthMonth, birthMonth, birthWeek);

        }

        public PopularityDescription DescribePopularity(int pop)
        {
            PopularityDescription description;

            if (pop <= 10) description = PopularityDescription.Unknown;
            else if (pop <= 25) description = PopularityDescription.RelativeUnknown;
            else if (pop <= 40) description = PopularityDescription.Neutral;
            else if (pop <= 60) description = PopularityDescription.LocalFavorite;
            else if (pop <= 75) description = PopularityDescription.VeryPopular;
            else if (pop <= 90) description = PopularityDescription.ExtremelyPopular;
            else description = PopularityDescription.Superstar;

            return description;
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