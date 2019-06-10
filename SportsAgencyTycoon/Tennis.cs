using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class Tennis
    {
        public string RunTournament(Event e, World world)
        {
            //string to print out results to user in newsLabel
            string results = "";
            Random rnd = new Random();
            int x = 0;
            List<Player> resultsList = new List<Player>();
            List<Player> tempList = new List<Player>();
            List<Player> winnerList = new List<Player>();
            List<Player> playerList = world.ATP.PlayerList.OrderBy(o => o.WorldRanking).ToList();
            List<TennisPlayer> listOfTennisPlayers = new List<TennisPlayer>();
            foreach (TennisPlayer t in playerList) listOfTennisPlayers.Add(t);
            //add players to event
            for (int i = 0; i < e.NumberOfEntrants; i++)
            {
                e.EntrantList.Add(listOfTennisPlayers[i]);
            }

            foreach (Player p in e.EntrantList) tempList.Add(p);

            while (resultsList.Count < e.NumberOfEntrants - 1)
            {
                winnerList.Clear();
                for (x = 0; x < (tempList.Count / 2); x++)
                {
                    int result;
                    result = PlayTennisMatch(tempList[x], tempList[tempList.Count - x - 1], rnd);
                    if (result == 1)
                    {
                        winnerList.Add(tempList[x]);
                        resultsList.Insert(0, tempList[tempList.Count - x - 1]);
                    }
                    else if (result == 2)
                    {
                        winnerList.Add(tempList[tempList.Count - x - 1]);
                        resultsList.Insert(0, tempList[x]);
                    }
                }
                tempList.Clear();
                foreach (Player p in winnerList) tempList.Add(p);
            }
            resultsList.Insert(0, winnerList[0]);
            string tournamentResults = "";
            for (var i = 1; i < 8; i++)
            {
                tournamentResults += i + 1 + ") " + resultsList[i].FirstName + " " + resultsList[i].LastName + Environment.NewLine;
            }
            results = resultsList[0].FirstName + " " + resultsList[0].LastName
                + " is the " + e.Year.ToString() + " " + e.Name.ToString() + " champion!"
                + Environment.NewLine + "Here are the rest of the quarterfinalists:"
                + Environment.NewLine + tournamentResults + Environment.NewLine;
            AwardPayoutsAndTourPoints(e, resultsList, world);

            return results;
        }
        private int PlayTennisMatch(Player p1, Player p2, Random rnd)
        {
            int winningPlayer = 0;
            TennisPlayer t1 = (TennisPlayer)p1;
            TennisPlayer t2 = (TennisPlayer)p2;
            //Console.WriteLine("{0} {1} ({2}) vs. {3} {4} ({5}).", t1.FirstName, t1.LastName, t1.SkillLevel, t2.FirstName, t2.LastName, t2.SkillLevel);

            int t1SetsWon = 0;
            int t2SetsWon = 0;

            int totalSkill = t1.SkillLevel + t2.SkillLevel;

            while (t1SetsWon < 4 && t2SetsWon < 4)
            {
                int luckyNumber = rnd.Next(0, totalSkill);
                if (luckyNumber <= t1.SkillLevel) t1SetsWon++;
                else t2SetsWon++;
            }
            if (t1SetsWon == 4)
            {
                //Console.WriteLine("{0} {1} defeats {2} {3} {4} sets to {5}.", t1.FirstName, t1.LastName, t2.FirstName, t2.LastName, t1SetsWon, t2SetsWon);
                winningPlayer = 1;
            }
            else if (t2SetsWon == 4)
            {
                //Console.WriteLine("{0} {1} defeats {2} {3} {4} sets to {5}.", t2.FirstName, t2.LastName, t1.FirstName, t1.LastName, t2SetsWon, t1SetsWon);
                winningPlayer = 2;
            }
            return winningPlayer;
        }

        private void AwardPayoutsAndTourPoints(Event e, List<Player> list, World world)
        {
            int purse = e.PrizePool;
            int[] GrandSlamPoints = new int[] { 2000, 1200, 720, 360, 180, 90, 45, 10 };
            int[] Masters1000Points = new int[] { 1000, 600, 360, 180, 90, 45, 22, 5 };
            int[] AwardedPoints;
            EventType eventType = e.TypeOfEvent;

            List<TennisPlayer> tennisPlayers = new List<TennisPlayer>();
            foreach (TennisPlayer p in list) tennisPlayers.Add(p);

            if (eventType == EventType.GrandSlam) AwardedPoints = GrandSlamPoints;
            else AwardedPoints = Masters1000Points;

            for (var i = 0; i < tennisPlayers.Count; i++)
            {
                //champ gets 17.06%
                if (i < 1)
                {
                    if (eventType == EventType.GrandSlam) tennisPlayers[i].GrandSlams++;
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .1706));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[0]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                    tennisPlayers[i].TournamentWins++;
                    tennisPlayers[i].QuarterFinals++;
                }
                //runner-up: 9.21%
                else if (i < 2)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0921));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[1]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                    tennisPlayers[i].QuarterFinals++;
                }
                //Semi-Finalists Losers: 4.66%
                else if (i < 4)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0466));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[2]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                    tennisPlayers[i].QuarterFinals++;
                }
                //Quarter-Finalists Losers: 2.56%
                else if (i < 8)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0256));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[3]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                    tennisPlayers[i].QuarterFinals++;
                }
                //4th Round Losers: 1.40%
                else if (i < 16)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0140));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[4]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                }
                //3rd Round Losers: 0.79%
                else if (i < 32)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0079));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[5]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                }
                //2nd Round Losers: 0.46%
                else if (i < 64)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0046));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[6]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                }
                //1st Round Losers: 0.25%
                else if (i < 128)
                {
                    tennisPlayers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * .0025));
                    tennisPlayers[i].TourPointsList.Add(AwardedPoints[7]);
                    tennisPlayers[i].TourPoints = CalculatePlayerTourPoints(tennisPlayers[i], world);
                }
            }
            UpdateATPPlayerList(tennisPlayers, world);
        }
        public int CalculatePlayerTourPoints(TennisPlayer p, World world)
        {
            int tourPoints = 0;
            if (p.TourPointsList.Count > world.ATP.EventList.Count) p.TourPointsList.RemoveAt(0);

            foreach (int i in p.TourPointsList) tourPoints += i;

            return tourPoints;
        }

        private void UpdateATPPlayerList(List<TennisPlayer> resultList, World world)
        {
            resultList = resultList.OrderByDescending(o => o.TourPoints).ToList();
            for (var j = 0; j < resultList.Count; j++)
            {
                resultList[j].WorldRanking = j + 1;
                for (var i = 0; i < world.ATP.PlayerList.Count; i++)
                {
                    if (resultList[j].FirstName == world.ATP.PlayerList[i].FirstName && resultList[j].LastName == world.ATP.PlayerList[i].LastName && resultList[j].Age == world.ATP.PlayerList[i].Age)
                    {
                        world.ATP.PlayerList[i] = resultList[j];
                    }
                }
            }
        }
    }
}
