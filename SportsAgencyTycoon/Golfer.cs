﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Golfer : Player
    {
        public int TournamentWins;
        public int TopTenFinishes;
        public double TourPoints;
        public List<int> TourPointsList;
        public List<int> CurrentTournamentScores;
        public int CurrentScore;
        public bool MadeCut;
        public bool MadePlayoff;
        public int PlayoffHoles;

        public Golfer(Sports sport, FirstName firstName, LastName lastName, int skillLevel, int age, Months birthMonth, int birthWeek) 
            : base(sport, firstName, lastName, skillLevel, age, birthMonth, birthWeek)
        {
            TournamentWins = 0;
            TopTenFinishes = 0;
            TourPoints = 0;
            TourPointsList = new List<int>();
            CurrentTournamentScores = new List<int>();
            CurrentScore = 0;
        }
    }
}
