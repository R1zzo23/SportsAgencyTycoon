using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class Boxing
    {
        Agency myAgency;

        public Boxing(Agency agency)
        {
            myAgency = agency;
        }
        public string RunBoxingEvent(Event e, World world)
        {
            //string to print out results to user in newsLabel
            string results = "";
            Random rnd = new Random();

            //lists to run tournament 
            List<Boxer> loserList = new List<Boxer>();
            List<Boxer> winnerList = new List<Boxer>();
            List<Boxer> tempList = new List<Boxer>();
            List<Boxer> boxers = new List<Boxer>();
            List<Boxer> entrantList = new List<Boxer>();

            //grab list of boxers from world.WBA
            List<Player> playerList = world.WBA.PlayerList.OrderBy(o => o.WorldRanking).ToList();

            foreach (Boxer b in playerList)
            {
                b.KnockedOut = false;
                boxers.Add(b);
            }

            //create entrantList based on which event is taking place
            entrantList = DetermineEntrantList(boxers, e);
            foreach (Boxer b in entrantList) e.EntrantList.Add(b);

            for (int x = 0; x < entrantList.Count / 2; x++)
            {
                int winner;
                winner = SimBoxingMatch(entrantList[x * 2], entrantList[x * 2 + 1], rnd);
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
        private List<Boxer> DetermineEntrantList(List<Boxer> list, Event e)
        {
            List<Boxer> boxers = new List<Boxer>();

            if (e.Name == "Up-And-Comers Showcase")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++) 
                    boxers.Insert(0, list[list.Count - 1 - i]);
                    
            }
            else if (e.Name == "NYC Prize Fights")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    boxers.Add(list[i + 10]);
            }
            else if (e.Name == "Top Rank Boxing Premiere")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++)
                    boxers.Add(list[i]);
            }
            else if (e.Name == "King of the Ring")
            {
                for (var i = 0; i < e.NumberOfEntrants; i++) 
                    boxers.Add(list[i]);
            }

            return boxers;
        }
        private int SimBoxingMatch(Boxer b1, Boxer b2, Random rnd)
        {
            int winningBoxer = 0;
            int totalSkill = b1.CurrentSkill + b2.CurrentSkill;
            int b1ScoreCard = 0;
            int b2ScoreCard = 0;
            int totalRounds = 12;

            // right now fight goes distance every time
            // once I add KnockedOut logic, can re-introduce this while loop
            //while (!b1.KnockedOut && !b2.KnockedOut)
            //{
                for (int i = 0; i < totalRounds; i++)
                {
                    int luckyNumber = rnd.Next(1, totalSkill + 1);
                    if (luckyNumber <= b1.CurrentSkill)
                    {
                        b1ScoreCard += 10;
                        if (luckyNumber <= Math.Floor(b1.CurrentSkill * 0.2)) b2ScoreCard += 8;
                        else b2ScoreCard += 9;
                    }
                    else
                    {
                        b2ScoreCard += 10;
                        if (luckyNumber >= Math.Floor(totalSkill - b2.CurrentSkill * 0.2)) b1ScoreCard += 8;
                        else b1ScoreCard += 9;
                    }
                }
            //}

            if (b1ScoreCard > b2ScoreCard) winningBoxer = 1;
            else if (b2ScoreCard > b1ScoreCard) winningBoxer = 2;
            else if (b1ScoreCard == b2ScoreCard)
            {
                int tieBreaker = rnd.Next(1, totalSkill + 1);
                if (tieBreaker < b1.CurrentSkill) winningBoxer = 1;
                else winningBoxer = 2;
            }

            return winningBoxer;
        }
        private void AwardPayoutsAndUpdateRecords(Event e, List<Boxer> winnerList, List<Boxer> loserList, World world)
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
            UpdateWBAPlayerList(winnerList, loserList, world);
        }
        private void UpdateWBAPlayerList(List<Boxer> winnerList, List<Boxer> loserList, World world)
        {
            List<Boxer> resultList = new List<Boxer>();
            foreach (Boxer b in winnerList) resultList.Add(b);
            foreach (Boxer x in loserList) resultList.Add(x);

            for (var i = 0; i < resultList.Count; i++)
            {
                for (int j = 0; j < world.WBA.PlayerList.Count; j++)
                {
                    if (resultList[i].FirstName == world.WBA.PlayerList[j].FirstName && resultList[i].LastName == world.WBA.PlayerList[j].LastName && resultList[i].Age == world.WBA.PlayerList[j].Age)
                    {
                        world.WBA.PlayerList[j] = resultList[i];
                    }
                }
            }
        }
        public void CalculateWorldRankings(World world)
        {
            List<Boxer> boxers = new List<Boxer>();
            foreach (Boxer b in world.WBA.PlayerList) boxers.Add(b);

            //resultsList.OrderBy(o => o.PlayoffHoles).ThenBy(o => o.MadePlayoff).ThenBy(o => o.MadeCut).ThenBy(o => o.CurrentScore);
            //boxers = boxers.OrderBy(o => o.Losses).OrderByDescending(o => o.Wins).OrderByDescending(o => o.CurrentSkill).ToList();
            boxers = boxers.OrderByDescending(o => o.CurrentSkill).ThenBy(o => o.Wins).OrderBy(o => o.Losses).ToList();

            for (int i = 0; i < boxers.Count; i++)
            {
                boxers[i].WorldRanking = i + 1;
                for (int j = 0; j < world.WBA.PlayerList.Count - 1; j++)
                {
                    if (boxers[i].FirstName == world.WBA.PlayerList[j].FirstName && boxers[i].LastName == world.WBA.PlayerList[j].LastName && boxers[i].Age == world.WBA.PlayerList[j].Age)
                        world.WBA.PlayerList[j] = boxers[i];
                }
            }
        }
    }
}
