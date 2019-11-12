using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Hockey
    {
        public MainForm mainForm;
        public Random rnd;
        public League NHL;
        public World World;
        public int index;
        public int losingIndex;
        public List<string> Conferences;
        public List<string> Divisions;
        public List<Team> EasternConference = new List<Team>();
        public List<Team> WesternConference = new List<Team>();
        public List<Team> Metropolitan = new List<Team>();
        public List<Team> Atlantic = new List<Team>();
        public List<Team> Central = new List<Team>();
        public List<Team> Pacific = new List<Team>();
        public List<Team> EasternPlayoffs = new List<Team>();
        public List<Team> WesternPlayoffs = new List<Team>();
        public List<int> EasternLoserIndex = new List<int>();
        public List<int> WesternLoserIndex = new List<int>();
        public List<BasketballPlayer> DPOYCandidates = new List<BasketballPlayer>();
        public Hockey(MainForm mf, Random r, World w, League l)
        {
            mainForm = mf;
            rnd = r;
            World = w;
            NHL = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            FillLists();
        }
        private void FillLists()
        {
            foreach (Team t in NHL.TeamList)
                if (t.Conference == "Eastern")
                {
                    EasternConference.Add(t);
                    if (t.Division == "Metropolitan") Metropolitan.Add(t);
                    else Atlantic.Add(t);
                }
                else
                {
                    WesternConference.Add(t);
                    if (t.Division == "Central") Central.Add(t);
                    else Pacific.Add(t);
                }
        }
        public void SimWeek()
        {
            if (NHL.WeekNumber == 0)
                InitializeGAA();
            NHL.WeekNumber++;
            if (!NHL.Playoffs)
            {
                SimulateGames();
                UpdateStats();
            }
            if (NHL.WeekNumber == 32)
            {
                NHL.Playoffs = true;
                foreach (Team t in NHL.TeamList)
                {
                    foreach (HockeyPlayer p in t.Roster)
                    {
                        if (p.Position == Position.W || p.Position == Position.C)
                            CalculateForwardOfTheYearScore(p);
                        else if (p.Position == Position.D)
                            CalculateDefensemanOfTheYearScore(p);
                        else if (p.Position == Position.G)
                            CalculateGoalieOfTheYearScore(p);
                    }
                }
                mainForm.newsLabel.Text = DisplayForwardOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayDefensemanOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;
                mainForm.newsLabel.Text = DisplayGoalieOfTheYearTop5() + Environment.NewLine + mainForm.newsLabel.Text;

                mainForm.newsLabel.Text = DeterminePlayoffField() + Environment.NewLine + mainForm.newsLabel.Text;
            }
            if (NHL.Playoffs)
            {

            }
        }
        #region Statistics
        private void InitializeGAA()
        {

        }
        private void UpdateStats()
        {

        }
        #endregion
        #region Simulation
        private void SimulateGames()
        {

        }
        private string DeterminePlayoffField()
        {
            string results = "";


            return results;
        }
        #endregion
        #region Awards
        private void CalculateForwardOfTheYearScore(HockeyPlayer p)
        {

        }
        private void CalculateDefensemanOfTheYearScore(HockeyPlayer p)
        {

        }
        private void CalculateGoalieOfTheYearScore(HockeyPlayer p)
        {

        }
        private string DisplayForwardOfTheYearTop5()
        {
            string results = "";
            return results;
        }
        private string DisplayDefensemanOfTheYearTop5()
        {
            string results = "";
            return results;
        }
        private string DisplayGoalieOfTheYearTop5()
        {
            string results = "";
            return results;
        }
        #endregion
    }
}
