using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class PlayerGenomeProject
    {
        public char Athletics;
        public char Behavior;
        public char Composure;
        public char Greed;
        public char Intelligence;
        public char Leadership;
        public char Skills;
        public char WorkEthic;
        public Random rnd;
        public string CreatePGP(Random r)
        {
            string PGP = "";

            rnd = r;
            Athletics = DetermineAthletics(rnd.Next(1, 11));
            Behavior = DetermineBehavior(rnd.Next(1, 6));
            Composure = DetermineComposure(rnd.Next(1, 6));
            Greed = DetermineGreed(rnd.Next(1, 6));
            Intelligence = DetermineIntelligence(rnd.Next(1, 6));
            Leadership = DetermineLeadership(rnd.Next(1, 6));
            Skills = DetermineSkills(rnd.Next(1, 11));
            WorkEthic = DetermineWorkEthic(rnd.Next(1, 11));

            PGP = Athletics.ToString() + Behavior.ToString() + Composure.ToString() + Greed.ToString() + Intelligence.ToString() + Leadership.ToString() + Skills.ToString() + WorkEthic.ToString();
        }
        private char DetermineAthletics(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("W");
            else if (i == 2) A = Convert.ToChar("A");
            else if (i == 3) A = Convert.ToChar("S");
            else if (i == 4) A = Convert.ToChar("T");
            else if (i == 5) A = Convert.ToChar("Z");
            else if (i == 6) A = Convert.ToChar("C");
            else if (i == 7) A = Convert.ToChar("G");
            else if (i == 8) A = Convert.ToChar("X");
            else if (i == 9) A = Convert.ToChar("B");
            else A = Convert.ToChar("Y");

            return A;
        }
        private char DetermineBehavior(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("A");
            else if (i == 2) A = Convert.ToChar("T");
            else if (i == 3) A = Convert.ToChar("G");
            else if (i == 4) A = Convert.ToChar("C");
            else A = Convert.ToChar("Z");

            return A;
        }
        private char DetermineComposure(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("X");
            else if (i == 2) A = Convert.ToChar("W");
            else if (i == 3) A = Convert.ToChar("Y");
            else if (i == 4) A = Convert.ToChar("S");
            else A = Convert.ToChar("B");

            return A;
        }
        private char DetermineGreed(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("G");
            else if (i == 2) A = Convert.ToChar("Y");
            else if (i == 3) A = Convert.ToChar("B");
            else if (i == 4) A = Convert.ToChar("A");
            else A = Convert.ToChar("X");

            return A;
        }
        private char DetermineIntelligence(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("Z");
            else if (i == 2) A = Convert.ToChar("C");
            else if (i == 3) A = Convert.ToChar("A");
            else if (i == 4) A = Convert.ToChar("W");
            else A = Convert.ToChar("T");

            return A;
        }
        private char DetermineLeadership(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("B");
            else if (i == 2) A = Convert.ToChar("Y");
            else if (i == 3) A = Convert.ToChar("S");
            else if (i == 4) A = Convert.ToChar("T");
            else A = Convert.ToChar("A");

            return A;
        }
        private char DetermineSkills(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("G");
            else if (i == 2) A = Convert.ToChar("C");
            else if (i == 3) A = Convert.ToChar("A");
            else if (i == 4) A = Convert.ToChar("T");
            else if (i == 5) A = Convert.ToChar("Y");
            else if (i == 6) A = Convert.ToChar("X");
            else if (i == 7) A = Convert.ToChar("Z");
            else if (i == 8) A = Convert.ToChar("W");
            else if (i == 9) A = Convert.ToChar("B");
            else A = Convert.ToChar("S");

            return A;
        }
        private char DetermineWorkEthic(int i)
        {
            char A;

            if (i == 1) A = Convert.ToChar("A");
            else if (i == 2) A = Convert.ToChar("B");
            else if (i == 3) A = Convert.ToChar("C");
            else if (i == 4) A = Convert.ToChar("X");
            else if (i == 5) A = Convert.ToChar("Y");
            else if (i == 6) A = Convert.ToChar("Z");
            else if (i == 7) A = Convert.ToChar("G");
            else if (i == 8) A = Convert.ToChar("S");
            else if (i == 9) A = Convert.ToChar("T");
            else A = Convert.ToChar("W");

            return A;
        }
    }
}
