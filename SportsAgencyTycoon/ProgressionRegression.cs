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
        public double over31Modifier;
        public double over30Modifier;
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
            modifierSum = ageModifier + workEthicModifier + starterModifier + over30Modifier + over31Modifier;
        }
        public void DetermineAgeModifier(Player player)
        {
            ageModifier = (30 - player.Age) / 75;

            if (player.Age > 30)
            {
                over30Modifier = -0.05;
            }
                
            else over30Modifier = 0.00;

            if (player.Age > 31)
            {
                over31Modifier = over30Modifier * (player.Age - 31) * 5;
                if (skillDifference == 0) skillDifference = player.Age - 30;
            }                
            else over31Modifier = 0.00;
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
    }
}
