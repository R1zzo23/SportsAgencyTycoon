﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class TennisPlayer : Player
    {
        public int TournamentWins;
        public int QuarterFinals;
        public int GrandSlams;
        public int TourPoints;
        public List<int> TourPointsList;

        public TennisPlayer(Random rnd, int id, Sports sport, int age)
            : base(rnd, id, sport, age)
        {
            TournamentWins = 0;
            QuarterFinals = 0;
            GrandSlams = 0;
            TourPoints = 0;
            //array should be as large as the number of tennis events on the schedule
            TourPointsList = new List<int>();
        }
    }
}
