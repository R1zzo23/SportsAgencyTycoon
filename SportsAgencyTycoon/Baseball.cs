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
            //FillLists();
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

                DeterminePlayoffField();
            }
            if (MLB.Playoffs)
            {
                if (MLB.WeekNumber == 26) SimulateWildCardRound();
                if (MLB.WeekNumber == 27 || MLB.WeekNumber == 28) SimulateRoundsTwoAndThree();
                if (MLB.WeekNumber == 29) SimulateWorldSeries();
            }
        }
        private void InitializeStats()
        {

        }
        private void UpdateStats()
        {

        }
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

        }
        private void SimulateWildCardRound()
        {

        }
        private void SimulateRoundsTwoAndThree()
        {

        }
        private void SimulateWorldSeries()
        {

        }
    }
}
