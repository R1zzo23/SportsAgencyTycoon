using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class BaseballDraft
    {
        public Random rnd;
        public World world;
        public int rounds;
        public int numberOfEntrants;
        public League league;
        public BaseballDraft(Random r, World w, League l)
        {
            rnd = r;
            world = w;
            league = l;
            DetermineRoundsAndNumberOfEntrants();
        }
        public void DetermineRoundsAndNumberOfEntrants()
        {
            if (league.Sport == Sports.Basketball) rounds = 2;
            else if (league.Sport == Sports.Football) rounds = 7;
            else if (league.Sport == Sports.Hockey) rounds = 7;
            else if (league.Sport == Sports.Baseball) rounds = 10;
            else if (league.Sport == Sports.Soccer) rounds = 4;
            else rounds = 0;

            numberOfEntrants = rnd.Next(((league.TeamList.Count + 10) * rounds), ((league.TeamList.Count + 20) * rounds));
        }
        public void CreateDraftEntrants()
        {
            for (int i = 0; i < numberOfEntrants; i++)
            {
                BaseballPlayer b = new BaseballPlayer(rnd, league.IdCount, Sports.Baseball, rnd.Next(18, 22), DeterminePosition());
                b.PlayerType = PlayerType.DraftEntrant;
                b.League = league;
                league.DraftEntrants.Add(b);
            }
            ModifyDraftClass(league.DraftEntrants);
            ModifyDraftClassPopularityAndPGP(league.DraftEntrants);
        }
        public void ModifyDraftClassPopularityAndPGP(List<Player> draftEntrants)
        {
            foreach (Player p in draftEntrants)
            {
                world.PGP.CreatePGP(rnd, p);
                p.Popularity = p.DeterminePopularity(p.CurrentSkill, p.PotentialSkill, p.Age);
                p.PopularityDescription = p.DescribePopularity(p.Popularity);
                p.PopularityString = p.EnumToString(p.PopularityDescription.ToString());
            }
        }
        public void ModifyDraftClass(List<Player> draftEntrants)
        {
            int draftStrength = rnd.Next(1, 11);
            int superStars = DetermineSuperStars(draftStrength);

            #region Testing Data
            int potential80count = 0;
            int potential70count = 0;
            int current80count = 0;
            int current70count = 0;
            int current60count = 0;

            int league80potential = 0;
            int league70potential = 0;
            int league80current = 0;
            int league70current = 0;
            int league60current = 0;

            foreach (Team t in league.TeamList)
            {
                foreach (Player p in t.Roster)
                {
                    if (p.PotentialSkill >= 80) league80potential++;
                    if (p.PotentialSkill >= 70 && p.PotentialSkill < 80) league70potential++;
                    if (p.CurrentSkill >= 80) league80current++;
                    if (p.CurrentSkill >= 70 && p.CurrentSkill < 80) league70current++;
                    if (p.CurrentSkill >= 60 && p.CurrentSkill < 70) league60current++;
                }
            }
            #endregion
            draftEntrants = draftEntrants.OrderByDescending(o => o.CurrentSkill).ToList();
            for (int i = 0; i < draftEntrants.Count; i++)
            {
                if (i < superStars)
                {
                    draftEntrants[i].PotentialSkill = rnd.Next(75, 86);
                    draftEntrants[i].CurrentSkill = rnd.Next(60, 71);
                }
                else if (i < 25 + draftStrength)
                {
                    draftEntrants[i].CurrentSkill = rnd.Next(50, 65);
                    draftEntrants[i].PotentialSkill = draftEntrants[i].CurrentSkill + rnd.Next(5, 15);
                }
                else if (i < 65)
                {
                    draftEntrants[i].CurrentSkill = rnd.Next(45, 58);
                    draftEntrants[i].PotentialSkill = draftEntrants[i].CurrentSkill + rnd.Next(5, 11);
                }
                else if (i < 100)
                {
                    draftEntrants[i].CurrentSkill = rnd.Next(35, 45);
                    draftEntrants[i].PotentialSkill = draftEntrants[i].CurrentSkill + rnd.Next(0, 7);
                }
                else
                {
                    draftEntrants[i].CurrentSkill = rnd.Next(20, 35);
                    draftEntrants[i].PotentialSkill = draftEntrants[i].CurrentSkill + rnd.Next(0, 6);
                }
            }

            DetermineRandomPotentialBoosts(draftEntrants);
            #region Display Testing Data
            foreach (Player p in draftEntrants)
            {
                /*if (p.Age >= 21) p.PotentialSkill = p.CurrentSkill + rnd.Next(3, 20);
                else if (p.Age == 20) p.PotentialSkill = p.CurrentSkill + rnd.Next(8, 25);
                else if (p.Age == 19) p.PotentialSkill = p.CurrentSkill + rnd.Next(10, 30);
                else p.PotentialSkill = p.CurrentSkill + rnd.Next(15, 40);

                int nerf = rnd.Next(15-draftStrength, 30-draftStrength);
                p.CurrentSkill -= nerf;
                p.PotentialSkill -= nerf;*/

                if (p.PotentialSkill >= 80) potential80count++;
                if (p.PotentialSkill >= 70 && p.PotentialSkill < 80) potential70count++;
                if (p.CurrentSkill >= 80) current80count++;
                if (p.CurrentSkill >= 70 && p.CurrentSkill < 80) current70count++;
                if (p.CurrentSkill >= 60 && p.CurrentSkill < 70) current60count++;
            }

            Console.WriteLine("Draft Strength: " + draftStrength);
            Console.WriteLine("Draft with 70+ Potential: " + potential70count);
            Console.WriteLine("Draft with 80+ Potential: " + potential80count);
            Console.WriteLine("Draft with 60+ Current: " + current60count);
            Console.WriteLine("Draft with 70+ Current: " + current70count);
            Console.WriteLine("Draft with 80+ Current: " + current80count);

            Console.WriteLine("Players with 70+ Potential: " + league70potential);
            Console.WriteLine("Players with 80+ Potential: " + league80potential);
            Console.WriteLine("Players with 60+ Current: " + league60current);
            Console.WriteLine("Players with 70+ Current: " + league70current);
            Console.WriteLine("Players with 80+ Current: " + league80current);
            #endregion
        }
        public void DetermineRandomPotentialBoosts(List<Player> draftEntrants)
        {
            int randomBoosts = rnd.Next(3, 11);
            for (int i = 0; i < randomBoosts; i++)
            {
                int index = rnd.Next(100, draftEntrants.Count);
                draftEntrants[index].PotentialSkill += 10;
            }
        }
        public int DetermineSuperStars(int i)
        {
            int numberOfSuperStars = 0;

            if (i == 10) numberOfSuperStars = rnd.Next(5, 12);
            else if (i == 9) numberOfSuperStars = rnd.Next(4, 10);
            else if (i == 8) numberOfSuperStars = rnd.Next(4, 8;
            else if (i == 7) numberOfSuperStars = rnd.Next(4, 7);
            else if (i == 6) numberOfSuperStars = rnd.Next(3, 7);
            else if (i == 5) numberOfSuperStars = rnd.Next(3, 6);
            else numberOfSuperStars = 2;

            return numberOfSuperStars;
        }
        public Position DeterminePosition()
        {
            int position = rnd.Next(1, 20);
            if (position <= 5) return Position.SP;
            else if (position <= 11) return Position.RP;
            else if (position <= 15) return Position.INF;
            else if (position <= 18) return Position.OF;
            else return Position.C;
        }
        public string RunDraft()
        {
            string results = "";

            List<Team> DraftOrder = new List<Team>();
            foreach (Team t in league.TeamList) DraftOrder.Add(t);

            DraftOrder = DraftOrder.OrderBy(o => o.Wins).ThenBy(o => o.TitleConteder).ToList();

            for (int i = 1; i < rounds + 1; i++)
            {
                results = results + "Round #" + i + " Results:" + Environment.NewLine;
                for (int j = 0; j < DraftOrder.Count; j++)
                {
                    // team selects player to draft
                    Player draftedPlayer = DraftOrder[j].DraftPlayer(rnd, league.DraftEntrants, league.Sport, rounds, j, i);
                    // player has info updated for Round # and Pick #
                    RecordPlayerDraftPosition(draftedPlayer, i, j);
                    // draftedPlayer receives rookie contract
                    draftedPlayer.Contract = CreateRookieContract(draftedPlayer);
                    // check for achivements for agent
                    DraftedPlayerAchievementCheck(i, j, draftedPlayer);
                    // add draftedPlayer to his respective team
                    AddPlayerToTeam(draftedPlayer, DraftOrder[j], i, j);
                    // remove player from draft pool
                    RemoveDraftedPlayerFromDraftPool(draftedPlayer);
                    // add selection to results to be printed later
                    results = results + i + "." + j + " - " + DraftOrder[j].Abbreviation + " selects " + draftedPlayer.Position.ToString() + " " + draftedPlayer.FullName + Environment.NewLine;
                }
            }

            RemoveUndraftedPlayersFromDraftPool();

            Console.WriteLine(results);

            league.DeclaredEntrants = false;

            return results;
        }
        public void RecordPlayerDraftPosition(Player draftedPlayer, int i, int j)
        {
            draftedPlayer.DraftRound = i;
            draftedPlayer.DraftPick = j;
        }
        public void DraftedPlayerAchievementCheck(int i, int j, Player draftedPlayer)
        {
            if (draftedPlayer.MemberOfAgency)
            {
                draftedPlayer.Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Gridiron Dream Realized")]);
                if (i == 1)
                {
                    draftedPlayer.Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Face of the Franchise: Football")]);
                    if (j == 1)
                        draftedPlayer.Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Future Gridiron Superstar")]);
                }
            }
        }
        public void RemoveUndraftedPlayersFromDraftPool()
        {
            for (int i = league.DraftEntrants.Count - 1; i >= 0; i--)
            {
                league.DraftEntrants[i].PlayerStatus = PlayerType.Active;
                league.DraftEntrants[i].PlayerType = PlayerType.Team;
                league.DraftEntrants[i].FreeAgent = true;
                league.FreeAgents.Add(league.DraftEntrants[i]);
                league.DraftEntrants.RemoveAt(i);
            }
        }
        public void AddPlayerToTeam(Player p, Team t, int round, int pick)
        {
            p.PlayerStatus = PlayerType.Active;
            p.PlayerType = PlayerType.Team;
            p.CareerStartYear = world.Year;
            p.Team = t;
            p.DetermineTeamHappiness(rnd, p.IsStarter);
            t.Roster.Add(p);
        }
        public void RemoveDraftedPlayerFromDraftPool(Player draftedPlayer)
        {
            int index = league.DraftEntrants.FindIndex(o => o == draftedPlayer);
            league.DraftEntrants.RemoveAt(index);
        }
        public Contract CreateRookieContract(Player player)
        {
            Contract contract = null;
            int salary = 0;
            int years = 0;
            int signingBonus = 0;

            if (player.DraftRound == 1)
            {
                years = 5;
                salary = 5500000 - ((player.DraftPick - 1) * 120000);
                signingBonus = Convert.ToInt32(salary * .1);
            }
            else if (player.DraftRound == 2)
            {
                years = 4;
                salary = 1320000 - ((player.DraftPick - 1) * 16250);
                signingBonus = Convert.ToInt32(salary * .05);
            }
            else if (player.DraftRound == 3 || player.DraftRound == 4)
            {
                years = 4;
                salary = 800000 - ((player.DraftPick - 1) * 6250);
            }
            else
            {
                years = 3;
                salary = 600000 - ((player.DraftPick - 1) * 5625);
            }

            contract = new Contract(years, salary, salary / 10, new Date(9, Months.October, 1), new Date(6, Months.July, 1), signingBonus, 3, PaySchedule.Monthly);

            return contract;
        }
    }
}
