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
        public List<Team> AFC = new List<Team>();
        public List<Team> NFC = new List<Team>();
        public List<Team> AFCPlayoffs = new List<Team>();
        public List<Team> NFCPlayoffs = new List<Team>();
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
            NFL.WeekNumber++;
            if (!NFL.Playoffs)
            {
                SimulateGames();
                DetermineStats();
            }
            if (NFL.WeekNumber == 16)
            {
                NFL.Playoffs = true;
                foreach (Team t in World.NFL.TeamList)
                    foreach (FootballPlayer p in t.Roster)
                    {
                        World.Football.MVPScore(p);
                        if (p.Position == Position.QB || p.Position == Position.RB || p.Position == Position.WR || p.Position == Position.TE)
                            World.Football.OPOYScore(p);
                        else if (p.Position == Position.DT || p.Position == Position.DE || p.Position == Position.LB || p.Position == Position.CB || p.Position == Position.FS || p.Position == Position.SS)
                            World.Football.DPOYScore(p);
                    }
                mainForm.newsLabel.Text = DisplayDPOYTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayOPOYTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayMVPTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
            }
            if (NFL.Playoffs)
            {
                if (NFL.WeekNumber == 17) SimulateWildCardRound();
                if (NFL.WeekNumber == 18 || NFL.WeekNumber == 19) SimulateDivisionalAndConferenceChampionship();
                if (NFL.WeekNumber == 20) SimulateSuperBowl();
            }
        }
        public void MVPScore(FootballPlayer p)
        {
            double score = 0;
            score = p.CurrentSkill * 2 + p.Team.Wins * 5 + p.Popularity * 2 + (p.PassingYards / 4) + p.RushingYards 
                + p.PassingYards + (p.PassingTDs / 2) + p.RushingTDs + p.ReceivingTDs + (p.Tackles * 12) 
                + (p.Sacks * 1.5) + (p.DefensiveInterceptions * 5) + (p.PassesDefended * 2);
            p.MVPScore = score;
        }
        public void OPOYScore(FootballPlayer p)
        {
            double score = 0;
            score = p.CurrentSkill * 2 + p.Team.Wins * 5 + p.Popularity * 2 + (p.PassingYards / 4) + p.RushingYards
                + p.PassingYards + (p.PassingTDs / 2) + p.RushingTDs + p.ReceivingTDs;
            p.OPOYScore = score;
        }
        public void DPOYScore(FootballPlayer p)
        {
            double score = 0;
            score = p.CurrentSkill * 2 + p.Team.Wins * 5 + p.Popularity * 2 + (p.Tackles * 12)
                + (p.Sacks * 1.5) + (p.DefensiveInterceptions * 5) + (p.PassesDefended * 2);
            p.MVPScore = score;
        }
        public string DisplayMVPTop5()
        {
            string results = "";

            List<FootballPlayer> mvpRanks = new List<FootballPlayer>();
            foreach (Team t in World.NFL.TeamList)
                foreach (FootballPlayer p in t.Roster)
                    mvpRanks.Add(p);

            mvpRanks = mvpRanks.OrderByDescending(o => o.MVPScore).ToList();

            results = mvpRanks[0].Team.City + "'s " + mvpRanks[0].FullName + " has been named NFL MVP!" + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + mvpRanks[i - 1].Team.Abbreviation + "] " + mvpRanks[i - 1].FullName;
            }

            //give the winner the award
            mvpRanks[0].Awards.Add(new Award(World.Year, "NFL MVP"));

            return results;
        }
        public string DisplayOPOYTop5()
        {
            string results = "";

            List<FootballPlayer> OPOYCandidates = new List<FootballPlayer>();
            foreach (Team t in World.NFL.TeamList)
                foreach (FootballPlayer p in t.Roster)
                    OPOYCandidates.Add(p);

            OPOYCandidates = OPOYCandidates.OrderByDescending(o => o.OPOYScore).ToList();

            results = OPOYCandidates[0].Team.City + "'s " + OPOYCandidates[0].FullName + " has been named NFL Offensive Player of the Year!" + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + OPOYCandidates[i - 1].Team.Abbreviation + "] " + OPOYCandidates[i - 1].FullName;
            }

            //give the winner the award
            OPOYCandidates[0].Awards.Add(new Award(World.Year, "NFL Offensive Player of the Year"));

            return results;
        }
        public string DisplayDPOYTop5()
        {
            string results = "";

            List<FootballPlayer> DPOYCandidates = new List<FootballPlayer>();
            foreach (Team t in World.NFL.TeamList)
                foreach (FootballPlayer p in t.Roster)
                    DPOYCandidates.Add(p);

            DPOYCandidates = DPOYCandidates.OrderByDescending(o => o.DPOYScore).ToList();

            results = DPOYCandidates[0].Team.City + "'s " + DPOYCandidates[0].FullName + " has been named NFL Defensive Player of the Year!" + Environment.NewLine +
                "Here are the rest of the top-5:";
            for (int i = 2; i < 6; i++)
            {
                results += Environment.NewLine + i + ") [" + DPOYCandidates[i - 1].Team.Abbreviation + "] " + DPOYCandidates[i - 1].FullName;
            }

            //give the winner the award
            DPOYCandidates[0].Awards.Add(new Award(World.Year, "NFL Defensive Player of the Year"));

            return results;
        }
        public void SimulateSuperBowl()
        {
            AFCPlayoffs[0].Awards.Add(new Award(World.Year - 1, "AFC Champions"));
            NFCPlayoffs[0].Awards.Add(new Award(World.Year - 1, "NFC Champions"));
            int winner = SimulatePlayoffGame(AFCPlayoffs[0], NFCPlayoffs[0]);
            if (winner == 1)
            {
                AFCPlayoffs[0].Awards.Add(new Award(World.Year, "Super Bowl Champions"));
                mainForm.newsLabel.Text = "The " + AFCPlayoffs[0].Mascot + " defeat the " + NFCPlayoffs[0].Mascot + " and are crowned Super Bowl champs!" + Environment.NewLine + mainForm.newsLabel.Text;
            }
            else
            {
                NFCPlayoffs[0].Awards.Add(new Award(World.Year, "Super Bowl Champions"));
                mainForm.newsLabel.Text = "The " + NFCPlayoffs[0].Mascot + " defeat the " + AFCPlayoffs[0].Mascot + " and are crowned Super Bowl champs!" + Environment.NewLine + mainForm.newsLabel.Text;
            }
        }
        public void SimulateDivisionalAndConferenceChampionship()
        {
            List<Team> afcTemp = new List<Team>();
            List<Team> nfcTemp = new List<Team>();
            for (int i = 0; i < AFCPlayoffs.Count; i++)
            {
                afcTemp.Add(AFCPlayoffs[i]);
                nfcTemp.Add(NFCPlayoffs[i]);
            }
            AFCPlayoffs.Clear();
            NFCPlayoffs.Clear();
            for (int j = 0; j < afcTemp.Count / 2; j++)
            {
                int afcResult = 0;
                int nfcResult = 0;
                afcResult = SimulatePlayoffGame(afcTemp[j], afcTemp[afcTemp.Count - 1 - j]);
                nfcResult = SimulatePlayoffGame(nfcTemp[j], nfcTemp[afcTemp.Count - 1 - j]);
                if (afcResult == 1) AFCPlayoffs.Add(afcTemp[j]);
                else AFCPlayoffs.Add(afcTemp[afcTemp.Count - 1 - j]);
                if (nfcResult == 1) NFCPlayoffs.Add(nfcTemp[j]);
                else NFCPlayoffs.Add(nfcTemp[nfcTemp.Count - 1 - j]);
            }
            if (NFL.WeekNumber == 18)
                mainForm.newsLabel.Text = "Results from NFL Divisional Game Weekend:" + Environment.NewLine + mainForm.newsLabel.Text;
            else if (NFL.WeekNumber == 19)
                mainForm.newsLabel.Text = "Results from Conference Championship Weekend:" + Environment.NewLine + mainForm.newsLabel.Text;
        }
        public void SimulateWildCardRound()
        {
            List<Team> afcTemp = new List<Team>();
            List<Team> nfcTemp = new List<Team>();
            for (int i = 0; i < AFCPlayoffs.Count; i++)
            {
                afcTemp.Add(AFCPlayoffs[i]);
                nfcTemp.Add(NFCPlayoffs[i]);
            }
            AFCPlayoffs.Clear();
            NFCPlayoffs.Clear();
            for (int j = 0; j < 2; j++)
            {
                AFCPlayoffs.Add(afcTemp[j]);
                NFCPlayoffs.Add(nfcTemp[j]);
            }
            int afcWildCard1 = SimulatePlayoffGame(afcTemp[2], afcTemp[5]);
            int nfcWildCard1 = SimulatePlayoffGame(nfcTemp[2], nfcTemp[5]);
            int afcWildCard2 = SimulatePlayoffGame(afcTemp[3], afcTemp[4]);
            int nfcWildCard2 = SimulatePlayoffGame(nfcTemp[3], nfcTemp[4]);
            if (afcWildCard2 == 1) AFCPlayoffs.Add(afcTemp[3]);
            else AFCPlayoffs.Add(afcTemp[4]);
            if (afcWildCard1 == 1) AFCPlayoffs.Insert(2, afcTemp[2]);
            else AFCPlayoffs.Add(afcTemp[5]);
            if (nfcWildCard2 == 1) NFCPlayoffs.Add(nfcTemp[3]);
            else NFCPlayoffs.Add(nfcTemp[4]);
            if (nfcWildCard1 == 1) NFCPlayoffs.Insert(2, nfcTemp[2]);
            else NFCPlayoffs.Add(nfcTemp[5]);
            mainForm.newsLabel.Text = "Results from NFL Wild Card Weekend:" + Environment.NewLine + mainForm.newsLabel.Text;
        }
        public string DeterminePlayoffField()
        {
            string playoffSeedings = "";
            AFC.Clear();
            NFC.Clear();
            AFCPlayoffs.Clear();
            NFCPlayoffs.Clear();

            foreach (Team t in World.NFL.TeamList)
            {
                if (t.Conference == "AFC") AFC.Add(t);
                else NFC.Add(t);
            }

            AFC = AFC.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NFC = NFC.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();

            int afcEastIndex = AFC.FindIndex(t => t.Division == "East");
            AFCPlayoffs.Add(AFC[afcEastIndex]);
            AFC[afcEastIndex].Awards.Add(new Award(World.Year, AFC[afcEastIndex].Conference + " " + AFC[afcEastIndex].Division + " Division Champs"));
            AFC.RemoveAt(afcEastIndex);

            int afcSouthIndex = AFC.FindIndex(t => t.Division == "South");
            AFCPlayoffs.Add(AFC[afcSouthIndex]);
            AFC[afcSouthIndex].Awards.Add(new Award(World.Year, AFC[afcSouthIndex].Conference + " " + AFC[afcSouthIndex].Division + " Division Champs"));
            AFC.RemoveAt(afcSouthIndex);

            int afcNorthIndex = AFC.FindIndex(t => t.Division == "North");
            AFCPlayoffs.Add(AFC[afcNorthIndex]);
            AFC[afcNorthIndex].Awards.Add(new Award(World.Year, AFC[afcNorthIndex].Conference + " " + AFC[afcNorthIndex].Division + " Division Champs"));
            AFC.RemoveAt(afcNorthIndex);

            int afcWestIndex = AFC.FindIndex(t => t.Division == "West");
            AFCPlayoffs.Add(AFC[afcWestIndex]);
            AFC[afcWestIndex].Awards.Add(new Award(World.Year, AFC[afcWestIndex].Conference + " " + AFC[afcWestIndex].Division + " Division Champs"));
            AFC.RemoveAt(afcWestIndex);

            AFCPlayoffs = AFCPlayoffs.OrderByDescending(o => o.Wins).ToList();
            for (int i = 0; i < 2; i++)
                AFCPlayoffs.Add(AFC[i]);

            int nfcEastIndex = NFC.FindIndex(t => t.Division == "East");
            NFCPlayoffs.Add(NFC[nfcEastIndex]);
            NFC[nfcEastIndex].Awards.Add(new Award(World.Year, NFC[nfcEastIndex].Conference + " " + NFC[nfcEastIndex].Division + " Division Champs"));
            NFC.RemoveAt(nfcEastIndex);

            int nfcSouthIndex = NFC.FindIndex(t => t.Division == "South");
            NFCPlayoffs.Add(NFC[nfcSouthIndex]);
            NFC[nfcSouthIndex].Awards.Add(new Award(World.Year, NFC[nfcSouthIndex].Conference + " " + NFC[nfcSouthIndex].Division + " Division Champs"));
            NFC.RemoveAt(nfcSouthIndex);

            int nfcNorthIndex = NFC.FindIndex(t => t.Division == "North");
            NFCPlayoffs.Add(NFC[nfcNorthIndex]);
            NFC[nfcNorthIndex].Awards.Add(new Award(World.Year, NFC[nfcNorthIndex].Conference + " " + NFC[nfcNorthIndex].Division + " Division Champs"));
            NFC.RemoveAt(nfcNorthIndex);

            int nfcWestIndex = NFC.FindIndex(t => t.Division == "West");
            NFCPlayoffs.Add(NFC[nfcWestIndex]);
            NFC[nfcWestIndex].Awards.Add(new Award(World.Year, NFC[nfcWestIndex].Conference + " " + NFC[nfcWestIndex].Division + " Division Champs"));
            NFC.RemoveAt(nfcWestIndex);

            NFCPlayoffs = NFCPlayoffs.OrderByDescending(o => o.Wins).ToList();
            for (int i = 0; i < 2; i++)
                NFCPlayoffs.Add(NFC[i]);

            playoffSeedings = "AFC Playoffs:" + Environment.NewLine + "#1 " + AFCPlayoffs[0].Mascot + Environment.NewLine + "#2 " +
                AFCPlayoffs[1].Mascot + Environment.NewLine + "#3 " + AFCPlayoffs[2].Mascot + " vs. #6 " + AFCPlayoffs[5].Mascot + Environment.NewLine
                + "#4 " + AFCPlayoffs[3].Mascot + " vs. #5 " + AFCPlayoffs[4].Mascot + Environment.NewLine;
            playoffSeedings += "NFC Playoffs:" + Environment.NewLine + "#1 " + NFCPlayoffs[0].Mascot + Environment.NewLine + "#2 " +
                NFCPlayoffs[1].Mascot + Environment.NewLine + "#3 " + NFCPlayoffs[2].Mascot + " vs. #6 " + NFCPlayoffs[5].Mascot + Environment.NewLine
                + "#4 " + NFCPlayoffs[3].Mascot + " vs. #5 " + NFCPlayoffs[4].Mascot + Environment.NewLine;

            return playoffSeedings;
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
                {
                    DetermineXPAttempts(p);
                    DetermineFGAttempts(p);
                }
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
                int highRoll = 0;
                for (int j = 0; j < numberOfRolls; j++)
                {
                    diceRoll = DiceRoll();
                    if (diceRoll > highRoll) highRoll = diceRoll;
                }
                if (highRoll > 4) fgMakes++;
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
            foreach (FootballPlayer p in WRandTE) p.ReceivingYardsThisWeek = 0;
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
                    WRandTE[i].ReceivingYardsThisWeek += receivingYards;
                    teamPassingYardsUsed += receivingYards;
                }
                //give remaining receiving yards to random receiver
                int randomReceiver = rnd.Next(0, WRandTE.Count);
                int leftoverYards = teamPassingYards - teamPassingYardsUsed;
                WRandTE[randomReceiver].ReceivingYards += leftoverYards;
                WRandTE[randomReceiver].ReceivingYardsThisWeek += leftoverYards;
                teamPassingYardsUsed += leftoverYards;
            }
            foreach (FootballPlayer p in WRandTE)
                if (p.ReceivingYardsThisWeek > 0) DetermineReceptions(p);
        }

        private void DetermineReceptions(FootballPlayer p)
        {
            int receptions = 0;

            if (p.ReceivingYardsThisWeek < 15) receptions = 1;
            else
            {
                double yardsPerReception = Convert.ToDouble(rnd.Next(70, 400)) / 10;
                receptions = Convert.ToInt32(Math.Ceiling(p.ReceivingYardsThisWeek / yardsPerReception));
            }
            p.Receptions += receptions;
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
        public int SimulatePlayoffGame(Team team1, Team team2)
        {
            int winningTeam = 0;

            int totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
            int winningNumber = rnd.Next(1, totalNumber);
            if (winningNumber <= team1.TitleConteder)
            {
                Console.WriteLine(team1.Mascot + " beat the " + team2.Mascot);
                mainForm.newsLabel.Text = team1.Mascot + " beat the " + team2.Mascot + Environment.NewLine + mainForm.newsLabel.Text;
                winningTeam = 1;
            }
            else
            {
                Console.WriteLine(team2.Mascot + " beat the " + team1.Mascot);
                mainForm.newsLabel.Text = team2.Mascot + " beat the " + team1.Mascot + Environment.NewLine + mainForm.newsLabel.Text;
                winningTeam = 2;
            }

            return winningTeam;
        }
        public void SimulateGames()
        {
            List<Team> teams = new List<Team>();
            teams = NFL.TeamList;
            teams = teams.OrderBy(o => o.Conference).ThenBy(o => o.Division).ThenBy(o => o.City).ToList();
            if (NFL.WeekNumber == 1 || NFL.WeekNumber == 14)
                DivisionGame1(teams);
            else if (NFL.WeekNumber == 2 || NFL.WeekNumber == 15)
                DivisionGame2(teams);
            else if (NFL.WeekNumber == 3 || NFL.WeekNumber == 16)
                DivisionGame3(teams);
            else
            {
                List<int> indexList = new List<int>();
                for (int i = 0; i < NFL.TeamList.Count; i++)
                {
                    indexList.Add(i);
                }

                for (int j = 0; j < NFL.TeamList.Count / 2; j++)
                {
                    int opponentIndex = rnd.Next(1, indexList.Count);
                    SimulateGame(NFL.TeamList[indexList[0]], NFL.TeamList[indexList[opponentIndex]]);
                    indexList.RemoveAt(opponentIndex);
                    indexList.RemoveAt(0);
                }
            }
        }
        public void DivisionGame1(List<Team> teams)
        {
            for (int i = 0; i < teams.Count / 4; i++)
            {
                SimulateGame(teams[i * 4], teams[i * 4 + 1]);
                SimulateGame(teams[i * 4 + 2], teams[i * 4 + 3]);
            }
        }
        public void DivisionGame2(List<Team> teams)
        {
            for (int i = 0; i < teams.Count / 4; i++)
            {
                SimulateGame(teams[i * 4], teams[i * 4 + 2]);
                SimulateGame(teams[i * 4 + 1], teams[i * 4 + 3]);
            }
        }
        public void DivisionGame3(List<Team> teams)
        {
            for (int i = 0; i < teams.Count / 4; i++)
            {
                SimulateGame(teams[i * 4], teams[i * 4 + 3]);
                SimulateGame(teams[i * 4 + 1], teams[i * 4 + 2]);
            }
        }

        public void SimulateGame(Team team1, Team team2)
        {
            bool conferenceGame = false;
            bool divisionGame = false;
            if (team1.Conference == team2.Conference)
            {
                conferenceGame = true;
                if (team1.Division == team2.Division) divisionGame = true;
            }
            int totalNumber = team1.TitleConteder + team2.TitleConteder + 1;
            int winningNumber = rnd.Next(1, totalNumber);
            if (winningNumber <= team1.TitleConteder)
            {
                team1.Wins++;
                team2.Losses++;
                if (divisionGame)
                {
                    team1.DivisionWins++;
                    team2.DivisionLosses++;
                }
                if (conferenceGame)
                {
                    team1.ConferenceWins++;
                    team2.ConferenceLosses++;
                }
                Console.WriteLine(team1.Mascot + " beat the " + team2.Mascot);
            }
            else
            {
                team1.Losses++;
                team2.Wins++;
                if (divisionGame)
                {
                    team2.DivisionWins++;
                    team1.DivisionLosses++;
                }
                if (conferenceGame)
                {
                    team2.ConferenceWins++;
                    team1.ConferenceLosses++;
                }
                Console.WriteLine(team2.Mascot + " beat the " + team1.Mascot);
            }
        }

        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
