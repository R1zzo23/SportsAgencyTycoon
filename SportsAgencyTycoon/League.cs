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
        public List<Team> TeamList;
        public int Popularity;
        public Date SeasonStart;
        public Date SeasonEnd;
        public int MaxSalary;
        public int MinSalary;

        public League(Sports sport, int popularity, Date seasonStart, Date seasonEnd, int maxSalary, int minSalary)
        {
            Sport = sport;
            TeamList = new List<Team>();
            Popularity = popularity;
            SeasonStart = seasonStart;
            SeasonEnd = seasonEnd;
            MaxSalary = maxSalary;
            MinSalary = minSalary;
        }
    }
}
