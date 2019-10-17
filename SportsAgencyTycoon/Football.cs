using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Football
    {
        public MainForm mainForm;
        public Random rnd;
        public League NFL;
        public World World;
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

        public List<FootballPlayer> QBs = new List<FootballPlayer>();
        public List<FootballPlayer> WRandTE = new List<FootballPlayer>();
        public List<FootballPlayer> RBandFB = new List<FootballPlayer>();
        public List<FootballPlayer> OL = new List<FootballPlayer>();
        public List<FootballPlayer> DL = new List<FootballPlayer>();
        public List<FootballPlayer> LB = new List<FootballPlayer>();
        public List<FootballPlayer> Secondary = new List<FootballPlayer>();
        public List<FootballPlayer> Kicker = new List<FootballPlayer>();
        public List<FootballPlayer> Punter = new List<FootballPlayer>();

        public List<FootballPlayer> DPOYCandidates = new List<FootballPlayer>();
        public int teamPassingYards = 0;
        public int teamPassingTDs = 0;
        public int teamPassingYardsUsed = 0;
        public int teamCarries = 0;
        public int teamRushingYards = 0;
        public int teamRushingTDs = 0;
        public int fgAttempts = 0;
        public int totalPossessions = 0;


        public Football(MainForm mf, Random r, World w, League l)
        {
            mainForm = mf;
            rnd = r;
            World = w;
            NFL = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            FillLists();
        }

        public void FillLists()
        {
            Conferences.Add("AFC");
            Conferences.Add("NFC");
            Divisions.Add("AFC East");
            Divisions.Add("AFC North");
            Divisions.Add("AFC South");
            Divisions.Add("AFC West");
            Divisions.Add("NFC East");
            Divisions.Add("NFC North");
            Divisions.Add("NFC South");
            Divisions.Add("NFC West");
        }

        public void SimWeek()
        {
            if (!NFL.Playoffs)
            {
                SimulateGames();
                DetermineStats();
            }
        }
        public void GroupPlayersByPosition(Team t)
        {
            QBs.Clear();
            WRandTE.Clear();
            RBandFB.Clear();
            OL.Clear();
            DL.Clear();
            LB.Clear();
            Secondary.Clear();
            Kicker.Clear();
            Punter.Clear();

            foreach (FootballPlayer p in t.Roster)
            {
                if (p.Position == Position.QB)
                {
                    QBs.Add(p);
                }
                else if (p.Position == Position.RB || p.Position == Position.FB)
                {
                    RBandFB.Add(p);
                }
                else if (p.Position == Position.WR || p.Position == Position.TE)
                {
                    WRandTE.Add(p);
                }
                else if (p.Position == Position.OG || p.Position == Position.OT || p.Position == Position.C)
                {
                    OL.Add(p);
                }
                else if (p.Position == Position.DT || p.Position == Position.DE)
                {
                    DL.Add(p);
                }
                else if (p.Position == Position.LB)
                {
                    LB.Add(p);
                }
                else if (p.Position == Position.CB || p.Position == Position.FS || p.Position == Position.SS)
                {
                    Secondary.Add(p);
                }
                else if (p.Position == Position.K)
                {
                    Kicker.Add(p);
                }
                else if (p.Position == Position.P)
                {
                    Punter.Add(p);
                }
            }
        }
        #region DetermineStats
        public void DetermineStats()
        {
            foreach (Team t in NFL.TeamList)
            {
                teamPassingYards = 0;
                teamPassingTDs = 0;
                teamPassingYardsUsed = 0;
                teamCarries = 0;
                teamRushingYards = 0;
                teamRushingTDs = 0;
                fgAttempts = 0;
                totalPossessions = rnd.Next(11, 15);

                GroupPlayersByPosition(t);

                foreach (FootballPlayer p in QBs)
                {
                    DeterminePassingTD(p);
                    DeterminePassingYards(p);
                    DetermineInterceptions(p);
                }

                foreach (FootballPlayer p in RBandFB)
                {
                    DetermineCarries(p);
                }

                if (teamPassingTDs > 0)
                    DetermineReceivingTDs(WRandTE);

                DetermineReceivingYards(WRandTE);

                totalPossessions = totalPossessions - (teamPassingTDs + teamRushingTDs);

                int totalOLSkill = 0;
                foreach (FootballPlayer p in OL)
                {
                    if (p.IsStarter)
                    {
                        totalOLSkill += p.CurrentSkill;
                    }
                }
                double averageOLSkill = Convert.ToDouble(totalOLSkill) / 5.0;
                DeterminePancakeBlocks(OL, averageOLSkill);
                DetermineSacksAllowed(OL, averageOLSkill);
                DetermineOLineYPC(OL);

                foreach (FootballPlayer p in DL)
                    DetermineTackles(p);
                foreach (FootballPlayer p in LB)
                    DetermineTackles(p);
                foreach (FootballPlayer p in Secondary)
                    DetermineTackles(p);
                foreach (FootballPlayer p in Punter)
                    DeterminePunts(p);
                foreach (FootballPlayer p in Kicker)
                    DetermineXPAttempts(p);
            }
        }
        private void DetermineNetPuntYards(FootballPlayer p, int punts)
        {
            int yards = 0;
            for (int i = 0; i < punts; i++)
            {
                if (p.CurrentSkill >= 70) yards += rnd.Next(40, 49);
                else if (p.CurrentSkill >= 50) yards += rnd.Next(35, 45);
                else yards += rnd.Next(31, 40);
            }
            p.NetPuntYards += yards;
            p.NetPuntAverage = Convert.ToDouble(p.NetPuntYards) / Convert.ToDouble(p.Punts);
        }

        private void DeterminePunts(FootballPlayer p)
        {
            int punts = Convert.ToInt32(Math.Floor(Convert.ToDouble(totalPossessions) * 0.8));
            p.Punts += punts;
            totalPossessions -= punts;
            DetermineNetPuntYards(p, punts);
        }

        private void DetermineFGMakes(FootballPlayer p, int fgAttempts)
        {
            int fgMakes = 0;
            int diceRoll = 0;
            int numberOfRolls = 0;
            if (p.CurrentSkill >= 70) numberOfRolls = 3;
            else if (p.CurrentSkill >= 50) numberOfRolls = 2;
            else numberOfRolls = 1;

            for (int i = 0; i < fgAttempts; i++)
            {
                for (int j = 0; j < numberOfRolls; j++)
                {
                    diceRoll = DiceRoll();
                    if (diceRoll > 3)
                    {
                        fgMakes++;
                        return;
                    }
                }
            }
            p.FGMakes += fgMakes;
        }

        private void DetermineFGAttempts(FootballPlayer p)
        {
            int fgAttempts = totalPossessions;
            p.FGAttempts += fgAttempts;
            DetermineFGMakes(p, fgAttempts);
        }

        private void DetermineXPMakes(FootballPlayer p, int xpAttempts)
        {
            int xpMakes = 0;
            for (int i = 0; i < xpAttempts; i++)
            {
                int sumOfDice = DiceRoll();
                if (sumOfDice > 2) xpMakes++;
            }
            p.XPMakes += xpMakes;
        }

        private void DetermineXPAttempts(FootballPlayer p)
        {
            int xpAttempts = teamPassingTDs + teamRushingTDs;
            p.XPAttempts += xpAttempts;
            DetermineXPMakes(p, xpAttempts);
        }

        private void DetermineDefensiveInterceptions(FootballPlayer p, int passesDefended)
        {
            int interceptions = 0;
            for (int i = 0; i < passesDefended; i++)
            {
                int sumOfDice = DiceRoll();
                if (sumOfDice == 2) interceptions++;
            }
            p.DefensiveInterceptions += interceptions;
        }

        private void DeterminePassesDefended(FootballPlayer p)
        {
            int diceRolls = 0;
            int passesDefended = 0;
            if (p.CurrentSkill >= 80) diceRolls = 6;
            else if (p.CurrentSkill >= 70) diceRolls = 5;
            else if (p.CurrentSkill >= 60) diceRolls = 4;
            else if (p.CurrentSkill >= 50) diceRolls = 3;
            else if (p.CurrentSkill >= 40) diceRolls = 2;
            else diceRolls = 1;

            for (int i = 0; i < diceRolls; i++)
            {
                int sumOfDice = DiceRoll();
                if (sumOfDice == 2) passesDefended++;
            }

            if (passesDefended > 0)
            {
                p.PassesDefended += passesDefended;
                DetermineDefensiveInterceptions(p, passesDefended);
            }
        }

        private void DetermineTacklesForLoss(FootballPlayer p, int tackles)
        {
            int tacklesForLoss = 0;
            for (int i = 0; i < tackles; i++)
            {
                int diceRoll = DiceRoll();
                if (diceRoll <= 4) tacklesForLoss++;
            }
            p.TacklesForLoss += tacklesForLoss;
            if (p.Position == Position.LB || p.Position == Position.DE || p.Position == Position.DT)
                if (tacklesForLoss > 0)
                    DetermineSacks(p, tacklesForLoss);
        }

        private void DetermineSacks(FootballPlayer p, int tacklesForLoss)
        {
            int sacks = 0;
            for (int i = 0; i < tacklesForLoss; i++)
            {
                int diceRoll = DiceRoll();
                if (diceRoll <= 5) sacks++;
            }
            p.Sacks += sacks;
        }

        private void DetermineTackles(FootballPlayer p)
        {
            int tackles = 0;
            if (p.IsStarter)
            {
                tackles = rnd.Next(2, 8);
                if (p.Position == Position.LB && p.CurrentSkill >= 65)
                    tackles += DiceRoll() / 2;
                if (p.Position == Position.CB)
                    if (tackles > 4) tackles -= 2;
                if (p.CurrentSkill < 50)
                    tackles = tackles / 2;
            }
            else
            {
                int diceRoll = DiceRoll();
                if (diceRoll >= 10)
                    tackles = rnd.Next(1, 4);
                if (p.Position == Position.CB && tackles == 3)
                {
                    tackles--;
                }
                    
            }
            p.Tackles += tackles;
            if (tackles > 0 && (p.Position == Position.LB || p.Position == Position.DE || p.Position == Position.DT))
                DetermineTacklesForLoss(p, tackles);
            else if (p.Position == Position.CB || p.Position == Position.FS || p.Position == Position.SS)
                DeterminePassesDefended(p);
        }

        private void DetermineOLineYPC(List<FootballPlayer> list)
        {
            List<FootballPlayer> Starters = new List<FootballPlayer>();
            foreach (FootballPlayer p in list)
                if (p.IsStarter) Starters.Add(p);
            foreach (FootballPlayer p in Starters)
            {
                p.Carries += teamCarries;
                p.RushingYards += teamRushingYards;
                p.RushingTDs += teamRushingTDs;
                p.YardsPerCarry = Convert.ToDouble(p.RushingYards) / Convert.ToDouble(p.Carries);
            }
        }

        private void DeterminePancakeBlocks(List<FootballPlayer> list, double averageSkill)
        {
            int numberOfPancakes = 0;
            List<FootballPlayer> Starters = new List<FootballPlayer>();
            foreach (FootballPlayer p in list)
                if (p.IsStarter) Starters.Add(p);
            if (averageSkill >= 60)
            {
                numberOfPancakes = rnd.Next(4, 8);
                int diceRoll = DiceRoll();
                if (diceRoll <= 3) numberOfPancakes++;
            }
            else if (averageSkill >= 50) numberOfPancakes = rnd.Next(2, 7);
            else if (averageSkill >= 40) numberOfPancakes = rnd.Next(1, 6);
            else numberOfPancakes = rnd.Next(0, 4);

            for (int i = 0; i < numberOfPancakes; i++)
            {
                int pancakeCount = 0;
                foreach (FootballPlayer p in Starters) pancakeCount += p.CurrentSkill;
                int pancakeNumber = rnd.Next(0, pancakeCount);
                if (pancakeNumber <= Starters[0].CurrentSkill) Starters[0].PancakeBlocks++;
                else if (pancakeNumber > Starters[0].CurrentSkill && pancakeNumber <= (Starters[0].CurrentSkill + Starters[1].CurrentSkill)) Starters[1].PancakeBlocks++;
                else if (pancakeNumber > (Starters[0].CurrentSkill + Starters[1].CurrentSkill) && pancakeNumber <= (Starters[0].CurrentSkill + Starters[1].CurrentSkill + Starters[2].CurrentSkill)) Starters[2].PancakeBlocks++;
                else if (pancakeNumber > (Starters[0].CurrentSkill + Starters[1].CurrentSkill + Starters[2].CurrentSkill) && pancakeNumber <= (Starters[0].CurrentSkill + Starters[1].CurrentSkill + Starters[2].CurrentSkill + Starters[3].CurrentSkill)) Starters[3].PancakeBlocks++;
                else Starters[4].PancakeBlocks++;
            }
        }

        private void DetermineSacksAllowed(List<FootballPlayer> list, double averageSkill)
        {
            int numberOfSacks = 0;
            List<FootballPlayer> Starters = new List<FootballPlayer>();
            foreach (FootballPlayer p in list)
                if (p.IsStarter) Starters.Add(p);
            if (averageSkill >= 60) numberOfSacks = rnd.Next(0, 3);
            else if (averageSkill >= 40) numberOfSacks = rnd.Next(1, 4);
            else
            {
                numberOfSacks = rnd.Next(2, 4);
                int diceRoll = DiceRoll();
                if (diceRoll <= 3) numberOfSacks++;
            }
            for (int i = 0; i < numberOfSacks; i++)
            {
                foreach (FootballPlayer fp in Starters)
                {
                    int numberOfRolls = 0;
                    if (fp.CurrentSkill >= 80) numberOfRolls = 1;
                    else if (fp.CurrentSkill >= 60) numberOfRolls = 2;
                    else if (fp.CurrentSkill >= 40) numberOfRolls = 3;
                    else numberOfRolls = 4;
                    fp.DiceRoll = 0;
                    for (int j = 0; j < numberOfRolls; j++)
                        fp.DiceRoll += DiceRoll();
                }
                list = list.OrderByDescending(o => o.DiceRoll).ToList();
                list[0].SacksAllowed++;
            }
        }

        private void DetermineReceivingTDs(List<FootballPlayer> WRandTE)
        {
            for (int i = 0; i < teamPassingTDs; i++)
            {
                int tdCount = 0;
                foreach (FootballPlayer p in WRandTE) tdCount += p.CurrentSkill;
                int tdNumber = rnd.Next(0, tdCount);
                if (tdNumber <= WRandTE[0].CurrentSkill) WRandTE[0].ReceivingTDs++;
                else if (tdNumber > WRandTE[0].CurrentSkill && tdNumber <= (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill)) WRandTE[1].ReceivingTDs++;
                else if (tdNumber > (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill) && tdNumber <= (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill + WRandTE[2].CurrentSkill)) WRandTE[2].ReceivingTDs++;
                else if (tdNumber > (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill + WRandTE[2].CurrentSkill) && tdNumber <= (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill + WRandTE[2].CurrentSkill + WRandTE[3].CurrentSkill)) WRandTE[3].ReceivingTDs++;
                else if (tdNumber > (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill + WRandTE[2].CurrentSkill + WRandTE[3].CurrentSkill) && tdNumber <= (WRandTE[0].CurrentSkill + WRandTE[1].CurrentSkill + WRandTE[2].CurrentSkill + WRandTE[3].CurrentSkill + WRandTE[4].CurrentSkill)) WRandTE[4].ReceivingTDs++;
                else WRandTE[5].ReceivingTDs++;
            }
        }

        private void DetermineReceivingYards(List<FootballPlayer> WRandTE)
        {

            WRandTE = WRandTE.OrderByDescending(o => o.CurrentSkill).ToList();
            int totalDiceSum = 0;

            for (int i = 0; i < WRandTE.Count; i++)
            {
                WRandTE[i].DiceRoll = 0;
                int diceRolls = WRandTE[i].CurrentSkill / 20;
                if (i == 0)
                {
                    //check if there's a true elite #1 weapon in passing game
                    if (WRandTE[0].CurrentSkill > WRandTE[1].CurrentSkill + 15)
                    {
                        diceRolls++;
                    }
                }
                for (int d = 0; d < diceRolls; d++)
                {
                    int diceCount = DiceRoll();
                    WRandTE[i].DiceRoll += diceCount;
                    totalDiceSum += diceCount;
                }
            }
            while (teamPassingYards - teamPassingYardsUsed > 0)
            {
                for (int i = 0; i < WRandTE.Count; i++)
                {
                    double yardPercent = Convert.ToDouble(WRandTE[i].DiceRoll) / Convert.ToDouble(totalDiceSum);

                    int receivingYards = Convert.ToInt32(Math.Floor(yardPercent * teamPassingYards));
                    WRandTE[i].ReceivingYards += receivingYards;
                    teamPassingYardsUsed += receivingYards;
                }
                //give remaining receiving yards to random receiver
                int randomReceiver = rnd.Next(0, WRandTE.Count);
                int leftoverYards = teamPassingYards - teamPassingYardsUsed;
                WRandTE[randomReceiver].ReceivingYards += leftoverYards;
                teamPassingYardsUsed += leftoverYards;
            }
        }

        private void DetermineReceptions(FootballPlayer p)
        {
            
        }

        private void DetermineFumbles(FootballPlayer p, int carries)
        {
            for (int i = 0; i < carries; i++)
            {
                int firstRoll = DiceRoll();
                int secondRoll = DiceRoll();
                if (firstRoll + secondRoll <= 6) p.Fumbles++;
            }
        }

        private void DetermineRushingTD(FootballPlayer p, int carries, int yards, int chunkPlays)
        {
            int sumOfSkillAndProduction = p.CurrentSkill + yards + carries;
            int random = rnd.Next(1, 251);
            if (sumOfSkillAndProduction >= random)
            {
                p.RushingTDs++;
                teamRushingTDs++;
            }
                
            if (chunkPlays > 0)
            {
                for (int i = 0; i < chunkPlays; i++)
                {
                    int diceRoll = DiceRoll();
                    if (diceRoll >= 11) p.RushingTDs++;
                }
            }
            DetermineFumbles(p, carries);
        }

        private void DetermineRushingYards(FootballPlayer p, int carries)
        {
            int chunkPlays = 0;
            int chunkYards = 0;
            for (int i = 0; i < carries / 2; i++)
            {
                int diceRoll1 = DiceRoll();
                if (diceRoll1 == 2) chunkPlays++;
            }
            if (chunkPlays > 0)
            {
                for (int j = 0; j < chunkPlays; j++)
                {
                    int bigChunk = DiceRoll();
                    if (bigChunk == 2) chunkYards += rnd.Next(41, 76);
                    else chunkYards += rnd.Next(20, 41);
                    p.ChunkPlays++;
                }
            }
            double ypc = DetermineYPC(p);
            int yards = Convert.ToInt32(Math.Round(ypc * Convert.ToDouble((carries - chunkPlays))) + chunkYards);
            p.RushingYards += yards;
            teamRushingYards += yards;
            p.YardsPerCarry = Convert.ToDouble(p.RushingYards) / Convert.ToDouble(p.Carries);
            DetermineRushingTD(p, carries, yards, chunkPlays);
        }

        private double DetermineYPC(FootballPlayer p)
        {
            double ypc = 0.0;
            bool greatGame = false;
            bool goodGame = false;
            bool badGame = false;
            int diceTotal = 0;
            int numberOfDiceRolls = p.CurrentSkill / 20;
            for (int i = 0; i < numberOfDiceRolls; i++)
            {
                diceTotal += DiceRoll();
            }
            if (diceTotal <= 9) badGame = true;
            else if (diceTotal >= 10 && diceTotal <= 20) goodGame = true;
            else greatGame = true;

            if (badGame) ypc = Convert.ToDouble(rnd.Next(21, 31)) / 10;
            else if (goodGame) ypc = Convert.ToDouble(rnd.Next(31, 41)) / 10;
            else if (greatGame) ypc = Convert.ToDouble(rnd.Next(41, 56)) / 10;

            return ypc;
        }

        private void DetermineCarries(FootballPlayer p)
        {
            if (p.IsStarter && p.Position == Position.RB)
            {
                int carries = rnd.Next(8, 26);
                p.Carries += carries;
                teamCarries += carries;
                DetermineRushingYards(p, carries);
            }
            else if (p.DepthChart == 2 && p.Position == Position.RB)
            {
                int carries = rnd.Next(3, 8);
                p.Carries += carries;
                teamCarries += carries;
                DetermineRushingYards(p, carries);
            }
            else if (p.DepthChart == 3 && p.Position == Position.RB)
            {
                int diceRoll = DiceRoll();
                if (diceRoll <= 3)
                {
                    int carries = rnd.Next(1, 4);
                    p.Carries += carries;
                    teamCarries += carries;
                    DetermineRushingYards(p, carries);
                }
            }
            else if (p.IsStarter && p.Position == Position.FB)
            {
                int diceRoll = DiceRoll();
                if (diceRoll <= 3)
                {
                    int carries = rnd.Next(1, 4);
                    p.Carries += carries;
                    teamCarries += carries;
                    DetermineRushingYards(p, carries);
                }
            }
        }

        private void DetermineInterceptions(FootballPlayer p)
        {
            if (World.IsFootballStarter(p.Team, p))
            {
                int randomINTChance = rnd.Next(1, 101);
                if (randomINTChance > p.CurrentSkill)
                {
                    if (p.CurrentSkill >= 70) p.Interceptions++;
                    else if (p.CurrentSkill >= 50) p.Interceptions += rnd.Next(1, 3);
                    else if (p.CurrentSkill >= 40) p.Interceptions += rnd.Next(1, 4);
                    else p.Interceptions += rnd.Next(2, 4);
                }
            }
        }

        private void DeterminePassingYards(FootballPlayer p)
        {
            if (World.IsFootballStarter(p.Team, p))
            {
                int passingYards = 0;
                if (p.CurrentSkill >= 70) passingYards += rnd.Next(175, 451);
                else if (p.CurrentSkill >= 60) passingYards += rnd.Next(175, 390);
                else if (p.CurrentSkill >= 50) passingYards += rnd.Next(150, 301);
                else if (p.CurrentSkill >= 40) passingYards += rnd.Next(130, 251);
                else passingYards += rnd.Next(125, 201);

                p.PassingYards += passingYards;
                teamPassingYards += passingYards;
            }
            else
            {
                //backup gets 10% chance of playing time
                int blowout = rnd.Next(1, 11);
                if (blowout == 10)
                {
                    //backup passes for between 0-65 yards
                    int yards = rnd.Next(0, 66);
                    p.PassingYards += yards;
                    teamPassingYards += yards;
                    //20% chance of backup tossing a TD if yards >= 30
                    if (yards >= 30)
                    {
                        int tdPass = rnd.Next(1, 11);
                        if (tdPass <= 2) p.PassingTDs++;
                    }
                    else
                    {
                        //if backup doesn't throw a TD, 30% chance of throwing INT
                        int INT = rnd.Next(1, 11);
                        if (INT <= 3) p.Interceptions++;
                    }
                }
            }
        }

        private void DeterminePassingTD(FootballPlayer p)
        {
            if (World.IsFootballStarter(p.Team, p))
            {
                int passingTDs = 0;
                if (p.CurrentSkill >= 70) passingTDs += rnd.Next(1, 5);
                else if (p.CurrentSkill >= 60) passingTDs += rnd.Next(1, 4);
                else if (p.CurrentSkill >= 50) passingTDs += rnd.Next(0, 3);
                else passingTDs += rnd.Next(0, 2);

                p.PassingTDs += passingTDs;
                teamPassingTDs += passingTDs;
            }
        }
        #endregion
        public void SimulateGames()
        {

        }

        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
