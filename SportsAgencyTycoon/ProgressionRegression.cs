using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class ProgressionRegression
    {
        public Random rnd;
        public double ageModifier;
        public double workEthicModifier;
        public double starterModifier;
        public double regressionAgeModifier;
        public double pastPrimeModifier;
        public int primeAge;
        public int regressionAge;
        public int skillDifference;
        public double modifierSum;
        public int diceRoll;

        public ProgressionRegression(Random r)
        {
            rnd = r;
        }

        public void PlayerProgression(Player player)
        {
            int skillAdded;

            DeterminePrimeAndRegressionAges(player.Sport);
            DetermineSkillDifference(player);
            DetermineAgeModifier(player);
            DetermineStarterModifier(player);
            DetermineWorkEthicModifier(player);
            ModifierSum();
            DiceRollForBoost(player);

            int current = player.CurrentSkill;
            player.PreviousCurrentSkill = current;

            skillAdded = Convert.ToInt32(modifierSum * skillDifference) + diceRoll;
            player.CurrentSkill += skillAdded;
            if (player.CurrentSkill > player.PotentialSkill) player.CurrentSkill = player.PotentialSkill;
            if (player.CurrentSkill < 1) player.CurrentSkill = 1;
            ShouldPlayerRetire(player);
        }
        public void DeterminePrimeAndRegressionAges(Sports sport)
        {
            if (sport == Sports.Basketball)
            {
                primeAge = 30;
                regressionAge = 31;
            }
            else if (sport == Sports.Baseball)
            {
                primeAge = 28;
                regressionAge = 32;
            }
            else if (sport == Sports.Football)
            {
                primeAge = 28;
                regressionAge = 30;
            }
            else if (sport == Sports.Hockey)
            {
                primeAge = 27;
                regressionAge = 31;
            }
            else if (sport == Sports.Soccer)
            {
                primeAge = 25;
                regressionAge = 29;
            }
            else if (sport == Sports.Golf)
            {
                primeAge = 35;
                regressionAge = 45;
            }
            else if (sport == Sports.Tennis)
            {
                primeAge = 25;
                regressionAge = 28;
            }
            else if (sport == Sports.MMA)
            {
                primeAge = 24;
                regressionAge = 27;
            }
            else if (sport == Sports.Boxing)
            {
                primeAge = 26;
                regressionAge = 28;
            }
        }
        public void DiceRollForBoost(Player player)
        {
            bool gettingBoost = false;
            int boostCheck = rnd.Next(1, 7);
            if (boostCheck == 6) gettingBoost = true;

            if (gettingBoost)
                diceRoll = rnd.Next(1, 7);
            else
                diceRoll = 0;
        }
        public void DetermineSkillDifference(Player player)
        {
            skillDifference = player.PotentialSkill - player.CurrentSkill;
        }
        public void ModifierSum()
        {
            modifierSum = ageModifier + workEthicModifier + starterModifier + pastPrimeModifier + regressionAgeModifier;
        }
        public void DetermineAgeModifier(Player player)
        {
            ageModifier = (primeAge - player.Age) / 75;

            if (player.Age > primeAge)
            {
                pastPrimeModifier = -0.05;
            }
                
            else pastPrimeModifier = 0.00;

            if (player.Age > regressionAge)
            {
                regressionAgeModifier = pastPrimeModifier * (player.Age - regressionAge) * 5;
                if (skillDifference == 0) skillDifference = player.Age - primeAge;
            }                
            else regressionAgeModifier = 0.00;
        }
        public void DetermineWorkEthicModifier(Player player)
        {
            if (player.WorkEthicDescription == WorkEthicDescription.SkipsTraining)
                workEthicModifier = -0.2;
            else if (player.WorkEthicDescription == WorkEthicDescription.MandatarySessionsOnly)
                workEthicModifier = 0.0;
            else if (player.WorkEthicDescription == WorkEthicDescription.OffSeasonGains)
                workEthicModifier = 0.01;
            else if (player.WorkEthicDescription == WorkEthicDescription.GymRat)
                workEthicModifier = 0.05;
            else workEthicModifier = 0.08;
        }
        public void DetermineStarterModifier(Player player)
        {
            if (player.IsStarter)
                starterModifier = 0.05;
            else starterModifier = 0.00;
        }
        public void ShouldPlayerRetire(Player player)
        {
            if (player.PlayerType == PlayerType.Team)
            {
                if (!player.FreeAgent)
                {
                    if (player.CurrentSkill <= 30)
                    {
                        if (player.Contract.Years < 3)
                        {
                            if (player.Contract.YearlySalary < 5000000)
                            {
                                player.Retiring = true;
                            }
                        }
                        else if (player.Contract.Years <2)
                        {
                            if (player.Contract.YearlySalary < 7000000)
                            {
                                player.Retiring = true;
                            }
                        }
                    }
                    else if (player.CurrentSkill <= 20)
                    {
                        if (player.Contract.Years == 0)
                        {
                            player.Retiring = true;
                        }
                        else if (player.Contract.Years == 1)
                        {
                            if (player.Contract.YearlySalary < 5000000)
                            {
                                player.Retiring = true;
                            }
                        }
                    }
                    else if (player.CurrentSkill <= 15)
                    {
                        player.Retiring = true;
                    }
                }
                else
                {
                    if (player.CurrentSkill < 25) player.Retiring = true;
                }
            }
        }
    }
}
