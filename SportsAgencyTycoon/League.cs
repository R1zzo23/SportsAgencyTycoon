using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class League
    {
        public bool Initialized;
        public bool Playoffs;
        public Sports Sport;
        public string Name;
        public string Abbreviation;
        public List<Team> TeamList;
        public int Popularity;
        public Date SeasonStart;
        public Date SeasonEnd;
        public Date DraftDeclarationDate;
        public bool DeclaredEntrants = false;
        public int GamesPerSeason;
        public int MonthsInSeason;
        public int MaxSalary;
        public int MinSalary;
        public List<Player> FreeAgents = new List<Player>();
        public List<Player> DraftEntrants = new List<Player>();
        public List<Player> RetiredPlayers = new List<Player>();
        public bool InSeason;
        public int IdCount = 0;
        public int WeekNumber = 0;

        public League(Sports sport, string name, string abbreviation, int popularity, Date seasonStart, Date seasonEnd, int gamesPerSeason, int monthsInSeason, int maxSalary, int minSalary)
        {
            Initialized = false;
            Playoffs = false;
            Sport = sport;
            Name = name;
            Abbreviation = abbreviation;
            TeamList = new List<Team>();
            FreeAgents = new List<Player>();
            Popularity = popularity;
            SeasonStart = seasonStart;
            SeasonEnd = seasonEnd;
            GamesPerSeason = gamesPerSeason;
            MonthsInSeason = monthsInSeason;
            MaxSalary = maxSalary;
            MinSalary = minSalary;
            DraftDeclarationDate = DetermineDeclareDate(sport);
        }

        public void AddPlayer(List<Player> l, Player p)
        {
            l.Add(p);
            IdCount++;
        }
        public Date DetermineDeclareDate(Sports sport)
        {
            if (sport == Sports.Basketball) return new Date(3, Months.April, 1);
            else if (sport == Sports.Football) return new Date(0, Months.January, 3);
            else if (sport == Sports.Hockey) return new Date(4, Months.May, 4);
            else if (sport == Sports.Soccer) return new Date(11, Months.December, 1);
            else return new Date(4, Months.May, 1);
        }
    }
}
