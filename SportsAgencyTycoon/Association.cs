using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Association
    {
        public Sports Sport;
        public List<Player> PlayerList;
        public List<Event> EventList;
        public int Popularity;

        public Association(Sports sport, int popularity)
        {
            Sport = sport;
            PlayerList = new List<Player>();
            EventList = new List<Event>();
            Popularity = popularity;
        }
    }
}
