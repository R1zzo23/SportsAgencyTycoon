using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class BasketballDraft
    {
        Random rnd;
        int rounds = 2;
        int numberOfEntrants;
        League league;
        public BasketballDraft(Random r, League l)
        {
            rnd = r;
            league = l;
            numberOfEntrants = rnd.Next(85, 101);
        }
        public void CreateDraftEntrants()
        {
            for (int i = 0; i < numberOfEntrants; i++)
            {
                BasketballPlayer b = new BasketballPlayer(rnd, league.IdCount, Sports.Basketball, rnd.Next(18, 23), DeterminePosition());
                b.PlayerType = PlayerType.DraftEntrant;
                league.DraftEntrants.Add(b);
            }
        }
        public Position DeterminePosition()
        {
            int position = rnd.Next(1, 6);
            if (position == 1) return Position.PG;
            else if (position == 2) return Position.SG;
            else if (position == 3) return Position.SF;
            else if (position == 4) return Position.PF;
            else return Position.C;
        }
        public string RunDraft()
        {
            string results = "";

            List<Team> DraftOrder = new List<Team>();
            foreach (Team t in league.TeamList) DraftOrder.Add(t);

            DraftOrder = DraftOrder.OrderBy(o => o.Wins).ThenBy(o => o.TitleConteder).ToList();

            for (int i = 0; i < rounds; i++)
            {
                results = results + "Round #" + i + " Results:" + Environment.NewLine;
                for (int j = 0; j < DraftOrder.Count; j++)
                {
                    Player draftedPlayer = DraftOrder[j].DraftPlayer(rnd, league.DraftEntrants, j, i);
                    AddPlayerToTeam(draftedPlayer, DraftOrder[j], i, j);
                    RemoveDraftedPlayerFromDraftPool(draftedPlayer);
                    results = results + i + "." + j + " - " + DraftOrder[j].Abbreviation + " selects " + draftedPlayer.Position.ToString() + " " + draftedPlayer.FullName + Environment.NewLine;
                }
            }
            for (int i = league.DraftEntrants.Count - 1; i >= 0; i--)
            {
                league.DraftEntrants[i].PlayerStatus = PlayerType.Active;
                league.DraftEntrants[i].FreeAgent = true;
                league.FreeAgents.Add(league.DraftEntrants[i]);
                league.DraftEntrants.RemoveAt(i);
            }

            return results;
        }
        public void AddPlayerToTeam(Player p, Team t, int round, int pick)
        {
            p.DraftRound = round + 1;
            p.DraftPick = pick + 1;
            p.PlayerStatus = PlayerType.Active;
            t.Roster.Add(p);
        }
        public void RemoveDraftedPlayerFromDraftPool(Player draftedPlayer)
        {
            int index = league.DraftEntrants.FindIndex(o => o == draftedPlayer);
            league.DraftEntrants.RemoveAt(index);
        }
    }
}
