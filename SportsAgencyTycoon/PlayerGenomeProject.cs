using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class PlayerGenomeProject
    {
        public char Behavior;
        public char Composure;
        public char Greed;
        public char Leadership;
        public char WorkEthic;
        public Random rnd;
        public void CreatePGP(Random r, Player p)
        {
            //string PGP = "";

            rnd = r;
            DetermineBehavior(p, rnd.Next(1, 101));
            DetermineComposure(p, rnd.Next(1, 101));
            DetermineGreed(p, rnd.Next(1, 101));
            DetermineLeadership(p, rnd.Next(1, 101));
            DetermineWorkEthic(p, rnd.Next(1, 101));
        }
        private void DetermineBehavior(Player p, int i)
        {
            p.Behavior = i;

            string text = "";

            if (i <= 10)
            {
                text = "Serious off field issues.";
                p.BehaviorDescription = BehaviorDescription.OffFieldIssues;
            }
            else if (i <= 20)
            {
                text = "A social media menace.";
                p.BehaviorDescription = BehaviorDescription.SocialMediaMenace;
            }
            else if (i <= 30)
            {
                text = "Likes to push team rules.";
                p.BehaviorDescription = BehaviorDescription.PushesTeamRules;
            }
            else if (i <= 40)
            {
                text = "Does what he's told but isn't happy about it.";
                p.BehaviorDescription = BehaviorDescription.ReluctantlyListens;
            }
            else if (i <= 50)
            {
                text = "A 'yes man' towards coaches, front office and law enforcement.";
                p.BehaviorDescription = BehaviorDescription.YesMan;
            }
            else if (i <= 70)
            {
                text = "A family man when not at work.";
                p.BehaviorDescription = BehaviorDescription.FamilyMan;
            } 
            else if (i <= 90)
            {
                text = "Volunteers for charity in his free time.";
                p.BehaviorDescription = BehaviorDescription.CharityVolunteer;
            }
            else
            {
                text = "An absolute saint of a man.";
                p.BehaviorDescription = BehaviorDescription.Saint;
            }
                
            p.BehaviorString = text;
        }
        private void DetermineComposure(Player p, int i)
        {
            p.Composure = i;

            string text = "";

            if (i <= 10)
            {
                text = "An ejection waiting to happen.";
                p.ComposureDescription = ComposureDescription.EjectionWaitingToHappen;
            }
            else if (i <= 30)
            {
                text = "Let's calls effect his play.";
                p.ComposureDescription = ComposureDescription.EffectedByCalls;
            }
            else if (i <= 50)
            {
                text = "Looks to coach for support against refs.";
                p.ComposureDescription = ComposureDescription.NeedsCoachControl;
            }
            else if (i <= 60)
            {
                text = "Emotionless while playing; very robotic.";
                p.ComposureDescription = ComposureDescription.Emotionless;
            }
            else if (i <= 90)
            {
                text = "Always keeps emotions in check.";
                p.ComposureDescription = ComposureDescription.Composed;
            }
            else
            {
                text = "Accepts any call against him.";
                p.ComposureDescription = ComposureDescription.AcceptsCalls;
            }

            p.ComposureString = text;
        }
        private void DetermineGreed(Player p, int i)
        {
            p.Greed = i;

            string text = "";

            if (i <= 20)
            {
                text = "Better back up the Brinks truck!";
                p.GreedDescription = GreedDescription.BrinksTruck;
            }
            else if (i <= 50)
            {
                text = "Wants to be paid what he's worth.";
                p.GreedDescription = GreedDescription.PayMe;
            }
            else if (i <= 80)
            {
                text = "Willing to negotiate a fair deal.";
                p.GreedDescription = GreedDescription.WillingToNegotiate;
            }
            else if (i <= 90)
            {
                text = "Willing to take a hometown discount.";
                p.GreedDescription = GreedDescription.HometownDiscount;
            }
            else
            {
                text = "Will accept any offer thrown his way.";
                p.GreedDescription = GreedDescription.AcceptAnyOffer;
            }

            p.GreedString = text;
        }
        private void DetermineLeadership(Player p, int i)
        {
            p.Leadership = i;

            string text = "";

            if (i <= 10)
            {
                text = "A total diva.";
                p.LeadershipDescription = LeadershipDescription.Diva;
            }
            else if (i <= 30)
            {
                text = "Easily influenced by a strong personality whether bad or good.";
                p.LeadershipDescription = LeadershipDescription.Sheep;
            }
            else if (i <= 50)
            {
                text = "A follower of the strongest personality in the locker room.";
                p.LeadershipDescription = LeadershipDescription.Follower;
            }
            else if (i <= 60)
            {
                text = "A loner on and off the field of play.";
                p.LeadershipDescription = LeadershipDescription.Loner;
            }
            else if (i <= 80)
            {
                text = "A team player that gets along with everyone.";
                p.LeadershipDescription = LeadershipDescription.TeamPlayer;
            }
            else if (i <= 90)
            {
                text = "The prototypical alpha male.";
                p.LeadershipDescription = LeadershipDescription.AlphaMale;
            }
            else
            {
                text = "A player-coach type that's an extension of the coaching staff.";
                p.LeadershipDescription = LeadershipDescription.PlayerCoach;
            }

            p.LeadershipString = text;
        }
        private void DetermineWorkEthic(Player p, int i)
        {
            p.WorkEthic = i;

            string text = "";

            if (i <= 10)
            {
                text = "Known to skip training sessions.";
                p.WorkEthicDescription = WorkEthicDescription.SkipsTraining;
            }
            else if (i <= 40)
            {
                text = "Will show up to mandatory training sessions only.";
                p.WorkEthicDescription = WorkEthicDescription.MandatarySessionsOnly;
            }
            else if (i <= 70)
            {
                text = "Routinely will make offseason gains.";
                p.WorkEthicDescription = WorkEthicDescription.OffSeasonGains;
            }
            else if (i <= 90)
            {
                text = "A gym rat before, during and after the season.";
                p.WorkEthicDescription = WorkEthicDescription.GymRat;
            }
            else
            {
                text = "First person in the gym. Last person to leave. Every. Single. Day.";
                p.WorkEthicDescription = WorkEthicDescription.FirstInLastOut;
            }

            p.WorkEthicString = text;
        }
    }
}
