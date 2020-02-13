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
        public void ModifyDraftClass(List<Player> draftEntrants)
        {
            int potential80count = 0;
            int potential70count = 0;
            int current80count = 0;
            int current70count = 0;
            int current60count = 0;

            foreach (Player p in draftEntrants)
            {
                if (p.Age >= 21) p.PotentialSkill = p.CurrentSkill + rnd.Next(3, 20);
                else if (p.Age == 20) p.PotentialSkill = p.CurrentSkill + rnd.Next(8, 25);
                else if (p.Age == 19) p.PotentialSkill = p.CurrentSkill + rnd.Next(10, 30);
                else p.PotentialSkill = p.CurrentSkill + rnd.Next(15, 40);

                if (p.PotentialSkill >= 80) potential80count++;
                if (p.PotentialSkill >= 70) potential70count++;
                if (p.CurrentSkill >= 80) current80count++;
                if (p.CurrentSkill >= 70) current70count++;
                if (p.CurrentSkill >= 60) current60count++;

                Console.WriteLine("Players with 70+ Potential: " + potential70count);
                Console.WriteLine("Players with 80+ Potential: " + potential80count);
                Console.WriteLine("Players with 60+ Current: " + current60count);
                Console.WriteLine("Players with 70+ Current: " + current70count);
                Console.WriteLine("Players with 80+ Potential: " + current80count);
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
