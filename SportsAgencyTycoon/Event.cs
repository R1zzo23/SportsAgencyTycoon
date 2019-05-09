using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Event
    {
        public string Name;
        public string Location;
        public int PrizePool;
        public int SpotsPaidOut;
        public int NumberOfEntrants;
        public Date EventDate;

        public Event(string name, string location, int prizePool, int spotsPaidOut, int numberOfEntrants, Date eventDate)
        {
            Name = name;
            Location = location;
            PrizePool = prizePool;
            SpotsPaidOut = spotsPaidOut;
            NumberOfEntrants = numberOfEntrants;
            EventDate = eventDate;
        }
    }
}
