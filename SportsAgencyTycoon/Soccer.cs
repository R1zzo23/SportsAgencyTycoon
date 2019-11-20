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

                mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
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
            foreach (Team t in MLS.TeamList)
                foreach (SoccerPlayer p in t.Roster)
                {
                    if (p.Position == Position.GK)
                    {
                        DetermineShotsFaced(p);
                    }
                    else
                    {
                        UpdateMatchRating(p);
                        DetermineShotsTaken(p);
                    }
                }
        }
        #region Goal Keeper Stats
        private void DetermineShotsFaced(SoccerPlayer p)
        {
            if (p.DepthChart == 1)
            {
                p.GamesPlayed++;
                int shotsFaced = rnd.Next(2, 7);
                p.ShotsFaced = shotsFaced;

                DetermineSaves(p, shotsFaced);
            }
        }
        private void DetermineSaves(SoccerPlayer p, int shotsFaced)
        {
            int gkSaveRoll = 0;
            int goalsAllowed = 0;
            int diceRoll = 0;
            if (p.CurrentSkill >= 70) gkSaveRoll = 3;
            else if (p.CurrentSkill >= 60) gkSaveRoll = 4;
            else if (p.CurrentSkill >= 50) gkSaveRoll = 5;
            else if (p.CurrentSkill >= 40) gkSaveRoll = 6;
            else gkSaveRoll = 7;

            for (int i = 0; i < shotsFaced; i++)
            {
                diceRoll = DiceRoll();
                if (diceRoll <= gkSaveRoll) goalsAllowed++;
            }
            if (goalsAllowed > 4) goalsAllowed = 4;
            p.GoalsAllowed += goalsAllowed;
            if (goalsAllowed == 0) p.CleanSheets++;
            CalculateGAA(p);
            CalculateSavePercentage(p);
        }
        private void CalculateGAA(SoccerPlayer p)
        {
            p.GAA = Convert.ToDouble(p.GoalsAllowed) / Convert.ToDouble(p.GamesPlayed);
        }
        private void CalculateSavePercentage(SoccerPlayer p)
        {
            p.SavePercentage = Convert.ToDouble(p.Saves) / Convert.ToDouble(p.ShotsFaced);
        }
        #endregion
        #region Non-Goal Keeper Stats
        private void UpdateMatchRating(SoccerPlayer p)
        {
            double change = Convert.ToDouble(rnd.Next(-50, 51)) / 1000;
            p.MatchRating *= (1 + change);
            if (p.MatchRating > 10) p.MatchRating = 10.0;
        }
        private void DetermineShotsTaken(SoccerPlayer p)
        {
            int shotsTaken = 0;
            int diceRoll = DiceRoll();
            int diceRoll2 = DiceRoll();
            if (p.Position == Position.F)
            {
                if (p.DepthChart == 1)
                {
                    if (p.CurrentSkill >= 65)
                    {
                        if (diceRoll <= 3) shotsTaken = 3;
                        else if (diceRoll == 12) shotsTaken = 1;
                        else shotsTaken = 2;
                    }
                    else if (p.CurrentSkill >= 45)
                    {
                        if (diceRoll <= 3) shotsTaken = 2;
                        else if (diceRoll == 12) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                    else
                    {
                        if (diceRoll == 2) shotsTaken = 2;
                        if (diceRoll >= 11) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                }
                else
                {
                    if (p.CurrentSkill >= 55)
                    {
                        if (diceRoll <= 3) shotsTaken = 2;
                        else if (diceRoll >= 10) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                    else
                    {
                        if (diceRoll <= 4) shotsTaken = 1;
                        else shotsTaken = 0;
                    }
                }
            }
            else if (p.Position == Position.MID)
            {
                if (p.DepthChart == 1)
                {
                    if (p.CurrentSkill >= 65)
                    {
                        if (diceRoll <= 3) shotsTaken = 2;
                        else if (diceRoll >= 11) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                    else if (p.CurrentSkill >= 45)
                    {
                        if (diceRoll == 2) shotsTaken = 2;
                        else if (diceRoll == 7) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                    else
                    {
                        if (diceRoll == 2) shotsTaken = 2;
                        if (diceRoll >= 8) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                }
                else
                {
                    if (p.CurrentSkill >= 55)
                    {
                        if (diceRoll == 2) shotsTaken = 2;
                        else if (diceRoll >= 6) shotsTaken = 0;
                        else shotsTaken = 1;
                    }
                    else
                    {
                        if (diceRoll <= 3) shotsTaken = 1;
                        else shotsTaken = 0;
                    }
                }
            }
            else if (p.Position == Position.D)
            {
                if (p.DepthChart == 1)
                {
                    if (p.CurrentSkill >= 60)
                    {
                        if (diceRoll <= 3) shotsTaken = 1;
                        else shotsTaken = 0;
                    }
                    else
                    {
                        if (diceRoll == 2) shotsTaken = 1;
                        else shotsTaken = 0;
                    }
                }
                else
                {
                    diceRoll2 = DiceRoll();
                    if (diceRoll <= 3 && diceRoll2 <= 3) shotsTaken = 1;
                    else shotsTaken = 0;
                }
            }
            DetermineGoals(p, shotsTaken);
        }
        private void DetermineGoals(SoccerPlayer p, int shotsTaken)
        {

        }
        #endregion
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
            }
            team1.PlayedGameThisCycle = true;
            team2.PlayedGameThisCycle = true;
        }
        private void CalculateTeamPoints(Team t)
        {
            t.Points = (t.Wins * 3) + t.Ties;
        }
        #endregion
        #region Playoffs
        private string DeterminePlayoffField()
        {
            string playoffSeedings = "";
            EasternConference.Clear();
            WesternConference.Clear();
            EasternPlayoffs.Clear();
            WesternPlayoffs.Clear();

            foreach (Team t in World.MLS.TeamList)
            {
                if (t.Conference == "Eastern") EasternConference.Add(t);
                else WesternConference.Add(t);
            }

            EasternConference = EasternConference.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
            WesternConference = WesternConference.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();

            for (int i = 0; i < 7; i++)
            {
                EasternPlayoffs.Add(EasternConference[i]);
                WesternPlayoffs.Add(WesternConference[i]);
            }

            playoffSeedings = "Eastern Conference Playoffs:" + Environment.NewLine + "#1 " + EasternPlayoffs[0].Abbreviation +
                Environment.NewLine + "#2 " + EasternPlayoffs[1].Abbreviation + " vs. #7 " + EasternPlayoffs[6].Abbreviation +
                Environment.NewLine + "#3 " + EasternPlayoffs[2].Abbreviation + " vs. #6 " + EasternPlayoffs[5].Abbreviation +
                Environment.NewLine + "#4 " + EasternPlayoffs[3].Abbreviation + " vs. #5 " + EasternPlayoffs[4].Abbreviation +
                Environment.NewLine;
                
            playoffSeedings += "Western Conference Playoffs:" + Environment.NewLine + "#1 " + WesternPlayoffs[0].Abbreviation +
                Environment.NewLine + "#2 " + WesternPlayoffs[1].Abbreviation + " vs. #7 " + WesternPlayoffs[6].Abbreviation +
                Environment.NewLine + "#3 " + WesternPlayoffs[2].Abbreviation + " vs. #6 " + WesternPlayoffs[5].Abbreviation +
                Environment.NewLine + "#4 " + WesternPlayoffs[3].Abbreviation + " vs. #5 " + WesternPlayoffs[4].Abbreviation +
                Environment.NewLine;

            return playoffSeedings;
        }
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
        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
