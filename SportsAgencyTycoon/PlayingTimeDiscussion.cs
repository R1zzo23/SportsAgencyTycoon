using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsAgencyTycoon
{
    public class PlayingTimeDiscussion
    {
        private Random rnd;
        private bool TitleContender = false;
        private bool PlayoffTeam = false;
        private bool InTheHunt = false;
        private bool Rebuilding = false;
        private bool Tanking = false;
        private int GMCertainty = 100;
        private bool GMAgreed = false;
        private World world;
        private Team team;
        private League league;
        private Player player;
        private Player starter;
        private List<Player> playersAtPosition = new List<Player>();
        public PlayingTimeDiscussion(Random r, World w, Team t, League l, Player p)
        {
            rnd = r;
            world = w;
            team = t;
            league = l;
            player = p;
            ResolveBools();
            PlayersAtPosition();
            DetermineStarterInFrontOfClient();
        }
        private void PlayersAtPosition()
        {
            foreach (Player p in team.Roster)
                if (p.Position == player.Position && p.DepthChart < player.DepthChart)
                    playersAtPosition.Add(p);

            playersAtPosition = playersAtPosition.OrderBy(o => o.DepthChart).ToList();
        }
        private void DetermineStarterInFrontOfClient()
        {
            if (player.DepthChart != 1)
                starter = playersAtPosition[player.DepthChart - 2];
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

            //want best players playing
            if (playersAtPosition[player.DepthChart - 2].CurrentSkill > player.CurrentSkill)
            {
                response = "Sorry but we are pushing for a title and need the best players starting.";
                GMCertainty -= (starter.CurrentSkill - player.CurrentSkill);
            }
            else
            {
                response = "We need the best players starting for us. Let's get your client out there!";
                ChangeDepthChartPositions();
            }

            return response;
        }
        private string PlayoffTeamPT()
        {
            string response = "";

            //want best players playing, but could shake things up
            if (starter.CurrentSkill > player.CurrentSkill)
            {
                int diceRoll = DiceRoll();
                if (diceRoll <= 5)
                {
                    response = "We could use a bit of a shakeup for this playoff push. Let's do it!";
                    ChangeDepthChartPositions();
                }
                else if (diceRoll <= 10)
                {
                    response = "We are in the postseason as of now and want to keep it that way.";
                    GMCertainty = GMCertainty -= (starter.CurrentSkill - player.CurrentSkill);
                }
                else
                {
                    response = "Don't ask! We are in the postseason and I'm not risking losing those extra gates!";
                    GMCertainty = 100;
                }
                response = "Sorry but we are pushing for a title and need the best players starting.";
                GMCertainty -= (starter.CurrentSkill - player.CurrentSkill);
            }
            else
            {
                response = "We need the best players starting for us. Let's get your client out there!";
                ChangeDepthChartPositions();
            }

            return response;
        }
        private string InTheHuntPT()
        {
            string response = "";

            int youthRoll = DiceRoll();
            // fighting for postseason but want to do it with youth if they can contribute now
            if (youthRoll <= 6)
            {
                if (player.Age < starter.Age)
                {
                    //player is younger but almost as good as starter
                    if (player.CurrentSkill + 10 >= starter.CurrentSkill)
                    {
                        response = "I think it would be exciting to make the dance with our youth! Let's give it a shot.";
                        ChangeDepthChartPositions();
                    }
                    //player is younger but not close in terms of currentSkill
                    else
                    {
                        response = "I'd like to go young but " + player.FirstName + " isn't ready to get us there just yet.";
                        GMCertainty -= (starter.CurrentSkill - player.CurrentSkill);
                    }
                }
                else
                {
                    //is starter younger but not ready to contribute?
                    if (player.CurrentSkill >= starter.CurrentSkill + 10)
                    {
                        response = "Adding your client to the lineup may be the boost we need. We are willing to try it out.";
                        ChangeDepthChartPositions();
                    }
                    // starter is both younger and better than client
                    else
                    {
                        response = starter.FullName + " is younger and better. He's the guy we are sticking with.";
                        GMCertainty -= ((starter.CurrentSkill - player.CurrentSkill) / (player.Age - starter.Age));
                    }
                }
            }
            // youth doesn't matter, so just use PlayoffTeamPT method
            else
            {
                response = PlayoffTeamPT();
            }

            return response;
        }
        private string RebuildingPT()
        {
            string response = "";

            //is player much younger than starter
            if (player.Age <= starter.Age - 4)
            {
                if (starter.PopularityDescription == PopularityDescription.ExtremelyPopular ||
                    starter.PopularityDescription == PopularityDescription.Superstar)
                {
                    response = "We might be losing but still need to sell tickets." + starter.FullName + " does just that!";
                    GMCertainty = 100;
                }
                else if (player.PotentialSkill > starter.CurrentSkill)
                {
                    response = "We see promise in " + player.FullName + " and need to see him play.";
                    ChangeDepthChartPositions();
                }
                else
                {
                    int roll = DiceRoll();
                    if (roll <= 9)
                    {
                        response = "I guess we aren't going anywhere and need to see what " + player.FirstName + " can do...";
                        ChangeDepthChartPositions();
                    }
                    else
                    {
                        response = "Coach likes what " + starter.FullName + " brings every night. We will stick it out for now.";
                        GMCertainty -= (starter.CurrentSkill - player.PotentialSkill) * 2;
                    }
                }
            }
            else if (player.Age <= starter.Age)
            {
                if (player.PotentialSkill > starter.PotentialSkill)
                {
                    if (starter.Popularity > player.Popularity + 20)
                    {
                        response = starter.FirstName + " is a fan favorite here and we need to keep rolling him out there.";
                        GMCertainty -= (starter.Popularity - player.Popularity);
                    }
                    else
                    {
                        response = player.FullName + " could help us push for a youth movement. Let's give it a shot.";
                        ChangeDepthChartPositions();
                    }
                }
            }
            else
            {
                if (starter.PotentialSkill >= player.CurrentSkill)
                {
                    response = "Sorry but we are sticking with the kid ahead of you.";
                    GMCertainty = 90;
                }
                else if (starter.Popularity + 15 > player.Popularity)
                {
                    response = starter.FullName + " is younger and the fans want to see him play. Sorry.";
                    GMCertainty -= ((starter.Popularity + 15) - player.Popularity);
                }
                else
                {
                    int roll = DiceRoll();
                    if (roll <= 9)
                    {
                        response = "The kid stays ahead of you. Simple as that.";
                        GMCertainty -= (player.CurrentSkill - starter.PotentialSkill);
                    }
                }
            }

            return response;
        }
        private string TankingPT()
        {
            string response = "";

            if (starter.Age < player.Age)
            {
                response = "We are playing all the younger players so the lineup stays as is!";
                GMCertainty = 95;
            }
            else
            {
                response = "Let's see if we can squeeze some potential out of your guy.";
                ChangeDepthChartPositions();
            }

            return response;
        }

        public int OutputGMCertainty()
        {
            return GMCertainty;
        }

        private void ChangeDepthChartPositions()
        {
            starter.DepthChart++;
            player.DepthChart--;
            GMAgreed = true;
        }
        public bool OutputGMAgreed()
        {
            return GMAgreed;
        }
        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
