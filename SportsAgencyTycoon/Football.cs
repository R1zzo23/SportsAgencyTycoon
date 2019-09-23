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
            if (World.MonthName == Months.September && World.WeekNumber == 3)
                InitializeStats();

            if (!NFL.Playoffs)
            {
                SimulateGames();
                UpdateStats();
            }
        }

        public void InitializeStats()
        {
            Console.WriteLine("Initializing football stats...");
        }
        public void SimulateGames()
        {

        }
        public void UpdateStats()
        {

        }
    }
}
