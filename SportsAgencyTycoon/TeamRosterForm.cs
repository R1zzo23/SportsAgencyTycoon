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

        private void FillTeamRosterComboBox()
        {
            League selectedLeague = World.Leagues[cbLeagues.SelectedIndex];
            Team selectedTeam = selectedLeague.TeamList[cbTeamList.SelectedIndex];

            cbTeamRoster.Items.Clear();

            string playerName;

            foreach(Player p in selectedTeam.Roster)
            {
                playerName = p.FullName;
                cbTeamRoster.Items.Add(playerName);
            }
        }

        private void cbLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTeamComboBox();
        }

        private void cbTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTeamRosterComboBox();

            League selectedLeague = World.Leagues[cbLeagues.SelectedIndex];
            Team selectedTeam = selectedLeague.TeamList[cbTeamList.SelectedIndex];

            lblAwards.Text = "";
            if (selectedTeam.Awards.Count > 0)
                foreach (Award award in selectedTeam.Awards)
                    lblAwards.Text += award.Year + " " + award.AwardName + Environment.NewLine;

            lblRoster.Text = "[POS] LAST, FIRST:                CUR/POT          AGE" + Environment.NewLine;
            
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

        private void cbTeamRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            League selectedLeague = World.Leagues[cbLeagues.SelectedIndex];
            Team selectedTeam = selectedLeague.TeamList[cbTeamList.SelectedIndex];
            Player selectedPlayer = selectedTeam.Roster[cbTeamRoster.SelectedIndex];

            if (selectedLeague.Sport == Sports.Basketball)
            {
                List<BasketballPlayer> hoopsRoster = new List<BasketballPlayer>();
                foreach (BasketballPlayer bp in selectedTeam.Roster)
                    hoopsRoster.Add(bp);
                DisplayBasketballStats(hoopsRoster[cbTeamRoster.SelectedIndex]);
            }
                

            if (selectedPlayer.Sport == Sports.Baseball)
            {
                BaseballPlayer baseballPlayer = (BaseballPlayer)selectedPlayer;
                lblPosition.Text = "Position: " + baseballPlayer.Position.ToString();
            }
            else if (selectedPlayer.Sport == Sports.Basketball) 
            {
                BasketballPlayer basketballPlayer = (BasketballPlayer)selectedPlayer;
                lblPosition.Text = "Position: " + basketballPlayer.Position.ToString();
            }
            else if (selectedPlayer.Sport == Sports.Football)
            {
                FootballPlayer footballPlayer = (FootballPlayer)selectedPlayer;
                lblPosition.Text = "Position: " + footballPlayer.Position.ToString();
            }
            else if (selectedPlayer.Sport == Sports.Hockey)
            {
                HockeyPlayer hockeyPlayer = (HockeyPlayer)selectedPlayer;
                lblPosition.Text = "Position: " + hockeyPlayer.Position.ToString();
            }
            else if (selectedPlayer.Sport == Sports.Soccer)
            {
                SoccerPlayer soccerPlayer = (SoccerPlayer)selectedPlayer;
                lblPosition.Text = "Position: " + soccerPlayer.Position.ToString();
            }
            lblFullName.Text = selectedPlayer.FullName;
            lblAge.Text = "Age: " + selectedPlayer.Age.ToString();
            lblSkillLevel.Text = "Skill Level: " + selectedPlayer.CurrentSkill.ToString() + "/" + selectedPlayer.PotentialSkill.ToString();

            lblYearlySalary.Text = "Yearly Salary: " + selectedPlayer.Contract.YearlySalary.ToString("C0");
            lblYearsLeft.Text = "Years Left: " + selectedPlayer.Contract.Years.ToString();
            lblAgentPaid.Text = "Agent Paid: " + selectedPlayer.Contract.AgentPaySchedule.ToString();
            lblAgentPercent.Text = "Agent Percentage: " + selectedPlayer.Contract.AgentPercentage.ToString() + "%";

            lblPopularity.Text = "Popularity: " + selectedPlayer.PopularityString;
            lblGreed.Text = "Greed: " + selectedPlayer.Greed.ToString();
            lblLifestyle.Text = "Lifestyle: " + selectedPlayer.Lifestyle.ToString();
            lblLoyalty.Text = "Loyalty: " + selectedPlayer.Loyalty.ToString();
            lblPlayForTitle.Text = "Play for Title: " + selectedPlayer.PlayForTitleContender.ToString();
            lblTeamHappiness.Text = "Team Happiness: " + selectedPlayer.TeamHappinessString;
            lblAgencyHappiness.Text = "Agency Happiness: " + selectedPlayer.AgencyHappinessString;
        }
        public void DisplayBasketballStats(BasketballPlayer player)
        {
            lblStats.Text = "PTS: " + player.Points.ToString("0.##") + Environment.NewLine + "REB: " + player.Rebounds.ToString("0.##") + Environment.NewLine
                + "AST: " + player.Assists.ToString("0.##") + Environment.NewLine + "BLK: " + player.Blocks.ToString("0.##") + Environment.NewLine
                + "STL: " + player.Steals.ToString("0.##");
        }
    }
}
