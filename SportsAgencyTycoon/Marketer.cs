using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Marketer
    {
        public string First;
        public string Last;
        public int Salary;
        public int CareerEarnings;
        public int Creativity;
        public int Outreach;
        public int Strategy;
        public int Level;
        public Roles Role;

        public Marketer(string firstName, string lastName, int salary, int creativity, int outreach, int strategy, int levelRating, Roles role)
        {
            First = firstName;
            Last = lastName;
            Salary = salary;
            CareerEarnings = 0;
            Creativity = creativity;
            Outreach = outreach;
            Strategy = strategy;
            Level = levelRating;
            Role = role;
        }
    }
}
