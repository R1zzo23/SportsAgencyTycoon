using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Team
    {
        public string City;
        public string Mascot;
        public string Abbreviation;
        public int MarketValue;
        public int TitleConteder;
        public List<Player> Roster = new List<Player>();

        public Team (string city, string mascot, string abbreviation, int marketValue, int titleContender)
        {
            City = city;
            Mascot = mascot;
            Abbreviation = abbreviation;
            MarketValue = marketValue;
            TitleConteder = titleContender;
        }
    }
}
