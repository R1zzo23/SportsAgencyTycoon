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
            
            lblTeamInfo.Text = selectedTeam.City + " " + selectedTeam.Mascot + "(" + selectedTeam.Wins + "-" + selectedTeam.Losses + ")   Title Contender (" + selectedTeam.TitleConteder + ") || Market Value: (" + selectedTeam.MarketValue + ")";
            if (selectedLeague.Sport == Sports.Basketball)
                foreach (BasketballPlayer p in selectedTeam.Roster)
                    lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + p.AgencyHappinessString + " " + p.TeamHappinessString + " " + p.PopularityString + Environment.NewLine;
            else if (selectedLeague.Sport == Sports.Football)
                lblRoster.Text = DisplayFootballTeamStats(selectedTeam);
                //foreach (FootballPlayer p in selectedTeam.Roster)
                    //lblRoster.Text += "[" + p.Position.ToString() + "] " + p.LastName + ", " + p.FirstName + ": " + p.CurrentSkill + "/" + p.PotentialSkill + " - " + p.Age + "-years old" + Environment.NewLine;
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

            lblDepthChart.Text = "Spot on Depth Chart: " + selectedPlayer.DepthChart.ToString();
            if (selectedPlayer.IsStarter) lblStarter.Text = "Starter: yes";
            else lblStarter.Text = "Starter: no";

            if (selectedLeague.Sport == Sports.Basketball)
            {
                List<BasketballPlayer> hoopsRoster = new List<BasketballPlayer>();
                foreach (BasketballPlayer bp in selectedTeam.Roster)
                    hoopsRoster.Add(bp);
                DisplayBasketballStats(hoopsRoster[cbTeamRoster.SelectedIndex]);
            }
            else if (selectedLeague.Sport == Sports.Football)
            {
                List<FootballPlayer> footballRoster = new List<FootballPlayer>();
                foreach (FootballPlayer fp in selectedTeam.Roster)
                    footballRoster.Add(fp);
                DisplayFootballStats(footballRoster[cbTeamRoster.SelectedIndex]);
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
        public void DisplayFootballStats(FootballPlayer player)
        {
            if (player.Position == Position.QB) lblStats.Text = DisplayQBStats(player);
            if (player.Position == Position.RB || player.Position == Position.FB) lblStats.Text = DisplayRushingStats(player);
            if (player.Position == Position.WR || player.Position == Position.TE) lblStats.Text = DisplayReceivingStats(player);
            if (player.Position == Position.OT || player.Position == Position.OG || player.Position == Position.C) lblStats.Text = DisplayOLStats(player);
        }
        public string DisplayQBStats(FootballPlayer player)
        {
            string stats = "YDS: " + player.PassingYards.ToString() + Environment.NewLine + "TDS: " + player.PassingTDs.ToString()
                + Environment.NewLine + "INT: " + player.Interceptions.ToString();
            return stats;
        }
        public string DisplayRushingStats(FootballPlayer player)
        {
            string stats = "YDS: " + player.RushingYards.ToString() + Environment.NewLine + "TDS: " + player.RushingTDs.ToString()
                + Environment.NewLine + "CAR: " + player.Carries.ToString() + Environment.NewLine + "YPC: " + player.YardsPerCarry.ToString("0.##") 
                + Environment.NewLine + "CHNK: " + player.ChunkPlays.ToString() + Environment.NewLine + "FUM: " + player.Fumbles.ToString();
            return stats;
        }
        public string DisplayReceivingStats(FootballPlayer player)
        {
            string stats = "REC: " + player.Receptions.ToString() + Environment.NewLine +
                "YDS: " + player.ReceivingYards.ToString() + Environment.NewLine +
                "TDS: " + player.ReceivingTDs.ToString();
            return stats;
        }
        public string DisplayOLStats(FootballPlayer player)
        {
            string stats = "Pancakes: " + player.PancakeBlocks + Environment.NewLine + "Sacks Allowed: " + player.SacksAllowed +
                Environment.NewLine + "Rushing YPC: " + player.YardsPerCarry + Environment.NewLine + "Rushing TDS: " + player.RushingTDs;
            return stats;
        }
        public string DisplayFootballTeamStats(Team t)
        {
            List<FootballPlayer> QBS = new List<FootballPlayer>();
            List<FootballPlayer> Backs = new List<FootballPlayer>();
            List<FootballPlayer> PassCatchers = new List<FootballPlayer>();
            string stats = "";

            foreach (FootballPlayer p in t.Roster)
            {
                if (p.Position == Position.QB) QBS.Add(p);
                else if (p.Position == Position.RB || p.Position == Position.FB) Backs.Add(p);
                else if (p.Position == Position.WR || p.Position == Position.TE) PassCatchers.Add(p);
            }

            QBS = QBS.OrderByDescending(o => o.PassingYards).ToList();
            Backs = Backs.OrderByDescending(o => o.RushingYards).ToList();
            PassCatchers = PassCatchers.OrderByDescending(o => o.Receptions).ToList();

            foreach (FootballPlayer fp in QBS)
                stats += fp.FullName + " " + fp.PassingYards + " YDS || " + fp.PassingTDs + " TDS || " + fp.Interceptions + " INT" + Environment.NewLine;
            stats += Environment.NewLine;
            foreach (FootballPlayer fp in Backs)
                stats += fp.FullName + " " + fp.Carries + " CAR || " + fp.RushingYards + " YDS || " + fp.RushingTDs + " TDS || " + fp.Fumbles + " fumbles" + Environment.NewLine;
            stats += Environment.NewLine;
            foreach (FootballPlayer fp in PassCatchers)
                stats += fp.FullName + " " + fp.Receptions + " REC || " + fp.ReceivingYards + " YDS || " + fp.ReceivingTDs + " TDS || " + Environment.NewLine;

            return stats;
        }
    }
}
