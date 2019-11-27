using System;
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
    public partial class CallTeamForClientForm : Form
    {
        private Agent _Agent;
        private Random rnd;
        private Player _Client;
        private League _League;
        private World _World;
        private string _InterestLevel;
        private List<Player> _PositionPlayers;
        private List<Control> _PlayerCards = new List<Control>();
        private List<Control> _AgeLabels = new List<Control>();
        private List<Control> _SkillLabels = new List<Control>();
        private List<Control> _ContractLabels = new List<Control>();
        private RelationshipWithTeam relationship;
        public int years;
        public int yearlySalary;
        public int maxSalaryWillingToOffer;
        public int maxYears;
        public bool willingToNegotiate = false;

        public CallTeamForClientForm(Random random, Agent agent, Player client, League league, World world)
        {
            InitializeComponent();
            rnd = random;
            _Agent = agent;
            _Client = client;
            _League = league;
            _World = world;
            _PositionPlayers = new List<Player>();
            lblLeagueName.Text = _League.Name;
            FillLists();
            FillClientInfo();
            PopulateTeamList();
        }

        private void FillClientInfo()
        {
            clientCard.Text = _Client.FullName;
            clientAgeLabel.Text = _Client.Age + "-years old";
            clientSkillLevel.Text = "Skill Level: " + _Client.CurrentSkill + "/" + _Client.PotentialSkill;
            clientPosition.Text = "Position: " + _Client.Position.ToString();
        }

        private void FillLists()
        {
            _PlayerCards.Add(playerCard1);
            _PlayerCards.Add(playerCard2);
            _PlayerCards.Add(playerCard3);
            _PlayerCards.Add(playerCard4);
            _PlayerCards.Add(playerCard5);
            _PlayerCards.Add(playerCard6);
            _PlayerCards.Add(playerCard7);
            _PlayerCards.Add(playerCard8);
            _PlayerCards.Add(playerCard9);
            _PlayerCards.Add(playerCard10);
            _AgeLabels.Add(ageLabel1);
            _AgeLabels.Add(ageLabel2);
            _AgeLabels.Add(ageLabel3);
            _AgeLabels.Add(ageLabel4);
            _AgeLabels.Add(ageLabel5);
            _AgeLabels.Add(ageLabel6);
            _AgeLabels.Add(ageLabel7);
            _AgeLabels.Add(ageLabel8);
            _AgeLabels.Add(ageLabel9);
            _AgeLabels.Add(ageLabel10);
            _SkillLabels.Add(skillLabel1);
            _SkillLabels.Add(skillLabel2);
            _SkillLabels.Add(skillLabel3);
            _SkillLabels.Add(skillLabel4);
            _SkillLabels.Add(skillLabel5);
            _SkillLabels.Add(skillLabel6);
            _SkillLabels.Add(skillLabel7);
            _SkillLabels.Add(skillLabel8);
            _SkillLabels.Add(skillLabel9);
            _SkillLabels.Add(skillLabel10);
            _ContractLabels.Add(contractLabel1);
            _ContractLabels.Add(contractLabel2);
            _ContractLabels.Add(contractLabel3);
            _ContractLabels.Add(contractLabel4);
            _ContractLabels.Add(contractLabel5);
            _ContractLabels.Add(contractLabel6);
            _ContractLabels.Add(contractLabel7);
            _ContractLabels.Add(contractLabel8);
            _ContractLabels.Add(contractLabel9);
            _ContractLabels.Add(contractLabel10);
        }

        public void PopulateTeamList()
        {
            foreach (Team t in _League.TeamList)
            {
                string team = t.City + " " + t.Mascot;
                cbTeamList.Items.Add(team);
            }
        }

        public void CreatePlayerCards(Team team)
        {
            _PositionPlayers.Clear();
            
            //add all players at same position on the team to list
            foreach (Player p in team.Roster)
                if (p.Position == _Client.Position)
                {
                    _PositionPlayers.Add(p);
                }

            for (int i = 0; i < _PlayerCards.Count - _PositionPlayers.Count; i++)
            {
                _PlayerCards[_PlayerCards.Count - 1 - i].Hide();
            }

            //order list of players by currentSkill
            _PositionPlayers = _PositionPlayers.OrderByDescending(o => o.CurrentSkill).ToList();

            //create player cards
            for (int i = 0; i < _PositionPlayers.Count; i++)
            {
                _PlayerCards[i].Text = _PositionPlayers[i].FullName;
                _AgeLabels[i].Text = _PositionPlayers[i].Age.ToString() + "-years old";
                _SkillLabels[i].Text = "Skill Level: " + _PositionPlayers[i].CurrentSkill.ToString() + "/" + _PositionPlayers[i].PotentialSkill.ToString();
                _ContractLabels[i].Text = "Years Left: " + _PositionPlayers[i].Contract.Years + " at " + _PositionPlayers[i].Contract.YearlySalary.ToString("C0") + " per season";
            }

            GenerateTeamInterest();

            lblTeamInterestLevel.Text = "Interest In Signing: ???";

            ContractOffer();
        }

        private void cbTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            negotiationGroupBox.Visible = false;
            Team team = _League.TeamList[cbTeamList.SelectedIndex];
            lblTitleContender.Text = "Title Contender: " + team.TitleConteder.ToString();
            lblMarketValue.Text = "Market Value: " + team.MarketValue.ToString();
            CreatePlayerCards(team);
        }

        private void GenerateTeamInterest()
        {
            btnAcceptOffer.Enabled = false;
            bool goodRelationshipWithTeam = false;
            if (relationship.Relationship >= 75) goodRelationshipWithTeam = true;

            string interestLevel = "";
            //would free agent be a starter?
            if (_Client.CurrentSkill > _PositionPlayers[0].CurrentSkill)
            {
                //is current starter a young stud?
                if (_PositionPlayers[0].Age <= 25 && _PositionPlayers[0].PotentialSkill >= 70)
                {
                    interestLevel = "HIGH";
                }
                else interestLevel = "VERY HIGH";
            }
            else if (_Client.CurrentSkill > _PositionPlayers[_PositionPlayers.Count - 1].CurrentSkill)
            {
                //is lowest ranked player at position a young player with more potential?
                if (_PositionPlayers[_PositionPlayers.Count - 1].Age <= 25 && _PositionPlayers[_PositionPlayers.Count - 1].PotentialSkill > _Client.PotentialSkill)
                {
                    if (goodRelationshipWithTeam)
                    {
                        //good relationship gains your client a little more love from team
                        interestLevel = "LOW";
                    }
                    else
                    {
                        //not playing over young developmental prospect
                        interestLevel = "VERY LOW";
                    }
                }
                else
                {
                    //starter much better than client
                    if (_PositionPlayers[0].CurrentSkill >= _Client.CurrentSkill + 25)
                    {
                        if (goodRelationshipWithTeam)
                            interestLevel = "MEDIUM";
                        else 
                            interestLevel = "LOW";
                    }
                    else
                    {
                        if (goodRelationshipWithTeam)
                            interestLevel = "HIGH";
                        else
                            interestLevel = "MEDIUM";
                    }
                        
                }
            }
            else interestLevel = "ZERO INTEREST";

            _InterestLevel = interestLevel;
        }

        private void ContractOffer()
        {
            int randomNumber = rnd.Next(1, 101);

            if (_InterestLevel == "ZERO INTEREST")
            {
                years = 0;
                yearlySalary = 0;
                willingToNegotiate = false;
            }
            else if (_InterestLevel == "VERY LOW")
            {
                if (randomNumber > 50)
                {
                    years = 1;
                    yearlySalary = _Client.League.MinSalary;
                    maxSalaryWillingToOffer = _Client.League.MinSalary;
                }
                else
                {
                    years = 0;
                    yearlySalary = 0;
                    maxSalaryWillingToOffer = 0;
                }
                willingToNegotiate = false;
            }
            else if (_InterestLevel == "LOW")
            {
                years = 1;
                yearlySalary = _Client.League.MinSalary;
                maxSalaryWillingToOffer = rnd.Next(_Client.League.MinSalary, _Client.League.MinSalary + 750000);
                maxYears = rnd.Next(1,3);
                if (relationship.Relationship >= 75)
                    willingToNegotiate = true;
                else 
                    willingToNegotiate = false;
            }
            else if (_InterestLevel == "MEDIUM")
            {
                years = rnd.Next(1, 3);
                maxYears = rnd.Next(1, 3);
                if (maxYears < years) maxYears = years;
                yearlySalary = _Client.DetermineYearlySalary(rnd);
                double percent = rnd.Next(1, 21);
                double maxPercent = rnd.Next(5, 101);
                yearlySalary = Convert.ToInt32((double)yearlySalary * (1 - (percent / 100)));
                maxSalaryWillingToOffer = Convert.ToInt32((double)yearlySalary * (1 + ((maxPercent + 25) / 100)));

                if (yearlySalary > maxSalaryWillingToOffer)
                {
                    int temp = yearlySalary;
                    yearlySalary = maxSalaryWillingToOffer;
                    maxSalaryWillingToOffer = temp;
                }

                willingToNegotiate = true;
            }
            else if (_InterestLevel == "HIGH")
            {
                years = rnd.Next(2, 4);
                maxYears = rnd.Next(2, 5);
                if (maxYears < years) maxYears = years + 1;
                yearlySalary = _Client.DetermineYearlySalary(rnd);
                maxSalaryWillingToOffer = Convert.ToInt32((double)yearlySalary * (1 + (rnd.Next(20, 101) / 100)));
                willingToNegotiate = true;
            }
            else // _InterestLevel == "VERY HIGH"
            {
                years = rnd.Next(3, 5);
                maxYears = rnd.Next(3, 5);
                if (maxYears < years) maxYears = years;
                yearlySalary = _Client.DetermineYearlySalary(rnd);
                double percent = rnd.Next(10, 26);
                yearlySalary = Convert.ToInt32((double)yearlySalary * (1 + (percent / 100)));
                if (yearlySalary > _Client.League.MaxSalary) yearlySalary = _Client.League.MaxSalary;
                maxSalaryWillingToOffer = Convert.ToInt32((double)yearlySalary * (1 + (rnd.Next(50, 151) / 100)));
                if (yearlySalary > maxSalaryWillingToOffer) maxSalaryWillingToOffer = yearlySalary;
                willingToNegotiate = true;
            }

            if (yearlySalary > _Client.League.MaxSalary) yearlySalary = _Client.League.MaxSalary;
            if (maxSalaryWillingToOffer > _Client.League.MaxSalary) maxSalaryWillingToOffer = _Client.League.MaxSalary;

            lblContractYears.Text = "# of Years: " + years.ToString();
            lblContractSalary.Text = "Yearly Salary: " + yearlySalary.ToString("C0");
            if (willingToNegotiate)
            {
                lblWillingToNegotiate.Text = "Willing to Negotiate: YES";
                btnCounterOffer.Enabled = true;
            }
            else
            {
                lblWillingToNegotiate.Text = "Willing to Negotiate: NO";
                btnCounterOffer.Enabled = false;
            }

            if (years > 0) btnAcceptOffer.Enabled = true;
        }

        private void TeamConsidersCounterOffer()
        {
            int counteredYears = Convert.ToInt32(txtYears.Text);
            int counteredSalary = Convert.ToInt32(txtSalary.Text);

            txtSalary.Clear();
            txtYears.Clear();

            bool acceptedYears = false;
            bool acceptedSalary = false;

            //check if years is acceptable
            if (counteredYears >= years && counteredYears <= maxYears)
            {
                acceptedYears = true;
                years = counteredYears;
                lblContractYears.Text = "# of Years: " + years.ToString();
            }
                
            else
            {
                acceptedYears = false;
                MessageBox.Show("We want the length of the contract to be more like this.");
                lblContractYears.Text = "# of Years: " + rnd.Next(years, maxYears + 1).ToString();
            }
            //check if salary is acceptable
            if (counteredSalary <= maxSalaryWillingToOffer)
            {
                acceptedSalary = true;
                yearlySalary = counteredSalary;
                lblContractSalary.Text = "Yearly Salary: " + yearlySalary.ToString("C0");
            }
                
            else
            {
                acceptedSalary = false;
                MessageBox.Show("That salary is not what we are looking at. How about this?");
                Console.WriteLine("Random salary between " + yearlySalary.ToString("C0") + " and " + maxSalaryWillingToOffer.ToString("C0"));
                int teamCounteredSalary = rnd.Next(yearlySalary, maxSalaryWillingToOffer);
                yearlySalary = teamCounteredSalary;
                lblContractSalary.Text = "Yearly Salary: " + yearlySalary.ToString("C0");
            }
            //check if both salary and years were accepted
            if (acceptedYears && acceptedSalary)
            {
                MessageBox.Show("Welcome to the team, " + _Client.FirstName + "!");
                DealHasBeenAgreedUpon();
            }
        }

        private void btnCounterOffer_Click(object sender, EventArgs e)
        {
            if (txtYears.Text == "")
                MessageBox.Show("Enter length of contract for your client!");
            else if (CheckSalaryCounterOffer())
            {
                MessageBox.Show("Counter offer sent!");
                TeamConsidersCounterOffer();
            }
        }

        private bool CheckSalaryCounterOffer()
        {
            bool isBetweenMinAndMax = false;

            if (int.Parse(txtSalary.Text) < _Client.League.MinSalary)
            {
                MessageBox.Show("Countered salary is below league minimum. We will now adjust it for you.");
                txtSalary.Text = _Client.League.MinSalary.ToString();
                isBetweenMinAndMax = false;
            }
            else if (int.Parse(txtSalary.Text) > _Client.League.MaxSalary)
            {
                MessageBox.Show("Countered salary is above league maximum. We will now adjust it for you.");
                txtSalary.Text = _Client.League.MaxSalary.ToString();
                isBetweenMinAndMax = false;
            }
            else isBetweenMinAndMax = true;

            return isBetweenMinAndMax;
        }

        private void btnAcceptOffer_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Offer accepted! Years: " + years + " Salary: " + yearlySalary);
            DealHasBeenAgreedUpon();
        }

        private void DealHasBeenAgreedUpon()
        {
            _Client.Contract.YearlySalary = yearlySalary;
            _Client.Contract.Years = years;
            _Client.Team = _Client.League.TeamList[cbTeamList.SelectedIndex];
            _Client.FreeAgent = false;
            _Client.Team.Roster.Add(_Client);

            bool isStarter = false;

            if (_Client.Sport == Sports.Baseball) isStarter = _World.IsBaseballStarter(_Client.Team, _Client);
            else if (_Client.Sport == Sports.Basketball) isStarter = _World.IsBasketballStarter(_Client.Team, _Client);
            else if (_Client.Sport == Sports.Football) isStarter = _World.IsFootballStarter(_Client.Team, _Client);
            else if (_Client.Sport == Sports.Hockey) isStarter = _World.IsHockeyStarter(_Client.Team, _Client);
            else if (_Client.Sport == Sports.Soccer) isStarter = _World.IsSoccerStarter(_Client.Team, _Client);
            _Client.DetermineTeamHappiness(rnd, isStarter);
            _Client.Contract.MonthlySalary = Convert.ToInt32((double)_Client.Contract.YearlySalary / (double)_Client.League.MonthsInSeason);
            _Client.Contract.StartDate = _Client.League.SeasonStart;
            _Client.Contract.EndDate = _Client.League.SeasonEnd;

            

            MessageBox.Show("You've got a deal! Welcome to the team!");
            this.Close();
        }

        private void TxtYears_KeyPress(object sender, KeyPressEventArgs e)
        {
            //whole numbers only between 1 & 4
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            txtYears.MaxLength = 1;
            //AdjustYears(int.Parse(txtYears.Text));
        }

        private void TxtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtYears_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtYears.Text != "")
            {
                int years = int.Parse(txtYears.Text);
                if (years < 1)
                {
                    MessageBox.Show("Minimum years you can suggest is 1.");
                    txtYears.Text = "1";
                }
                if (years > 4)
                {
                    MessageBox.Show("Maximum years you can suggest is 4.");
                    txtYears.Text = "4";
                }
            }
        }

        private void btnOpenNegotiations_Click(object sender, EventArgs e)
        {
            if (_InterestLevel == "ZERO INTEREST")
            {
                MessageBox.Show("I think we've made it clear that we have no interest in your client.");
                relationship.Relationship -= rnd.Next(1, 4);
            }
            else if (_InterestLevel == "VERY LOW")
            {
                int coinFlip = rnd.Next(1, 3);
                if (coinFlip == 1)
                {
                    MessageBox.Show("Thanks for checking in but we aren't interested right now.");
                    relationship.Relationship -= rnd.Next(0, 3);
                }
                else
                {
                    MessageBox.Show("We will toss you an offer.");
                    negotiationGroupBox.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("I think there's something we could work on here...");
                negotiationGroupBox.Visible = true;
            }
        }

        private void btnAskInterest_Click(object sender, EventArgs e)
        {
            int relationshipIndex = _Agent.RelationshipsWithTeams.FindIndex(o => o.Team == _League.TeamList[cbTeamList.SelectedIndex]);
            if (relationshipIndex < 0)
            {
                relationship = new RelationshipWithTeam(_League.TeamList[cbTeamList.SelectedIndex], 50);
                _Agent.RelationshipsWithTeams.Add(relationship);
            }
            else
                relationship = _Agent.RelationshipsWithTeams[relationshipIndex];

            foreach (RelationshipWithTeam r in _Agent.RelationshipsWithTeams)
                Console.WriteLine("Team: " + r.Team.Mascot + ", Relationship: " + r.Relationship);

            lblTeamInterestLevel.Text = "Interest In Signing: " + _InterestLevel;
        }

        private void btnSmoothTalk_Click(object sender, EventArgs e)
        {

        }
    }
}
