using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class MMA
    {
        Agency myAgency;

        public MMA(Agency agency)
        {
            myAgency = agency;
        }

        public string RunMMAEvent (Event e, World world)
        {
            //string to print out results to user in newsLabel
            string results = "";
            Random rnd = new Random();

            //lists to run tournament 
            List<MMAFighter> loserList = new List<MMAFighter>();
            List<MMAFighter> winnerList = new List<MMAFighter>();
            List<MMAFighter> tempList = new List<MMAFighter>();
            List<MMAFighter> fighters = new List<MMAFighter>();
            List<MMAFighter> entrantList = new List<MMAFighter>();

            //grab list of MMAFighters from world.WBA
            List<Player> playerList = world.UFC.PlayerList.OrderBy(o => o.WorldRanking).ToList();

            foreach (MMAFighter m in playerList)
            {
                m.KnockedOut = false;
                fighters.Add(m);
            }

            //create entrantList based on which event is taking place
            entrantList = DetermineEntrantList(fighters, e);
            foreach (MMAFighter m in entrantList) e.EntrantList.Add(m);

            for (int x = 0; x < entrantList.Count / 2; x++)
            {
                int winner;
                winner = SimMMAFight(entrantList[x * 2], entrantList[x * 2 + 1], rnd);
                if (winner == 1)
                {
                    winnerList.Add(entrantList[x * 2]);
                    loserList.Add(entrantList[x * 2 + 1]);
                }
                else if (winner == 2)
                {
                    winnerList.Add(entrantList[x * 2 + 1]);
                    loserList.Add(entrantList[x * 2]);
                }
            }
            results = "Results written in console.";
            AwardPayoutsAndUpdateRecords(e, winnerList, loserList, world);

            return results;
        }
        private List<MMAFighter> DetermineEntrantList(List<MMAFighter> list, Event e)
        {
            List<MMAFighter> fighters = new List<MMAFighter>();

            if (e.Name == "Fully Loaded")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    fighters.Insert(0, list[list.Count - 1 - i]);

            }
            else if (e.Name == "Bloodied and Bruised")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    fighters.Add(list[i + 10]);
            }
            else if (e.Name == "Elite Title Showdown")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    fighters.Add(list[i]);
            }
            else if (e.Name == "King Kong of the Octagon")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    fighters.Add(list[i]);
            }

            return fighters;
        }
        private int SimMMAFight(MMAFighter m1, MMAFighter m2, Random rnd)
        {
            int winningFighter = 0;
            int totalSkill = m1.CurrentSkill + m2.CurrentSkill;
            int m1ScoreCard = 0;
            int m2ScoreCard = 0;
            int totalRounds = 5;

            // right now fight goes distance every time
            // once I add KnockedOut logic, can re-introduce this while loop
            //while (!b1.KnockedOut && !b2.KnockedOut)
            //{
            for (int i = 0; i < totalRounds; i++)
            {
                int luckyNumber = rnd.Next(1, totalSkill + 1);
                if (luckyNumber <= m1.CurrentSkill)
                {
                    m1ScoreCard += 10;
                    if (luckyNumber <= Math.Floor(m1.CurrentSkill * 0.2)) m2ScoreCard += 8;
                    else m2ScoreCard += 9;
                }
                else
                {
                    m2ScoreCard += 10;
                    if (luckyNumber >= Math.Floor(totalSkill - m2.CurrentSkill * 0.2)) m1ScoreCard += 8;
                    else m1ScoreCard += 9;
                }
            }
            //}

            if (m1ScoreCard > m2ScoreCard) winningFighter = 1;
            else if (m2ScoreCard > m1ScoreCard) winningFighter = 2;
            else if (m1ScoreCard == m2ScoreCard)
            {
                int tieBreaker = rnd.Next(1, totalSkill + 1);
                if (tieBreaker < m1.CurrentSkill) winningFighter = 1;
                else winningFighter = 2;
            }

            return winningFighter;
        }
        private void AwardPayoutsAndUpdateRecords(Event e, List<MMAFighter> winnerList, List<MMAFighter> loserList, World world)
        {
            int purse = e.PrizePool;
            int winnersPurse = (int)Math.Floor(purse * 0.7);
            int topWinnerCash = (int)Math.Floor(winnersPurse * 0.35);
            int remainingWinnerCash = winnersPurse - topWinnerCash;
            int losersPurse = (int)Math.Floor(purse * 0.3);
            int topLoserCash = (int)Math.Floor(losersPurse * 0.35);
            int remainingLoserCash = losersPurse - topLoserCash;

            for (int i = 0; i < winnerList.Count; i++)
            {
                if (i == 0)
                {
                    winnerList[i].CareerEarnings += topWinnerCash;
                    if (winnerList[i].MemberOfAgency) myAgency.Money += Convert.ToInt32(topWinnerCash * (winnerList[i].Contract.AgentPercentage / 100));
                }

                else
                {
                    winnerList[i].CareerEarnings += (int)(remainingWinnerCash / (winnerList.Count - 1));
                    if (winnerList[i].MemberOfAgency) myAgency.Money += Convert.ToInt32(((int)(remainingWinnerCash / (winnerList.Count - 1))) * (winnerList[i].Contract.AgentPercentage / 100));
                }

                winnerList[i].Wins++;
            }
            for (int j = 0; j < loserList.Count; j++)
            {
                if (j == 0)
                {
                    loserList[j].CareerEarnings += topLoserCash;
                    if (loserList[j].MemberOfAgency) myAgency.Money += Convert.ToInt32(topLoserCash * (loserList[j].Contract.AgentPercentage / 100));
                }

                else
                {
                    loserList[j].CareerEarnings += (int)(remainingLoserCash / (loserList.Count - 1));
                    if (loserList[j].MemberOfAgency) myAgency.Money += Convert.ToInt32(((int)(remainingLoserCash / (loserList.Count - 1))) * (loserList[j].Contract.AgentPercentage / 100));
                }

                loserList[j].Losses++;
            }
            UpdateUFCPlayerList(winnerList, loserList, world);
        }
        private void UpdateUFCPlayerList(List<MMAFighter> winnerList, List<MMAFighter> loserList, World world)
        {
            List<MMAFighter> resultList = new List<MMAFighter>();
            foreach (MMAFighter b in winnerList) resultList.Add(b);
            foreach (MMAFighter x in loserList) resultList.Add(x);

            for (var i = 0; i < resultList.Count; i++)
            {
                for (int j = 0; j < world.UFC.PlayerList.Count; j++)
                {
                    if (resultList[i].FirstName == world.UFC.PlayerList[j].FirstName && resultList[i].LastName == world.UFC.PlayerList[j].LastName && resultList[i].Age == world.UFC.PlayerList[j].Age)
                    {
                        world.UFC.PlayerList[j] = resultList[i];
                    }
                }
            }
        }
        public void CalculateWorldRankings(World world)
        {
            List<MMAFighter> fighters = new List<MMAFighter>();
            foreach (MMAFighter b in world.UFC.PlayerList) fighters.Add(b);

            //resultsList.OrderBy(o => o.PlayoffHoles).ThenBy(o => o.MadePlayoff).ThenBy(o => o.MadeCut).ThenBy(o => o.CurrentScore);
            //fighters = fighters.OrderBy(o => o.Losses).OrderByDescending(o => o.Wins).OrderByDescending(o => o.CurrentSkill).ToList();
            fighters = fighters.OrderByDescending(o => o.CurrentSkill).ThenBy(o => o.Wins).OrderBy(o => o.Losses).ToList();

            for (int i = 0; i < fighters.Count; i++)
            {
                fighters[i].WorldRanking = i + 1;
                for (int j = 0; j < world.UFC.PlayerList.Count - 1; j++)
                {
                    if (fighters[i].FirstName == world.UFC.PlayerList[j].FirstName && fighters[i].LastName == world.UFC.PlayerList[j].LastName && fighters[i].Age == world.UFC.PlayerList[j].Age)
                        world.UFC.PlayerList[j] = fighters[i];
                }
            }
        }
    }
}
