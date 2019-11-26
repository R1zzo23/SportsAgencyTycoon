using System;
namespace SportsAgencyTycoon
{
    public class RelationshipWithTeam
    {
        public Team Team;
        public int Relationship;
        public bool TeamWillingToWorkWithAgent;
        public RelationshipWithTeam(Team t, int relationship)
        {
            Team = t;
            Relationship = relationship;
            TeamWillingToWorkWithAgent = true;
        }
    }
}
