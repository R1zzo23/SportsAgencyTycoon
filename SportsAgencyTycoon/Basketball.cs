using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Basketball
    {
        public Random rnd;
        public League NBA;
        public World World;
        public int index;
        public List<string> Conferences;
        public List<string> Divisions;

        public Basketball(Random r, World w, League l)
        {
            rnd = r;
            World = w;
            NBA = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            FillLists();
        }

        public void FillLists()
        {
            Conferences.Add("Eastern");
            Conferences.Add("Western");
            Divisions.Add("Atlantic");
            Divisions.Add("Central");
            Divisions.Add("Southeast");
            Divisions.Add("Northwest");
            Divisions.Add("Southwest");
            Divisions.Add("Pacific");
        }

        public void SimulateGames()
        {
            int gamesThisWeek = HowManyGamesThisWeek();
            for (int i = 0; i < gamesThisWeek; i++)
            {
                foreach (Team t in NBA.TeamList) t.PlayedGameThisCycle = false;
                for (int j = 0; j < NBA.TeamList.Count; j++)
                {
                    if (!NBA.TeamList[j].PlayedGameThisCycle)
                    {
                        int opponentIndex = j + index;
                        if (opponentIndex > NBA.TeamList.Count - 1) opponentIndex -= NBA.TeamList.Count;
                        if (NBA.TeamList[opponentIndex].PlayedGameThisCycle)
                        {
                            for (int x = opponentIndex + 1; x < NBA.TeamList.Count; x++)
                            {
                                if (!NBA.TeamList[x].PlayedGameThisCycle)
                                {
                                    opponentIndex = x;
                                    return;
                                }
                            }
                        }
                        Console.WriteLine("Team1 Index: " + j + " - Team2 Index: " + opponentIndex);
                        SimulateGame(NBA.TeamList[j], NBA.TeamList[opponentIndex]);
                    }
                }
                index++;
                if (index == 30) index = 1;
            }
        }

        public int HowManyGamesThisWeek()
        {
            int gamesThisWeek = 3;

            //only two games per week for first two weeks
            if ((World.MonthName == Months.October && World.WeekNumber == 4) ||
                (World.MonthName == Months.November && World.WeekNumber == 1))
            {
                gamesThisWeek = 2;
            }

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
    }
}
