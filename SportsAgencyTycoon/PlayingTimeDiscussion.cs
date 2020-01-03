using System.Collections.Generic;
using System.Linq;

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
        private List<Player> playersAtPosition = new List<Player>();
        public PlayingTimeDiscussion(Team t, League l, Player p)
        {
            team = t;
            league = l;
            player = p;
            ResolveBools();
            PlayersAtPosition();
        }
        private void PlayersAtPosition()
        {
            foreach (Player p in team.Roster)
                if (p.Position == player.Position && p.DepthChart < player.DepthChart)
                    playersAtPosition.Add(p);
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
        public string GMResponse()
        {
            string response = "";

            if (TitleContender) response = TitleContenderPT();
            else if (PlayoffTeam) response = PlayoffTeamPT();
            else if (InTheHunt) response = InTheHuntPT();
            else if (Rebuilding) response = RebuildingPT();
            else if (Tanking) response = TankingPT();

            return response;
        }
        private string TitleContenderPT()
        {
            string response = "";

            

            return response;
        }
        private string PlayoffTeamPT()
        {
            string response = "";



            return response;
        }
        private string InTheHuntPT()
        {
            string response = "";



            return response;
        }
        private string RebuildingPT()
        {
            string response = "";



            return response;
        }
        private string TankingPT()
        {
            string response = "";



            return response;
        }
    }
}
