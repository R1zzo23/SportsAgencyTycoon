using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Soccer
    {
        public MainForm mainForm;
        public Random rnd;
        public League MLS;
        public World World;
        public int gamesThisWeek = 0;
        public int gamesForStarter = 0;
        public int gamesForBackup = 0;
        public int starterWins = 0;
        public int backupWins = 0;
        public int index;
        public int losingIndex;
        public List<string> Conferences;
        public List<string> Divisions;
        public List<Team> EasternConference = new List<Team>();
        public List<Team> WesternConference = new List<Team>();
        public List<Team> EasternPlayoffs = new List<Team>();
        public List<Team> WesternPlayoffs = new List<Team>();
        public List<int> EasternLoserIndex = new List<int>();
        public List<int> WesternLoserIndex = new List<int>();
        public Soccer(MainForm mf, Random r, World w, League l)
        {
            mainForm = mf;
            rnd = r;
            World = w;
            MLS = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            FillLists();
        }
        private void FillLists()
        {
            foreach (Team t in MLS.TeamList)
            {
                if (t.Conference == "Eastern") EasternConference.Add(t);
                else WesternConference.Add(t);
            }
        }
        public void SimWeek()
        {
            if (MLS.WeekNumber == 0)
                foreach (Team t in MLS.TeamList)
                    foreach (SoccerPlayer p in t.Roster)
                        InitializeMatchRating(p);
            MLS.WeekNumber++;
            Console.WriteLine("MLS.WeekNumber: " + MLS.WeekNumber);
            if (!MLS.Playoffs)
            {
                SimulateGames();
                foreach (Team t in MLS.TeamList)
                    CalculateTeamPoints(t);
                UpdateStats();
            }
            if (MLS.WeekNumber == 34)
            {
                MLS.Playoffs = true;
                foreach (Team t in MLS.TeamList)
                {
                    foreach (SoccerPlayer p in t.Roster)
                    {
                        if (p.Position == Position.F || p.Position == Position.MID || p.Position == Position.D)
                            CalculatePlayerOfTheYearScore(p);
                        else if (p.Position == Position.GK)
                            CalculateKeeperOfTheYearScore(p);
                    }
                }
                //mainForm.newsLabel.Text = DisplayForwardOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                //mainForm.newsLabel.Text = DisplayDefensemanOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                //mainForm.newsLabel.Text = DisplayKeeperOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;

                //mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
            }
            if (MLS.Playoffs)
            {
                //if (MLS.WeekNumber == 32 || MLS.WeekNumber == 33 || MLS.WeekNumber == 34) mainForm.newsLabel.Text = SimulatePlayoffRound() + Environment.NewLine + mainForm.newsLabel.Text;
                //else if (MLS.WeekNumber == 35) mainForm.newsLabel.Text = SimulateStanleyCup() + Environment.NewLine + mainForm.newsLabel.Text;
            }
        }
        #region Statistics
        private void InitializeMatchRating(SoccerPlayer p)
        {
            if (p.CurrentSkill >= 70) p.MatchRating = Convert.ToDouble(rnd.Next(850, 1000)) / 100;
            else if (p.CurrentSkill >= 50) p.MatchRating = Convert.ToDouble(rnd.Next(700, 850)) / 100;
            else if (p.CurrentSkill >= 40) p.MatchRating = Convert.ToDouble(rnd.Next(600, 750)) / 100;
            else if (p.CurrentSkill >= 30) p.MatchRating = Convert.ToDouble(rnd.Next(500, 650)) / 100;
            else if (p.CurrentSkill >= 20) p.MatchRating = Convert.ToDouble(rnd.Next(450, 550)) / 100;
            else p.MatchRating = Convert.ToDouble(rnd.Next(180, 195)) / 100;

        }
        private void UpdateStats()
        {

        }
        #endregion
        #region Simulation
        #region Regular Season
        private void SimulateGames()
        {
            var indexList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};

            for (int j = 0; j < MLS.TeamList.Count / 2; j++)
            {
                int opponentIndex = rnd.Next(1, indexList.Count);
                SimulateGame(MLS.TeamList[indexList[0]], MLS.TeamList[indexList[opponentIndex]]);
                indexList.RemoveAt(opponentIndex);
                indexList.RemoveAt(0);
            }
            index++;
            if (index == 30) index = 1;
        }
        private void SimulateGame(Team team1, Team team2)
        {
            int totalNumber = 0;
            int winningNumber = 0;
            int tieRandom = rnd.Next(1, 101);
            if (tieRandom <= 30)
            {
                team1.Ties++;
                team2.Ties++;
                Console.WriteLine(team1.City + " " + team1.Mascot + " and  " + team2.City + " " + team2.Mascot + " played to a tie!");
            }
            else
            {
                totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
                winningNumber = rnd.Next(1, totalNumber);
                if (winningNumber <= team1.TitleConteder)
                {
                    team1.Wins++;
                    team2.Losses++;
                    Console.WriteLine(team1.Mascot + " beat the " + team2.Mascot);
                }
                else
                {
                    team1.Losses++;
                    team2.Wins++;
                    Console.WriteLine(team2.Mascot + " beat the " + team1.Mascot);
                }
                team1.PlayedGameThisCycle = true;
                team2.PlayedGameThisCycle = true;
            }
        }
        private void CalculateTeamPoints(Team t)
        {
            t.Points = (t.Wins * 3) + t.Ties;
        }
        #endregion
        #region Playoffs

        #endregion
        #endregion
        #region Awards
        private void CalculatePlayerOfTheYearScore(SoccerPlayer p)
        {

        }
        private void CalculateKeeperOfTheYearScore(SoccerPlayer p)
        {

        }
        #endregion
    }
}
