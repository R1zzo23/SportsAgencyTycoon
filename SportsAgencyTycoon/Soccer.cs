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
                mainForm.newsLabel.Text = DisplayForwardOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayMidfielderOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayDefensemanOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayKeeperOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;

                mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
            }
            if (MLS.Playoffs)
            {
                if (MLS.WeekNumber == 35) mainForm.newsLabel.Text = SimulateFirstRound() + Environment.NewLine + mainForm.newsLabel.Text;
                else if (MLS.WeekNumber == 36 || MLS.WeekNumber == 37) mainForm.newsLabel.Text = SimulateSecondAndThirdRounds() + Environment.NewLine + mainForm.newsLabel.Text;
                else if (MLS.WeekNumber == 38) mainForm.newsLabel.Text = SimulateChampionship() + Environment.NewLine + mainForm.newsLabel.Text;
            }
        }
        #region Statistics
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
                        DetermineAssists(p);
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
                p.ShotsFaced += shotsFaced;

                DetermineSaves(p, shotsFaced);
            }
        }
        private void DetermineSaves(SoccerPlayer p, int shotsFaced)
        {
            int gkSaveRoll = 0;
            int goalsAllowed = 0;
            int diceRoll = 0;
            if (p.CurrentSkill >= 65) gkSaveRoll = 5;
            else if (p.CurrentSkill >= 50) gkSaveRoll = 6;
            else if (p.CurrentSkill >= 35) gkSaveRoll = 7;
            else gkSaveRoll = 8;

            for (int i = 0; i < shotsFaced; i++)
            {
                diceRoll = DiceRoll();
                if (diceRoll <= gkSaveRoll) goalsAllowed++;
            }
            if (goalsAllowed > 4) goalsAllowed = 4;
            p.GoalsAllowed += goalsAllowed;
            p.Saves += shotsFaced - goalsAllowed;
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
        private void InitializeMatchRating(SoccerPlayer p)
        {
            if (p.CurrentSkill >= 70) p.MatchRating = Convert.ToDouble(rnd.Next(750, 900)) / 100;
            else if (p.CurrentSkill >= 50) p.MatchRating = Convert.ToDouble(rnd.Next(600, 750)) / 100;
            else if (p.CurrentSkill >= 40) p.MatchRating = Convert.ToDouble(rnd.Next(500, 650)) / 100;
            else if (p.CurrentSkill >= 30) p.MatchRating = Convert.ToDouble(rnd.Next(400, 550)) / 100;
            else if (p.CurrentSkill >= 20) p.MatchRating = Convert.ToDouble(rnd.Next(300, 450)) / 100;
            else p.MatchRating = Convert.ToDouble(rnd.Next(200, 350)) / 100;
        }
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
                if (p.DepthChart == 1 || p.DepthChart == 2)
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
                if (p.DepthChart == 1 || p.DepthChart == 2)
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
                if (p.DepthChart == 1 || p.DepthChart == 2)
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
            int goalsScored = 0;
            int diceRoll = 0;
            for (int i = 0; i < shotsTaken; i++)
            {
                diceRoll = DiceRoll();
                if (p.Position == Position.F)
                {
                    if (p.CurrentSkill >= 65 && diceRoll <= 6) goalsScored++;
                    else if (p.CurrentSkill >= 50 && diceRoll <= 5) goalsScored++;
                    else if (p.CurrentSkill >= 35 && diceRoll <= 4) goalsScored++;
                    else if (p.CurrentSkill < 35 && diceRoll <= 3) goalsScored++;
                }
                else if (p.Position == Position.MID)
                {
                    if (p.CurrentSkill >= 65 && diceRoll <= 5) goalsScored++;
                    else if (p.CurrentSkill >= 50 && diceRoll <= 4) goalsScored++;
                    else if (p.CurrentSkill >= 35 && diceRoll <= 3) goalsScored++;
                    else if (p.CurrentSkill < 35 && diceRoll <= 2) goalsScored++;
                }
                else if (p.Position == Position.D)
                {
                    if (p.CurrentSkill >= 65 && diceRoll <= 4) goalsScored++;
                    else if (p.CurrentSkill >= 50 && diceRoll <= 3) goalsScored++;
                    else if (p.CurrentSkill < 50 && diceRoll == 2) goalsScored++;
                }
            }
            p.Goals += goalsScored;
        }
        private void DetermineAssists(SoccerPlayer p)
        {
            bool assist = false;
            int diceRoll = DiceRoll();
            if (p.Position == Position.F || p.Position == Position.D)
            {
                if (p.DepthChart == 1 || p.DepthChart == 2)
                {
                    if (p.CurrentSkill >= 65)
                    {
                        if (diceRoll <= 4) assist = true;
                    }
                    else if (p.CurrentSkill >= 45)
                    {
                        if (diceRoll <= 3) assist = true; 
                    }
                    else
                    {
                        if (diceRoll == 2) assist = true;
                    }
                }
                else
                {
                    if (p.CurrentSkill >= 55)
                    {
                        if (diceRoll <= 3) assist = true;
                    }
                    else
                    {
                        if (diceRoll == 2) assist = true;
                    }
                }
            }
            else if (p.Position == Position.MID)
            {
                if (p.DepthChart == 1 || p.DepthChart == 2)
                {
                    if (p.CurrentSkill >= 65)
                    {
                        if (diceRoll <= 6) assist = true;
                    }
                    else if (p.CurrentSkill >= 45)
                    {
                        if (diceRoll <= 4) assist = true;
                    }
                    else
                    {
                        if (diceRoll <= 3) assist = true;
                    }
                }
                else
                {
                    if (p.CurrentSkill >= 55)
                    {
                        if (diceRoll <= 3) assist = true;
                    }
                    else
                    {
                        if (diceRoll == 2) assist = true;
                    }
                }
            }
            if (assist) p.Assists++;
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
            }
            else
            {
                totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
                winningNumber = rnd.Next(1, totalNumber);
                if (winningNumber <= team1.TitleConteder)
                {
                    team1.Wins++;
                    team2.Losses++;
                }
                else
                {
                    team1.Losses++;
                    team2.Wins++;
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
        public void RemoveLosingTeamsFromPlayoffs()
        {
            EasternLoserIndex = EasternLoserIndex.OrderByDescending(i => i).ToList();
            WesternLoserIndex = WesternLoserIndex.OrderByDescending(i => i).ToList();

            foreach (int i in EasternLoserIndex) EasternPlayoffs.RemoveAt(i);
            foreach (int i in WesternLoserIndex) WesternPlayoffs.RemoveAt(i);
        }
        private string SimulatePlayoffGame(Team team1, Team team2, int teamIndex1, int teamIndex2)
        {
            int totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
            int winningNumber = rnd.Next(1, totalNumber);
            if (winningNumber <= team1.TitleConteder)
            {
                losingIndex = teamIndex2;
                return team1.City + " " + team1.Mascot + " defeats " + team2.City + " " + team2.Mascot + "!";
            }
            else
            {
                losingIndex = teamIndex1;
                return team2.City + " " + team2.Mascot + " defeats " + team1.City + " " + team1.Mascot + "!";
            }
        }
        private string SimulateFirstRound()
        {
            string results = "";
            EasternLoserIndex.Clear();
            WesternLoserIndex.Clear();

            for (int i = 0; i < EasternPlayoffs.Count / 2; i++)
            {
                results += SimulatePlayoffGame(EasternPlayoffs[i], EasternPlayoffs[EasternPlayoffs.Count - 1 - i], i, EasternPlayoffs.Count - 1 - i) + Environment.NewLine;
                EasternLoserIndex.Add(losingIndex);
                results += SimulatePlayoffGame(WesternPlayoffs[i], WesternPlayoffs[EasternPlayoffs.Count - 1 - i], i, WesternPlayoffs.Count - 1 - i) + Environment.NewLine;
                WesternLoserIndex.Add(losingIndex);
            }

            RemoveLosingTeamsFromPlayoffs();

            return results;
        }
        private string SimulateSecondAndThirdRounds()
        {
            string results = "";

            EasternLoserIndex.Clear();
            WesternLoserIndex.Clear();

            for (int i = 0; i < EasternPlayoffs.Count / 2; i++)
            {
                results += SimulatePlayoffGame(EasternPlayoffs[i], EasternPlayoffs[EasternPlayoffs.Count - 1 - i], i, EasternPlayoffs.Count - 1 - i) + Environment.NewLine;
                EasternLoserIndex.Add(losingIndex);
                results += SimulatePlayoffGame(WesternPlayoffs[i], WesternPlayoffs[EasternPlayoffs.Count - 1 - i], i, WesternPlayoffs.Count - 1 - i) + Environment.NewLine;
                WesternLoserIndex.Add(losingIndex);
            }

            RemoveLosingTeamsFromPlayoffs();

            return results;
        }
        private string SimulateChampionship()
        {
            string results = "";

            EasternPlayoffs[0].Awards.Add(new Award(World.Year, "Eastern Conference Champs"));
            WesternPlayoffs[0].Awards.Add(new Award(World.Year, "Western Conference Champs"));

            results = SimulatePlayoffGame(EasternPlayoffs[0], WesternPlayoffs[0], 0, 1);
            if (losingIndex == 0)
            {
                results = "The " + WesternPlayoffs[0].City + " " + WesternPlayoffs[0].Mascot + " are the " + World.Year + " MLS champions!" + Environment.NewLine + results;
                WesternPlayoffs[0].Awards.Add(new Award(World.Year, "MLS Champions"));
            }

            else
            {
                results = "The " + EasternPlayoffs[0].City + " " + EasternPlayoffs[0].Mascot + " are the " + World.Year + " MLS champions!" + Environment.NewLine + results;
                EasternPlayoffs[0].Awards.Add(new Award(World.Year, "MLS Champions"));
            }

            return results;
        }
        #endregion
        #endregion
        #region Awards
        private void CalculatePlayerOfTheYearScore(SoccerPlayer p)
        {
            int goalModifier = 4;
            int assistModifier = 1;
            double score = 0;
            if (p.Position == Position.F) goalModifier = 7;
            if (p.Position == Position.MID) assistModifier = 5;

            score = (p.CurrentSkill * 2) + (p.Team.Points * 2) + (p.Popularity * 2) + (p.MatchRating * 10) + (p.Goals * goalModifier) + (p.Assists * assistModifier);
            p.PlayerOfYearScore = score;
        }
        private void CalculateKeeperOfTheYearScore(SoccerPlayer p)
        {
            double score = 0;
            score = (p.CurrentSkill * 2) + (p.Team.Wins * 7) + (p.Popularity) + (2 - (p.GAA * 100)) + (p.CleanSheets * 5) + (p.Saves) + p.SavePercentage;
            if (p.DepthChart != 1) score = score / 2;
            p.PlayerOfYearScore = score;
        }
        private string DisplayForwardOfTheYearTop5()
        {
            string results = "";

            List<SoccerPlayer> forwardRanks = new List<SoccerPlayer>();
            foreach (Team t in World.MLS.TeamList)
                foreach (SoccerPlayer p in t.Roster)
                    if (p.Position == Position.F)
                        forwardRanks.Add(p);

            forwardRanks = forwardRanks.OrderByDescending(o => o.PlayerOfYearScore).ToList();

            results = forwardRanks[0].Team.City + " " + forwardRanks[0].Team.Mascot + "'s " + forwardRanks[0].FullName + " has been named MLS Forward of the Year! " + forwardRanks[0].PlayerOfYearScore + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + forwardRanks[i - 1].Team.Abbreviation + "] " + forwardRanks[i - 1].FullName + " " + forwardRanks[i - 1].PlayerOfYearScore;
            }

            //give the winner the award
            forwardRanks[0].Awards.Add(new Award(World.Year, "Forward of the Year"));

            return results;
        }
        private string DisplayMidfielderOfTheYearTop5()
        {
            string results = "";

            List<SoccerPlayer> midfielderRanks = new List<SoccerPlayer>();
            foreach (Team t in World.MLS.TeamList)
                foreach (SoccerPlayer p in t.Roster)
                    if (p.Position == Position.MID)
                        midfielderRanks.Add(p);

            midfielderRanks = midfielderRanks.OrderByDescending(o => o.PlayerOfYearScore).ToList();

            results = midfielderRanks[0].Team.City + " " + midfielderRanks[0].Team.Mascot + "'s " + midfielderRanks[0].FullName + " has been named MLS Midfiedler of the Year! " + midfielderRanks[0].PlayerOfYearScore + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + midfielderRanks[i - 1].Team.Abbreviation + "] " + midfielderRanks[i - 1].FullName + " " + midfielderRanks[i - 1].PlayerOfYearScore;
            }

            //give the winner the award
            midfielderRanks[0].Awards.Add(new Award(World.Year, "Midfielder of the Year"));

            return results;
        }
        private string DisplayDefensemanOfTheYearTop5()
        {
            string results = "";

            List<SoccerPlayer> defenderRanks = new List<SoccerPlayer>();
            foreach (Team t in World.MLS.TeamList)
                foreach (SoccerPlayer p in t.Roster)
                    if (p.Position == Position.D)
                        defenderRanks.Add(p);

            defenderRanks = defenderRanks.OrderByDescending(o => o.PlayerOfYearScore).ToList();

            results = defenderRanks[0].Team.City + " " + defenderRanks[0].Team.Mascot + "'s " + defenderRanks[0].FullName + " has been named MLS Defender of the Year! " + defenderRanks[0].PlayerOfYearScore + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + defenderRanks[i - 1].Team.Abbreviation + "] " + defenderRanks[i - 1].FullName + " " + defenderRanks[i - 1].PlayerOfYearScore;
            }

            //give the winner the award
            defenderRanks[0].Awards.Add(new Award(World.Year, "Defender of the Year"));

            return results;
        }
        private string DisplayKeeperOfTheYearTop5()
        {
            string results = "";

            List<SoccerPlayer> keeperRanks = new List<SoccerPlayer>();
            foreach (Team t in World.MLS.TeamList)
                foreach (SoccerPlayer p in t.Roster)
                    if (p.Position == Position.GK)
                        keeperRanks.Add(p);

            keeperRanks = keeperRanks.OrderByDescending(o => o.PlayerOfYearScore).ToList();

            results = keeperRanks[0].Team.City + " " + keeperRanks[0].Team.Mascot + "'s " + keeperRanks[0].FullName + " has been named MLS Keeper of the Year! " + keeperRanks[0].PlayerOfYearScore + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + keeperRanks[i - 1].Team.Abbreviation + "] " + keeperRanks[i - 1].FullName + " " + keeperRanks[i - 1].PlayerOfYearScore;
            }

            //give the winner the award
            keeperRanks[0].Awards.Add(new Award(World.Year, "Keeper of the Year"));

            return results;
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
