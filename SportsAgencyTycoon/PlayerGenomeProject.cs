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
            Behavior = DetermineBehavior(rnd.Next(1, 101));
            Composure = DetermineComposure(rnd.Next(1, 101));
            Greed = DetermineGreed(rnd.Next(1, 101));
            Leadership = DetermineLeadership(rnd.Next(1, 101));
            WorkEthic = DetermineWorkEthic(rnd.Next(1, 101));

            p.PGP = Behavior.ToString() + Composure.ToString() + Greed.ToString() + Leadership.ToString() + WorkEthic.ToString();
        }
        private char DetermineBehavior(int i)
        {
            char A;

            if (i <= 15) A = Convert.ToChar("Y"); // off field/court issues
            else if (i <= 45) A = Convert.ToChar("X"); // pushes team rules
            else if (i <= 80) A = Convert.ToChar("B"); // first in, last out
            else A = Convert.ToChar("A"); // pro off & on court

            return A;
        }
        private char DetermineComposure(int i)
        {
            char A;

            if (i <= 15) A = Convert.ToChar("B"); // ejection waiting to happen
            else if (i <= 45) A = Convert.ToChar("A"); // let's calls effect him
            else if (i <= 80) A = Convert.ToChar("X"); // always under control
            else A = Convert.ToChar("Y"); // emotionless

            return A;
        }
        private char DetermineGreed(int i)
        {
            char A;

            if (i <= 20) A = Convert.ToChar("A"); // Brink's truck
            else if (i <= 65) A = Convert.ToChar("B"); // pay me what I'm worth
            else if (i <= 85) A = Convert.ToChar("X"); // hometown discount
            else A = Convert.ToChar("Y"); // I'll accept any offer

            return A;
        }
        private char DetermineLeadership(int i)
        {
            char A;

            if (i <= 10) A = Convert.ToChar("Y"); // total diva
            else if (i <= 50) A = Convert.ToChar("B"); // loner
            else if (i <= 00) A = Convert.ToChar("A"); // sheep
            else A = Convert.ToChar("X"); // alpha male

            return A;
        }
        private char DetermineWorkEthic(int i)
        {
            char A;

            if (i <= 10) A = Convert.ToChar("A"); // skips training sessions
            else if (i <= 40) A = Convert.ToChar("X"); // mandatory sessions only
            else if (i <= 85) A = Convert.ToChar("Y"); // off season gains
            else A = Convert.ToChar("B"); // gym rat

            return A;
        }
    }
}
