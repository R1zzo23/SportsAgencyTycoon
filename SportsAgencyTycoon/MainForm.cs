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
        public Tennis Tennis;
        public Golf Golf;
        public Boxing Boxing;
        public MMA MMA;
        public Random rnd = new Random();
        public ProgressionRegression ProgressionRegression;
        public MainForm()
        {
            InitializeComponent();
            CreateManagerAndAgency();
            InitializeWorld();
            PopUpStartGameForm();
            Golf = new Golf(agency);
            Boxing = new Boxing(agency);
            MMA = new MMA(agency);
            Tennis = new Tennis(agency);
            world.InitializeCalendar(myManager.LicensesHeld[0]);
            ProgressionRegression = new ProgressionRegression(rnd, world);
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
            PopulateLeagues();
            PopulateAssociations();
            PopulateEventList();
            DetermineSeasons();
            //NerfFreeAgentsToStart();
            world.Basketball = new Basketball(this, rnd, world, world.NBA);
            world.Baseball = new Baseball(this, rnd, world, world.MLB);
            world.Football = new Football(this, rnd, world, world.NFL);
            world.Hockey = new Hockey(this, rnd, world, world.NHL);
            world.Soccer = new Soccer(this, rnd, world, world.MLS);
            world.CreateGlobalAchievements();
            world.CreateTeamRelationships(myManager);
        }

        public void NerfFreeAgentsToStart()
        {
            foreach (League l in world.Leagues)
                foreach (Player p in l.FreeAgents)
                {
                    if (p.CurrentSkill > 45) p.CurrentSkill = 40;
                    if (p.PotentialSkill > 55) p.PotentialSkill = 50;
                }                   
        }

        public void DetermineSeasons()
        {
            if (world.MLB.InSeason) lblBaseballSeason.Text = "YES";
            else lblBaseballSeason.Text = "NO";

            if (world.NBA.InSeason) lblBasketballSeason.Text = "YES";
            else lblBasketballSeason.Text = "NO";

            if (world.NFL.InSeason) lblFootballSeason.Text = "YES";
            else lblFootballSeason.Text = "NO";

            if (world.NHL.InSeason) lblHockeySeason.Text = "YES";
            else lblHockeySeason.Text = "NO";

            if (world.MLS.InSeason) lblSoccerSeason.Text = "YES";
            else lblSoccerSeason.Text = "NO";
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
            myManager.FullName = myManager.First + " " + myManager.Last;
            myManager.LicensesHeld.Add(world.TeamSportLicenses[teamIndex]);
            world.AvailableLicenses.Add(world.TeamSportLicenses[teamIndex]);
            world.IndividualSportLicense.Remove(world.TeamSportLicenses[teamIndex]);
            myManager.LicensesHeld.Add(world.IndividualSportLicense[individualIndex]);
            world.AvailableLicenses.Add(world.IndividualSportLicense[individualIndex]);
            world.IndividualSportLicense.Remove(world.IndividualSportLicense[individualIndex]);
            agency.Agents[0] = myManager;

            Sports licensedSport = myManager.LicensesHeld[0].Sport;
            if (licensedSport == Sports.Baseball)
            {
                world.MonthName = world.MLB.SeasonStart.MonthName;
                world.MonthNumber = (int)world.MonthName;
                world.WeekNumber = world.MLB.SeasonStart.Week;
                world.MLB.Initialized = true;
            }
            else if (licensedSport == Sports.Basketball)
            {
                world.MonthName = world.NBA.SeasonStart.MonthName;
                world.MonthNumber = (int)world.MonthName;
                world.WeekNumber = world.NBA.SeasonStart.Week;
                world.NBA.Initialized = true;
            }
            else if (licensedSport == Sports.Football)
            {
                world.MonthName = world.NFL.SeasonStart.MonthName;
                world.MonthNumber = (int)world.MonthName;
                world.WeekNumber = world.NFL.SeasonStart.Week;
                world.NFL.Initialized = true;
            }
            else if (licensedSport == Sports.Hockey)
            {
                world.MonthName = world.NHL.SeasonStart.MonthName;
                world.MonthNumber = (int)world.MonthName;
                world.WeekNumber = world.NHL.SeasonStart.Week;
                world.NHL.Initialized = true;
            }
            else if (licensedSport == Sports.Soccer)
            {
                world.MonthName = world.MLS.SeasonStart.MonthName;
                world.MonthNumber = (int)world.MonthName;
                world.WeekNumber = world.MLS.SeasonStart.Week;
                world.MLS.Initialized = true;
            }
            yearLabel.Text = world.Year.ToString();
            monthLabel.Text = world.MonthName.ToString();
            weekLabel.Text = world.WeekNumber.ToString();

            UpdateAgencyInfo();
            UpdateAgentInfo(myManager);
            PopulateAgencyAgentList();
        }
        public void CreateManagerAndAgency()
        {
            Random rnd = new Random();
            agency = new Agency("New Age Agency", 1000000, 0);
            myManager = new Agent("First", "Last", 0, 10, 10, 10, 10, 1, Roles.Manager);
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
            cbAvailableClients.SelectedIndex = -1;
            if (cbAgencyAgentList.SelectedIndex >= 0)
            {
                Agent agent = agency.Agents[cbAgencyAgentList.SelectedIndex];

                int agentSkills = (int)((agent.IndustryPower + agent.Intelligence + agent.Negotiating) * .75);

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
                        if (league.DeclaredEntrants)
                            foreach (Player p in league.DraftEntrants)
                                availableClients.Add(p);
                        foreach (Player p in league.FreeAgents)
                            if ((p.WillingToNegotiate) && (p.AgencyHappinessDescription == HappinessDescription.Disgruntled || p.AgencyHappinessDescription == HappinessDescription.Displeased) && (p.CurrentSkill + p.PotentialSkill) < agentSkills)
                                availableClients.Add(p);

                        foreach (Team t in league.TeamList)
                            foreach (Player p in t.Roster)
                                if ((p.WillingToNegotiate) && (p.AgencyHappinessDescription == HappinessDescription.Disgruntled || p.AgencyHappinessDescription == HappinessDescription.Displeased) && (p.CurrentSkill + p.PotentialSkill) < agentSkills)
                                    availableClients.Add(p);
                    }
                    else
                    {
                        foreach (Player p in association.PlayerList)
                            if ((p.WillingToNegotiate) && (p.AgencyHappinessDescription == HappinessDescription.Disgruntled || p.AgencyHappinessDescription == HappinessDescription.Displeased) && (p.CurrentSkill/* + p.PotentialSkill*/) < agentSkills)
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

                foreach (Player p in world.AvailableClients)
                {
                    if (p.Sport == Sports.Baseball || p.Sport == Sports.Basketball || p.Sport == Sports.Football || p.Sport == Sports.Hockey || p.Sport == Sports.Soccer)
                        if (p.Team != null)
                        {
                            nameAndSport = "[" + p.Team.Abbreviation + "] " + p.Position.ToString() + " " + p.FullName + " - " + p.Sport.ToString();
                        }
                        else
                        {
                            if (p.PlayerType == PlayerType.DraftEntrant)
                                nameAndSport = "[Draft] " + p.Position.ToString() + " " + p.FullName + " - " + p.Sport.ToString();
                            else
                                nameAndSport = "[Free Agent] " + p.Position.ToString() + " " + p.FullName + " - " + p.Sport.ToString();
                        }
                    else
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

            //populate combo box
            foreach (Player client in agent.ClientList)
            {
                if (client.PlayerType == PlayerType.Individual)
                    cbAgentClientList.Items.Add(client.FullName + " [" + client.Sport.ToString() + "]");
                else
                {
                    if (client.FreeAgent)
                        cbAgentClientList.Items.Add("Free Agent " + client.Position.ToString() + " " + client.FullName);
                    else
                    {
                        if (client.PlayerType == PlayerType.DraftEntrant)
                            cbAgentClientList.Items.Add("Draft Entrant " + client.Position.ToString() + " " + client.FullName);
                        else
                            cbAgentClientList.Items.Add(client.Team.Mascot + " " + client.Position.ToString() + " " + client.FullName);
                    }
                }
                    
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
            //check for WS Fanboy achievement
            if (agency.Name == "Wolverine Studios")
                agency.AddAchievementToAgency(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "WS Fanboy")]);

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
            //check for Dev Flattery achievement
            if ((myManager.First == "Adam" && myManager.Last == "Rizzo") || (myManager.First == "Gary" && myManager.Last == "Gorski"))
                myManager.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Dev Flattery")]);

            agentNameLabel.Text = agent.First + " " + agent.Last;
            lblAgentCareerEarnings.Text = agent.CareerEarnings.ToString("C0");
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
            if (agent.ClientCount < agent.Level * 2) btnNegotiatePercent.Enabled = true;
            else btnNegotiatePercent.Enabled = false;
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
            if (selectedAgent.ClientList.Count > 0 && cbAgentClientList.SelectedIndex >= 0)
            {
                Player selectedClient = selectedAgent.ClientList[cbAgentClientList.SelectedIndex];
                if (selectedClient.PlayerType == PlayerType.Individual) btnClientCallTeams.Enabled = false;
                else
                {
                    btnClientCallTeams.Enabled = true;
                    if (selectedClient.FreeAgent) btnClientCallTeams.Text = "Call Teams";
                    else btnClientCallTeams.Text = "Call GM";
                }

                clientSportLabel.Text = selectedClient.Sport.ToString();
                clientNameLabel.Text = selectedClient.FullName;
                clientPopularityLabel.Text = selectedClient.PopularityString;

                clientTeamHappinessLabel.Text = selectedClient.TeamHappinessString;
                clientAgencyHappinessLabel.Text = selectedClient.AgencyHappinessString;
                clientSkillLabel.Text = selectedClient.CurrentSkill.ToString() + "/" + selectedClient.PotentialSkill.ToString();
                lblClientPriorSkill.Text = "Prior Skill: " + selectedClient.PreviousCurrentSkill.ToString();
                clientAgeLabel.Text = selectedClient.Age.ToString();
                lblBirthMonth.Text = selectedClient.BirthMonth.ToString();
                lblBirthWeek.Text = selectedClient.BirthWeek.ToString();
                lblCareerEarnings.Text = selectedClient.CareerEarnings.ToString("C0");
                clientPositionLabel.Text = selectedClient.Position.ToString();
                if (selectedClient.FreeAgent) clientTeamLabel.Text = "Free Agent";
                else if (selectedClient.PlayerType == PlayerType.DraftEntrant) clientTeamLabel.Text = "Draft Entrant";
                else if (selectedClient.Sport == Sports.Golf || selectedClient.Sport == Sports.Tennis || selectedClient.Sport == Sports.MMA || selectedClient.Sport == Sports.Boxing)
                {
                    clientTeamLabel.Text = "";
                    clientPositionLabel.Text = "";
                }
                else
                {
                    clientTeamLabel.Text = selectedClient.Team.Mascot;
                }
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
                lblCareerEarnings.Text = "";
                clientTeamLabel.Text = "";
                clientPositionLabel.Text = "";
            }
        }

        #endregion

        private void cbAgencyAgentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            cbAgentClientList.SelectedIndex = -1;
            UpdateAgentInfo(selectedAgent);

            //reset Available Licenses section
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

            //simulate games if league in initialized and in-season
            if (world.NBA.Initialized && world.NBA.InSeason)
            {
                world.Basketball.SimWeek();
            }
                
            if (world.NFL.Initialized && world.NFL.InSeason)
            {
                world.Football.SimWeek();
            }

            if (world.MLB.Initialized && world.MLB.InSeason)
            {
                world.Baseball.SimWeek();
            }

            if (world.NHL.Initialized && world.NHL.InSeason)
            {
                world.Hockey.SimWeek();
            }
            if (world.MLS.Initialized && world.MLS.InSeason)
            {
                world.Soccer.SimWeek();
            }

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

        public void UpdateWorldCalendar()
        {
            yearLabel.Text = world.Year.ToString();
            monthLabel.Text = world.MonthName.ToString();
            weekLabel.Text = world.WeekNumber.ToString();
            //beginning of a new month
            if (world.WeekNumber == 1)
            {
                //pay agency rent
                agency.DeductMonthlyCosts(world);
                //allow agents to ask GM about playing time again
                foreach (Agent a in agency.Agents)
                    foreach (Player p in a.ClientList)
                        p.AgentPushedForMorePT = false;
            }
        }

        #endregion

        private void CbAvailableClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAvailableClients.SelectedIndex >= 0)
            {
                Player selectedClient = world.AvailableClients[cbAvailableClients.SelectedIndex];

                availableClientAgeLabel.Text = selectedClient.Age.ToString();
                availableClientCurrentSkillLabel.Text = selectedClient.CurrentSkill.ToString();
                availableClientNameLabel.Text = selectedClient.FullName;
                availableClientSportLabel.Text = selectedClient.Sport.ToString();
                availableClientPopularityLabel.Text = selectedClient.PopularityString;
                if (selectedClient.League != null && selectedClient.Contract != null)
                {
                    lblClientSalary.Text = selectedClient.Contract.YearlySalary.ToString("C0");
                    lblClientMonthlySalary.Text = selectedClient.Contract.MonthlySalary.ToString("C0");
                    lblClientAgentPercent.Text = selectedClient.Contract.AgentPercentage.ToString() + "%";
                }
                    
                else
                {
                    lblClientSalary.Text = "";
                    lblClientMonthlySalary.Text = "";
                    lblClientAgentPercent.Text = "";
                }
                lblPlayerGenome.Text = "Behavior: " + Regex.Replace(selectedClient.BehaviorDescription.ToString(), @"\B[A-Z]", " $0") + Environment.NewLine +
                    "Composure: " + Regex.Replace(selectedClient.ComposureDescription.ToString(), @"\B[A-Z]", " $0") + Environment.NewLine +
                    "Greed: " + Regex.Replace(selectedClient.GreedDescription.ToString(), @"\B[A-Z]", " $0") + Environment.NewLine +
                    "Leadership: " + Regex.Replace(selectedClient.LeadershipDescription.ToString(), @"\B[A-Z]", " $0") + Environment.NewLine +
                    "Work Ethic: " + Regex.Replace(selectedClient.WorkEthicDescription.ToString(), @"\B[A-Z]", " $0");

            }
            else
            {
                availableClientAgeLabel.Text = "";
                availableClientCurrentSkillLabel.Text = "";
                availableClientNameLabel.Text = "";
                availableClientSportLabel.Text = "";
                availableClientPopularityLabel.Text = "";
                lblClientSalary.Text = "";
                lblClientAgentPercent.Text = "";
                lblPlayerGenome.Text = "";
            }
        }

        private void cbLeagueStandings_SelectedIndexChanged(object sender, EventArgs e)
        {
            League selectedLeague = world.Leagues[cbLeagueStandings.SelectedIndex];

            leagueStandingsLabel.Text = "";

            List<Team> teamList = selectedLeague.TeamList;
            if (selectedLeague.Sport == Sports.Hockey)
            {
                teamList = teamList.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
                for (int i = 0; i < teamList.Count; i++)
                {
                    leagueStandingsLabel.Text += (i + 1) + ") " + teamList[i].City + " " + teamList[i].Mascot + " " + teamList[i].Wins + "-" + teamList[i].Losses + "-" + teamList[i].OTLosses + ") - " + teamList[i].Points + " points" + Environment.NewLine;
                }
            }
            else if (selectedLeague.Sport == Sports.Soccer)
            {
                teamList = teamList.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
                for (int i = 0; i < teamList.Count; i++)
                {
                    leagueStandingsLabel.Text += (i + 1) + ") " + teamList[i].City + " " + teamList[i].Mascot + " (" + teamList[i].Wins + "-" + teamList[i].Losses + "-" + teamList[i].Ties + ") - " + teamList[i].Points + " points" + Environment.NewLine;
                }
            }
            else
            {
                teamList = teamList.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
                for (int i = 0; i < teamList.Count; i++)
                {
                    leagueStandingsLabel.Text += (i + 1) + ") " + teamList[i].City + " " + teamList[i].Mascot + " " + teamList[i].Wins + "-" + teamList[i].Losses + ")" + Environment.NewLine;
                }
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
            if (world.MonthName == Months.July && world.WeekNumber == 1 && world.NBA.Initialized)
                newsLabel.Text += world.Basketball.basketballDraft.RunDraft() + Environment.NewLine + newsLabel.Text;
            if (world.MonthName == Months.April && world.WeekNumber == 3 && world.NFL.Initialized)
                newsLabel.Text += world.Football.footballDraft.RunDraft() + Environment.NewLine + newsLabel.Text;
            if (world.MonthName == Months.January && world.WeekNumber == 2 && world.MLB.Initialized)
                newsLabel.Text += world.Baseball.baseballDraft.RunDraft() + Environment.NewLine + newsLabel.Text;
            if (world.MonthName == Months.June && world.WeekNumber == 4 && world.NHL.Initialized)
                newsLabel.Text += world.Hockey.hockeyDraft.RunDraft() + Environment.NewLine + newsLabel.Text;
            if (world.MonthName == Months.January && world.WeekNumber == 2 && world.MLS.Initialized)
                newsLabel.Text += world.Soccer.soccerDraft.RunDraft() + Environment.NewLine + newsLabel.Text;

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
                        Event thisEvent = world.PGA.EventList.Find(x => x.Id == e.EventID && x.Year == world.Year);
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
                    else if (e.Sport == Sports.Baseball)
                    {
                        for (int i = 0; i < world.MLB.TeamList.Count; i++)
                            for (int j = 0; j < world.MLB.TeamList[i].Roster.Count; j++)
                                if (world.MLB.TeamList[i].Roster[j].Id == e.PlayerID) world.MLB.TeamList[i].Roster[j].Age++;
                    }
                    else if (e.Sport == Sports.Basketball)
                    {
                        for (int i = 0; i < world.NBA.TeamList.Count; i++)
                            for (int j = 0; j < world.NBA.TeamList[i].Roster.Count; j++)
                                if (world.NBA.TeamList[i].Roster[j].Id == e.PlayerID) world.NBA.TeamList[i].Roster[j].Age++;
                    }
                    else if (e.Sport == Sports.Football)
                    {
                        for (int i = 0; i < world.NFL.TeamList.Count; i++)
                            for (int j = 0; j < world.NFL.TeamList[i].Roster.Count; j++)
                                if (world.NFL.TeamList[i].Roster[j].Id == e.PlayerID) world.NFL.TeamList[i].Roster[j].Age++;
                    }
                    else if (e.Sport == Sports.Hockey)
                    {
                        for (int i = 0; i < world.NHL.TeamList.Count; i++)
                            for (int j = 0; j < world.NHL.TeamList[i].Roster.Count; j++)
                                if (world.NHL.TeamList[i].Roster[j].Id == e.PlayerID) world.NHL.TeamList[i].Roster[j].Age++;
                    }
                    else if (e.Sport == Sports.Soccer)
                    {
                        for (int i = 0; i < world.MLS.TeamList.Count; i++)
                            for (int j = 0; j < world.MLS.TeamList[i].Roster.Count; j++)
                                if (world.MLS.TeamList[i].Roster[j].Id == e.PlayerID) world.MLS.TeamList[i].Roster[j].Age++;
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
                else if (e.EventType == CalendarEventType.LeagueYearBegins)
                {
                    League l = null;
                    if (e.Sport == Sports.Baseball)
                    {
                        world.MLB.InSeason = true;
                        world.MLB.Initialized = true;
                        world.MLB.WeekNumber = 0;
                        l = world.MLB;
                        ResetTeamRecords(world.MLB);
                        ResetPlayerStats(world.MLB);
                    }   
                    else if (e.Sport == Sports.Basketball)
                    {
                        world.NBA.InSeason = true;
                        world.NBA.Initialized = true;
                        l = world.NBA;
                        ResetTeamRecords(world.NBA);
                        ResetPlayerStats(world.NBA);
                    }
                    else if (e.Sport == Sports.Football)
                    {
                        world.NFL.InSeason = true;
                        world.NFL.Initialized = true;
                        world.NFL.WeekNumber = 0;
                        l = world.NFL;
                        ResetTeamRecords(world.NFL);
                        ResetPlayerStats(world.NFL);
                    }
                    else if (e.Sport == Sports.Hockey)
                    {
                        world.NHL.InSeason = true;
                        world.NHL.Initialized = true;
                        world.NHL.WeekNumber = 0;
                        l = world.NHL;
                        ResetTeamRecords(world.NHL);
                        ResetPlayerStats(world.NHL);
                    }
                    else if (e.Sport == Sports.Soccer)
                    {
                        world.MLS.InSeason = true;
                        world.MLS.Initialized = true;
                        world.MLS.WeekNumber = 0;
                        l = world.MLS;
                        ResetTeamRecords(world.MLS);
                        ResetPlayerStats(world.MLS);
                    }
                    DetermineSeasons();
                    world.ReorderDepthCharts(l);
                    foreach (Team t in l.TeamList)
                    {
                        world.SetTeamTitleContender(l, t);
                        foreach (Player p in t.Roster)
                        {
                            if (p.Sport == Sports.Baseball) p.IsStarter = world.IsBaseballStarter(t, p);
                            else if (p.Sport == Sports.Basketball) p.IsStarter = world.IsBasketballStarter(t, p);
                            else if (p.Sport == Sports.Football) p.IsStarter = world.IsFootballStarter(t, p);
                            else if (p.Sport == Sports.Hockey) p.IsStarter = world.IsHockeyStarter(t, p);
                            else if (p.Sport == Sports.Soccer) p.IsStarter = world.IsSoccerStarter(t, p);
                        }
                    }
                }
                else if (e.EventType == CalendarEventType.LeagueYearEnds)
                {
                    if (e.Sport == Sports.Baseball) world.PayPlayersAnnualSalary(world.MLB);
                    else if (e.Sport == Sports.Basketball) world.PayPlayersAnnualSalary(world.NBA);
                    else if (e.Sport == Sports.Football) world.PayPlayersAnnualSalary(world.NFL);
                    else if (e.Sport == Sports.Hockey) world.PayPlayersAnnualSalary(world.NHL);
                    else if (e.Sport == Sports.Soccer) world.PayPlayersAnnualSalary(world.MLS);
                    DetermineSeasons();
                }
                else if (e.EventType == CalendarEventType.ProgressionRegression)
                {
                    League league = null;
                    Association association = null;
                    newsLabel.Text = "Running progression and regression for " + e.EventName + Environment.NewLine + newsLabel.Text;
                    if (e.Sport == Sports.Baseball) league = world.MLB;
                    else if (e.Sport == Sports.Basketball) league = world.NBA;
                    else if (e.Sport == Sports.Football) league = world.NFL;
                    else if (e.Sport == Sports.Hockey) league = world.NHL;
                    else if (e.Sport == Sports.Soccer) league = world.MLS;
                    else if (e.Sport == Sports.Boxing) association = world.WBA;
                    else if (e.Sport == Sports.Golf) association = world.PGA;
                    else if (e.Sport == Sports.Tennis) association = world.ATP;
                    else if (e.Sport == Sports.MMA) association = world.UFC;

                    if (league != null)
                    {
                        foreach (Team t in league.TeamList)
                            foreach (Player p in t.Roster) ProgressionRegression.PlayerProgression(p);

                        foreach (Player p in league.FreeAgents) ProgressionRegression.PlayerProgression(p);

                        world.RetireLeaguePlayers(league);
                    }
                    else if (association != null)
                    {
                        foreach (Player p in association.PlayerList) ProgressionRegression.PlayerProgression(p);

                        world.RetireAssociationPlayers(association);
                    }

                    newsLabel.Text = agency.DisplayAgencyProgressionRegression(e.Sport) + Environment.NewLine + newsLabel.Text;
                    newsLabel.Text = agency.DisplayAgencyRetirements(e.Sport);
                }
                else if (e.EventType == CalendarEventType.DraftDeclaration)
                {
                    if (e.Sport == Sports.Basketball)
                    {
                        world.NBA.DeclaredEntrants = true;
                        newsLabel.Text = "NBA Declared Entrants = " + world.NBA.DeclaredEntrants + Environment.NewLine + newsLabel.Text;
                    }
                    else if (e.Sport == Sports.Baseball) world.MLB.DeclaredEntrants = true;
                    else if (e.Sport == Sports.Football)
                    {
                        world.NFL.DeclaredEntrants = true;
                        newsLabel.Text = "NFL Declared Entrants = " + world.NFL.DeclaredEntrants + Environment.NewLine + newsLabel.Text;
                    }
                    else if (e.Sport == Sports.Hockey) world.NHL.DeclaredEntrants = true;
                    else world.MLS.DeclaredEntrants = true;
                }
            }
        }
        public void ResetPlayerStats(League l)
        {
            foreach (Team t in l.TeamList)
            {
                if (l.Sport == Sports.Football)
                {
                    foreach (FootballPlayer p in t.Roster)
                    {
                        p.PassingTDs = 0;
                        p.PassingYards = 0;
                        p.Interceptions = 0;
                        p.Receptions = 0;
                        p.ReceivingYards = 0;
                        p.ReceivingYardsThisWeek = 0;
                        p.ReceivingTDs = 0;
                        p.Carries = 0;
                        p.RushingYards = 0;
                        p.YardsPerCarry = 0.0;
                        p.RushingTDs = 0;
                        p.Fumbles = 0;
                        p.Tackles = 0;
                        p.DefensiveInterceptions = 0;
                        p.TacklesForLoss = 0;
                        p.Sacks = 0;
                        p.FGAttempts = 0;
                        p.FGMakes = 0;
                        p.XPAttempts = 0;
                        p.XPMakes = 0;
                        p.Punts = 0;
                        p.NetPuntYards = 0;
                        p.NetPuntAverage = 0;
                        p.SacksAllowed = 0;
                        p.PassesDefended = 0;
                        p.PancakeBlocks = 0;
                        p.MVPScore = 0;
                        p.OPOYScore = 0;
                        p.DPOYScore = 0;
                    }
                }
                else if (l.Sport == Sports.Basketball)
                {
                    foreach (BasketballPlayer p in t.Roster)
                    {
                        p.Points = 0.0;
                        p.Rebounds = 0.0;
                        p.Assists = 0.0;
                        p.Steals = 0.0;
                        p.Blocks = 0.0;
                        p.MVPScore = 0.0;
                        p.DPOYScore = 0.0;
                    }
                }
                else if (l.Sport == Sports.Baseball)
                {
                    foreach (BaseballPlayer p in t.Roster)
                    {
                        p.Average = 0.0;
                        p.HomeRuns = 0;
                        p.RBI = 0;
                        p.ERA = 0.00;
                        p.Wins = 0;
                        p.Losses = 0;
                        p.Saves = 0;
                    }
                }
                else if (l.Sport == Sports.Hockey)
                {
                    foreach (HockeyPlayer p in t.Roster)
                    {
                        p.Goals = 0;
                        p.Assists = 0;
                        p.Points = 0;
                        p.GAA = 0.0;
                        p.GoalsAllowed = 0;
                        p.SavePercentage = 0.0;
                        p.Saves = 0;
                        p.ShutOuts = 0;
                        p.GamesPlayed = 0;
                        p.Wins = 0;
                        p.Losses = 0;
                    }
                }
                else if (l.Sport == Sports.Soccer)
                {
                    foreach (SoccerPlayer p in t.Roster)
                    {
                        p.Goals = 0;
                        p.Assists = 0;
                        p.MatchRating = 0.00;
                        p.Saves = 0;
                        p.CleanSheets = 0;
                    }
                }
            }
                

        }
        private void ResetTeamRecords(League l)
        {
            foreach (Team t in l.TeamList)
            {
                t.Wins = 0;
                t.ConferenceWins = 0;
                t.DivisionWins = 0;
                t.Losses = 0;
                t.Ties = 0;
                t.OTLosses = 0;
                t.ConferenceLosses = 0;
                t.DivisionLosses = 0;
            }
            //set league.Playoffs to false
            l.Playoffs = false;
        }
        private void btnHireAgent_Click(object sender, EventArgs e)
        {
            Agent hiredAgent = null;
            AgentSearch newAgentSearch = new AgentSearch(agency);

            if (agency.ClientCount < 5)
                MessageBox.Show("Not enough clients to need additional agents.");
            else
            {
                newAgentSearch.BringToFront();
                newAgentSearch.ShowDialog();

                int fundsSpent = newAgentSearch.FundsSpent;
                string agentType = newAgentSearch.AgentType;
                agency.Money -= fundsSpent;
                UpdateAgencyInfo();

                if (fundsSpent != 0)
                {
                    HireAgentForm hireAgentForm = new HireAgentForm(world, rnd, fundsSpent, agentType);
                    hireAgentForm.HowManyAgents();
                    hireAgentForm.CreateApplicants(world, rnd);
                    hireAgentForm.BringToFront();
                    hireAgentForm.ShowDialog();

                    hiredAgent = hireAgentForm.HiredAgent;
                    if (hiredAgent != null)
                    {
                        agency.Agents.Add(hiredAgent);
                        agency.AgentCount++;
                        world.CreateTeamRelationships(hiredAgent);

                        if (agency.AgentCount == 2)
                            agency.AddAchievementToAgency(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Hire 1st Agent")]);

                        UpdateAgencyInfo();
                        PopulateAgencyAgentList();
                    }
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

        private void btnNegotiatePercent_Click(object sender, EventArgs e)
        {
            if (cbAgencyAgentList.SelectedIndex > -1)
            {
                Agent agent = agency.Agents[cbAgencyAgentList.SelectedIndex];
                Player client = world.AvailableClients[cbAvailableClients.SelectedIndex];
                NegotiatePercentageForm negotiatePercentageForm = new NegotiatePercentageForm(rnd, agent, client);
                negotiatePercentageForm.BringToFront();
                negotiatePercentageForm.ShowDialog();

                Player player = FindPlayer(client);

                if (negotiatePercentageForm.Percentage > 0)
                {
                    if (player.League != null)
                        if (player.Contract != null)
                            player.Contract.AgentPercentage = negotiatePercentageForm.Percentage;
                        else player.Contract = new Contract(0, 0, 0, player.League.SeasonStart, player.League.SeasonEnd, 0, negotiatePercentageForm.Percentage, PaySchedule.Monthly);
                    else player.Contract = new Contract(50, 0, 0, new Date(0, Months.January, 1), new Date(11, Months.December, 5), 0, negotiatePercentageForm.Percentage, PaySchedule.Winnings);

                    player.MemberOfAgency = true;
                    //player.CurrentSkill += 20;

                    if (negotiatePercentageForm.NumberOfAgentOffers == 1)
                        agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Smooth Signing")]);

                    if (negotiatePercentageForm.Percentage == negotiatePercentageForm.ClientsAcceptancePercent)
                        agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Percentage On Point")]);

                    SignClient(player);
                }
                else if (negotiatePercentageForm.Percentage == 0)
                {
                    player.WillingToNegotiate = false;
                    PopulateAvailableClientsList();
                }
            }
        }

        private void SignClient(Player player)
        {
            agency.AddClient(player);
            Agent agent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            agent.AddClient(player);

            //check for first client signed Achievement
            agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Sign 1st Client")]);

            //check for client in 3 different sports
            if (agent.SportsRepresented.Count >= 3) agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Athletically Diversified")]);

            //check for 3 clients in same sport
            int clientsInSport = 0;
            foreach (Player p in agent.ClientList)
                if (p.Sport == player.Sport)
                    clientsInSport++;
            if (clientsInSport >= 3)
                agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Gaining Traction")]);
            if (clientsInSport >= 5)
                agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Making A Name")]);
            if (clientsInSport >= 10)
                agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Taking Over The Sport")]);

            UpdateAgencyInfo();
            UpdateAgentInfo(agent);
            PopulateAvailableClientsList();

            cbAvailableClients.Text = "";
            cbAvailableClients.SelectedIndex = -1;
            btnNegotiatePercent.Enabled = false;
        }

        private Player FindPlayer(Player client)
        {
            Player player;
            //find player and change AgencyHappiness
            if (client.PlayerType == PlayerType.Team || client.PlayerType == PlayerType.DraftEntrant)
            {
                int leagueIndex = -1;
                int teamIndex = -1;
                int playerIndex = -1;

                for (int i = 0; i < world.Leagues.Count; i++)
                    if (client.League.Name == world.Leagues[i].Name) leagueIndex = i;
                
                if (!client.FreeAgent && client.PlayerType != PlayerType.DraftEntrant)
                {
                    for (int i = 0; i < world.Leagues[leagueIndex].TeamList.Count; i++)
                        if (client.Team.Mascot == world.Leagues[leagueIndex].TeamList[i].Mascot) teamIndex = i;
                    
                    for (int i = 0; i < world.Leagues[leagueIndex].TeamList[teamIndex].Roster.Count; i++)
                        if (client.FullName == world.Leagues[leagueIndex].TeamList[teamIndex].Roster[i].FullName) playerIndex = i;

                    world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].AgencyHappiness = 50;
                    world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].AgencyHappinessDescription = world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].DescribeHappiness(world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].AgencyHappiness);
                    world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].AgencyHappinessString = world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].EnumToString(world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex].AgencyHappinessDescription.ToString());

                    player = world.Leagues[leagueIndex].TeamList[teamIndex].Roster[playerIndex];
                }
                else
                {
                    if (client.PlayerType == PlayerType.DraftEntrant)
                    {
                        for (int i = 0; i < world.Leagues[leagueIndex].DraftEntrants.Count; i++)
                            if (client.FullName == world.Leagues[leagueIndex].DraftEntrants[i].FullName) playerIndex = i;

                        world.Leagues[leagueIndex].DraftEntrants[playerIndex].AgencyHappiness = 50;
                        world.Leagues[leagueIndex].DraftEntrants[playerIndex].AgencyHappinessDescription = world.Leagues[leagueIndex].DraftEntrants[playerIndex].DescribeHappiness(world.Leagues[leagueIndex].DraftEntrants[playerIndex].AgencyHappiness);
                        world.Leagues[leagueIndex].DraftEntrants[playerIndex].AgencyHappinessString = world.Leagues[leagueIndex].DraftEntrants[playerIndex].EnumToString(world.Leagues[leagueIndex].DraftEntrants[playerIndex].AgencyHappinessDescription.ToString());

                        player = world.Leagues[leagueIndex].DraftEntrants[playerIndex];
                    }
                    else
                    {
                        for (int i = 0; i < world.Leagues[leagueIndex].FreeAgents.Count; i++)
                            if (client.FullName == world.Leagues[leagueIndex].FreeAgents[i].FullName) playerIndex = i;

                        world.Leagues[leagueIndex].FreeAgents[playerIndex].AgencyHappiness = 50;
                        world.Leagues[leagueIndex].FreeAgents[playerIndex].AgencyHappinessDescription = world.Leagues[leagueIndex].FreeAgents[playerIndex].DescribeHappiness(world.Leagues[leagueIndex].FreeAgents[playerIndex].AgencyHappiness);
                        world.Leagues[leagueIndex].FreeAgents[playerIndex].AgencyHappinessString = world.Leagues[leagueIndex].FreeAgents[playerIndex].EnumToString(world.Leagues[leagueIndex].FreeAgents[playerIndex].AgencyHappinessDescription.ToString());

                        player = world.Leagues[leagueIndex].FreeAgents[playerIndex];
                    }
                }



            }
            else
            {
                int associationIndex = -1;
                for (int i = 0; i < world.Associations.Count; i++)
                    if (client.Sport == world.Associations[i].Sport) associationIndex = i;
                int playerIndex = -1;
                for (int i = 0; i < world.Associations[associationIndex].PlayerList.Count; i++)
                    if (client.FullName == world.Associations[associationIndex].PlayerList[i].FullName) playerIndex = i;

                world.Associations[associationIndex].PlayerList[playerIndex].AgencyHappiness = 50;
                world.Associations[associationIndex].PlayerList[playerIndex].AgencyHappinessDescription = world.Associations[associationIndex].PlayerList[playerIndex].DescribeHappiness(world.Associations[associationIndex].PlayerList[playerIndex].AgencyHappiness);
                world.Associations[associationIndex].PlayerList[playerIndex].AgencyHappinessString = world.Associations[associationIndex].PlayerList[playerIndex].EnumToString(world.Associations[associationIndex].PlayerList[playerIndex].AgencyHappinessDescription.ToString());

                player = world.Associations[associationIndex].PlayerList[playerIndex];
            }

            return player;
        }

        private void btnClientCallTeams_Click(object sender, EventArgs e)
        {
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            if (cbAgentClientList.SelectedIndex > -1)
            {
                Player selectedClient = selectedAgent.ClientList[cbAgentClientList.SelectedIndex];

                if (selectedClient.FreeAgent)
                    selectedAgent.CallTeamsForClient(rnd, world, selectedClient);
                else selectedAgent.CallTeamGM(rnd, world, selectedClient);
            }
        }

        private void BtnViewStandings_Click(object sender, EventArgs e)
        {
            ViewStandingsForm viewStandingsForm = new ViewStandingsForm(world);
            viewStandingsForm.BringToFront();
            viewStandingsForm.ShowDialog();
        }

        private void BtnMarketPlayer_Click(object sender, EventArgs e)
        {
            Player selectedClient;
            Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
            if (cbAgentClientList.SelectedIndex < 0)
                MessageBox.Show("You must first select a player to market!");
            else
                selectedClient = selectedAgent.ClientList[cbAgentClientList.SelectedIndex];
            
            if (agency.MarketerCount == 0)
                MessageBox.Show("Hire a marketer before running a marketing plan for this player.");
            else
            {
                //create drop down list of agency.Markterers
                //send selectedMarketer and selectedClient to MarketingPlanForm
            }
        }

        private void BtnHireMarketer_Click(object sender, EventArgs e)
        {
            Marketer hiredMarketer = null;
            MarketerSearch marketerSearch = new MarketerSearch(rnd, agency);
            marketerSearch.BringToFront();
            marketerSearch.ShowDialog();
            int fundsSpent = marketerSearch.FundsSpent;
            string marketerType = marketerSearch.MarketerType;
            agency.Money -= fundsSpent;
            UpdateAgencyInfo();

            if (fundsSpent != 0)
            {
                HireMarketerForm hireMarketerForm = new HireMarketerForm(fundsSpent, marketerType);
                hireMarketerForm.HowManyMarketers();
                hireMarketerForm.CreateApplicants(world, rnd);
                hireMarketerForm.BringToFront();
                hireMarketerForm.ShowDialog();

                hiredMarketer = hireMarketerForm.HiredMarketer;
                if (hiredMarketer != null)
                {
                    agency.Marketers.Add(hiredMarketer);
                    agency.MarketerCount++;
                    if (agency.MarketerCount == 1)
                        agency.AddAchievementToAgency(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Hire 1st Marketer")]);
                    UpdateAgencyInfo();
                    PopulateAgencyAgentList();
                }
            }
        }

        private void btnClientCard_Click(object sender, EventArgs e)
        {
            if (cbAgencyAgentList.SelectedIndex > -1 && cbAgentClientList.SelectedIndex > -1)
            {
                Agent selectedAgent = agency.Agents[cbAgencyAgentList.SelectedIndex];
                Player selectedClient = selectedAgent.ClientList[cbAgentClientList.SelectedIndex];
                PlayerCard playerCard = new PlayerCard(selectedClient);
                playerCard.BringToFront();
                playerCard.ShowDialog();
            }
        }
    }
}
