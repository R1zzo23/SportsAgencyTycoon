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
    public partial class CallTeamForClientForm : Form
    {
        private Agent _Agent;
        private Player _Client;
        private League _League;
        private List<Player> _PositionPlayers;
        private List<Control> _PlayerCards = new List<Control>();
        private List<Control> _AgeLabels = new List<Control>();
        private List<Control> _SkillLabels = new List<Control>();
        private List<Control> _ContractLabels = new List<Control>();

        public CallTeamForClientForm(Agent agent, Player client, League league)
        {
            InitializeComponent();
            _Agent = agent;
            _Client = client;
            _League = league;
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

            /*for (int i = _PlayerCards.Count; i > _PlayerCards.Count - _PositionPlayers.Count; i--)
            {
                _PlayerCards[i - 1].Hide();
            }*/

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

            lblTeamInterestLevel.Text = "Interest In Signing: " + GenerateTeamInterest();
        }

        private void cbTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Team team = _League.TeamList[cbTeamList.SelectedIndex];
            lblTitleContender.Text = "Title Contender: " + team.TitleConteder.ToString();
            lblMarketValue.Text = "Market Value: " + team.MarketValue.ToString();
            CreatePlayerCards(team);
        }

        private string GenerateTeamInterest()
        {
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
                    //not playing over young developmental prospect
                    interestLevel = "VERY LOW";
                }
                else
                {
                    //starter much better than client
                    if (_PositionPlayers[0].CurrentSkill >= _Client.CurrentSkill + 25)
                        interestLevel = "LOW";
                    else
                        interestLevel = "MEDIUM";
                }
            }
            else interestLevel = "ZERO INTEREST";

            return interestLevel;
        }
    }
}
