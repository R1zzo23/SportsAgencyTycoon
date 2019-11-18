using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Team
    {
        public string City;
        public string Mascot;
        public string Abbreviation;
        public string Conference;
        public string Division;
        public int MarketValue;
        public int TitleConteder;
        public int Wins;
        public int WinsThisWeek;
        public int ConferenceWins;
        public int DivisionWins;
        public int Losses;
        public int Ties;
        public int OTLosses;
        public int Points;
        public int ConferenceLosses;
        public int DivisionLosses;
        public bool PlayedGameThisCycle;
        public List<Player> Roster = new List<Player>();
        public List<Award> Awards = new List<Award>();

        public Team (string city, string mascot, string abbreviation, int marketValue, int titleContender)
        {
            City = city;
            Mascot = mascot;
            Abbreviation = abbreviation;
            MarketValue = marketValue;
            TitleConteder = titleContender;
            Wins = 0;
            ConferenceWins = 0;
            DivisionWins = 0;
            Losses = 0;
            ConferenceLosses = 0;
            DivisionLosses = 0;
        }
        public Team(string city, string mascot, string conference, string division, string abbreviation, int marketValue, int titleContender)
        {
            City = city;
            Mascot = mascot;
            Conference = conference;
            Division = division;
            Abbreviation = abbreviation;
            MarketValue = marketValue;
            TitleConteder = titleContender;
            Wins = 0;
            ConferenceWins = 0;
            DivisionWins = 0;
            Losses = 0;
            ConferenceLosses = 0;
            DivisionLosses = 0;
        }
    }
}
