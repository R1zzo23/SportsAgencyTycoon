using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class PlayingTimeDiscussion
    {
        private bool TitleContender = false;
        private bool PlayoffTeam = false;
        private bool InTheHunt = false;
        private bool Rebuilding = false;
        private bool Tanking = false;
        private Team team;
        private League league;
        private Player player;
        public PlayingTimeDiscussion(Team t, League l, Player p)
        {
            team = t;
            league = l;
            player = p;
            ResolveBools();
        }
        private void ResolveBools()
        {
            List<Team> teams = new List<Team>();
            foreach (Team t in league.TeamList)
                teams.Add(t);

            // less than 30 games played; sort by TitleContender rating
            if (teams[0].Wins + teams[0].Losses < 30)
                teams = teams.OrderByDescending(o => o.TitleConteder).ToList();
            // 30+ games played; sort by record
            else teams = teams.OrderByDescending(o => o.Wins).ToList();

            // find index of player's team in the list
            int index = teams.FindIndex(o => o == team);

            if (index < 6) TitleContender = true;
            else if (index < teams.Count / 2) PlayoffTeam = true;
            else if (index < teams.Count * .7) InTheHunt = true;
            else if (index >= teams.Count - 4) Tanking = true;
            else Rebuilding = true;
        }
    }
}
