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
        public void TrainAgentsForTest()
        {
            foreach (Agent agent in Agents)
            {
                //check if agent has applied for a new license
                if (agent.AppliedLicense != null)
                {
                    Random rnd = new Random();
                    double addedTestPrep = 0;

                    //check if agent is being trained by agency
                    if (agent.BeingTrainedForTest)
                    {
                        addedTestPrep = Math.Round(Math.Round((double)agent.Intelligence / 5 ) * (double)(1 + (rnd.Next(-5, 26) / 100)));
                    }
                    //else agent is training on his/her own
                    else
                    {
                        addedTestPrep = Math.Round(Math.Round((double)agent.Intelligence / 15 )* (double)(1 + (rnd.Next(-25, 10) / 100)));
                    }
                    agent.LicenseTestPrep += (int)addedTestPrep;
                }
            }
        }
    }
}
