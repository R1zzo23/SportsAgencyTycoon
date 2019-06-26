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
        public string PopularityDescription;
        public int AgencyHappiness;
        public string AgencyHappinessDescription;
        public int TeamHappiness;
        public string TeamHappinessDescription;
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
            AgencyHappiness = agencyHappiness;
            AgencyHappinessDescription = DescribeHappiness(agencyHappiness);
            TeamHappiness = teamHappiness;
            TeamHappinessDescription = DescribeHappiness(teamHappiness);
            Sport = sport;
            BirthMonth = birthMonth;
            BirthWeek = birthWeek;
            Birthday = new Date((int)birthMonth, birthMonth, birthWeek);

        }

        public string DescribePopularity(int pop)
        {
            string description = "";

            if (pop <= 10) description = "Unknown";
            else if (pop <= 25) description = "Relative Unknown";
            else if (pop <= 40) description = "Neutral";
            else if (pop <= 60) description = "Local Favorite";
            else if (pop <= 75) description = "Very Popular";
            else if (pop <= 90) description = "Extremely Popular";
            else description = "Superstar";

            return description;
        }

        public string DescribeHappiness(int happy)
        {
            string description = "";

            if (happy <= 20) description = "Disgruntled";
            else if (happy <= 40) description = "Displeased";
            else if (happy <= 60) description = "Neutral";
            else if (happy <= 80) description = "Happy";
            else description = "Ecstatic";

            return description;
        }
    }
}