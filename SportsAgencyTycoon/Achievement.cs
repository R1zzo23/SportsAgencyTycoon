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
    }
}
