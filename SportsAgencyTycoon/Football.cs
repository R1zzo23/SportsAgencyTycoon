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
        public List<BasketballPlayer> DPOYCandidates = new List<BasketballPlayer>();

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
        #region DetermineStats
        public void DetermineStats()
        {
            foreach (Team t in NFL.TeamList)
                foreach (FootballPlayer p in t.Roster)
                {
                    if (p.Position == Position.QB)
                    {
                        DeterminePassingTD(p);
                        DeterminePassingYards(p);
                        DetermineInterceptions(p);
                    }
                    else if (p.Position == Position.RB)
                    {
                        DetermineCarries(p);
                        DetermineRushingYards(p);
                        DetermineRushingTD(p);
                        DetermineFumbles(p);
                    }
                    else if (p.Position == Position.WR)
                    {
                        DetermineReceptions(p);
                        DetermineReceivingYards(p);
                        DetermineReceivingTDs(p);
                    }
                    else if (p.Position == Position.OG || p.Position == Position.OT || p.Position == Position.C)
                    {
                        DetermineSacksAllowed(p);
                        DeterminePancakeBlocks(p);
                    }
                    else if (p.Position == Position.DT || p.Position == Position.DE || p.Position == Position.LB)
                    {
                        DetermineTackles(p);
                        DetermineSacks(p);
                        DetermineTacklesForLoss(p);
                    }
                    else if (p.Position == Position.CB || p.Position == Position.FS || p.Position == Position.SS)
                    {
                        DetermineTackles(p);
                        DeterminePassesDefended(p);
                        DetermineDefensiveInterceptions(p);
                    }
                    else if (p.Position == Position.K)
                    {
                        DetermineXPAttempts(p);
                        DetermineXPMakes(p);
                        DetermineFGAttempts(p);
                        DetermineFGMakes(p);
                    }
                    else if (p.Position == Position.P)
                    {
                        DeterminePunts(p);
                        DetermineNetPuntYards(p);
                    }
                }
        }
        private void DetermineNetPuntYards(FootballPlayer p)
        {
            
        }

        private void DeterminePunts(FootballPlayer p)
        {
            
        }

        private void DetermineFGMakes(FootballPlayer p)
        {
            
        }

        private void DetermineFGAttempts(FootballPlayer p)
        {
            
        }

        private void DetermineXPMakes(FootballPlayer p)
        {
            
        }

        private void DetermineXPAttempts(FootballPlayer p)
        {
           
        }

        private void DetermineDefensiveInterceptions(FootballPlayer p)
        {
            
        }

        private void DeterminePassesDefended(FootballPlayer p)
        {
            
        }

        private void DetermineTacklesForLoss(FootballPlayer p)
        {
            
        }

        private void DetermineSacks(FootballPlayer p)
        {
            
        }

        private void DetermineTackles(FootballPlayer p)
        {
            
        }

        private void DeterminePancakeBlocks(FootballPlayer p)
        {
            
        }

        private void DetermineSacksAllowed(FootballPlayer p)
        {
            
        }

        private void DetermineReceivingTDs(FootballPlayer p)
        {
            
        }

        private void DetermineReceivingYards(FootballPlayer p)
        {
            
        }

        private void DetermineReceptions(FootballPlayer p)
        {
            
        }

        private void DetermineFumbles(FootballPlayer p)
        {
            
        }

        private void DetermineRushingTD(FootballPlayer p)
        {
            
        }

        private void DetermineRushingYards(FootballPlayer p)
        {
            
        }

        private void DetermineCarries(FootballPlayer p)
        {
            
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
                if (p.CurrentSkill >= 70) p.PassingYards += rnd.Next(175, 451);
                else if (p.CurrentSkill >= 60) p.PassingYards += rnd.Next(175, 390);
                else if (p.CurrentSkill >= 50) p.PassingYards += rnd.Next(150, 301);
                else if (p.CurrentSkill >= 40) p.PassingYards += rnd.Next(130, 251);
                else p.PassingYards += rnd.Next(125, 201);
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
                if (p.CurrentSkill >= 70) p.PassingTDs += rnd.Next(1, 5);
                else if (p.CurrentSkill >= 60) p.PassingTDs += rnd.Next(1, 4);
                else if (p.CurrentSkill >= 50) p.PassingTDs += rnd.Next(0, 3);
                else p.PassingTDs += rnd.Next(0, 2);
            }
        }
        #endregion
        public void SimulateGames()
        {

        }
    }
}
