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
        public World world;
        public MainForm()
        {
            InitializeComponent();
            InitializeWorld();
            CreateManagerAndAgency();
        }
        public void InitializeWorld()
        {
            world = new World();
            newsLabel.Text = "Welcome to DDS:Sports Agency Tycoon!";
            world.InitializeLicenses();
            PopulateAvailableLicenses();
        }
        public void CreateManagerAndAgency()
        {
            Random rnd = new Random();
            agency = new Agency("New Age Agency", 1000000, 1);
            myManager = new Agent("First", "Last", 10, 10, 10, 75, 1, Roles.Manager);
            agency.AddAgent(myManager);
            Client client = new Client("Harry", "Giles", 19, rnd.Next(1, 5), rnd.Next(4, 7), rnd.Next(20, 50), 100, 0, Sports.Basketball);
            agency.AddClient(client);
            myManager.AddClient(client);
            PopulateAgentClientList();
            PopulateAgencyAgentList();
            UpdateAgencyInfo();
            UpdateAgentInfo();
        }
        private void PopulateAgencyAgentList()
        {
            //store current selected client
            int currentSelection = cbAgencyAgentList.SelectedIndex;

            //empty combo box to repopulate
            cbAgencyAgentList.Items.Clear();

            string fullName;

            //populate combo box
            foreach (Agent agent in agency.Agents)
            {
                fullName = agent.First + " " + agent.Last;
                cbAgencyAgentList.Items.Add(fullName);
            }

            //go back to selected client
            cbAgencyAgentList.SelectedIndex = currentSelection;
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
            agentClientCountLabel.Text = myManager.ClientCount.ToString();
            agency.Agents[0] = myManager;
            PopulateAgencyAgentList();
        }

        public void PopulateAgentClientList()
        {
            //store current selected client
            int currentSelection = cbAgentClientList.SelectedIndex;

            //empty combo box to repopulate
            cbAgentClientList.Items.Clear();

            string fullName;

            //populate combo box
            foreach (Client client in myManager.ClientList)
            {
                fullName = client.First + " " + client.Last;
                cbAgentClientList.Items.Add(fullName);
            }

            //go back to selected client
            cbAgentClientList.SelectedIndex = currentSelection;
        }

        public void PopulateAvailableLicenses()
        {
            //store current selected client
            int currentSelection = cbAvailableLicenses.SelectedIndex;

            //empty combo box to repopulate
            cbAvailableLicenses.Items.Clear();

            string name;

            //populate combo box
            foreach (Licenses license in world.AvailableLicenses)
            {
                name = license.Sport.ToString();
                cbAvailableLicenses.Items.Add(name);
            }

            //go back to selected client
            cbAgentClientList.SelectedIndex = currentSelection;
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
            Client selectedClient = myManager.ClientList[cbAgentClientList.SelectedIndex];

            clientSportLabel.Text = selectedClient.Sport.ToString();
            clientNameLabel.Text = selectedClient.First + " " + selectedClient.Last;
            clientPopularityLabel.Text = selectedClient.PopularityDescription;
            clientTeamHappinessLabel.Text = selectedClient.TeamHappinessDescription;
            clientAgencyHappinessLabel.Text = selectedClient.AgencyHappinessDescription;
            clientSkillLabel.Text = selectedClient.CurrentSkill.ToString() + "/" + selectedClient.PotentialSkill.ToString();
            clientAgeLabel.Text = selectedClient.Age.ToString();
        }

        private void cbAvailableLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Licenses selectedLicense = world.AvailableLicenses[cbAvailableLicenses.SelectedIndex];
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];

            //int index = selectedAgent.LicensesHeld.FindIndex(selectedLicense => selectedLicense.Sport == selectedLicense.Sport);
            //if (index >= 0) licenseIsAgentLicensedLabel.Text = "YES";
            //else licenseIsAgentLicensedLabel.Text = "NO";

            if (selectedAgent.LicensesHeld.Contains(selectedLicense)) licenseIsAgentLicensedLabel.Text = "YES";
            else licenseIsAgentLicensedLabel.Text = "NO";

            licenseApplicationFeeLabel.Text = selectedLicense.ApplicationFee.ToString("C");
            licenseYearlyDuesLabel.Text = selectedLicense.YearlyDues.ToString("C");
            licenseRenewalMonthLabel.Text = selectedLicense.MonthOfRenewal.ToString();

        }
    }
}
