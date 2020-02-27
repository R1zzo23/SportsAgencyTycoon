﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportsAgencyTycoon
{
    public class FootballDraft
    {
        public Random rnd;
        public World world;
        public int rounds;
        public int numberOfEntrants;
        public League league;
        public FootballDraft(Random r, World w, League l)
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
                FootballPlayer f = new FootballPlayer(rnd, league.IdCount, Sports.Basketball, rnd.Next(21, 24), DeterminePosition());
                f.PlayerType = PlayerType.DraftEntrant;
                f.League = league;
                league.DraftEntrants.Add(f);
            }
            for (int i = 0; i < 5; i++)
            {
                FootballPlayer f = new FootballPlayer(rnd, league.IdCount, Sports.Football, rnd.Next(22, 25), Position.K);
                FootballPlayer p = new FootballPlayer(rnd, league.IdCount, Sports.Football, rnd.Next(22, 25), Position.P);
                f.PlayerType = PlayerType.DraftEntrant;
                f.League = league;
                league.DraftEntrants.Add(f);
                p.PlayerType = PlayerType.DraftEntrant;
                p.League = league;
                league.DraftEntrants.Add(p);
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
            int potential80count = 0;
            int potential70count = 0;
            int current80count = 0;
            int current70count = 0;
            int current60count = 0;

            int draftStrength = rnd.Next(1, 11);

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


            foreach (Player p in draftEntrants)
            {
                /*if (p.Age >= 21) p.PotentialSkill = p.CurrentSkill + rnd.Next(3, 20);
                else if (p.Age == 20) p.PotentialSkill = p.CurrentSkill + rnd.Next(8, 25);
                else if (p.Age == 19) p.PotentialSkill = p.CurrentSkill + rnd.Next(10, 30);
                else p.PotentialSkill = p.CurrentSkill + rnd.Next(15, 40);*/

                int nerf = rnd.Next(15 - draftStrength, 30 - draftStrength);
                p.CurrentSkill -= nerf;
                p.PotentialSkill -= nerf;

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

        }
        public Position DeterminePosition()
        {
            int position = rnd.Next(1, 6);
            if (position == 1) return Position.QB;
            else if (position == 2) return Position.RB;
            else if (position == 3) return Position.FB;
            else if (position == 4) return Position.WR;
            else if (position == 5) return Position.TE;
            else if (position == 6) return Position.OT;
            else if (position == 7) return Position.OG;
            else if (position == 8) return Position.C;
            else if (position == 9) return Position.DT;
            else if (position == 10) return Position.DE;
            else if (position == 11) return Position.LB;
            else if (position == 12) return Position.CB;
            else if (position == 13) return Position.FS;
            else return Position.SS;
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
                    Player draftedPlayer = DraftOrder[j].DraftPlayer(rnd, league.DraftEntrants, league.Sport, rounds, j, i);
                    draftedPlayer.DraftRound = i + 1;
                    draftedPlayer.DraftPick = j + 1;
                    draftedPlayer.Contract = CreateRookieContract(draftedPlayer);
                    if (draftedPlayer.MemberOfAgency)
                    {
                        draftedPlayer.Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Hoop Dream Realized")]);
                        if (i == 0)
                        {
                            draftedPlayer.Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Shake My Hand, Commish!")]);
                            if (j == 0)
                                draftedPlayer.Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Future Hoops Superstar")]);
                        }

                    }

                    AddPlayerToTeam(draftedPlayer, DraftOrder[j], i, j);
                    RemoveDraftedPlayerFromDraftPool(draftedPlayer);
                    results = results + i + "." + j + " - " + DraftOrder[j].Abbreviation + " selects " + draftedPlayer.Position.ToString() + " " + draftedPlayer.FullName + Environment.NewLine;
                }
            }
            for (int i = league.DraftEntrants.Count - 1; i >= 0; i--)
            {
                league.DraftEntrants[i].PlayerStatus = PlayerType.Active;
                league.DraftEntrants[i].PlayerType = PlayerType.Team;
                league.DraftEntrants[i].FreeAgent = true;
                league.FreeAgents.Add(league.DraftEntrants[i]);
                league.DraftEntrants.RemoveAt(i);
            }

            Console.WriteLine(results);

            league.DeclaredEntrants = false;

            return results;
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


            if (player.DraftRound == 1)
            {
                years = 4;
                salary = 9000000 - ((player.DraftPick - 1) * 250000);
            }
            else
            {
                if (player.DraftPick <= 15)
                {
                    years = 2;
                    salary = Convert.ToInt32(league.MinSalary * 1.5);
                }
                else
                {
                    years = 1;
                    salary = league.MinSalary;
                }
            }

            contract = new Contract(years, salary, salary / 10, new Date(9, Months.October, 1), new Date(6, Months.July, 1), 0, 3, PaySchedule.Monthly);

            return contract;
        }
    }
}
