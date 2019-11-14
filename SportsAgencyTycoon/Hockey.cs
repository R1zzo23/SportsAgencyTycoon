using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Hockey
    {
        public MainForm mainForm;
        public Random rnd;
        public League NHL;
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
        public List<Team> Metropolitan = new List<Team>();
        public List<Team> Atlantic = new List<Team>();
        public List<Team> Central = new List<Team>();
        public List<Team> Pacific = new List<Team>();
        public List<Team> EasternPlayoffs = new List<Team>();
        public List<Team> WesternPlayoffs = new List<Team>();
        public List<int> EasternLoserIndex = new List<int>();
        public List<int> WesternLoserIndex = new List<int>();
        public List<BasketballPlayer> DPOYCandidates = new List<BasketballPlayer>();
        public Hockey(MainForm mf, Random r, World w, League l)
        {
            mainForm = mf;
            rnd = r;
            World = w;
            NHL = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            FillLists();
        }
        private void FillLists()
        {
            foreach (Team t in NHL.TeamList)
                if (t.Conference == "Eastern")
                {
                    EasternConference.Add(t);
                    if (t.Division == "Metropolitan") Metropolitan.Add(t);
                    else Atlantic.Add(t);
                }
                else
                {
                    WesternConference.Add(t);
                    if (t.Division == "Central") Central.Add(t);
                    else Pacific.Add(t);
                }
        }
        public void SimWeek()
        {
            if (NHL.WeekNumber == 0)
                //InitializeGAA();
            NHL.WeekNumber++;
            if (!NHL.Playoffs)
            {
                SimulateGames();
                UpdateStats();
            }
            if (NHL.WeekNumber == 32)
            {
                NHL.Playoffs = true;
                foreach (Team t in NHL.TeamList)
                {
                    foreach (HockeyPlayer p in t.Roster)
                    {
                        if (p.Position == Position.W || p.Position == Position.C)
                            CalculateForwardOfTheYearScore(p);
                        else if (p.Position == Position.D)
                            CalculateDefensemanOfTheYearScore(p);
                        else if (p.Position == Position.G)
                            CalculateGoalieOfTheYearScore(p);
                    }
                }
                mainForm.newsLabel.Text = DisplayForwardOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayDefensemanOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayGoalieOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;

                mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
            }
            if (NHL.Playoffs)
            {

            }
        }
        #region Statistics
        private void UpdateStats()
        {
            foreach (Team t in NHL.TeamList)
            {
                int gamesForStarterRoll = DiceRoll();
                if (gamesForStarterRoll >= 9) gamesForStarter = gamesThisWeek - 1;
                else if (gamesForStarterRoll < 7) gamesForStarter = gamesThisWeek;
                gamesForBackup = gamesThisWeek - gamesForStarter;

                foreach (HockeyPlayer p in t.Roster)
                {
                    if (p.Position == Position.G)
                    {
                        if (t.WinsThisWeek > 0)
                            DetermineGoalieWins(t.WinsThisWeek);
                        DetermineShotsFaced(p);
                    }
                }
            }
                
        }
        private void DetermineGoalieWins(int teamWins)
        {
            if (gamesForBackup == 0)
                starterWins = teamWins;
            else
            {
                int diceRoll = DiceRoll();
                if (diceRoll >= 8)
                {
                    backupWins = 1;
                    starterWins = teamWins - 1;
                }
            }
        }
        private void DetermineShotsFaced(HockeyPlayer p)
        {
            int shotsFaced = 0;
            int numberOfGames = 0;
            if (p.DepthChart == 1)
            {
                p.Wins += starterWins;
                numberOfGames = gamesForStarter;
            }
            else
            {
                p.Wins += backupWins;
                numberOfGames = gamesForBackup;
            }

            if (numberOfGames > 0)
            {
                for (int i = 0; i < numberOfGames; i++)
                {
                    shotsFaced = rnd.Next(25, 36);
                    p.ShotsFaced += shotsFaced;
                    DetermineShotsSaved(p, shotsFaced);
                }
            }
            p.GamesPlayed += numberOfGames;
        }
        private void DetermineShotsSaved(HockeyPlayer p, int shotsFaced)
        {
            int saves = 0;
            int goalsAllowed = 0;

            for (int i = 0; i < shotsFaced; i++)
            {
                int saveRoll = DiceRoll();
                if (p.CurrentSkill >= 65)
                {
                    if (saveRoll > 2) saves++;
                }
                else if (p.CurrentSkill >= 45)
                {
                    if (saveRoll > 3) saves++;
                }
                else
                {
                    if (saveRoll > 4) saves++;
                }
            }
            p.Saves += saves;
            goalsAllowed = shotsFaced - saves;
            p.GoalsAllowed += goalsAllowed;

            if (saves == shotsFaced) p.ShutOuts++;
            CalculateSavePercentage(p);
            CalculateGAA(p);
        }
        private void CalculateSavePercentage(HockeyPlayer p)
        {
            if (p.ShotsFaced > 0)
            {
                p.SavePercentage = Convert.ToDouble(p.Saves) / Convert.ToDouble(p.ShotsFaced);
            }
            else p.SavePercentage = 0.00;
        }
        private void CalculateGAA(HockeyPlayer p)
        {
            if (p.ShotsFaced > 0)
            {
                p.GAA = Convert.ToDouble(p.GoalsAllowed) / Convert.ToDouble(p.GamesPlayed);
            }
            else p.GAA = 99.99;
        }
        #endregion
        #region Simulation
        private void SimulateGames()
        {
            gamesThisWeek = HowManyGamesThisWeek();
            foreach (Team t in NHL.TeamList) t.WinsThisWeek = 0;
            for (int i = 0; i < gamesThisWeek; i++)
            {
                var indexList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

                for (int j = 0; j < NHL.TeamList.Count / 2; j++)
                {
                    int opponentIndex = rnd.Next(1, indexList.Count);
                    SimulateGame(NHL.TeamList[indexList[0]], NHL.TeamList[indexList[opponentIndex]]);
                    indexList.RemoveAt(opponentIndex);
                    indexList.RemoveAt(0);
                }
                index++;
                if (index == 30) index = 1;
                int rndWinNumber = rnd.Next(1, 101);
                if (rndWinNumber <= NHL.TeamList[indexList[0]].TitleConteder)
                {
                    NHL.TeamList[indexList[0]].Wins++;
                    NHL.TeamList[indexList[0]].WinsThisWeek++;
                }
                else NHL.TeamList[indexList[0]].Losses++;
            }
        }
        private int HowManyGamesThisWeek()
        {
            int games = 0;

            if (NHL.WeekNumber <= 18) games = 3;
            else games = 2;

            return games;
        }
        private void SimulateGame(Team team1, Team team2)
        {
            int totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
            int winningNumber = rnd.Next(1, totalNumber);
            if (winningNumber <= team1.TitleConteder)
            {
                team1.Wins++;
                team1.WinsThisWeek++;
                team2.Losses++;
                Console.WriteLine(team1.Mascot + " beat the " + team2.Mascot);
            }
            else
            {
                team1.Losses++;
                team2.WinsThisWeek++;
                team2.Wins++;
                Console.WriteLine(team2.Mascot + " beat the " + team1.Mascot);
            }
            team1.PlayedGameThisCycle = true;
            team2.PlayedGameThisCycle = true;
        }
        private string DeterminePlayoffField()
        {
            string playoffSeedings = "";
            EasternConference.Clear();
            WesternConference.Clear();
            EasternPlayoffs.Clear();
            WesternPlayoffs.Clear();

            foreach (Team t in World.NHL.TeamList)
            {
                if (t.Conference == "Eastern") EasternConference.Add(t);
                else WesternConference.Add(t);
            }

            EasternConference = EasternConference.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            WesternConference = WesternConference.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();

            int atlanticIndex = EasternConference.FindIndex(t => t.Division == "Atlantic");
            EasternPlayoffs.Add(EasternConference[atlanticIndex]);
            EasternConference[atlanticIndex].Awards.Add(new Award(World.Year, EasternConference[atlanticIndex].Division + " Division Champs"));
            EasternConference.RemoveAt(atlanticIndex);

            int metropolitanIndex = EasternConference.FindIndex(t => t.Division == "Metropolitan");
            EasternPlayoffs.Add(EasternConference[metropolitanIndex]);
            EasternConference[metropolitanIndex].Awards.Add(new Award(World.Year, EasternConference[metropolitanIndex].Division + " Division Champs"));
            EasternConference.RemoveAt(metropolitanIndex);
                      
            EasternPlayoffs = EasternPlayoffs.OrderByDescending(o => o.Wins).ToList();
            for (int i = 0; i < 6; i++)
                EasternPlayoffs.Add(EasternConference[i]);

            int centralIndex = WesternConference.FindIndex(t => t.Division == "Central");
            WesternPlayoffs.Add(WesternConference[centralIndex]);
            WesternConference[centralIndex].Awards.Add(new Award(World.Year, WesternConference[centralIndex].Division + " Division Champs"));
            WesternConference.RemoveAt(centralIndex);

            int pacificIndex = WesternConference.FindIndex(t => t.Division == "Pacific");
            WesternPlayoffs.Add(WesternConference[pacificIndex]);
            WesternConference[pacificIndex].Awards.Add(new Award(World.Year, WesternConference[pacificIndex].Division + " Division Champs"));
            WesternConference.RemoveAt(pacificIndex);
            
            WesternPlayoffs = WesternPlayoffs.OrderByDescending(o => o.Wins).ToList();
            for (int i = 0; i < 6; i++)
                WesternPlayoffs.Add(WesternConference[i]);

            for (int i = 0; i < 4; i++)
            {
                playoffSeedings += i + 1 + ") " + EasternPlayoffs[i].Abbreviation + " vs. " + (EasternPlayoffs.Count - i) + ") " + EasternPlayoffs[EasternPlayoffs.Count - 1 - i].Abbreviation + Environment.NewLine;
                playoffSeedings += i + 1 + ") " + WesternPlayoffs[i].Abbreviation + " vs. " + (WesternPlayoffs.Count - i) + ") " + WesternPlayoffs[WesternPlayoffs.Count - 1 - i].Abbreviation + Environment.NewLine;
            }

            return playoffSeedings;
        }
        #endregion
        #region Awards
        private void CalculateForwardOfTheYearScore(HockeyPlayer p)
        {

        }
        private void CalculateDefensemanOfTheYearScore(HockeyPlayer p)
        {

        }
        private void CalculateGoalieOfTheYearScore(HockeyPlayer p)
        {

        }
        private string DisplayForwardOfTheYearTop5()
        {
            string results = "";
            return results;
        }
        private string DisplayDefensemanOfTheYearTop5()
        {
            string results = "";
            return results;
        }
        private string DisplayGoalieOfTheYearTop5()
        {
            string results = "";
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
