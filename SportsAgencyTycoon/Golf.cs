using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class Golf
    {
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

            /*
             *  - sort winners
             *  - if tied, play sudden death hole
             *  - AwardPayoutsAndTourPoints();
             *  - CalculatePlayerTourPoints();
             *  - UpdatePGAPlayerList();            
             * 
             */

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

            if (goodOrBadHole <= p.SkillLevel)
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
                int meltDown = rnd.Next(0, 30);
                if (meltDown >= 23) score += 2; //double bogey
                else score++; //bogey
            }

            return score;
        }

        private int CalculateCurrentTournamentScore(Golfer p)
        {
            int score = 0;
            foreach (int i in p.CurrentTournamentScores)
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
            int bestScore = list[0].CurrentScore;
            if (list[1].CurrentScore == bestScore) return true;
            else return false;
        }

        private void AwardPayoutsAndTourPoints(Event e, List<Player> list, World world)
        {

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
                    if (resultList[j].FirstName == world.PGA.PlayerList[i].FirstName && resultList[j].LastName == world.PGA.PlayerList[i].LastName && resultList[j].Age == world.ATP.PlayerList[i].Age)
                    {
                        world.PGA.PlayerList[i] = resultList[j];
                    }
                }
            }
        }
    }
}
