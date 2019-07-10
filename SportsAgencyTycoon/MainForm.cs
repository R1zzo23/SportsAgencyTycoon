﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            PopUpStartGameForm();
        }

        #region Game Start

        public void InitializeWorld()
        {
            world = new World(this);
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
            //PopulateAvailableClientsList();
            PopulateLeagues();
            PopulateAssociations();
            PopulateEventList();

            // added to test functionality
            // needs to be deleted once everything is working correctly.
            Player p = world.PGA.PlayerList[0];
            Client c = new Client(rnd, 98, p.FirstName.ToString(), p.LastName.ToString(), p.Age, p.CurrentSkill, p.PotentialSkill, 25, 100, 0, p.Sport, p.BirthMonth, p.BirthWeek);
            Player b = world.WBA.PlayerList[0];
            Client d = new Client(rnd, 99, b.FirstName.ToString(), b.LastName.ToString(), b.Age, b.CurrentSkill, b.PotentialSkill, 25, 100, 0, b.Sport, b.BirthMonth, b.BirthWeek);
            agency.AddClient(c);
            agency.Agents[0].AddClient(c);
            world.Calendar.AddCalendarEvent(new CalendarEvent(c));
            agency.AddClient(d);
            agency.Agents[0].AddClient(d);
            world.Calendar.AddCalendarEvent(new CalendarEvent(d));
            // delete above when ready
        }
        public void PopUpStartGameForm()
        {
            StartGameForm startGameForm = new StartGameForm();
            startGameForm.BringToFront();
            startGameForm.ShowDialog();

            string agencyName = startGameForm.AgencyName;
            string firstName = startGameForm.FirstName;
            string lastName = startGameForm.LastName;
            int teamIndex = startGameForm.TeamIndex;
            int individualIndex = startGameForm.IndividualIndex;

            if (teamIndex == world.TeamSportLicenses.Count) teamIndex = rnd.Next(0, world.TeamSportLicenses.Count);
            if (individualIndex == world.IndividualSportLicense.Count) individualIndex = rnd.Next(0, world.IndividualSportLicense.Count);

            agency.Name = agencyName;
            myManager.First = firstName;
            myManager.Last = lastName;
            myManager.LicensesHeld.Add(world.TeamSportLicenses[teamIndex]);
            world.AvailableLicenses.Add(world.TeamSportLicenses[teamIndex]);
            world.IndividualSportLicense.Remove(world.TeamSportLicenses[teamIndex]);
            myManager.LicensesHeld.Add(world.IndividualSportLicense[individualIndex]);
            world.AvailableLicenses.Add(world.IndividualSportLicense[individualIndex]);
            world.IndividualSportLicense.Remove(world.IndividualSportLicense[individualIndex]);
            agency.Agents[0] = myManager;
            UpdateAgencyInfo();
            UpdateAgentInfo(myManager);
            PopulateAgencyAgentList();
        }
        public void CreateManagerAndAgency()
        {
            Random rnd = new Random();
            agency = new Agency("New Age Agency", 1000000, 5);
            myManager = new Agent("First", "Last", 0, 25, 25, 25, 25, 1, Roles.Manager);
            agency.AddAgent(myManager);
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

        //available clients to sign are based on selectedAgent
        //agent needs to have the necessary license, player needs
        //to be unhappy with his current agency and player skill
        //cannot heavily outweigh the agent's abilities
        private void PopulateAvailableClientsList()
        {
            List<Player> availableClients = new List<Player>();
            League league = null;
            Association association = null;
            bool isLeague;

            if (cbAgencyAgentList.SelectedIndex >= 0)
            {
                Agent agent = agency.Agents[cbAgencyAgentList.SelectedIndex];
                int agentSkills = (int)((agent.IndustryPower + agent.Intelligence + agent.Negotiating) * 0.75);
                Console.WriteLine("agentSkills: " + agentSkills);

                foreach (Licenses l in agent.LicensesHeld)
                {
                    if (l.Sport == Sports.Baseball)
                    {
                        league = world.MLB;
                        isLeague = true;
                    }
                    else if (l.Sport == Sports.Basketball)
                    {
                        league = world.NBA;
                        isLeague = true;
                    }
                    else if (l.Sport == Sports.Football)
                    {
                        league = world.NFL;
                        isLeague = true;
                    }
                    else if (l.Sport == Sports.Hockey)
                    {
                        league = world.NHL;
                        isLeague = true;
                    }
                    else if (l.Sport == Sports.Soccer)
                    {
                        league = world.MLS;
                        isLeague = true;
                    }
                    else if (l.Sport == Sports.Boxing)
                    {
                        association = world.WBA;
                        isLeague = false;
                    }
                    else if (l.Sport == Sports.Golf)
                    {
                        association = world.PGA;
                        isLeague = false;
                    }
                    else if (l.Sport == Sports.MMA)
                    {
                        association = world.UFC;
                        isLeague = false;
                    }
                    else if (l.Sport == Sports.Tennis)
                    {
                        association = world.ATP;
                        isLeague = false;
                    }
                    else isLeague = false;

                    //add all players that are currently disgruntled with their current agency
                    //players currentSkill + potentialSkill needs to be less than the sum
                    //of the agent's IndustryPower, Intelligence and Negotiationg levels
                    if (isLeague)
                    {
                        foreach (Team t in league.TeamList)
                            foreach (Player p in t.Roster)
                                if ((p.AgencyHappinessDescription == HappinessDescription.Disgruntled || p.AgencyHappinessDescription == HappinessDescription.Displeased) && (p.CurrentSkill + p.PotentialSkill) < agentSkills)
                                    availableClients.Add(p);
                    }
                    else
                    {
                        foreach (Player p in association.PlayerList)
                            if (p.AgencyHappinessDescription == HappinessDescription.Disgruntled && (p.CurrentSkill + p.PotentialSkill) < agentSkills)
                                availableClients.Add(p);
                    }
                }
            
            }

            if (availableClients.Count > 0)
            {
                cbAvailableClients.Items.Clear();

                world.AvailableClients.Clear();
                foreach (Player p in availableClients) world.AvailableClients.Add(p);

                string nameAndSport;

                foreach (Player p in availableClients)
                {
                    nameAndSport = p.FullName + " - " + p.Sport.ToString();
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
            lblLicensesHeldByAgent.Text = "Licenses Held: ";
            for (int i = 0; i < agent.LicensesHeld.Count; i++)
            {
                lblLicensesHeldByAgent.Text += agent.LicensesHeld[i].Sport.ToString() + ", ";
            }
            lblLicensesHeldByAgent.Text = lblLicensesHeldByAgent.Text.Substring(0, lblLicensesHeldByAgent.Text.Length - 2);
            PopulateAgentClientList(agent);
            PopulateAvailableClientsList();
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
                clientPopularityLabel.Text = selectedClient.PopularityString;

                clientTeamHappinessLabel.Text = selectedClient.TeamHappinessString;
                clientAgencyHappinessLabel.Text = selectedClient.AgencyHappinessString;
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

            PopulateAvailableClientsList();
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
            //PopulateAvailableClientsList();
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
            RunEventsThisWeek();

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
            Player selectedClient = world.AvailableClients[cbAvailableClients.SelectedIndex];

            availableClientAgeLabel.Text = selectedClient.Age.ToString();
            availableClientCurrentSkillLabel.Text = selectedClient.CurrentSkill.ToString();
            availableClientNameLabel.Text = selectedClient.FullName;
            availableClientSportLabel.Text = selectedClient.Sport.ToString();

            availableClientPopularityLabel.Text = selectedClient.PopularityString;
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
                worldRankingsLabel.Text += "#) - NAME (AGE) | PTS | $$$ | TOP-10s | TITLES | MAJROS" + Environment.NewLine;
                foreach (Golfer golfer in playerList)
                {
                    worldRankingsLabel.Text += golfer.WorldRanking + ") " + golfer.FullName + " (" + golfer.Age + ") : " + golfer.TourPoints.ToString() + " | " + golfer.CareerEarnings.ToString("C0") + " | " + golfer.TopTenFinishes + " | " + golfer.TournamentWins + " | " + golfer.Majors +  Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.Tennis)
            {
                worldRankingsLabel.Text += "#) - NAME (AGE) | PTS | $$$ | QFIN APP | TITLES | SLAMS" + Environment.NewLine;
                foreach (TennisPlayer tp in playerList)
                {
                    worldRankingsLabel.Text += tp.WorldRanking + ") " + tp.FullName + " (" + tp.Age + ") : " + tp.TourPoints.ToString() + " | " + tp.CareerEarnings.ToString("C0") + " | " + tp.QuarterFinals + " | " + tp.TournamentWins + " | " + tp.GrandSlams + Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.Boxing)
            {
                worldRankingsLabel.Text += "World Ranking - Name (AG) (Skill): Earnings | (Wins - Losses)" + Environment.NewLine;
                foreach (Boxer boxer in playerList)
                {
                    worldRankingsLabel.Text += boxer.WorldRanking + ") " + boxer.FullName + " (" + boxer.Age + ") : (" + boxer.CurrentSkill + "): " + boxer.CareerEarnings.ToString("C0") + " | (" + boxer.Wins + " - " + boxer.Losses + ")" + Environment.NewLine;
                }
            }
            else if (selectedAssociation.Sport == Sports.MMA)
            {
                worldRankingsLabel.Text += "World Ranking - Name: Earnings | (Wins - Losses)" + Environment.NewLine;
                foreach (MMAFighter mma in playerList)
                {
                    worldRankingsLabel.Text += mma.WorldRanking + ") " + mma.FullName + " (" + mma.Age + ") : (" + mma.CurrentSkill + "): " + mma.CareerEarnings.ToString("C0") + " | (" + mma.Wins + " - " + mma.Losses + ")" + Environment.NewLine;
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
            string eventList = "";
            output = "Events this week:" + Environment.NewLine;
            foreach (CalendarEvent e in world.EventsThisWeek)
            {
                if (e.EventType != CalendarEventType.PlayerBirthday)
                    eventList += e.EventName + Environment.NewLine;
            }
            if (eventList != "")
            newsLabel.Text = output + eventList + Environment.NewLine + newsLabel.Text;
        }
        private void RunEventsThisWeek()
        {
            foreach (CalendarEvent e in world.EventsThisWeek)
            {
                if (e.EventType == CalendarEventType.AssociationEvent)
                {
                    if (e.Sport == Sports.Tennis)
                    {
                        int eventIndex = world.ATP.EventList.FindIndex(x => x.Id.Equals(e.EventID));
                        Event thisEvent = world.ATP.EventList.Find(x => x.Id == e.EventID);
                        newsLabel.Text = Tennis.RunTournament(world.ATP.EventList[eventIndex], world) + newsLabel.Text;
                    }
                    else if (e.Sport == Sports.Golf)
                    {
                        Event thisEvent = world.PGA.EventList.Find(x => x.Id == e.EventID);
                        newsLabel.Text = Golf.RunTournament(thisEvent, world) + newsLabel.Text;
                    }
                    else if (e.Sport == Sports.MMA)
                    {
                        Event thisEvent = world.UFC.EventList.Find(x => x.Id == e.EventID);
                        newsLabel.Text = MMA.RunMMAEvent(thisEvent, world) + newsLabel.Text;
                    }
                    else if (e.Sport == Sports.Boxing)
                    {
                        Event thisEvent = world.WBA.EventList.Find(x => x.Id == e.EventID);
                        newsLabel.Text = Boxing.RunBoxingEvent(thisEvent, world) + newsLabel.Text;
                    }
                }
                else if (e.EventType == CalendarEventType.PlayerBirthday)
                {
                    if (e.Sport == Sports.Tennis)
                    {
                        world.ATP.PlayerList.Find(x => x.Id == e.PlayerID).Age++;
                    }
                    else if (e.Sport == Sports.Golf)
                    {
                        world.PGA.PlayerList.Find(x => x.Id == e.PlayerID).Age++;
                    }
                    else if (e.Sport == Sports.MMA)
                    {
                        world.UFC.PlayerList.Find(x => x.Id == e.PlayerID).Age++;
                    }
                    else if (e.Sport == Sports.Boxing)
                    {
                        world.WBA.PlayerList.Find(x => x.Id == e.PlayerID).Age++;
                    }
                }
                else if (e.EventType == CalendarEventType.LoanRepayment)
                {
                    //repay loan
                }
                else if (e.EventType == CalendarEventType.ClientBirthday)
                {
                    int indx = agency.Clients.FindIndex(x => (x.FullName == e.PlayerName) && (x.Sport == e.Sport) && (x.Id == e.PlayerID));
                    agency.Clients[indx].Age++;
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

        private void btnViewCalendar_Click(object sender, EventArgs e)
        {
            CalendarForm calendarForm = new CalendarForm(world.Calendar);
            calendarForm.BringToFront();
            calendarForm.ShowDialog();
        }

        private void btnViewRosters_Click(object sender, EventArgs e)
        {
            TeamRosterForm teamRosterForm = new TeamRosterForm(world);
            teamRosterForm.BringToFront();
            teamRosterForm.ShowDialog();
        }
    }
}
