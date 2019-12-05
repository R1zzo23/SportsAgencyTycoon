using System;
namespace SportsAgencyTycoon
{
    public class Achievement
    {
        public string Name;
        public string Description;
        public bool Visible;
        public int Level;
        public int PointsToBoost;
        public string AttributeToBoost;

        public Achievement(string name, string description, int level, int pointsToBoost, string attributeToBoost)
        {
            Name = name;
            Description = description;
            Visible = false;
            Level = level;
            PointsToBoost = pointsToBoost;
            AttributeToBoost = attributeToBoost;
        }

        public void CreateGlobalAchievements(World world)
        {
            // sign your first client (Agent only)
            world.GlobalAchievements.Add(new Achievement("Sign 1st Client", "Agent has signed their 1st client.", 1, 2, "IndustryPower"));
            // sign your first agent (Agency only)
            world.GlobalAchievements.Add(new Achievement("Sign 1st Agent", "Agency has signed its 1st agent.", 1, 3, "IndustryInfluence"));
            // sign your first marketer (Agency only)
            world.GlobalAchievements.Add(new Achievement("Sign 1st Marketer", "Agency has signed its first marketer.", 1, 3, "IndustryInfluence"));
            // work for clients in 3 different sports (Agent Only)
            world.GlobalAchievements.Add(new Achievement("Athletically Diversified", "Agent has clients in 3 different sports.", 2, 3, "IndustyPower"));
            //
        }
    }
}
