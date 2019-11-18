using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Soccer
    {
        public MainForm mainForm;
        public Random rnd;
        public League MLS;
        public World World;
        public int gamesThisWeek = 0;
        public int gamesForStarter = 0;
        public int gamesForBackup = 0;
        public int starterWins = 0;
        public int backupWins = 0;
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
        public Soccer(MainForm mf, Random r, World w, League l)
        {
            mainForm = mf;
            rnd = r;
            World = w;
            MLS = l;
            index = 1;
            Conferences = new List<string>();
            Divisions = new List<string>();
            //FillLists();
        }
    }
}
