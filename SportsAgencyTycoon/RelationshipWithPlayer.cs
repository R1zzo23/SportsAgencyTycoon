using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class RelationshipWithPlayer
    {
        public int relationshipNumber;
        public RelationshipDescription relationshipDescription;
        public Player Teammate;
        public Random rnd;
        public int categoryChoice;
        public int playerRating = 0;
        public int teammateRating = 0;
        public List<Interaction> Interactions = new List<Interaction>();
        public RelationshipWithPlayer(Player p, Random r)
        {
            rnd = r;
            Teammate = p;
            relationshipNumber = 50;
            relationshipDescription = DescribeRelationship(relationshipNumber);
        }
        private RelationshipDescription DescribeRelationship(int i)
        {
            RelationshipDescription description;

            if (i <= 10) description = RelationshipDescription.Toxic;
            else if (i <= 25) description = RelationshipDescription.Poor;
            else if (i <= 35) description = RelationshipDescription.Decent;
            else if (i <= 55) description = RelationshipDescription.Fine;
            else if (i <= 70) description = RelationshipDescription.Good;
            else if (i <= 85) description = RelationshipDescription.Great;
            else description = RelationshipDescription.Excellent;

            return description;
        }
        public void DetermineInteraction(Player player)
        {
            DetermineCategory(player);
        }
        private void DetermineCategory(Player player)
        {
            categoryChoice = rnd.Next(1, 5);

            // Behavior check
            if (categoryChoice == 1)
            {
                playerRating = player.Behavior;
                teammateRating = Teammate.Behavior;
            }
            // Composure check
            else if (categoryChoice == 2)
            {
                playerRating = player.Composure;
                teammateRating = Teammate.Composure;
            }
            // Leadership check
            else if (categoryChoice == 3)
            {
                playerRating = player.Leadership;
                teammateRating = Teammate.Leadership;
            }
            // Work Ethic check
            else if (categoryChoice == 4)
            {
                playerRating = player.WorkEthic;
                teammateRating = Teammate.WorkEthic;
            }

        }
        private void GoodOrBadInteraction()
        {
            Interaction interaction;
            int ratingDifference = Math.Abs(playerRating - teammateRating);
            int interactionCheck = rnd.Next(1, 101);
            if (interactionCheck <= ratingDifference)
            {
                if (categoryChoice == 1)
                    interaction = new Interaction("Behavior", "")

            }
            else
            {
                // good interaction
            }
        }
    }
}
