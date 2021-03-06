﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class MMAFighter : Player
    {
        public int Wins;
        public int Losses;
        public bool KnockedOut;

        public MMAFighter(Random rnd, int id, Sports sport, int age)
            : base(rnd, id, sport, age)
        {
            Wins = 0;
            Losses = 0;
            KnockedOut = false;
        }
    }
}
