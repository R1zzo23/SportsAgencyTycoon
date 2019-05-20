using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Event
    {
        public Sports Sport;
        public int Year;
        public string Name;
        public string Location;
        public int PrizePool;
        public int SpotsPaidOut;
        public int NumberOfEntrants;
        public Date EventDate;
        public List<Player> EntrantList;

        public Event(Sports sport, int year, string name, string location, int prizePool, int numberOfEntrants, int spotsPaidOut,  Date eventDate)
        {
            Sport = sport;
            Year = year;
            Name = name;
            Location = location;
            PrizePool = prizePool;
            SpotsPaidOut = spotsPaidOut;
            NumberOfEntrants = numberOfEntrants;
            EventDate = eventDate;
            EntrantList = new List<Player>();
        }
    }
}
