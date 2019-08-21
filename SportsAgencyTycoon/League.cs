﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class League
    {
        public bool Initialized;
        public Sports Sport;
        public string Name;
        public string Abbreviation;
        public List<Team> TeamList;
        public int Popularity;
        public Date SeasonStart;
        public Date SeasonEnd;
        public int MonthsInSeason;
        public int MaxSalary;
        public int MinSalary;
        public List<Player> FreeAgents = new List<Player>();
        public bool InSeason;
        public int IdCount = 0;

        public League(Sports sport, string name, string abbreviation, int popularity, Date seasonStart, Date seasonEnd, int monthsInSeason, int maxSalary, int minSalary)
        {
            Initialized = false;
            Sport = sport;
            Name = name;
            Abbreviation = abbreviation;
            TeamList = new List<Team>();
            FreeAgents = new List<Player>();
            Popularity = popularity;
            SeasonStart = seasonStart;
            SeasonEnd = seasonEnd;
            MonthsInSeason = monthsInSeason;
            MaxSalary = maxSalary;
            MinSalary = minSalary;
        }

        public void AddPlayer(List<Player> l, Player p)
        {
            l.Add(p);
            IdCount++;
        }
    }
}
