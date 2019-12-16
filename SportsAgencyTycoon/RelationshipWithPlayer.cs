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
        public Player Client;
        public Player Teammate;
        public Random rnd;
        public int categoryChoice;
        public int playerRating = 0;
        public int teammateRating = 0;
        public bool goodInteraction;
        public List<Interaction> Interactions = new List<Interaction>();
        public RelationshipWithPlayer(Player client, Player t, Random r)
        {
            rnd = r;
            Client = client;
            Teammate = t;
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
            goodInteraction = GoodInteraction();
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
        private bool GoodInteraction()
        {
            int ratingDifference = Math.Abs(playerRating - teammateRating);
            int interactionCheck = rnd.Next(1, 101);
            if (interactionCheck <= ratingDifference)
                return false;
            else return true;
        }
        private string GoodOrBadInteraction()
        {
            string message = "";

            // bad interaction
            if (!goodInteraction)
            {
                //Behavior
                if (categoryChoice == 1)
                {
                    if (playerRating < teammateRating)
                        message = Client.FullName + " made disparaging comments about " + Teammate.FullName + "'s personal life.";
                    else message = Teammate.FullName + " made disparaging comments about " + Client.FullName + "'s personal life.";
                }
                //Composure
                else if (categoryChoice == 2)
                {
                    if (playerRating < teammateRating)
                        message = Teammate.FullName + " confronted " + Client.FullName + " about costing the team the game due to losing his cool.";
                    else message = Client.FullName + " confronted " + Teammate.FullName + " about costing the team the game due to losing his cool.";
                }
                //Leadership
                else if (categoryChoice == 3)
                {
                    if (playerRating < teammateRating)
                        message = Teammate.FullName + " called out " + Client.FullName + " for not stepping up to be more of a role model on the team.";
                    else message = Client.FullName + " called out " + Teammate.FullName + " for not stepping up to be more of a role model on the team.";
                }
                //Work Ethic
                else if (categoryChoice == 4)
                {
                if (playerRating < teammateRating)
                    message = Teammate.FullName + " questioned " + Client.FullName + "'s commitment for not putting in extra work in the weight room.";
                else message = Client.FullName + " questioned " + Teammate.FullName + "'s commitment for not putting in extra work in the weight room.";
                }

            }
            // Good Interaction
            else
            {
                //Behavior
                if (categoryChoice == 1)
                {
                    if (Client.BehaviorDescription == BehaviorDescription.CharityVolunteer)
                        message = Client.FullName + " brought " + Teammate.FullName + " to volunteer with him at his favorite charity.";
                    else if (Client.BehaviorDescription == BehaviorDescription.FamilyMan)
                        message = Client.FullName + " invited " + Teammate.FullName + " and his family over for dinner.";
                    else if (Client.BehaviorDescription == BehaviorDescription.Saint)
                        message = Client.FullName + " has taken " + Teammate.FullName + " under his wing and has been mentoring him.";
                    else message = Client.FullName + " has been spending time away from the team with " + Teammate.FullName;
                }
                //Composure
                else if (categoryChoice == 2)
                {
                    if (Client.ComposureDescription == ComposureDescription.AcceptsCalls)
                        message = Client.FullName + " has been a calming influence over " + Teammate.FullName + " when calls don't go his way.";
                    else message = Client.FullName + " saved " + Teammate.FullName + " from getting tossed from the last game.";
                }
                //Leadership
                else if (categoryChoice == 3)
                {
                    if (Client.LeadershipDescription == LeadershipDescription.PlayerCoach)
                        message = Client.FullName + " organized an extra film session with " + Teammate.FullName;
                    else if (Client.LeadershipDescription == LeadershipDescription.AlphaMale)
                        message = Client.FullName + " has been boosting " + Teammate.FullName + "'s confidence in games and at practice.";
                    else if (Client.LeadershipDescription == LeadershipDescription.TeamPlayer)
                        message = Client.FullName + " stuck up for " + Teammate.FullName + " when the media questioned " + Teammate.FirstName + "'s most recent struggles";
                    else message = Client.FullName + " has been playfully competing with " + Teammate.FullName + " before and after practice.";
                }
                //Work Ethic
                else if (categoryChoice == 4)
                {
                    if (Client.WorkEthicDescription == WorkEthicDescription.FirstInLastOut)
                        message = Client.FullName + " has been getting " + Teammate.FullName + " to stay after practice to work on a few skills.";
                    else if (Client.WorkEthicDescription == WorkEthicDescription.GymRat)
                        message = Client.FullName + " has been pushing " + Teammate.FullName + " in the gym with extra workout sessions.";
                    else message = Client.FullName + " and " + Teammate.FullName + " have been pushing each other during drills at practice.";
                }
            }

            return message;
        }
    }
}
