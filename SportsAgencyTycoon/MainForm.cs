using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsAgencyTycoon
{
    public partial class MainForm : Form
    {
        public Agency agency;
        public Agent myManager;
        public MainForm()
        {
            InitializeComponent();
            CreateManagerAndAgency();
        }
        public void CreateManagerAndAgency()
        {
            Random rnd = new Random();
            agency = new Agency("New Age Agency", 1000000, 1);
            myManager = new Agent("First", "Last", 10, 10, 10, 75, 1, Roles.Manager);
            Client client = new Client("Harry", "Giles", 19, rnd.Next(1, 5), rnd.Next(4, 7), rnd.Next(20, 50), 100, 0, RandomlySelectSport());
            agency.AddClient(client);
            myManager.AddClient(client);
            PopulateManagerClientList();
            UpdateAgencyInfo();
            UpdateAgentInfo();
        }
        public Sports RandomlySelectSport()
        {
            Random rnd = new Random();
            int sportNumber = rnd.Next(1, 7);
            if (sportNumber == 1) return Sports.Basketball;
            else if (sportNumber == 2) return Sports.Baseball;
            else if (sportNumber == 3) return Sports.Football;
            else if (sportNumber == 4) return Sports.Hockey;
            else if (sportNumber == 5) return Sports.Golf;
            else return Sports.Tennis;
        }
        public void UpdateAgencyInfo()
        {
            agencyNameLabel.Text = agency.Name;
            moneyLabel.Text = agency.Money.ToString("C");
            industryInfluenceLabel.Text = agency.IndustryInfluence.ToString() + "/100";
            clientCountLabel.Text = agency.ClientCount.ToString();
            agentCountLabel.Text = agency.AgentCount.ToString();
        }
        public void UpdateAgentInfo()
        {
            agentNameLabel.Text = myManager.First + " " + myManager.Last;
            greedLabel.Text = myManager.Greed.ToString();
            industryPowerLabel.Text = myManager.IndustryPower.ToString();
            levelLabel.Text = myManager.Level.ToString();
            negotiatingLabel.Text = myManager.Negotiating.ToString();
            roleLabel.Text = "Manager of " + agency.Name;
            iqLabel.Text = myManager.Intelligence.ToString();
            managerClientCountLabel.Text = myManager.ClientCount.ToString();
        }

        public void PopulateManagerClientList()
        {
            string fullName;

            foreach (Client client in myManager.ClientList)
            {
                fullName = client.First + " " + client.Last;
                cbManagerClientList.Items.Add(fullName);
            }
        }

        private void editAgentAgencyInfo_Click(object sender, EventArgs e)
        {
            if (managerFirstNameTextBox.Text.Length > 0)
            {
                myManager.First = managerFirstNameTextBox.Text.Trim();
            }
            if (managerLastNameTextBox.Text.Length > 0)
            {
                myManager.Last = managerLastNameTextBox.Text.Trim();
            }
            if (agencyNameTextBox.Text.Length > 0)
            {
                agency.Name = agencyNameTextBox.Text.Trim();
            }
            UpdateAgencyInfo();
            UpdateAgentInfo();
        }

        private void cbManagerClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client selectedManagerClient = myManager.ClientList[cbManagerClientList.SelectedIndex];

            managerClientSportLabel.Text = selectedManagerClient.Sport.ToString();
            managerClientNameLabel.Text = selectedManagerClient.First + " " + selectedManagerClient.Last;
            managerClientPopularityLabel.Text = selectedManagerClient.PopularityDescription;
            managerClientTeamHappinessLabel.Text = selectedManagerClient.TeamHappinessDescription;
            managerClientAgencyHappinessLabel.Text = selectedManagerClient.AgencyHappinessDescription;
            managerClientSkillLabel.Text = selectedManagerClient.CurrentSkill.ToString() + "/" + selectedManagerClient.PotentialSkill.ToString();
            managerClientAgeLabel.Text = selectedManagerClient.Age.ToString();
        }
    }
}
