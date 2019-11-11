using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Baseball
    {
        public MainForm mainForm;
        public Random rnd;
        public League MLB;
        public World World;
        public int index;
        public int losingIndex;
        public List<string> Conferences;
        public List<string> Divisions;
        public List<Team> AmericanLeague = new List<Team>();
        public List<Team> NationalLeague = new List<Team>();
        public List<Team> ALEast = new List<Team>();
        public List<Team> ALCentral = new List<Team>();
        public List<Team> ALWest = new List<Team>();
        public List<Team> NLEast = new List<Team>();
        public List<Team> NLCentral = new List<Team>();
        public List<Team> NLWest = new List<Team>();
        public List<Team> ALPlayoffs = new List<Team>();
        public List<Team> NLPlayoffs = new List<Team>();
        public List<int> ALLoserIndex = new List<int>();
        public List<int> NLLoserIndex = new List<int>();
        public Baseball(MainForm mf, Random r, World w, League l)
        {
            mainForm = mf;
            rnd = r;
            World = w;
            MLB = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            FillLists();
        }
        public void FillLists()
        {
            foreach (Team t in MLB.TeamList)
                if (t.Conference == "AL")
                {
                    AmericanLeague.Add(t);
                    if (t.Division == "East") ALEast.Add(t);
                    else if (t.Division == "Central") ALCentral.Add(t);
                    else ALWest.Add(t);
                }
                else
                {
                    NationalLeague.Add(t);
                    if (t.Division == "East") NLEast.Add(t);
                    else if (t.Division == "Central") NLCentral.Add(t);
                    else NLWest.Add(t);
                }
        }
        public void SimWeek()
        {
            if (MLB.WeekNumber == 0)
                InitializeStats();
            MLB.WeekNumber++;
            if (!MLB.Playoffs)
            {
                SimulateGames();
                UpdateStats();
            }
            if (MLB.WeekNumber == 25)
            {
                MLB.Playoffs = true;
                foreach (Team t in World.MLB.TeamList)
                {
                    foreach (BaseballPlayer p in t.Roster)
                    {
                        //hand out award scores
                    }
                }
                //mainForm.newsLabel.Text = DisplayDPOYTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                //mainForm.newsLabel.Text = DisplayOPOYTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                //mainForm.newsLabel.Text = DisplayMVPTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                //mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;

                mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
            }
            if (MLB.Playoffs)
            {
                if (MLB.WeekNumber == 26) mainForm.newsLabel.Text = SimulateWildCardRound() + Environment.NewLine + mainForm.newsLabel.Text;
                if (MLB.WeekNumber == 27 || MLB.WeekNumber == 28) mainForm.newsLabel.Text = SimulateRoundsTwoAndThree() + Environment.NewLine + mainForm.newsLabel.Text;
                if (MLB.WeekNumber == 29) mainForm.newsLabel.Text = SimulateWorldSeries() + Environment.NewLine + mainForm.newsLabel.Text;
            }
        }
        #region Statistics
        private void InitializeStats()
        {
            foreach (Team t in MLB.TeamList)
                foreach (BaseballPlayer p in t.Roster)
                    if (p.Position != Position.SP || p.Position != Position.RP)
                        InitializeAverage(p);
                    else InitializeERA(p);
        }
        private void InitializeAverage(BaseballPlayer p)
        {
            if (p.CurrentSkill >= 70) p.Average = Convert.ToDouble(rnd.Next(325, 376)) / 1000;
            else if (p.CurrentSkill >= 50) p.Average = Convert.ToDouble(rnd.Next(290, 325)) / 1000;
            else if (p.CurrentSkill >= 40) p.Average = Convert.ToDouble(rnd.Next(265, 290)) / 1000;
            else if (p.CurrentSkill >= 30) p.Average = Convert.ToDouble(rnd.Next(240, 265)) / 1000;
            else if (p.CurrentSkill >= 20) p.Average = Convert.ToDouble(rnd.Next(195, 240)) / 1000;
            else p.Average = Convert.ToDouble(rnd.Next(180, 195)) / 1000;
        }
        private void InitializeERA(BaseballPlayer p)
        {
            if (p.CurrentSkill >= 70) p.ERA = Convert.ToDouble(rnd.Next(100, 275)) / 100;
            else if (p.CurrentSkill >= 50) p.ERA = Convert.ToDouble(rnd.Next(275, 350)) / 100;
            else if (p.CurrentSkill >= 40) p.ERA = Convert.ToDouble(rnd.Next(325, 400)) / 100;
            else if (p.CurrentSkill >= 30) p.ERA = Convert.ToDouble(rnd.Next(375, 450)) / 100;
            else if (p.CurrentSkill >= 20) p.ERA = Convert.ToDouble(rnd.Next(450, 700)) / 100;
            else p.ERA = Convert.ToDouble(rnd.Next(550, 800)) / 100;
        }
        private void UpdateStats()
        {
            foreach (Team t in MLB.TeamList)
                foreach (BaseballPlayer p in t.Roster)
                    if (p.Position != Position.SP || p.Position != Position.RP)
                    {
                        UpdateAverage(p);
                        DetermineRBIs(p);
                    }
                    else
                    {
                        UpdateERA(p);
                        DetermineWinsAndLosses(p);
                        if (p.Position == Position.RP && p.DepthChart == 1)
                            DetermineSaves(p);
                    }
        }
        private void UpdateAverage(BaseballPlayer p)
        {
            double change = Convert.ToDouble(rnd.Next(-50, 51)) / 1000;
            p.Average *= (1 + change);
        }
        private void UpdateERA(BaseballPlayer p)
        {
            double change = Convert.ToDouble(rnd.Next(-50, 51)) / 1000;
            p.ERA *= (1 + change);
        }
        private void DetermineRBIs(BaseballPlayer p)
        {
            int RBI = 0;
            int rolls = 0;

            if (p.CurrentSkill >= 70) rolls = 5;
            else if (p.CurrentSkill >= 50) rolls = 4;
            else if (p.CurrentSkill >= 40) rolls = 3;
            else if (p.CurrentSkill >= 30) rolls = 2;
            else if (p.CurrentSkill >= 20) rolls = 1;

            for (int i = 0; i < rolls; i++)
            {
                int numberOnDice = DiceRoll();
                if (numberOnDice > 8) RBI++;
            }

            p.RBI += RBI;
            if (RBI > 0) DetermineHomeRuns(p, RBI);
        }
        private void DetermineHomeRuns(BaseballPlayer p, int RBI)
        {
            if (RBI % 2 == 1 && RBI >= 3) RBI -= 1;
            for (int i = 0; i < RBI / 2, i++)
            {
                int numberOnDice = DiceRoll();
                int playerNumber = 0;

                if (p.CurrentSkill >= 70) playerNumber = 8;
                else if (p.CurrentSkill >= 50) playerNumber = 9;
                else if (p.CurrentSkill >= 40) playerNumber = 10;
                else if (p.CurrentSkill >= 30) playerNumber = 11;
                else if (p.CurrentSkill >= 20) playerNumber = 12;

                if (numberOnDice >= playerNumber) p.HomeRuns++;
            }
        }
        private void DetermineWinsAndLosses(BaseballPlayer p)
        {
            int rolls = 0;
            bool didPitcherWin = false;
            if (p.CurrentSkill >= 70) rolls = 5;
            else if (p.CurrentSkill >= 50) rolls = 4;
            else if (p.CurrentSkill >= 40) rolls = 3;
            else if (p.CurrentSkill >= 30) rolls = 2;
            else if (p.CurrentSkill >= 20) rolls = 1;

            for (int i = 0; i < rolls; i++)
            {
                int numberOnDice = DiceRoll();
                if (numberOnDice > 8)
                {
                    didPitcherWin = true;
                    break;
                }
            }

            if (didPitcherWin) p.Wins++;
            else p.Losses++;
        }
        private void DetermineSaves(BaseballPlayer p)
        {
            int chances = 0;

            if (p.Team.TitleConteder > 70) chances = 5;
            else if (p.Team.TitleConteder > 60) chances = 4;
            else if (p.Team.TitleConteder > 50) chances = 3;
            else chances = 2;

            for (int i = 0; i < chances; i++)
            {
                int numberOnDice = DiceRoll();
                if (numberOnDice > 7) p.Saves++;
            }
        }
        #endregion
        #region Simulation
        private void SimulateGames()
        {
            int gamesThisWeek = HowManyGamesThisWeek();
            for (int i = 0; i < gamesThisWeek; i++)
            {
                var indexList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

                for (int j = 0; j < MLB.TeamList.Count / 2; j++)
                {
                    int opponentIndex = rnd.Next(1, indexList.Count);
                    Console.WriteLine("Team1 Index: " + MLB.TeamList[indexList[0]] + " - Team2 Index: " + MLB.TeamList[indexList[opponentIndex]]);
                    SimulateGame(MLB.TeamList[indexList[0]], MLB.TeamList[indexList[opponentIndex]]);
                    indexList.RemoveAt(opponentIndex);
                    indexList.RemoveAt(0);
                }
                index++;
                if (index == 30) index = 1;
            }
        }
        private int HowManyGamesThisWeek()
        {
            int gamesThisWeek = 6;

            //only two games per week for first two weeks
            if (MLB.WeekNumber < 13)
                gamesThisWeek = 7;

            return gamesThisWeek;
        }
        private void SimulateGame(Team team1, Team team2)
        {
            int totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
            int winningNumber = rnd.Next(1, totalNumber);
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
        private string DeterminePlayoffField()
        {
            string playoffSeedings = "";
            AmericanLeague.Clear();
            NationalLeague.Clear();
            ALEast.Clear();
            ALCentral.Clear();
            ALWest.Clear();
            NLEast.Clear();
            NLCentral.Clear();
            NLWest.Clear();
            ALPlayoffs.Clear();
            NLPlayoffs.Clear();

            foreach (Team t in World.MLB.TeamList)
            {
                if (t.Conference == "AL") AmericanLeague.Add(t);
                else NationalLeague.Add(t);
            }

            AmericanLeague = AmericanLeague.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NationalLeague = NationalLeague.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();

            int alEastIndex = AmericanLeague.FindIndex(t => t.Division == "East");
            ALPlayoffs.Add(AmericanLeague[alEastIndex]);
            AmericanLeague[alEastIndex].Awards.Add(new Award(World.Year, AmericanLeague[alEastIndex].Division + " Division Champs"));
            AmericanLeague.RemoveAt(alEastIndex);

            int alCentralIndex = AmericanLeague.FindIndex(t => t.Division == "Central");
            ALPlayoffs.Add(AmericanLeague[alCentralIndex]);
            AmericanLeague[alCentralIndex].Awards.Add(new Award(World.Year, AmericanLeague[alCentralIndex].Division + " Division Champs"));
            AmericanLeague.RemoveAt(alCentralIndex);

            int alWestIndex = AmericanLeague.FindIndex(t => t.Division == "West");
            ALPlayoffs.Add(AmericanLeague[alWestIndex]);
            AmericanLeague[alWestIndex].Awards.Add(new Award(World.Year, AmericanLeague[alWestIndex].Division + " Division Champs"));
            AmericanLeague.RemoveAt(alWestIndex);

            int nlEastIndex = NationalLeague.FindIndex(t => t.Division == "East");
            NLPlayoffs.Add(NationalLeague[nlEastIndex]);
            NationalLeague[nlEastIndex].Awards.Add(new Award(World.Year, NationalLeague[nlEastIndex].Division + " Division Champs"));
            NationalLeague.RemoveAt(nlEastIndex);

            int nlCentralIndex = NationalLeague.FindIndex(t => t.Division == "Central");
            NLPlayoffs.Add(NationalLeague[nlCentralIndex]);
            NationalLeague[nlCentralIndex].Awards.Add(new Award(World.Year, NationalLeague[nlCentralIndex].Division + " Division Champs"));
            NationalLeague.RemoveAt(nlCentralIndex);

            int nlWestIndex = NationalLeague.FindIndex(t => t.Division == "West");
            NLPlayoffs.Add(NationalLeague[nlWestIndex]);
            NationalLeague[nlWestIndex].Awards.Add(new Award(World.Year, NationalLeague[nlWestIndex].Division + " Division Champs"));
            NationalLeague.RemoveAt(nlWestIndex);

            ALPlayoffs = ALPlayoffs.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NLPlayoffs = NLPlayoffs.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();

            //add both Wild Card teams for each league
            for (int i = 0; i < 2; i++)
            {
                ALPlayoffs.Add(AmericanLeague[0]);
                AmericanLeague.RemoveAt(0);
                NLPlayoffs.Add(NationalLeague[0]);
                NationalLeague.RemoveAt(0);
            }

            playoffSeedings += "American League Seeds:" + Environment.NewLine;
            for (int i = 0; i < ALPlayoffs.Count; i++)
            {
                playoffSeedings += i+1 + ") " + ALPlayoffs[i].City + " " + ALPlayoffs[i].Mascot + Environment.NewLine;
            }
            playoffSeedings += "National League Seeds:" + Environment.NewLine;
            for (int i = 0; i < NLPlayoffs.Count; i++)
            {
                playoffSeedings += i+1 + ") " + NLPlayoffs[i].City + " " + NLPlayoffs[i].Mascot + Environment.NewLine;
            }

            return playoffSeedings;
        }
        private string SimulateWildCardRound()
        {
            string results = "";

            results += SimulateSeries(ALPlayoffs[3], ALPlayoffs[4], 3, 4) + Environment.NewLine;
            ALLoserIndex.Add(losingIndex);
            results += SimulateSeries(NLPlayoffs[3], NLPlayoffs[4], 3, 4);
            NLLoserIndex.Add(losingIndex);

            RemoveLosingTeamsFromPlayoffs();

            return results;
        }
        public void RemoveLosingTeamsFromPlayoffs()
        {
            ALLoserIndex = ALLoserIndex.OrderByDescending(i => i).ToList();
            NLLoserIndex = NLLoserIndex.OrderByDescending(i => i).ToList();

            foreach (int i in ALLoserIndex) ALPlayoffs.RemoveAt(i);
            foreach (int i in NLLoserIndex) NLPlayoffs.RemoveAt(i);
        }
        private string SimulateRoundsTwoAndThree()
        {
            string results = "";
            ALLoserIndex.Clear();
            NLLoserIndex.Clear();

            for (int i = 0; i < ALPlayoffs.Count / 2; i++)
            {
                results += SimulateSeries(ALPlayoffs[i], ALPlayoffs[ALPlayoffs.Count - 1 - i], i, ALPlayoffs.Count - 1 - i) + Environment.NewLine;
                ALLoserIndex.Add(losingIndex);
                results += SimulateSeries(NLPlayoffs[i], NLPlayoffs[NLPlayoffs.Count - 1 - i], i, NLPlayoffs.Count - 1 - i) + Environment.NewLine;
                NLLoserIndex.Add(losingIndex);
            }

            RemoveLosingTeamsFromPlayoffs();

            return results;
        }
        private string SimulateWorldSeries()
        {
            string results = "";

            ALPlayoffs[0].Awards.Add(new Award(World.Year, "American League Champs"));
            NLPlayoffs[0].Awards.Add(new Award(World.Year, "National League Champs"));

            results = SimulateSeries(ALPlayoffs[0], NLPlayoffs[0], 0, 1);
            if (losingIndex == 0)
            {
                results = "The " + NLPlayoffs[0].City + " " + NLPlayoffs[0].Mascot + " are the " + World.Year + " MLB champions!" + Environment.NewLine + results;
                NLPlayoffs[0].Awards.Add(new Award(World.Year, "World Series Champions"));
            }

            else
            {
                results = "The " + ALPlayoffs[0].City + " " + ALPlayoffs[0].Mascot + " are the " + World.Year + " MLB champions!" + Environment.NewLine + results;
                ALPlayoffs[0].Awards.Add(new Award(World.Year, "World Series Champions"));
            }

            return results;
        }
        public string SimulateSeries(Team team1, Team team2, int teamIndex1, int teamIndex2)
        {
            string seriesResult = "";
            int winsTeam1 = 0;
            int winsTeam2 = 0;

            int winsNeeded = 0;
            if (MLB.WeekNumber == 26) winsNeeded = 1;
            else if (MLB.WeekNumber == 27) winsNeeded = 3;
            else if (MLB.WeekNumber == 28 || MLB.WeekNumber == 29) winsNeeded = 4;

            while (winsTeam1 < winsNeeded && winsTeam2 < winsNeeded)
            {
                int result;
                result = SimulatePlayoffGame(team1, team2);
                if (result == 1) winsTeam1++;
                else winsTeam2++;
            }

            if (winsTeam1 == winsNeeded)
            {
                seriesResult = team1.Abbreviation + " defeats " + team2.Abbreviation + " in " + (winsTeam1 + winsTeam2) + " games.";
                losingIndex = teamIndex2;
            }
            else
            {
                seriesResult = team2.Abbreviation + " defeats " + team1.Abbreviation + " in " + (winsTeam1 + winsTeam2) + " games.";
                losingIndex = teamIndex1;
            }

            return seriesResult;
        }
        public int SimulatePlayoffGame(Team team1, Team team2)
        {
            int totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
            int winningNumber = rnd.Next(1, totalNumber);
            if (winningNumber <= team1.TitleConteder)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        #endregion
        #region Awards

        #endregion
        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
