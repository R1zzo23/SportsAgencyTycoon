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
            throw new NotImplementedException();
        }

        private void DeterminePunts(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineFGMakes(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineFGAttempts(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineXPMakes(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineXPAttempts(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineDefensiveInterceptions(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DeterminePassesDefended(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineTacklesForLoss(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineSacks(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineTackles(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DeterminePancakeBlocks(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineSacksAllowed(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineReceivingTDs(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineReceivingYards(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineReceptions(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineFumbles(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineRushingTD(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineRushingYards(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineCarries(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DetermineInterceptions(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DeterminePassingYards(FootballPlayer p)
        {
            throw new NotImplementedException();
        }

        private void DeterminePassingTD(FootballPlayer p)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void SimulateGames()
        {

        }
    }
}
