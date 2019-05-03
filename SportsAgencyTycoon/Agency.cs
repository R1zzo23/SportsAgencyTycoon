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
        public int MonthlyRent;

        public Agency(string name, int money, int industryInfluence)
        {
            Name = name;
            Money = money;
            IndustryInfluence = industryInfluence;
            Agents = new List<Agent>();
            Clients = new List<Client>();
            AgentCount = 0;
            ClientCount = 0;
            MonthlyRent = 5000;
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
                    if (agent.BeingTrainedForTest && agent.LicenseTestPrep < 100)
                    {
                        //intelligence * random percentage from -25% to +50% all divided by 10
                        addedTestPrep = (int)Math.Round((agent.Intelligence * (((double)rnd.Next(-25, 51) / 100) + 1)) / 10);
                    }
                    //else agent is training on his/her own
                    else
                    {
                        //intelligence * random percentage from -50% to +15% all divided by 10
                        addedTestPrep = (int)Math.Round((agent.Intelligence * (((double)rnd.Next(-50, 16) / 100) + 1)) / 10);
                    }
                    agent.LicenseTestPrep += (int)addedTestPrep;
                    if (agent.LicenseTestPrep > 100) agent.LicenseTestPrep = 100;
                }
            }
        }
        public void DeductMonthlyRent()
        {
            Money -= MonthlyRent;
        }
    }
}
