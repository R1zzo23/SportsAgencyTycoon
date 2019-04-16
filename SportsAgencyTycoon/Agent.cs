using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Agent
    {
        public string First;
        public string Last;
        public int Negotiating;
        public int Greed;
        public int IndustryPower;
        public int Level;
        public Roles Role;
        public List<Client> ClientList;

        public Agent(string firstName, string lastName, int negotiatingRating, int greedRating, int industryPowerRating, int levelRating, Roles role)
        {
            First = firstName;
            Last = lastName;
            Negotiating = negotiatingRating;
            Greed = greedRating;
            IndustryPower = industryPowerRating;
            Level = levelRating;
            Role = role;
            ClientList = new List<Client>();
        }
    }

    public enum Roles
    {
        Manager,
        Agent,
        Marketer
    }
}
