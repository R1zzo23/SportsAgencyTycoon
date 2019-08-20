using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class Golf
    {
        Agency myAgency;

        public Golf (Agency agency)
        {
            myAgency = agency;
        }

        public string RunTournament(Event e, World world)
        {
            //string to print out results to user in newsLabel
            string results = "";
            Random rnd = new Random();

            //lists to run tournament 
            List<Golfer> resultsList = new List<Golfer>();
            List<Golfer> tempList = new List<Golfer>();
            List<Golfer> madeCutList = new List<Golfer>();
            List<Golfer> listOfGolfers = new List<Golfer>();
            List<Golfer> playoffList = new List<Golfer>();

            //grab list of golfers from world.PGA
            List<Player> playerList = world.PGA.PlayerList.OrderBy(o => o.WorldRanking).ToList();


            foreach (Golfer g in playerList) listOfGolfers.Add(g);

            //add players to event and start fresh tournament scores
            for (int i = 0; i < e.NumberOfEntrants; i++)
            {
                listOfGolfers[i].CurrentTournamentScores.Clear();
                listOfGolfers[i].MadeCut = false;
                listOfGolfers[i].MadePlayoff = false;
                e.EntrantList.Add(listOfGolfers[i]);
            }

            //populate the EntrantList for the event
            foreach (Golfer p in e.EntrantList) tempList.Add(p);

            // every player in the tournament plays 2 rounds
            // scores get added to List<int> CurrentTournamentScores 
            // then added together for CurrentScore
            foreach (Golfer p in tempList)
            {
                p.CurrentTournamentScores.Add(PlayRound(p, rnd));
                p.CurrentTournamentScores.Add(PlayRound(p, rnd));
                p.CurrentScore = CalculateCurrentTournamentScore(p);
            }

            //create cut line based on median score of tournament after 2 rounds
            int cutLine = DecideCutLine(tempList);

            //remove golfers that didn't meet cut score
            foreach (Golfer p in tempList)
            {
                if (p.CurrentScore > cutLine)
                {
                    p.MadeCut = false;
                    p.MadePlayoff = false;
                    p.PlayoffHoles = 0;
                    resultsList.Add(p);
                }
                else
                {
                    p.MadeCut = true;
                    madeCutList.Add(p);
                }
            }
            //sort resultsList from best to worst for standings sake
            resultsList = resultsList.OrderBy(o => o.CurrentScore).ToList();

            //remaining golfers play 2 more rounds
            foreach (Golfer g in madeCutList)
            {
                g.CurrentTournamentScores.Add(PlayRound(g, rnd));
                g.CurrentTournamentScores.Add(PlayRound(g, rnd));
                g.CurrentScore = CalculateCurrentTournamentScore(g);
            }

            //sort madeCutList by score
            madeCutList = madeCutList.OrderBy(o => o.CurrentScore).ToList();
            tempList.Clear();
            foreach (Golfer g in madeCutList) tempList.Add(g);
            
            //check for tie
            while (IsThereATie(tempList))
            {
                playoffList.Clear();
                tempList = tempList.OrderBy(o => o.CurrentScore).ToList();
                //find the top score in the tournament
                int bestScore = tempList[0].CurrentScore;
                //add all golfers with matching score to playoffList
                for (int i = tempList.Count - 1; i > 0; i--)
                //foreach (Golfer g in tempList)
                {
                    //add Golfer to playoffList to keep playing
                    if (tempList[i].CurrentScore == bestScore)
                    {
                        tempList[i].MadePlayoff = true;
                        playoffList.Add(tempList[i]);
                    }
                    //or add them to resultsList because they are out
                    else resultsList.Insert(0, tempList[i]);
                    
                }
                //all golfers in playoffList play 1 hole at a time until someone wins
                foreach (Golfer g in playoffList)
                {
                    g.PlayoffHoles++;
                    g.CurrentScore += PlayHole(g, rnd);
                }
                playoffList = playoffList.OrderBy(o => o.CurrentScore).ToList();
                tempList.Clear();
                foreach (Golfer g in playoffList) tempList.Add(g);
            }
            //add all remaining golfers from tempList to resultsList
            for (int i = tempList.Count - 1; i > 0; i--) resultsList.Insert(0, tempList[i]);

            // Order list: Playoff Holes - Made Playoffs - Made Cut - Current Score
            resultsList.OrderBy(o => o.PlayoffHoles).ThenBy(o => o.MadePlayoff).ThenBy(o => o.MadeCut).ThenBy(o => o.CurrentScore);

            string tournamentResults = "";
            for (var i = 1; i < 10; i++)
            {
                tournamentResults += i + 1 + ") " + resultsList[i].FirstName + " " + resultsList[i].LastName + Environment.NewLine;
            }
            results = resultsList[0].FirstName + " " + resultsList[0].LastName
                + " is the " + e.Year.ToString() + " " + e.Name + " champion!"
                + Environment.NewLine + "Here are the rest of the top-10 finishers:"
                + Environment.NewLine + tournamentResults + Environment.NewLine;
            AwardPayoutsAndTourPoints(e, resultsList, world);

            return results;
        }

        private int PlayRound(Golfer p, Random rnd)
        {
            int score = 0;

            for (int i = 0; i < 18; i++)
            {
                score += PlayHole(p, rnd);
            }

            return score;
        }

        private int PlayHole(Golfer p, Random rnd)
        {
            int score = 0;
            int goodOrBadHole = rnd.Next(1, 101);

            if (goodOrBadHole <= p.CurrentSkill)
            {
                //good hole
                int birdieOrBetter = rnd.Next(0, 50);
                if (birdieOrBetter >= 47) score -= 2; //eagle
                else if (birdieOrBetter >= 30) score--; //birdie
                else score += 0; //par
            }
            else
            {
                //bad hole
                int meltDown = rnd.Next(0, 50);
                if (meltDown <= 37) score++; //bogey
                else if (meltDown <= 48) score += 2; //double bogey
                else score += 3; //tripe bogey
            }

            return score;
        }

        private int CalculateCurrentTournamentScore(Golfer p)
        {
            int score = 0;
            //foreach (int i in p.CurrentTournamentScores)
            for (int i = 0; i < p.CurrentTournamentScores.Count; i++)
            {
                score += p.CurrentTournamentScores[i];
            }
            return score;
        }

        private int DecideCutLine(List<Golfer> tempList)
        {
            tempList = tempList.OrderBy(o => o.CurrentScore).ToList();
            int medianIndex = tempList.Count / 2;
            return tempList[medianIndex].CurrentScore;
        }

        private bool IsThereATie(List<Golfer> list)
        {
            if (list.Count > 1)
            {
                int bestScore = list[0].CurrentScore;
                if (list[1].CurrentScore == bestScore) return true;
                else return false;
            }
            else return false;
        }

        private void AwardPayoutsAndTourPoints(Event e, List<Golfer> list, World world)
        {
            int purse = e.PrizePool;
            double[] MajorPoints = new double[] { 600, 330, 210, 150, 120, 110, 100, 94, 88, 82, 77, 72, 68, 64, 61, 59, 57, 55, 53, 51, 48.73, 46.47, 44.2, 41.93, 40.23, 38.53, 36.83, 35.13, 33.43, 31.73, 30.03, 28.33, 26.63, 24.93, 23.8, 22.67, 21.53, 20.4, 19.27, 18.13, 17, 15.87, 14.73, 13.6, 12.47, 11.9, 11.33, 10.77, 10.2, 9.63, 9.07, 8.5, 7.93, 7.37, 6.8, 6.57, 6.35, 6.12, 5.89, 5.67, 5.44, 5.21, 4.99, 4.76, 4.53, 4.31, 4.08, 3.85, 3.63, 3.4, 3.29, 3.17, 3.06, 2.95, 2.83, 2.72, 2.61, 2.49, 2.38, 2.27, 2.15, 2.04, 1.93, 1.81, 1.7 };
            double[] PGATourEventPoints = new double[] { 500, 300, 190, 135, 110, 100, 90, 85, 80, 75, 70, 65, 60, 57, 55, 53, 51, 49, 47, 45, 43, 41, 39, 37, 35.5, 34, 32.5, 31, 29.5, 28, 26.5, 25, 23.5, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10.5, 10, 9.5, 9, 8.5, 8, 7.5, 7, 6.5, 6, 5.8, 5.6, 5.4, 5.2, 5, 4.8, 4.6, 4.4, 4.2, 4, 3.8, 3.6, 3.4, 3.2, 3, 2.9, 2.8, 2.7, 2.6, 2.5, 2.4, 2.3, 2.2, 2.1, 2, 1.9, 1.8, 1.7, 1.6, 1.5 };
            double[] PayoutPercentages = new double[] { 18, 10.8, 6.8, 4.8, 4, 3.6, 3.35, 3.1, 2.9, 2.7, 2.5, 2.3, 2.1, 1.9, 1.8, 1.7, 1.6, 1.5, 1.4, 1.3, 1.2, 1.112, 1.04, 0.96, 0.88, 0.8, 0.77, 0.74, 0.71, 0.68, 0.65, 0.59, 0.565, 0.54, 0.515, 0.49, 0.47, 0.45, 0.43, 0.41, 0.39, 0.37, 0.35, 0.33, 0.31, 0.29, 0.274, 0.26, 0.252, 0.246, 0.24, 0.236, 0.232, 0.23, 0.228, 0.226, 0.224, 0.222, 0.22, 0.218, 0.216, 0.214, 0.212, 0.21, 0.208, 0.206, 0.204, 0.202, 0.2 };
            double[] AwardedPoints;
            EventType eventType = e.TypeOfEvent;

            List<Golfer> golfers = new List<Golfer>();
            foreach (Golfer g in list) golfers.Add(g);

            if (eventType == EventType.Major) AwardedPoints = MajorPoints;
            else AwardedPoints = PGATourEventPoints;

            for (var i = 0; i < golfers.Count; i++)
            {
                int p = i;
                if (i > (AwardedPoints.Length - 1)) p = AwardedPoints.Length - 1;

                if (i == 0)
                {
                    golfers[i].TournamentWins++;
                    if (e.TypeOfEvent == EventType.Major) golfers[i].Majors++;
                }

                if (i <= 9) golfers[i].TopTenFinishes++;
                if (golfers[i].MadeCut)
                {
                    golfers[i].CutsMade++;
                    if (i > PayoutPercentages.GetLength(0) - 1)
                    {
                        golfers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * (PayoutPercentages[PayoutPercentages.GetLength(0) - 1] / 100)));
                    }
                    else golfers[i].CareerEarnings += Convert.ToInt32(Math.Floor(purse * (PayoutPercentages[i] / 100)));

                    // if golfer is member of agency, add % of winnings to agency.Money
                    if (golfers[i].MemberOfAgency)
                    {
                        if (i > PayoutPercentages.GetLength(0) - 1)
                        {
                            myAgency.Money += Convert.ToInt32(Math.Floor(purse * (PayoutPercentages[PayoutPercentages.GetLength(0) - 1] / 100)));
                        }
                        else myAgency.Money += Convert.ToInt32(Math.Floor(purse * (PayoutPercentages[i] / 100)) * (golfers[i].Contract.AgentPercentage / 100));
                    }
                }
                golfers[i].EventsPlayed++;
                golfers[i].TourPointsList.Add(AwardedPoints[p]);
                golfers[i].TourPoints = CalculatePlayerTourPoints(golfers[i], world);
            }
            UpdatePGAPlayerList(golfers, world);
        }

        public int CalculatePlayerTourPoints(Golfer g, World world)
        {
            int tourPoints = 0;
            if (g.TourPointsList.Count > world.ATP.EventList.Count) g.TourPointsList.RemoveAt(0);

            foreach (int i in g.TourPointsList) tourPoints += i;

            return tourPoints;
        }

        private void UpdatePGAPlayerList(List<Golfer> resultList, World world)
        {
            resultList = resultList.OrderByDescending(o => o.TourPoints).ToList();
            for (var j = 0; j < resultList.Count; j++)
            {
                resultList[j].WorldRanking = j + 1;
                for (var i = 0; i < world.PGA.PlayerList.Count; i++)
                {
                    if (resultList[j].FirstName == world.PGA.PlayerList[i].FirstName && resultList[j].LastName == world.PGA.PlayerList[i].LastName && resultList[j].Age == world.PGA.PlayerList[i].Age)
                    {
                        world.PGA.PlayerList[i] = resultList[j];
                    }
                }
            }
        }
    }
}
