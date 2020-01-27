using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsAgencyTycoon
{
    public class Agent
    {
        public string First;
        public string Last;
        public string FullName;

        public int Salary;
        public int CareerEarnings;
        public Roles Role;

        public List<Achievement> Achievements = new List<Achievement>();

        public int Negotiating;
        public int Greed;
        public int IndustryPower;
        public int Intelligence;
        public int Level;

        public List<RelationshipWithTeam> RelationshipsWithTeams = new List<RelationshipWithTeam>();
        public List<Player> ClientList;
        public int ClientCount;
        public List<Licenses> LicensesHeld;
        public bool HasAppliedForLicense;
        public Licenses AppliedLicense;
        public int LicenseTestPrep;
        public bool BeingTrainedForTest;
        public bool TestedThisWeek;

        public List<Sports> SportsRepresented = new List<Sports>();
        public Agent(string firstName, string lastName, int salary, int negotiatingRating, int greedRating, int industryPowerRating, int intelligence, int levelRating, Roles role)
        {
            First = firstName;
            Last = lastName;
            FullName = firstName + " " + lastName;
            Salary = salary;
            CareerEarnings = 0;
            Negotiating = negotiatingRating;
            Greed = greedRating;
            IndustryPower = industryPowerRating;
            Intelligence = intelligence;
            Level = levelRating;
            Role = role;
            ClientList = new List<Player>();
            LicensesHeld = new List<Licenses>();
            BeingTrainedForTest = false;
            AppliedLicense = null;
            HasAppliedForLicense = false;
            TestedThisWeek = false;
        }

        public void AddClient(Player client)
        {
            ClientList.Add(client);
            ClientCount = ClientList.Count();


            //check if this is a new sport being represented by this Agent
            int index = SportsRepresented.FindIndex(o => o == client.Sport);
            if (index < 0) SportsRepresented.Add(client.Sport);
        }

        public string TakeTest(World world, Agency agency)
        {
            string message = "";
            //agent takes test
            Random rnd = new Random();

            double agentTestingScore = ((1 + (rnd.Next(-5, 11) / 100)) * Intelligence * 0.5) + ((1 + (rnd.Next(-5, 11) / 100)) * LicenseTestPrep * 0.8);

            //if agent obtains license
            if (agentTestingScore >= 75)
            {
                LicensesHeld.Add(AppliedLicense);
                //message into newsLabel
                message = First + " " + Last + " has earned a license for " + AppliedLicense.Sport.ToString().ToLower() + "!";
                
                //world.CreateNewClients(agency, AppliedLicense);

                //reset license information
                AppliedLicense = null;
                LicenseTestPrep = 0;
                HasAppliedForLicense = false;
            }
            //agent failed test
            else
            {
                message = First + " " + Last + " failed the test for " + AppliedLicense.Sport.ToString().ToLower() + "! Try again next week!";
            }
            TestedThisWeek = true;

            return message;
        }

        public void CallTeamsForClient(Random rnd, World world, Player client)
        {
            League l = world.Leagues[world.Leagues.FindIndex(o => o.Name == client.League.Name)];
            CallTeamForClientForm callTeamForClientForm = new CallTeamForClientForm(rnd, this, client, l, world);
            callTeamForClientForm.BringToFront();
            callTeamForClientForm.ShowDialog();
        }
        public void CallTeamGM(Random rnd, World world, Player client)
        {
            Team team = client.Team;
            CallTeamGMForm callTeamGMForm = new CallTeamGMForm(rnd, this, client, team, world);
            callTeamGMForm.BringToFront();
            callTeamGMForm.Show();
        }
        public void AddAchievementToAgent(Achievement a)
        {
            //check if Agent already has this achievement
            bool hasAchievement = DoesAgentHaveAchievement(a);

            if (!hasAchievement)
            {
                Achievements.Add(a);
                if (a.AttributeToBoost == "Negotiating")
                    Negotiating += a.PointsToBoost;
                else if (a.AttributeToBoost == "Greed")
                    Greed += a.PointsToBoost;
                else if (a.AttributeToBoost == "IndustryPower")
                    IndustryPower += a.PointsToBoost;
                else if (a.AttributeToBoost == "Intelligence")
                    Intelligence += a.PointsToBoost;
                else if (a.AttributeToBoost == "All")
                {
                    Negotiating += a.PointsToBoost;
                    Greed += a.PointsToBoost;
                    IndustryPower += a.PointsToBoost;
                    Intelligence += a.PointsToBoost;
                }

                MessageBox.Show("Congrats on earning the '" + a.Name + "' achievement!");
                Level++;
            }
        }
        public bool DoesAgentHaveAchievement(Achievement a)
        {
            bool hasAchievement = false;

            int index = Achievements.FindIndex(x => x.Name == a.Name);
            if (index >= 0)
                hasAchievement = true;

            return hasAchievement;
        }
    }
}
