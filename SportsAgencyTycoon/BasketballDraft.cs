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
        public void RunDraft()
        {
            List<Team> DraftOrder = new List<Team>();
            foreach (Team t in league.TeamList) DraftOrder.Add(t);

            DraftOrder = DraftOrder.OrderBy(o => o.Wins).ThenBy(o => o.TitleConteder).ToList();

            for (int i = 0; i < rounds; i++)
            {

            }

            // remove undrafted DraftEntrants from list, add to FreeAgents
        }
    }
}
