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
        public int EventsPlayed;
        public int CutsMade;
        public double TourPoints;
        public List<double> TourPointsList;
        public List<int> CurrentTournamentScores;
        public int CurrentScore;
        public int PlayoffScore;
        public bool MadeCut;
        public bool MadePlayoff;
        public int PlayoffHoles;
        public int Majors;

        public Golfer(Random rnd, int id, Sports sport, int age)
            : base(rnd, id, sport, age)
        {
            TournamentWins = 0;
            TopTenFinishes = 0;
            TourPoints = 0;
            TourPointsList = new List<double>();
            CurrentTournamentScores = new List<int>();
            CurrentScore = 0;
            Majors = 0;
        }
    }
}
