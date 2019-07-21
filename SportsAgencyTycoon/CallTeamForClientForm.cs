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

        public CallTeamForClientForm(Agent agent, Player client, League league)
        {
            InitializeComponent();
            _Agent = agent;
            _Client = client;
            _League = league;
            lblLeagueName.Text = _League.Name;
            PopulateTeamList();
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
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox) this.Controls.Remove(control);
            }

            List<Player> positionPlayer = new List<Player>();
            
            foreach (Player p in team.Roster)
                if (p.Position == _Client.Position)
                {
                    positionPlayer.Add(p);
                }
            for (int i = 0; i < positionPlayer.Count; i++)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Location = new Point(12 + (i * 200 + 5), 65);
                groupBox.Text = positionPlayer[i].FullName;
                groupBox.Size = new Size(200, 100);
                Label ageLabel = new Label();
                ageLabel.Text = positionPlayer[i].Age.ToString() + "-years old";
                ageLabel.Location = new Point(7, 20);
                groupBox.Controls.Add(ageLabel);
                Label skillLabel = new Label();
                skillLabel.Text = "Skill Level: " + positionPlayer[i].CurrentSkill.ToString() + "/" + positionPlayer[i].PotentialSkill.ToString();
                skillLabel.Location = new Point(7, 44);
                groupBox.Controls.Add(skillLabel);
                Label contractLabel = new Label();
                contractLabel.Size = new Size(150, 13);
                contractLabel.Text = "Years Left: " + positionPlayer[i].Contract.Years + " at " + positionPlayer[i].Contract.YearlySalary.ToString("C0") + " per season";
                contractLabel.Location = new Point(7, 68);
                groupBox.Controls.Add(contractLabel);
                this.Controls.Add(groupBox);
            }
        }

        private void cbTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Team team = _League.TeamList[cbTeamList.SelectedIndex];
            lblTitleContender.Text = "Title Contender: " + team.TitleConteder.ToString();
            lblMarketValue.Text = "Market Value: " + team.MarketValue.ToString();
            CreatePlayerCards(team);
        }
    }
}
