using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class World
    {
        public List<Licenses> AvailableLicenses;
        public List<Client> AvailableClients;
        public List<Agent> AvailableAgents;

        public World()
        {
            AvailableLicenses = new List<Licenses>();
            AvailableClients = new List<Client>();
            AvailableAgents = new List<Agent>();
        }

        public void InitializeLicenses()
        {
            Licenses basketballLicense = new Licenses(Sports.Basketball, 250, 1250, Months.July, 7);
            Licenses footballLicense = new Licenses(Sports.Football, 2500, 1650, Months.January, 1);
            AvailableLicenses.Add(basketballLicense);
            AvailableLicenses.Add(footballLicense);
        }
    }

    public enum Months
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}
