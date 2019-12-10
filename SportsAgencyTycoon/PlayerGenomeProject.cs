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

            if (i <= 10) text = "Serious off field issues.";
            else if (i <= 20) text = "A social media menace.";
            else if (i <= 30) text = "Likes to push team rules.";
            else if (i <= 40) text = "Does what he's told but isn't happy about it.";
            else if (i <= 50) text = "A 'yes man' towards coaches, front office and law enforcement.";
            else if (i <= 70) text = "A family man when not at work.";
            else if (i <= 90) text = "Volunteers for charity in his free time.";
            else text = "An absolute saint of a man.";

            p.BehaviorDescription = text;
        }
        private void DetermineComposure(Player p, int i)
        {
            p.Composure = i;

            string text = "";

            if (i <= 10) text = "An ejection waiting to happen.";
            else if (i <= 30) text = "Let's calls effect his play.";
            else if (i <= 50) text = "Looks to coach for support against refs.";
            else if (i <= 60) text = "Emotionless while playing; very robotic.";
            else if (i <= 90) text = "Always keeps emotions in check.";
            else text = "Accepts any call against him.";

            p.ComposureDescription = text;
        }
        private void DetermineGreed(Player p, int i)
        {
            p.Greed = i;

            string text = "";

            if (i <= 20) text = "Better back up the Brinks truck!";
            else if (i <= 50) text = "Wants to be paid what he's worth.";
            else if (i <= 80) text = "Willing to negotiate a fair deal.";
            else if (i <= 90) text = "Willing to take a hometown discount.";
            else text = "Will accept any offer thrown his way.";

            p.GreedDescription = text;
        }
        private void DetermineLeadership(Player p, int i)
        {
            p.Leadership = i;

            string text = "";

            if (i <= 10) text = "A total diva.";
            else if (i <= 30) text = "Easily influenced by a strong influence whether bad or good.";
            else if (i <= 50) text = "A follower of the strongest personality in the locker room.";
            else if (i <= 60) text = "A loner on and off the field of play.";
            else if (i <= 80) text = "A team player that gets along with everyone.";
            else if (i <= 90) text = "The prototypical alpha male.";
            else text = "A player-coach type that's an extension of the coaching staff.";

            p.LeadershipDescription = text;
        }
        private void DetermineWorkEthic(Player p, int i)
        {
            p.WorkEthic = i;

            string text = "";

            if (i <= 10) text = "Known to skip training sessions.";
            else if (i <= 40) text = "Will show up to mandatory training sessions only.";
            else if (i <= 70) text = "Routinely will make offseason gains.";
            else if (i <= 90) text = "A gym rat before, during and after the season.";
            else text = "First person in the gym. Last person to leave. Every. Single. Day.";

            p.WorkEthicDescription = text;
        }
    }
}
