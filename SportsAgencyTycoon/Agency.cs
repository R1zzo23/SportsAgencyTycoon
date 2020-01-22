using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsAgencyTycoon
{
    public class Agency
    {
        public string Name;
        public List<Agent> Agents;
        public List<Player> Clients;
        public List<Marketer> Marketers;
        public int AgentCount;
        public int ClientCount;
        public int MarketerCount;
        public int Money;
        public int IndustryInfluence;
        public int MonthlyRent;
        public Date LoanPaybackDate;
        public int LoanPaybackAmount;
        public List<Achievement> Achievements = new List<Achievement>();

        public Agency(string name, int money, int industryInfluence)
        {
            Name = name;
            Money = money;
            IndustryInfluence = industryInfluence;
            Agents = new List<Agent>();
            Marketers = new List<Marketer>();
            Clients = new List<Player>();
            AgentCount = 0;
            ClientCount = 0;
            MarketerCount = 0;
            MonthlyRent = 5000;
        }

        public void AddAgent(Agent agent)
        {
            Agents.Add(agent);
            AgentCount = Agents.Count;
        }
        public void AddClient(Player client)
        {
            Clients.Add(client);
            ClientCount = Clients.Count;
        }
        public void TrainAgentsForTest()
        {
            foreach (Agent agent in Agents)
            {
                //check if agent has applied for a new license
                if (agent.AppliedLicense != null && agent.LicenseTestPrep < 100)
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
                if (agent.LicenseTestPrep == 100 && agent.BeingTrainedForTest) agent.BeingTrainedForTest = false;
            }
        }
        public void DeductMonthlyCosts(World world)
        {
            Money -= CalculateMonthlyCosts();
            if (Money < 0)
            {
                if (LoanPaybackAmount == 0)
                {
                    BankruptcyForm bankruptcyForm = new BankruptcyForm(this);
                    bankruptcyForm.DisplayInformation();
                    bankruptcyForm.BringToFront();
                    bankruptcyForm.ShowDialog();

                    if (bankruptcyForm.AcceptedLoan)
                    {
                        Money += 200000;
                        LoanPaybackDate = new Date(world.MonthNumber, world.MonthName, world.WeekNumber);
                        LoanPaybackAmount = 350000;
                        Console.WriteLine(LoanPaybackAmount + " is due by " + LoanPaybackDate.MonthName + " Week #" + LoanPaybackDate.Week);
                    }
                }
                //else Game Over
            }
        }
        public int CalculateMonthlyCosts()
        {
            int monthlyCosts = 0;
            monthlyCosts += MonthlyRent;
            foreach (Agent a in Agents) monthlyCosts += a.Salary;
            foreach (Marketer m in Marketers) monthlyCosts += m.Salary;
            return monthlyCosts;
        }

        public Agent FindAgent(Player player)
        {
            for (int i = 0; i < Agents.Count; i++)
            {
                int index = Agents[i].ClientList.FindIndex(o => (o.FullName == player.FullName) && (o.Id == player.Id) && (o.Sport == player.Sport));
                if (index >= 0) return Agents[i];
            }
            return null;
        }

        public void AddAchievementToAgency(Achievement a)
        {
            bool agencyHasAchievement = DoesAgencyHaveAchievement(a);

            if (!agencyHasAchievement)
            {
                Achievements.Add(a);
                if (a.AttributeToBoost == "IndustryInfluence")
                    IndustryInfluence += a.PointsToBoost;
                else if (a.AttributeToBoost == "Money")
                    Money += a.PointsToBoost;
                else if (a.AttributeToBoost == "All")
                {
                    IndustryInfluence += a.PointsToBoost;
                    Money += a.PointsToBoost;
                }

                MessageBox.Show("Congrats on earning the '" + a.Name + "' achievement!");
            }            
        }
        public bool DoesAgencyHaveAchievement(Achievement a)
        {
            bool hasAchievement = false;

            int index = Achievements.FindIndex(x => x.Name == a.Name);
            if (index >= 0)
                hasAchievement = true;

            Console.WriteLine("Looking for " + a.Name + " achievement...");
            foreach (Achievement v in Achievements)
                Console.WriteLine("Agent has the '" + v.Name + "' achievement.");

            return hasAchievement;
        }
        public void ClientInteractions(Random rnd, MainForm mainForm)
        {
            foreach (Player p in Clients)
            {
                if (p.Team != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int relationshipIndex;
                        int teammateIndex = rnd.Next(0, p.Team.Roster.Count);
                        while (p == p.Team.Roster[teammateIndex])
                        {
                            teammateIndex = rnd.Next(0, p.Team.Roster.Count);
                        }
                        relationshipIndex = p.Relationships.FindIndex(o => o.Teammate == p.Team.Roster[teammateIndex]);
                        if (relationshipIndex < 0)
                        {
                            p.Relationships.Add(new RelationshipWithPlayer(p, p.Team.Roster[teammateIndex], rnd));
                            relationshipIndex = p.Relationships.Count - 1;
                        }
                        //irreperable relationship
                        if (p.Relationships[relationshipIndex].relationshipDescription == RelationshipDescription.Irreperable)
                        {
                            //code something bad for irreperable relationship
                            // demand trade
                            // fight
                            // public denouncement
                        }
                        else
                            p.Relationships[relationshipIndex].NewInteraction(mainForm);
                    }
                }
            }
        }

        public string DisplayAgencyProgressionRegression(Sports sport)
        {
            List<Player> progressionList = new List<Player>();

            foreach (Agent a in Agents)
                foreach (Player client in a.ClientList)
                    if (client.Sport == sport) progressionList.Add(client);

            string output = sport.ToString() + " progression/regression report:" + Environment.NewLine;

            for (int i = 0; i < progressionList.Count; i++)
            {
                if (progressionList[i].CurrentSkill >= progressionList[i].PreviousCurrentSkill)
                {
                    output += progressionList[i].FullName + " progressed by " + (progressionList[i].CurrentSkill - progressionList[i].PreviousCurrentSkill).ToString() + " points.";
                }
                else
                {
                    output += progressionList[i].FullName + " regressed by " + (progressionList[i].PreviousCurrentSkill - progressionList[i].CurrentSkill).ToString() + " points.";
                }
                output += Environment.NewLine;
            }

            return output;
        }
    }
}
