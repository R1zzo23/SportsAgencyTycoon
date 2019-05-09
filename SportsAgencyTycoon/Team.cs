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
        public int MarketValue;
        public int TitleConteder;

        public Team (string city, string mascot, int marketValue, int titleContender)
        {
            City = city;
            Mascot = mascot;
            MarketValue = marketValue;
            TitleConteder = titleContender;
        }
    }
}
