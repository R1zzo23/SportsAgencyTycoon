using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class Boxing
    {
        /*WBA.EventList.Add(new Event(Sports.Boxing, Year, "Up-And-Comers Showcase", EventType.Normal, "Las Vegas, NV", 15000000, 18, 18, new Date(1, Months.January, 1)));
          WBA.EventList.Add(new Event(Sports.Boxing, Year, "NYC Prize Fights", EventType.Normal, "New York City, NY", 35000000, 12, 12, new Date(5, Months.May, 2)));
          WBA.EventList.Add(new Event(Sports.Boxing, Year, "Top Rank Boxing Premiere", EventType.Normal, "Los Angeles, CA", 29000000, 10, 10, new Date(9, Months.September, 3)));
          WBA.EventList.Add(new Event(Sports.Boxing, Year, "King of the Ring", EventType.Normal, "Las Vegas, NV", 50000000, 40, 40, new Date(11, Months.November, 4)));*/

        public string RunBoxingEvent(Event e, World world)
        {
            //string to print out results to user in newsLabel
            string results = "";
            Random rnd = new Random();

            //lists to run tournament 
            List<Boxer> resultsList = new List<Boxer>();
            List<Boxer> tempList = new List<Boxer>();
            List<Boxer> boxers = new List<Boxer>();
            List<Boxer> entrantList = new List<Boxer>();

            //grab list of boxers from world.WBA
            List<Player> playerList = world.WBA.PlayerList.OrderBy(o => o.WorldRanking).ToList();

            foreach (Boxer b in playerList) boxers.Add(b);

            //create entrantList based on which event is taking place
            entrantList = DetermineEntrantList(boxers, e);
            foreach (Boxer b in entrantList) e.EntrantList.Add(b);


            return results;
        }
        private List<Boxer> DetermineEntrantList(List<Boxer> list, Event e)
        {
            List<Boxer> boxers = new List<Boxer>();

            if (e.Name == "Up-And-Comers Showcase")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++) 
                    boxers.Add(list[list.Count - 1 - i]);
            }
            else if (e.Name == "NYC Prize Fights")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    boxers.Add(list[i + 10]);
            }
            else if (e.Name == "Top Rank Boxing Premiere")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    boxers.Add(list[i]);
            }
            else if (e.Name == "King of the Ring")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++) 
                    boxers.Add(list[i]);
            }

            return boxers;
        }
        private void SimBoxingMatch(Boxer b1, Boxer b2)
        {

        }
    }
}
