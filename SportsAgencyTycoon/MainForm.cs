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
        public Tennis Tennis = new Tennis();
        public Golf Golf = new Golf();
        public Boxing Boxing = new Boxing();
        public MMA MMA = new MMA();
        public Random rnd = new Random();
        public MainForm()
        {
            InitializeComponent();
            CreateManagerAndAgency();
            InitializeWorld();
        }

        #region Game Start

        public void InitializeWorld()
        {
            world = new World();
            world.CreateLeaguesAssociationEventsPlayersAndTeams();
            newsLabel.Text = "**********************************************************************" + Environment.NewLine + 
                             "           Welcome to DDS:Sports Agency Tycoon!" + Environment.NewLine + 
                             "        Before clients become available, you need to " + Environment.NewLine +
                             "    apply for a new license and pass the respective test." + Environment.NewLine +
                             "   once a license has been obtained, you'll see potential" + Environment.NewLine + 
                             "    clients that you can sign to your agency. To ensure" + Environment.NewLine +
                             "       a better chance at passing the test, have your " + Environment.NewLine + 
                             "  agency train any agent for the test...if you can afford it!" + Environment.NewLine +
                             "**********************************************************************";
            UpdateWorldCalendar();
            world.InitializeLicenses();
            PopulateAvailableLicenses();
            PopulateAvailableClientsList();
            PopulateLeagues();
            PopulateAssociations();
            PopulateEventList();

            // added to test functionality
            // needs to be deleted once everything is working correctly.
            Player p = world.PGA.PlayerList[0];
            Client c = new Client(p.FirstName.ToString(), p.LastName.ToString(), p.Age, p.SkillLevel, p.PotentialSkill, 25, 100, 0, p.Sport, p.BirthMonth, p.BirthWeek);
            Player b = world.WBA.PlayerList[0];
            Client d = new Client(b.FirstName.ToString(), b.LastName.ToString(), b.Age, b.SkillLevel, b.PotentialSkill, 25, 100, 0, b.Sport, b.BirthMonth, b.BirthWeek);
            agency.AddClient(c);
            agency.Agents[0].AddClient(c);
            agency.AddClient(d);
            agency.Agents[0].AddClient(d);
        }
        public void CreateManagerAndAgency()
        {
            Random rnd = new Random();
            agency = new Agency("New Age Agency", 1000000, 5);
            myManager = new Agent("First", "Last", 0, 25, 25, 25, 25, 1, Roles.Manager);
            //Agent agent = new Agent("Tommy", "Twotime", 10000, 20, 20, 20, 50, 3, Roles.Agent);
            agency.AddAgent(myManager);
            //agency.AddAgent(agent);
            //Client client = new Client("Harry", "Giles", 19, rnd.Next(1, 5), rnd.Next(4, 7), rnd.Next(20, 50), 100, 0, Sports.Basketball);
            //agency.AddClient(client);
            //myManager.AddClient(client);
            PopulateAgentClientList(myManager);
            PopulateAgencyAgentList();
            UpdateAgencyInfo();
            UpdateAgentInfo(myManager);
            cbAgencyAgentList.SelectedIndex = 0;
        }

        #endregion

        #region Populate Combo Boxes
        private void PopulateAssociations()
        {
            if (world.Associations.Count > 0)
            {
                cbAssociationRankings.Items.Clear();

                string associationName;

                foreach (Association assoc in world.Associations)
                {
                    associationName = assoc.Name;
                    cbAssociationRankings.Items.Add(associationName);
                }
            }
        }
        private void PopulateEventList()
        {
            if (world.Associations.Count > 0)
            {
                if (cbAssociationRankings.SelectedIndex >= 0)
                {
                    Association selectedAssociation = world.Associations[cbAssociationRankings.SelectedIndex];
                    cbEventDetails.Items.Clear();

                    int eventYear;
                    string eventName;

                    foreach (Event e in selectedAssociation.EventList)
                    {
                        eventYear = e.Year;
                        eventName = e.Name;
                        if (e.Year == world.Year)
                            cbEventDetails.Items.Add(eventYear.ToString() + " " + eventName);
                    }
                }   
            }
        }

        private void PopulateLeagues()
        {
            if (world.Leagues.Count > 0)
            {
                cbLeagueStandings.Items.Clear();

                string leagueName;

                foreach (League league in world.Leagues)
                {
                    leagueName = league.Name;
                    cbLeagueStandings.Items.Add(leagueName);
                }
            }
        }

        private void PopulateAvailableClientsList()
        {
            if (world.AvailableClients.Count > 0)
            {
                cbAvailableClients.Items.Clear();

                string nameAndSport;

                foreach (Client client in world.AvailableClients)
                {
                    nameAndSport = client.First + " " + client.Last + " - " + client.Sport.ToString();
                    cbAvailableClients.Items.Add(nameAndSport);
                }
            }
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

        public void PopulateAgentClientList(Agent agent)
        {
            //empty combo box to repopulate
            cbAgentClientList.Items.Clear();

            string fullName;

            //populate combo box
            foreach (Client client in agent.ClientList)
            {
                fullName = client.First + " " + client.Last;
                cbAgentClientList.Items.Add(fullName);
            }

            //go back to selected client
            cbAgentClientList.SelectedIndex = -1;
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

            //go back to selected license
            cbAvailableLicenses.SelectedIndex = currentSelection;
        }

        #endregion

        #region Update Agency and Agent Info

        public void UpdateAgencyInfo()
        {
            agencyNameLabel.Text = agency.Name;
            moneyLabel.Text = agency.Money.ToString("C0");
            industryInfluenceLabel.Text = agency.IndustryInfluence.ToString() + "/100";
            clientCountLabel.Text = agency.ClientCount.ToString();
            agentCountLabel.Text = agency.AgentCount.ToString();
            monthlyRentLabel.Text = agency.MonthlyRent.ToString("C0");
            if (agency.Agents[0].LicenseTestPrep == 100) btnTrainAgent.Enabled = false;
        }
        public void UpdateAgentInfo(Agent agent)
        {
            agentNameLabel.Text = agent.First + " " + agent.Last;
            greedLabel.Text = agent.Greed.ToString();
            industryPowerLabel.Text = agent.IndustryPower.ToString();
            levelLabel.Text = agent.Level.ToString();
            negotiatingLabel.Text = agent.Negotiating.ToString();
            iqLabel.Text = agent.Intelligence.ToString();
            agentBeingTrainedLabel.Text = agent.BeingTrainedForTest.ToString();
            agentClientCountLabel.Text = agent.ClientCount.ToString();
            roleLabel.Text = "Role: " + agent.Role.ToString();
            agentSalaryLabel.Text = agent.Salary.ToString("C0");

            if (agent.AppliedLicense == null)
            {
                agentAppliedLicenseLabel.Text = "Applied License: ";
                licenseTestPrepLabel.Text = "License Test Prep: ";
            }
            else
            {
                agentAppliedLicenseLabel.Text = "Applied License: " + agent.AppliedLicense.Sport.ToString();
                licenseTestPrepLabel.Text = "License Test Prep: " + agent.LicenseTestPrep.ToString();
            }
            PopulateAgentClientList(agent);
        }

        #endregion

        #region Edit Agent and Agency Info

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
            agency.Agents[0] = myManager;
            UpdateAgencyInfo();
            PopulateAgencyAgentList();
            UpdateAgentInfo(myManager);
        }
        #endregion

        #region Licensure

        private void cbAvailableLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            Licenses selectedLicense;
            bool hasLicense = false;

            

            if (cbAvailableLicenses.SelectedIndex < 0) selectedLicense = world.AvailableLicenses[0];
            else selectedLicense = world.AvailableLicenses[cbAvailableLicenses.SelectedIndex];
                        
            for (var i = 0; i < selectedAgent.LicensesHeld.Count; i++)
            {
                if (selectedAgent.LicensesHeld[i].Sport == selectedLicense.Sport) hasLicense = true;
            }

            if (hasLicense)
            {
                licenseIsAgentLicensedLabel.Text = "YES";
                btnAgentApplyForLicense.Enabled = false;
            }
            else
            {
                licenseIsAgentLicensedLabel.Text = "NO";
                btnAgentApplyForLicense.Enabled = true;
            }

            if (CheckIfAgentHasAlreadyAppliedForALicense(selectedAgent)) btnAgentApplyForLicense.Enabled = false;

            licenseApplicationFeeLabel.Text = selectedLicense.ApplicationFee.ToString("C0");
            licenseYearlyDuesLabel.Text = selectedLicense.YearlyDues.ToString("C0");
            licenseRenewalMonthLabel.Text = selectedLicense.MonthOfRenewal.ToString();
        }

        public bool CheckIfAgentHasAlreadyAppliedForALicense(Agent selectedAgent)
        {
            
            if (selectedAgent.HasAppliedForLicense) return true;
            else return false;
        }

        private void btnAgentApplyForLicense_Click(object sender, EventArgs e)
        {
            Licenses selectedLicense = world.AvailableLicenses[cbAvailableLicenses.SelectedIndex];
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];

            //check if agent is already applied for a license
            if (CheckIfAgentHasAlreadyAppliedForALicense(selectedAgent))
            {
                newsLabel.Text = "This agent is already working towards a license and cannot apply again until test is passed." + Environment.NewLine + newsLabel.Text;
            }
            else
            {
                // check if agency can afford to license you
                if (agency.Money >= selectedLicense.ApplicationFee)
                {
                    //agent applies for license
                    selectedAgent.AppliedLicense = selectedLicense;

                    //agency loses money for application fee
                    agency.Money -= selectedLicense.ApplicationFee;
                    selectedAgent.LicenseTestPrep = 0;
                    selectedAgent.HasAppliedForLicense = true;
                    UpdateAgentInfo(selectedAgent);
                    UpdateAgencyInfo();
                    newsLabel.Text = selectedAgent.First + " " + selectedAgent.Last + " has applied for a license in " + selectedLicense.Sport.ToString().ToLower() + "." + Environment.NewLine + newsLabel.Text;

                    IsAgentBeingTrained(selectedAgent);
                    //logic needs to be added so that application only happens during certain months
                    //exams for different sports also happen at different times
                    //passing an exam will be determined based on IQ rating
                    if (CheckIfAgentHasAlreadyAppliedForALicense(selectedAgent)) btnAgentApplyForLicense.Enabled = false;
                }
                else newsLabel.Text = "The agency doesn't have enough funds to apply for this license." + Environment.NewLine + newsLabel.Text;
            }
        }

        #endregion

        #region Client Information
        private void cbAgentClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            if (selectedAgent.ClientList.Count > 0)
            {
                Client selectedClient = selectedAgent.ClientList[cbAgentClientList.SelectedIndex];

                clientSportLabel.Text = selectedClient.Sport.ToString();
                clientNameLabel.Text = selectedClient.First + " " + selectedClient.Last;
                clientPopularityLabel.Text = selectedClient.PopularityDescription;
                clientTeamHappinessLabel.Text = selectedClient.TeamHappinessDescription;
                clientAgencyHappinessLabel.Text = selectedClient.AgencyHappinessDescription;
                clientSkillLabel.Text = selectedClient.CurrentSkill.ToString() + "/" + selectedClient.PotentialSkill.ToString();
                clientAgeLabel.Text = selectedClient.Age.ToString();
                lblBirthMonth.Text = selectedClient.BirthMonth.ToString();
                lblBirthWeek.Text = selectedClient.BirthWeek.ToString();
            }
            else
            {
                clientSportLabel.Text = "";
                clientNameLabel.Text = "";
                clientPopularityLabel.Text = "";
                clientTeamHappinessLabel.Text = "";
                clientAgencyHappinessLabel.Text = "";
                clientSkillLabel.Text = "";
                clientAgeLabel.Text = "";
            }
        }

        #endregion

        private void cbAgencyAgentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            cbAgentClientList.SelectedIndex = -1;
            UpdateAgentInfo(selectedAgent);

            //resest Available Licenses section
            cbAvailableLicenses.SelectedIndex = -1;
            licenseApplicationFeeLabel.Text = "";
            licenseYearlyDuesLabel.Text = "";
            licenseRenewalMonthLabel.Text = "";
            btnAgentApplyForLicense.Enabled = false;
            licenseIsAgentLicensedLabel.Text = "";

            IsAgentBeingTrained(selectedAgent);
            if (selectedAgent.LicenseTestPrep == 100) btnTrainAgent.Enabled = false;
            CheckIfAgentHasAlreadyAppliedForALicense(selectedAgent);

            CheckTestingWindow(selectedAgent);
            if (selectedAgent.TestedThisWeek) btnTakeTest.Enabled = false;
        }

        #region Agent Training

        private void IsAgentBeingTrained(Agent selectedAgent)
        {
            //check if agent is being trained to enable/disable button
            if (selectedAgent.AppliedLicense == null) btnTrainAgent.Enabled = false;
            else
            {
                if (selectedAgent.BeingTrainedForTest) btnTrainAgent.Enabled = false;
                else btnTrainAgent.Enabled = true;
            }
        }

        private void btnTrainAgent_Click(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];

            //check for funds
            if (agency.Money < 1000) newsLabel.Text = "Agency does not have enough funds to train this agent." + Environment.NewLine + newsLabel.Text;
            else
            {
                agency.Money -= 1000;
                newsLabel.Text = selectedAgent.First + " " + selectedAgent.Last + " has begun training for his test." + Environment.NewLine + newsLabel.Text;
                selectedAgent.BeingTrainedForTest = true;
                UpdateAgencyInfo();
                UpdateAgentInfo(selectedAgent);
                IsAgentBeingTrained(selectedAgent);
            }
        }

        #endregion

        #region Testing
        private void btnTakeTest_Click(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];

            newsLabel.Text = selectedAgent.TakeTest(world, agency) + Environment.NewLine + newsLabel.Text;

            //reset agentInfo
            UpdateAgentInfo(selectedAgent);

            btnTakeTest.Enabled = false;
            PopulateAvailableClientsList();
        }
        private void CheckTestingWindow(Agent agent)
        {
            //check if testing window for specific sport is open
            if (agent.AppliedLicense != null)
            {
                if (world.CheckTestingWindow(world.MonthName, agent.AppliedLicense.Sport)) btnTakeTest.Enabled = true;
                else btnTakeTest.Enabled = false;
            }
        }
        #endregion

        #region Calendar

        private void AdvanceWeekBtn_Click(object sender, EventArgs e)
        {
            //reset if agent took a test this week
            foreach (Agent agent in agency.Agents) agent.TestedThisWeek = false;

            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            world.HandleCalendar(agency);
            UpdateWorldCalendar();

            //check if testing window for specific sport is open
            CheckTestingWindow(selectedAgent);         

            //check for agents training
            agency.TrainAgentsForTest();

            //update Agency and Agent panels
            UpdateAgentInfo(selectedAgent);
            UpdateAgencyInfo();
            world.CheckForEventsThisWeek();
            if (world.EventsThisWeek.Count > 0) DisplayEventsThisWeek();
            RunEventsThisWeek();


            //create new WBA world rankings before AND after King of the Ring event
            if ((world.WeekNumber == 4 && world.MonthName == Months.September) || (world.WeekNumber == 1 && world.MonthName == Months.December))
            {
                Boxing.CalculateWorldRankings(world);
            }
            //create new UFC world rankings before AND after King Kong of the Octagon event
            if ((world.WeekNumber == 4 && world.MonthName == Months.February) || (world.WeekNumber == 3 && world.MonthName == Months.October))
            {
                MMA.CalculateWorldRankings(world);
            }
        }   

        private void UpdateWorldCalendar()
        {
            yearLabel.Text = world.Year.ToString();
            monthLabel.Text = world.MonthName.ToString();
            weekLabel.Text = world.WeekNumber.ToString();
            //beginning of a new month
            if (world.WeekNumber == 1)
            {
                //pay agency rent
                agency.DeductMonthlyCosts(world);
            }
        }

        #endregion

        private void CbAvailableClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client selectedClient = world.AvailableClients[cbAvailableClients.SelectedIndex];

            availableClientAgeLabel.Text = selectedClient.Age.ToString();
            availableClientCurrentSkillLabel.Text = selectedClient.CurrentSkill.ToString();
            availableClientNameLabel.Text = selectedClient.First + " " + selectedClient.Last;
            availableClientSportLabel.Text = selectedClient.Sport.ToString();
            availableClientPopularityLabel.Text = selectedClient.PopularityDescription;
        }

        private void cbLeagueStandings_SelectedIndexChanged(object sender, EventArgs e)
        {
            League selectedLeague = world.Leagues[cbLeagueStandings.SelectedIndex];

            leagueStandingsLabel.Text = "";

            List<Team> teamList = selectedLeague.TeamList;
            teamList = teamList.OrderByDescending(o => o.TitleConteder).ToList();
            for (int i = 0; i < teamList.Count; i++)
            {
                leagueStandingsLabel.Text += (i + 1) + ") " + teamList[i].City + " " + teamList[i].Mascot + " " + teamList[i].TitleConteder + Environment.NewLine;
            }

        }

        private void CbAssociationRankings_SelectedIndexChanged(object sender, EventArgs e)
        {
            Association selectedAssociation = world.Associations[cbAssociationRankings.SelectedIndex];

            worldRankingsLabel.Text = "";

            List<Player> playerList = selectedAssociation.PlayerList;
            playerList = playerList.OrderBy(o => o.WorldRanking).ToList();

            if(selectedAssociation.Sport == Sports.Golf)
            {
                worldRankingsLabel.Text += "#) - NAME | PTS | $$$ | TOP-10s | TITLES | MAJROS" + Environment.NewLine;
                foreach (Golfer golfer in playerList)
                {
                    worldRankingsLabel.Text += golfer.WorldRanking + ") " + golfer.FirstName + " " + golfer.LastName + ": " + golfer.TourPoints.ToString() + " | " + golfer.CareerEarnings.ToString("C0") + " | " + golfer.TopTenFinishes + " | " + golfer.TournamentWins + " | " + golfer.Majors +  Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.Tennis)
            {
                worldRankingsLabel.Text += "#) - NAME | PTS | $$$ | QFIN APP | TITLES | SLAMS" + Environment.NewLine;
                foreach (TennisPlayer tp in playerList)
                {
                    worldRankingsLabel.Text += tp.WorldRanking + ") " + tp.FirstName + " " + tp.LastName + ": " + tp.TourPoints.ToString() + " | " + tp.CareerEarnings.ToString("C0") + " | " + tp.QuarterFinals + " | " + tp.TournamentWins + " | " + tp.GrandSlams + Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.Boxing)
            {
                worldRankingsLabel.Text += "World Ranking - Name (Skill): Earnings | (Wins - Losses)" + Environment.NewLine;
                foreach (Boxer boxer in playerList)
                {
                    worldRankingsLabel.Text += boxer.WorldRanking + ") " + boxer.FirstName + " " + boxer.LastName + " (" + boxer.SkillLevel + "): " + boxer.CareerEarnings.ToString("C0") + " | (" + boxer.Wins + " - " + boxer.Losses + ")" + Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.MMA)
            {
                worldRankingsLabel.Text += "World Ranking - Name: Earnings | (Wins - Losses)" + Environment.NewLine;
                foreach (MMAFighter mma in playerList)
                {
                    worldRankingsLabel.Text += mma.WorldRanking + ") " + mma.FirstName + " " + mma.LastName + " (" + mma.SkillLevel + "): " + mma.CareerEarnings.ToString("C0") + " | (" + mma.Wins + " - " + mma.Losses + ")" + Environment.NewLine;
                }
            }

            PopulateEventList();
            cbEventDetails.SelectedIndex = -1;
            ClearEventDetails();
        }

        private void CbEventDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssociationRankings.SelectedIndex >= 0)
            {
                Association selectedAssociation = world.Associations[cbAssociationRankings.SelectedIndex];
                Event selectedEvent = selectedAssociation.EventList[cbEventDetails.SelectedIndex];

                eventNameLabel.Text = selectedEvent.Name;
                eventLocationLabel.Text = selectedEvent.Location;

                string weekOfEvent = "";
                if (selectedEvent.EventDate.Week == 1) weekOfEvent = "1st";
                else if (selectedEvent.EventDate.Week == 2) weekOfEvent = "2nd";
                else if (selectedEvent.EventDate.Week == 3) weekOfEvent = "3rd";
                else if (selectedEvent.EventDate.Week == 4) weekOfEvent = "4th";
                else if (selectedEvent.EventDate.Week == 5) weekOfEvent = "5th";

                eventDateLabel.Text = weekOfEvent + " week of " + selectedEvent.EventDate.MonthName.ToString();

                eventPrizePoolLabel.Text = "Total Purse: " + selectedEvent.PrizePool.ToString("C0");
                eventNumberOfEntrantsLabel.Text = "# of Entrants: " + selectedEvent.NumberOfEntrants.ToString();
            }
        }
        private void ClearEventDetails()
        {
            eventNameLabel.Text = "";
            eventDateLabel.Text = "";
            eventPrizePoolLabel.Text = "";
            eventNumberOfEntrantsLabel.Text = "";
            eventLocationLabel.Text = "";
        }

        private void DisplayEventsThisWeek()
        {
            string output;
            output = "Events this week:" + Environment.NewLine;
            foreach (Event e in world.EventsThisWeek)
            {
                output += e.Name + Environment.NewLine;
            }
            newsLabel.Text = output + newsLabel.Text;
        }
        private void RunEventsThisWeek()
        {
            foreach (Event e in world.EventsThisWeek)
            {
                if (e.Sport == Sports.Tennis)
                {
                    newsLabel.Text = Tennis.RunTournament(e, world) + newsLabel.Text;
                }
                else if (e.Sport == Sports.Golf)
                {
                    newsLabel.Text = Golf.RunTournament(e, world) + newsLabel.Text;
                }
                else if (e.Sport == Sports.MMA)
                {
                    newsLabel.Text = MMA.RunMMAEvent(e, world) + newsLabel.Text;
                }
                else if (e.Sport == Sports.Boxing)
                {
                    newsLabel.Text = Boxing.RunBoxingEvent(e, world) + newsLabel.Text;
                }
            }
        }

        private void btnHireAgent_Click(object sender, EventArgs e)
        {
            Agent hiredAgent = null;
            AgentSearch newAgentSearch = new AgentSearch(agency);
            newAgentSearch.BringToFront();
            newAgentSearch.ShowDialog();

            int fundsSpent = newAgentSearch.FundsSpent;
            string agentType = newAgentSearch.AgentType;
            Console.WriteLine(fundsSpent);
            Console.WriteLine(agentType);
            agency.Money -= fundsSpent;
            UpdateAgencyInfo();

            if (fundsSpent != 0)
            {
                HireAgentForm hireAgentForm = new HireAgentForm(fundsSpent, agentType);
                hireAgentForm.HowManyAgents();
                hireAgentForm.CreateApplicants(world, rnd);
                hireAgentForm.BringToFront();
                hireAgentForm.ShowDialog();

                hiredAgent = hireAgentForm.HiredAgent;
                if (hiredAgent != null)
                {
                    agency.Agents.Add(hiredAgent);
                    agency.AgentCount++;
                    UpdateAgencyInfo();
                    PopulateAgencyAgentList();
                }
            }
        }
    }
}
