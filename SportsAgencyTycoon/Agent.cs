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
        public int Salary;
        public int CareerEarnings;
        public int Negotiating;
        public int Greed;
        public int IndustryPower;
        public int Intelligence;
        public int Level;
        public Roles Role;
        public List<Player> ClientList;
        public int ClientCount;
        public List<Licenses> LicensesHeld;
        public bool HasAppliedForLicense;
        public Licenses AppliedLicense;
        public int LicenseTestPrep;
        public bool BeingTrainedForTest;
        public bool TestedThisWeek;

        public Agent(string firstName, string lastName, int salary, int negotiatingRating, int greedRating, int industryPowerRating, int intelligence, int levelRating, Roles role)
        {
            First = firstName;
            Last = lastName;
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
        }

        public string TakeTest(World world, Agency agency)
        {
            string message = "";
            //agent takes test
            Random rnd = new Random();

            double agentTestingScore = ((1 + (rnd.Next(-10, 11) / 100)) * Intelligence * 0.5) + ((1 + (rnd.Next(-10, 11) / 100)) * LicenseTestPrep * 0.5);
            Console.WriteLine("Agent Testing Score: " + agentTestingScore);

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

        public void CallTeamsForClient(World world, Player client)
        {
            League l = world.Leagues[world.Leagues.FindIndex(o => o.Name == client.League.Name)];
            CallTeamForClientForm callTeamForClientForm = new CallTeamForClientForm(this, client, l);
            callTeamForClientForm.BringToFront();
            callTeamForClientForm.ShowDialog();
        }
    }
}
