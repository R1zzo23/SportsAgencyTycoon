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
        }
        public void CreateManagerAndAgency()
        {
            Random rnd = new Random();
            agency = new Agency("New Age Agency", 1000000, 5);
            myManager = new Agent("First", "Last", 0, 10, 10, 10, 75, 1, Roles.Manager);
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
                Client selectedClient = selectedAgent.ClientList[0];

                clientSportLabel.Text = selectedClient.Sport.ToString();
                clientNameLabel.Text = selectedClient.First + " " + selectedClient.Last;
                clientPopularityLabel.Text = selectedClient.PopularityDescription;
                clientTeamHappinessLabel.Text = selectedClient.TeamHappinessDescription;
                clientAgencyHappinessLabel.Text = selectedClient.AgencyHappinessDescription;
                clientSkillLabel.Text = selectedClient.CurrentSkill.ToString() + "/" + selectedClient.PotentialSkill.ToString();
                clientAgeLabel.Text = selectedClient.Age.ToString();
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
            world.HandleCalendar();
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
                agency.DeductMonthlyRent();
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
                worldRankingsLabel.Text += "World Ranking - Name | Earnings | Top Tens | Tourney Wins" + Environment.NewLine;
                foreach (Golfer golfer in playerList)
                {
                    worldRankingsLabel.Text += golfer.WorldRanking + ") " + golfer.FirstName + " " + golfer.LastName + ": " + golfer.CareerEarnings.ToString("C0") + " | " + golfer.TopTenFinishes + " | " + golfer.TournamentWins +  Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.Tennis)
            {
                worldRankingsLabel.Text += "World Ranking - Name | Earnings | Quarters | Tourney Wins | Grand Slams" + Environment.NewLine;
                foreach (TennisPlayer tp in playerList)
                {
                    worldRankingsLabel.Text += tp.WorldRanking + ") " + tp.FirstName + " " + tp.LastName + ": " + tp.CareerEarnings.ToString("C0") + " | " + tp.QuarterFinals + " | " + tp.TournamentWins + " | " + tp.GrandSlams + Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.Boxing)
            {
                worldRankingsLabel.Text += "World Ranking - Name: Earnings | (Wins - Losses)" + Environment.NewLine;
                foreach (Boxer boxer in playerList)
                {
                    worldRankingsLabel.Text += boxer.WorldRanking + ") " + boxer.FirstName + " " + boxer.LastName + ": " + boxer.CareerEarnings.ToString("C0") + " | (" + boxer.Wins + " - " + boxer.Losses + ")" + Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.MMA)
            {
                worldRankingsLabel.Text += "World Ranking - Name: Earnings | (Wins - Losses)" + Environment.NewLine;
                foreach (MMAFighter mma in playerList)
                {
                    worldRankingsLabel.Text += mma.WorldRanking + ") " + mma.FirstName + " " + mma.LastName + ": " + mma.CareerEarnings.ToString("C0") + " | (" + mma.Wins + " - " + mma.Losses + ")" + Environment.NewLine;
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
                eventNumberOfEntrantsLabel.Text = "# of Entratnts: " + selectedEvent.NumberOfEntrants.ToString();
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
                    RunTennisTournament(e);
                }
            }
        }
        private void RunTennisTournament(Event e)
        {
            Random rnd = new Random();
            int x = 0;
            List<Player> loserList = new List<Player>();
            List<Player> tempList = new List<Player>();
            List<Player> winnerList = new List<Player>();
            List<Player> playerList = world.ATP.PlayerList.OrderBy(o => o.WorldRanking).ToList();
            List<TennisPlayer> listOfTennisPlayers = new List<TennisPlayer>();
            foreach (TennisPlayer t in playerList) listOfTennisPlayers.Add(t);
            //add players to event
            for (int i = 0; i < e.NumberOfEntrants; i++)
            {
                e.EntrantList.Add(listOfTennisPlayers[i]);
            }

            foreach (Player p in e.EntrantList) tempList.Add(p);

            while (loserList.Count < e.NumberOfEntrants - 1)
            {
                winnerList.Clear();
                for (x = 0; x < (tempList.Count / 2); x++)
                {
                    int result;
                    result = PlayTennisMatch(tempList[x], tempList[tempList.Count - x - 1], rnd);
                    if (result == 1)
                    {
                        winnerList.Add(tempList[x]);
                        loserList.Insert(0, tempList[tempList.Count - x - 1]);
                    }
                    else if (result == 2)
                    {
                        winnerList.Add(tempList[tempList.Count - x - 1]);
                        loserList.Insert(0, tempList[x]);
                    }
                }
                tempList.Clear();
                foreach (Player p in winnerList) tempList.Add(p);
            }
            loserList.Insert(0, winnerList[0]);
            Console.WriteLine("{0} {1} is the {2} {3} champion!!!", loserList[0].FirstName, loserList[0].LastName, e.Year, e.Name);
            Console.WriteLine("Here is the rest of the top-10 finishers:");
            for (var i = 0; i < 9; i++)
            {
                Console.WriteLine(i + 1 + ") {0} {1}", loserList[i].FirstName, loserList[i].LastName);
            }
            AwardTennisPayouts(e, loserList);
            AwardTennisTourPoints();
        }
        private int PlayTennisMatch(Player p1, Player p2, Random rnd)
        {
            int winningPlayer = 0;
            TennisPlayer t1 = (TennisPlayer)p1;
            TennisPlayer t2 = (TennisPlayer)p2;
            Console.WriteLine("{0} {1} ({2}) vs. {3} {4} ({5}).", t1.FirstName, t1.LastName, t1.SkillLevel, t2.FirstName, t2.LastName, t2.SkillLevel);

            int t1SetsWon = 0;
            int t2SetsWon = 0;

            int totalSkill = t1.SkillLevel + t2.SkillLevel;

            while (t1SetsWon < 4 && t2SetsWon < 4)
            {
                int luckyNumber = rnd.Next(0, totalSkill);
                if (luckyNumber <= t1.SkillLevel) t1SetsWon++;
                else t2SetsWon++;
            }
            if (t1SetsWon == 4)
            {
                Console.WriteLine("{0} {1} defeats {2} {3} {4} sets to {5}.", t1.FirstName, t1.LastName, t2.FirstName, t2.LastName, t1SetsWon, t2SetsWon);
                winningPlayer = 1;
            }
            else if (t2SetsWon == 4)
            {
                Console.WriteLine("{0} {1} defeats {2} {3} {4} sets to {5}.", t2.FirstName, t2.LastName, t1.FirstName, t1.LastName, t2SetsWon, t1SetsWon);
                winningPlayer = 2;
            }
            return winningPlayer;
        }

        private void AwardTennisPayouts(Event e, List<Player> list)
        {
            int purse = e.PrizePool;

            for (var i = 0; i < list.Count; i++)
            {
                //champ gets 17.06%
                if (i < 1) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .1706));
                //runner-up: 9.21%
                else if (i < 2) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0921));
                //Semi-Finalists Losers: 4.66%
                else if (i < 4) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0466));
                //Quarter-Finalists Losers: 2.56%
                else if (i < 8) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0256));
                //4th Round Losers: 1.40%
                else if (i < 16) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0140));
                //3rd Round Losers: 0.79%
                else if (i < 32) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0079));
                //2nd Round Losers: 0.46%
                else if (i < 64) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0046));
                //1st Round Losers: 0.25%
                else if (i < 128) list[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0025));
            }
        }
        private void AwardTennisTourPoints()
        {
            // GrandSlamPoints = [2000, 1200, 720, 360, 180, 90, 45, 10]
        }
    }
}
