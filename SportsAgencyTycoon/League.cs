using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class League
    {
        public Sports Sport;
        public string Name;
        public string Abbreviation;
        public List<Team> TeamList;
        public int Popularity;
        public Date SeasonStart;
        public Date SeasonEnd;
        public int MaxSalary;
        public int MinSalary;

        public League(Sports sport, string name, string abbreviation, int popularity, Date seasonStart, Date seasonEnd, int maxSalary, int minSalary)
        {
            Sport = sport;
            Name = name;
            Abbreviation = abbreviation;
            TeamList = new List<Team>();
            Popularity = popularity;
            SeasonStart = seasonStart;
            SeasonEnd = seasonEnd;
            MaxSalary = maxSalary;
            MinSalary = minSalary;
        }
    }
}
