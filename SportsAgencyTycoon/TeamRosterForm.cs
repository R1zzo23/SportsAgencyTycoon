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
    public partial class TeamRosterForm : Form
    {
        public World World;
        public TeamRosterForm(World world)
        {
            InitializeComponent();
            World = world;
            FillLeagueComboBox();
        }

        private void FillLeagueComboBox()
        {
            cbLeagues.Items.Clear();

            string leagueName;

            foreach (League league in World.Leagues)
            {
                leagueName = league.Name;
                cbLeagues.Items.Add(leagueName);
            }
        }

        private void FillTeamComboBox()
        {
            League selectedLeague = World.Leagues[cbLeagues.SelectedIndex];

            cbTeamList.Items.Clear();

            string teamName;

            foreach (Team t in selectedLeague.TeamList)
            {
                teamName = t.City + " " + t.Mascot;
                cbTeamList.Items.Add(teamName);
            }
        }

        private void cbLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTeamComboBox();
        }

        private void cbTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRoster.Text = "[POS] LAST, FIRST:                CUR/POT          AGE" + Environment.NewLine;
            League selectedLeague = World.Leagues[cbLeagues.SelectedIndex];
            Team selectedTeam = selectedLeague.TeamList[cbTeamList.SelectedIndex];
            lblTeamInfo.Text = selectedTeam.City + " " + selectedTeam.Mascot + ": Title Contender (" + selectedTeam.TitleConteder + ") || Market Value: (" + selectedTeam.MarketValue + ")";
            if (selectedLeague.Sport == Sports.Basketball)
                foreach (BasketballPlayer p in selectedTeam.Roster)
                    lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + p.AgencyHappinessString + " " + p.TeamHappinessString + " " + p.PopularityString + Environment.NewLine;
            else if (selectedLeague.Sport == Sports.Football)
                foreach (FootballPlayer p in selectedTeam.Roster)
                    lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + Environment.NewLine;
            if (selectedLeague.Sport == Sports.Baseball)
                foreach (BaseballPlayer p in selectedTeam.Roster)
                    lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + Environment.NewLine;
            if (selectedLeague.Sport == Sports.Hockey)
                foreach (HockeyPlayer p in selectedTeam.Roster)
                    lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + Environment.NewLine;
            if (selectedLeague.Sport == Sports.Soccer)
                foreach (SoccerPlayer p in selectedTeam.Roster)
                    lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + Environment.NewLine;
        }
    }
}
