using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Agency
    {
        public string Name;
        public List<Agent> Agents;
        public List<Client> Clients;
        public int AgentCount;
        public int ClientCount;
        public int Money;
        public int IndustryInfluence;

        public Agency(string name, int money, int industryInfluence)
        {
            Name = name;
            Money = money;
            IndustryInfluence = industryInfluence;
            Agents = new List<Agent>();
            Clients = new List<Client>();
            AgentCount = 0;
            ClientCount = 0;
        }

        public void AddAgent(Agent agent)
        {
            Agents.Add(agent);
            AgentCount = Agents.Count;
        }
        public void AddClient(Client client)
        {
            Clients.Add(client);
            ClientCount = Clients.Count;
        }
    }
}
