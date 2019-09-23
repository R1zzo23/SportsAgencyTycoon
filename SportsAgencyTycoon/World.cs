using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class World
    {
        public MainForm MainForm;
        Random rnd = new Random();
        public List<Licenses> AvailableLicenses;
        public List<Licenses> TeamSportLicenses;
        public List<Licenses> IndividualSportLicense;
        public List<Player> AvailableClients;
        public List<Agent> AvailableAgents;
        public List<League> Leagues;
        public List<Association> Associations;
        public League NBA;
        public League MLB;
        public League NFL;
        public League NHL;
        public League MLS;
        public Basketball Basketball;
        //public Baseball Baseball;
        public Football Football;
        //public Hockey Hockey;
        //public Soccer Soccer;
        public Association PGA;
        public Association ATP;
        public Association WBA;
        public Association UFC;
        public int Year;
        public int MonthNumber;
        public Months MonthName;
        public int WeekNumber;
        public int firstNameCount = 200;
        public int lastNameCount = 214;
        public Calendar Calendar;
        public List<CalendarEvent> EventsThisWeek = new List<CalendarEvent>();
        public World(MainForm form)
        {
            AvailableLicenses = new List<Licenses>();
            TeamSportLicenses = new List<Licenses>();
            IndividualSportLicense = new List<Licenses>();
            AvailableClients = new List<Player>();
            AvailableAgents = new List<Agent>();
            Leagues = new List<League>();
            Associations = new List<Association>();
            
            Year = 2000;
            MonthNumber = 0;
            MonthName = Months.January;
            WeekNumber = 1;            

            MainForm = form;
            Calendar = new Calendar(form);
        }

        public void InitializeCalendar(Licenses teamLicense)
        {
            if (teamLicense.Sport == Sports.Basketball) SetInitialDate(NBA);
            else if (teamLicense.Sport == Sports.Football) SetInitialDate(NFL);
            else if (teamLicense.Sport == Sports.Baseball) SetInitialDate(MLB);
            else if (teamLicense.Sport == Sports.Hockey) SetInitialDate(NHL);
            else if (teamLicense.Sport == Sports.Soccer) SetInitialDate(MLS);

            SetDateOneWeekBack();
            MainForm.UpdateWorldCalendar();
        }

        public void SetInitialDate(League l)
        {
            MonthName = l.SeasonStart.MonthName;
            MonthNumber = l.SeasonStart.MonthNumber;
            WeekNumber = l.SeasonStart.Week;
        }

        public void SetDateOneWeekBack()
        {
            WeekNumber--;
            if (WeekNumber == 0)
            {
                MonthNumber--;
                MonthName = (Months)MonthNumber;
                if ((MonthNumber + 1) % 3 == 0) WeekNumber = 5;
                else WeekNumber = 4;
            }
        }

        public void InitializeLicenses()
        {
            Licenses baseballLicense = new Licenses(Sports.Baseball, 400, 1400, Months.March, 2);
            Licenses basketballLicense = new Licenses(Sports.Basketball, 250, 1250, Months.July, 6);
            Licenses footballLicense = new Licenses(Sports.Football, 2500, 1650, Months.January, 0);
            Licenses hockeyLicense = new Licenses(Sports.Hockey, 300, 900, Months.August, 7);
            Licenses soccerLicense = new Licenses(Sports.Soccer, 250, 750, Months.January, 0);

            TeamSportLicenses.Add(baseballLicense);
            TeamSportLicenses.Add(basketballLicense);
            TeamSportLicenses.Add(footballLicense);
            TeamSportLicenses.Add(hockeyLicense);
            TeamSportLicenses.Add(soccerLicense);

            Licenses boxingLicense = new Licenses(Sports.Boxing, 500, 600, Months.January, 0);
            Licenses golfLicense = new Licenses(Sports.Golf, 750, 1150, Months.January, 0);
            Licenses tennisLicense = new Licenses(Sports.Tennis, 350, 800, Months.January, 0);
            Licenses ultimateFightingLicense = new Licenses(Sports.MMA, 375, 550, Months.January, 0);

            IndividualSportLicense.Add(boxingLicense);
            IndividualSportLicense.Add(golfLicense);
            IndividualSportLicense.Add(tennisLicense);
            IndividualSportLicense.Add(ultimateFightingLicense);
        }
        public int HowManyNewClients(int influence)
        {
            int numberNewClients = 0;
            if (influence < 10) numberNewClients = 2;
            else if (influence >= 10 && influence < 25) numberNewClients = 3;
            else if (influence >= 25 && influence < 50) numberNewClients = 5;
            else if (influence >= 50 && influence < 65) numberNewClients = 7;
            else if (influence >= 65 && influence < 80) numberNewClients = 8;
            else numberNewClients = 10;

            return numberNewClients;
        }
        #region Create Leagues & Associations
        public void CreateLeaguesAssociationEventsPlayersAndTeams()
        {            
            CreateLeagues(rnd);
            CreateAssociations(rnd);
            CreateAllEvents();
            CreateAthletesForAssociations(rnd);
            CreateTeamsForLeagues(rnd);
            AddLeaguesAndAssociationsToWorld();
            CreateCalendarEventsForAssociationEvents();
            CreatePlayersForTeams(rnd);
            CreateCalendarEventsForLeagueStartAndEnd();
            CreatePlayerBirthdayCalendarEvents();
            CalculateWorldRankings();
            AssignTeamToPlayersInLeagues();
            DetermineTitleContenderForTeams();
            DetermineHappinessForPlayers();
        }
        public void CreateLeagues(Random rnd)
        {
            NBA = new League(Sports.Basketball, "National Basketball Association", "NBA", rnd.Next(45, 100), new Date(10, Months.October, 4), new Date(6, Months.June, 2), 8, 40000000, 582180);
            MLB = new League(Sports.Baseball, "Major League Baseball", "MLB", rnd.Next(30, 70), new Date(4, Months.April, 1), new Date(10, Months.October, 4), 6, 35000000, 555000);
            NFL = new League(Sports.Football, "National Football League", "NFL", rnd.Next(50, 100), new Date(8, Months.September, 3), new Date(2, Months.February, 1), 6, 30000000, 495000);
            NHL = new League(Sports.Hockey, "National Hockey League", "NHL", rnd.Next(30, 55), new Date(10, Months.October, 1), new Date(6, Months.June, 2), 8, 9500000, 650000);
            MLS = new League(Sports.Soccer, "Major League Soccer", "MLS", rnd.Next(15, 50), new Date(3, Months.March, 2), new Date(12, Months.December, 2), 9, 7000000, 56250);

            /*NBA.InSeason = true;
            MLB.InSeason = false;
            NFL.InSeason = true;
            NHL.InSeason = true;
            MLS.InSeason = false;*/
        }
        public void CreateCalendarEventsForLeagueStartAndEnd()
        {
            foreach (League l in Leagues)
            {
                Calendar.AddCalendarEvent(new CalendarEvent(l));
                Calendar.AddCalendarEvent(new CalendarEvent(l, "end"));
            }
        }
        public void CreateAssociations(Random rnd)
        {
            PGA = new Association(Sports.Golf, "Professional Golf Association", "PGA", rnd.Next(40, 80));
            ATP = new Association(Sports.Tennis, "Association of Tennis Professionals", "ATP", rnd.Next(20, 50));
            WBA = new Association(Sports.Boxing, "World Boxing Association", "WBA", rnd.Next(10, 40));
            UFC = new Association(Sports.MMA, "Ultimate Fighting Championship", "UFC", rnd.Next(30, 60));
        }
        #endregion
        #region Create Events for Associations
        public void CreateAllEvents()
        {
            CreatePGAEvents();
            CreateATPEvents();
            CreateWBAEvents();
            CreateUFCEvents();
        }
        public void CreatePGAEvents()
        {
            PGA.EventList.Add(new Event(Sports.Golf, Year, "World Golf Championships", 0, EventType.PGATourEvent, "Mexico City, MEX", 10250000, 100, 70, new Date(2, Months.February, 3)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "THE PLAYERS Championship", 1, EventType.Major, "Ponte Vedra Beach,  FL", 6700000, 75, 50, new Date(3, Months.March, 2)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "Masters Tournament", 2, EventType.Major, "Augusta, GA", 11500000, 60, 50, new Date(4, Months.April, 2)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "PGA Championship", 3, EventType.Major, "Farmingdale, NY", 11000000, 100, 70, new Date(5, Months.May, 3)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "U.S. Open", 4, EventType.PGATourEvent, "Pebble Beach, CA", 12000000, 100, 70, new Date(6, Months.June, 2)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "The Open Championship", 5, EventType.Major, "Portrush, NIR", 11000000, 85, 60, new Date(7, Months.July, 3)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "THE NORTHERN TRUST", 6, EventType.PGATourEvent, "Jersey City, NJ", 9250000, 100, 70, new Date(8, Months.August, 1)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "BMW Championship", 7,EventType.PGATourEvent, "Medinah, IL", 9250000, 100, 70, new Date(8, Months.August, 3)));
            PGA.EventList.Add(new Event(Sports.Golf, Year, "TOUR Championship", 8, EventType.PGATourEvent, "Atlanta, GA", 9000000, 100, 70, new Date(10, Months.October, 2)));
        }
        public void CreateATPEvents()
        {
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Australian Open", 0, EventType.GrandSlam, "Melbourne, AUS", 15000000, 128, 128, new Date(1, Months.January, 3)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "BNP Paribas Open", 1, EventType.Masters1000, "Indiand Wells, CA", 4230000, 128, 128, new Date(3, Months.March, 2)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Miami Open", 2, EventType.Masters1000, "Miami, FL", 4500000, 128, 128, new Date(3, Months.March, 4)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Rolex Monte-Carlo Masters", 3, EventType.Masters1000, "Monte Carlo, MON", 3000000, 128, 128, new Date(4, Months.April, 2)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Mutua Madrid Open", 4, EventType.Masters1000, "Madrid, ESP", 3750000, 128, 128, new Date(5, Months.May, 1)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Internazionali BNL d'Italia", 5, EventType.Masters1000, "Rome, ITA", 2900000, 128, 128, new Date(5, Months.May, 3)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "French Open", 6, EventType.GrandSlam, "Paris, FR", 16500000, 128, 128, new Date(5, Months.May, 4)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Wimbledon", 7, EventType.GrandSlam, "London, EN", 20000000, 128, 128, new Date(6, Months.June, 5)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Coupe Rogers", 8, EventType.Masters1000, "Montreal, CAN", 3100000, 128, 128, new Date(8, Months.August, 1)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Western and Southern Open", 9, EventType.Masters1000, "Cincinnati, OH", 3250000, 128, 128, new Date(8, Months.August, 2)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "US Open", 10, EventType.GrandSlam, "New York City, NY", 17500000, 128, 128, new Date(9, Months.September, 1)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Rolex Shanghai Masters", 11, EventType.Masters1000, "Shanghai, CHN", 4230000, 128, 128, new Date(10, Months.October, 1)));
            ATP.EventList.Add(new Event(Sports.Tennis, Year, "Rolex Paris Masters", 12, EventType.GrandSlam, "Paris, FRA", 3800000, 128, 128, new Date(10, Months.October, 4)));
        }
        public void CreateWBAEvents()
        {
            WBA.EventList.Add(new Event(Sports.Boxing, Year, "Up-And-Comers Showcase", 0, EventType.Normal, "Las Vegas, NV", 15000000, 18, 18, new Date(1, Months.January, 2)));
            WBA.EventList.Add(new Event(Sports.Boxing, Year, "NYC Prize Fights", 1, EventType.Normal, "New York City, NY", 35000000, 12, 12, new Date(5, Months.May, 2)));
            WBA.EventList.Add(new Event(Sports.Boxing, Year, "Top Rank Boxing Premiere", 2, EventType.Normal, "Los Angeles, CA", 29000000, 10, 10, new Date(9, Months.September, 3)));
            WBA.EventList.Add(new Event(Sports.Boxing, Year, "King of the Ring", 3, EventType.Normal, "Las Vegas, NV", 50000000, 40, 40, new Date(11, Months.November, 4)));
        }
        public void CreateUFCEvents()
        {
            UFC.EventList.Add(new Event(Sports.MMA, Year, "King Kong of the Octagon", 0, EventType.Normal, "Atlantic City, NJ", 12000000, 50, 50, new Date(2, Months.February, 3)));
            UFC.EventList.Add(new Event(Sports.MMA, Year, "Fully Loaded", 1, EventType.Normal, "Boston, MA", 3750000, 20, 20, new Date(4, Months.April, 2)));
            UFC.EventList.Add(new Event(Sports.MMA, Year, "Bloodied and Bruised", 2, EventType.Normal, "Chicago, IL", 2500000, 20, 20, new Date(6, Months.June, 1)));
            UFC.EventList.Add(new Event(Sports.MMA, Year, "Elite Title Showdown", 3, EventType.Normal, "Las Vegas, NV", 5000000, 10, 10, new Date(10, Months.October, 2)));
        }
        public void CreateCalendarEventsForAssociationEvents()
        {
            foreach (Association a in Associations)
                foreach (Event e in a.EventList) Calendar.AddCalendarEvent(new CalendarEvent(e));
        }
        #endregion
        #region Create Athletes for Associations and Leagues
        public void CreatePlayersForTeams(Random rnd)
        {
            CreateBasketballPlayers(rnd);
            CreateFootballPlayers(rnd);
            CreateHockeyPlayers(rnd);
            CreateBaseballPlayers(rnd);
            CreateSoccerPlayers(rnd);
        }
        public void CreateAthletesForAssociations(Random rnd)
        {
            CreateGolfers(rnd);
            CreateTennisPlayers(rnd);
            CreateBoxers(rnd);
            CreateMMAFighters(rnd);
        }
        public void CreateBasketballPlayers(Random rnd)
        {
            for (int i = 0; i < NBA.TeamList.Count; i++)
            {
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PG));   
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PG));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PG));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.SG));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.SG));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.SF));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.SF));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PF));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PF));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.CE));
                NBA.AddPlayer(NBA.TeamList[i].Roster, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.CE));
            }
            // Create Free Agents
            for (int j = 0; j < 7; j++)
            {
                NBA.AddPlayer(NBA.FreeAgents, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PG));
                NBA.AddPlayer(NBA.FreeAgents, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.SG));
                NBA.AddPlayer(NBA.FreeAgents, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.SF));
                NBA.AddPlayer(NBA.FreeAgents, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.PF));
                NBA.AddPlayer(NBA.FreeAgents, new BasketballPlayer(rnd, NBA.IdCount, Sports.Basketball, rnd.Next(18, 38), Position.CE));
            }
        }
        public void CreateFootballPlayers(Random rnd)
        {
            for (int i = 0; i < NFL.TeamList.Count; i++)
            {
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.QB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.QB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.RB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.RB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.FB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.FB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.WR));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.WR));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.TE));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.TE));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.C));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.C));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.OG));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.OG));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.OT));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.OT));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.DE));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.DE));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.DE));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.DE));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.DT));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.DT));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.LB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.LB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.LB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.LB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.CB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.CB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.CB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.CB));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.SS));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.SS));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.FS));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.FS));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.K));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.P));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.WR));
                NFL.AddPlayer(NFL.TeamList[i].Roster, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(21, 33), Position.WR));
            }
            // Create Free Agents
            for (int j = 0; j < 7; j++)
            {
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.QB));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.RB));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.FB));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.WR));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.TE));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.C));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.OG));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.OT));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.DE));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.DT));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.LB));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.CB));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.SS));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.FS));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.K));
                NFL.AddPlayer(NFL.FreeAgents, new FootballPlayer(rnd, NFL.IdCount, Sports.Football, rnd.Next(18, 38), Position.P));
            }
        }
        public void CreateHockeyPlayers(Random rnd)
        {
            for (int i = 0; i < NHL.TeamList.Count; i++)
            {
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.G));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.G));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.C));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.C));
                NHL.AddPlayer(NHL.TeamList[i].Roster, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.C));
            }
            // Create Free Agents
            for (int j = 0; j < 7; j++)
            {
                NHL.AddPlayer(NHL.FreeAgents, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.G));
                NHL.AddPlayer(NHL.FreeAgents, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.FreeAgents, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.D));
                NHL.AddPlayer(NHL.FreeAgents, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.FreeAgents, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.W));
                NHL.AddPlayer(NHL.FreeAgents, new HockeyPlayer(rnd, NHL.IdCount, Sports.Hockey, rnd.Next(18, 38), Position.C));
            }
        }
        public void CreateBaseballPlayers(Random rnd)
        {
            for (int i = 0; i < MLB.TeamList.Count; i++)
            {
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.C));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.C));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.INF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.OF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.OF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.OF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.OF));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.SP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.SP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.SP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.SP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.RP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.RP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.RP));
                MLB.AddPlayer(MLB.TeamList[i].Roster, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 37), Position.RP));
            }
            // Create Free Agents
            for (int j = 0; j < 7; j++)
            {
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.C));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.INF));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.INF));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.OF));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.OF));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.SP));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.SP));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.RP));
                MLB.AddPlayer(MLB.FreeAgents, new BaseballPlayer(rnd, MLB.IdCount, Sports.Baseball, rnd.Next(18, 38), Position.RP));
            }
        }
        public void CreateSoccerPlayers(Random rnd)
        {
            for (int i = 0; i < MLS.TeamList.Count; i++)
            {
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.GK));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.GK));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.D));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.D));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.D));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.D));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.D));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.MID));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.MID));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.MID));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.MID));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.MID));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.F));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.F));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.F));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.F));
                MLS.AddPlayer(MLS.TeamList[i].Roster, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 36), Position.F));
            }
            // Create Free Agents
            for (int j = 0; j < 7; j++)
            {
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.GK));
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.D));
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.D));
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.MID));
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.MID));
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.F));
                MLS.AddPlayer(MLS.FreeAgents, new SoccerPlayer(rnd, MLS.IdCount, Sports.Soccer, rnd.Next(16, 33), Position.F));
            }
        }
        public void CreateGolfers(Random rnd)
        {
            for (var i = 0; i < 144; i++)
            {
                PGA.PlayerList.Add(new Golfer(rnd, i, Sports.Golf, rnd.Next(18, 65)));
            }
        }
        public void CreateTennisPlayers(Random rnd)
        {
            for (var i = 0; i < 128; i++)
            {
                ATP.PlayerList.Add(new TennisPlayer(rnd, i, Sports.Tennis, rnd.Next(18, 35)));
            }
        }
        public void CreateBoxers(Random rnd)
        {
            for (var i = 0; i < 40; i++)
            {
                WBA.PlayerList.Add(new Boxer(rnd, i, Sports.Boxing, rnd.Next(16, 34)));
            }
        }
        public void CreateMMAFighters(Random rnd)
        {
            for (var i = 0; i < 50; i++)
            {
                UFC.PlayerList.Add(new MMAFighter(rnd, i, Sports.MMA, rnd.Next(17, 31)));
            }
        }
        public void CreatePlayerBirthdayCalendarEvents()
        {
            foreach (Association a in Associations)
                foreach (Player p in a.PlayerList)
                    Calendar.AddCalendarEvent(new CalendarEvent(p));

            foreach (League l in Leagues)
                foreach (Team t in l.TeamList)
                    foreach (Player p in t.Roster)
                        Calendar.AddCalendarEvent(new CalendarEvent(p));
        }
        #endregion
        public void AssignTeamToPlayersInLeagues()
        {
            foreach (League l in Leagues)
            {
                foreach (Player p in l.FreeAgents)
                {
                    p.League = l;
                    p.Team = null;
                    p.FreeAgent = true;
                }

                foreach (Team t in l.TeamList)
                    foreach (Player p in t.Roster)
                    {
                        p.Team = t;
                        p.League = l;
                        p.Contract = p.CreatePlayerContract(rnd);
                        p.FreeAgent = false;
                    }
            }                          
        }
        public void DetermineTitleContenderForTeams()
        {
            foreach (League l in Leagues)
                foreach (Team t in l.TeamList)
                {
                    int titleContender = 0;
                    int topHalfTotal = 0;
                    int numberOfStarters = 0;
                    int numberOfBackups = 0;
                    int bottomHalfAverage = 0;
                    int rosterCount = t.Roster.Count();

                    if (rosterCount % 2 == 0) numberOfStarters = rosterCount / 2;
                    else numberOfStarters = (int)((rosterCount / 2) - 0.5);

                    numberOfBackups = rosterCount - numberOfStarters;

                    List<Player> roster = new List<Player>();

                    foreach (Player p in t.Roster) roster.Add(p);

                    roster = roster.OrderByDescending(o => o.CurrentSkill).ToList();

                    // add every player's CurrentSkill from top half of roster
                    for (int i = 0; i < numberOfStarters; i++)
                    {
                        topHalfTotal += roster[i].CurrentSkill;
                    }

                    // add average of bottom half of the roster's CurrentSkill
                    for (int i = numberOfStarters; i < t.Roster.Count; i++)
                    {
                        bottomHalfAverage += roster[i].CurrentSkill;
                    }

                    bottomHalfAverage = bottomHalfAverage / numberOfBackups;

                    titleContender = (int)(topHalfTotal + bottomHalfAverage) / (numberOfStarters + 1);
                    t.TitleConteder = titleContender;
                }
        }
        public void DetermineHappinessForPlayers()
        {
            foreach (League l in Leagues)
                foreach (Team t in l.TeamList)
                    foreach (Player p in t.Roster)
                    {
                        bool isStarter;

                        // determine player TeamHappiness
                        if (p.Sport == Sports.Basketball) isStarter = IsBasketballStarter(t, p);
                        else if (p.Sport == Sports.Baseball) isStarter = IsBaseballStarter(t, p);
                        else if (p.Sport == Sports.Football) isStarter = IsFootballStarter(t, p);
                        else if (p.Sport == Sports.Hockey) isStarter = IsHockeyStarter(t, p);
                        else if (p.Sport == Sports.Soccer) isStarter = IsSoccerStarter(t, p);
                        else isStarter = false;

                        p.DetermineTeamHappiness(rnd, isStarter);

                        // determine player AgencyHappiness
                        p.DetermineAgencyHappiness(rnd, p.Contract);
                    }
            foreach (Association a in Associations)
                foreach (Player p in a.PlayerList)
                    p.DetermineAgencyHappiness(rnd, p.Contract);
        }
        #region Is Player A Starter
        public bool IsBasketballStarter(Team t, Player p)
        {
            bool starter = false;
            BasketballPlayer player = (BasketballPlayer)p;
            Position position = player.Position;
            List<BasketballPlayer> playersAtPosition = new List<BasketballPlayer>();

            foreach (BasketballPlayer b in t.Roster)
                if (b.Position == position) playersAtPosition.Add(b);

            playersAtPosition = playersAtPosition.OrderByDescending(o => o.CurrentSkill).ToList();
            if (player == playersAtPosition[0]) starter = true;
            else starter = false;

            return starter;
        }
        public bool IsBaseballStarter(Team t, Player p)
        {
            bool starter = false;
            int starterCount = 0;
            BaseballPlayer player = (BaseballPlayer)p;
            Position position = player.Position;
            List<BaseballPlayer> playersAtPosition = new List<BaseballPlayer>();

            foreach (BaseballPlayer b in t.Roster)
                if (b.Position == position) playersAtPosition.Add(b);

            playersAtPosition = playersAtPosition.OrderByDescending(o => o.CurrentSkill).ToList();

            if (position == Position.INF) starterCount = 4;
            else if (position == Position.OF) starterCount = 3;
            else if (position == Position.SP) starterCount = 3;
            else if (position == Position.C) starterCount = 1;
            else if (position == Position.RP) starterCount = 2;

            for (int i = 0; i < starterCount; i++)
            {
                if (player == playersAtPosition[i])
                {
                    starter = true;
                    return starter;
                }
            }

            return starter;
        }
        public bool IsFootballStarter(Team t, Player p)
        {
            bool starter = false;
            int starterCount = 0;
            FootballPlayer player = (FootballPlayer)p;
            Position position = player.Position;
            List<FootballPlayer> playersAtPosition = new List<FootballPlayer>();

            foreach (FootballPlayer f in t.Roster)
                if (f.Position == position) playersAtPosition.Add(f);

            playersAtPosition = playersAtPosition.OrderByDescending(o => o.CurrentSkill).ToList();

            if (position == Position.WR || position == Position.DE || position == Position.DT || position == Position.CB)
                starterCount = 2;
            else if (position == Position.LB) starterCount = 4;
            else starterCount = 1;

            for (int i = 0; i < starterCount; i++)
            {
                if (player == playersAtPosition[i])
                {
                    starter = true;
                    return starter;
                }
            }

            return starter;
        }
        public bool IsHockeyStarter(Team t, Player p)
        {
            bool starter = false;
            int starterCount = 0;
            HockeyPlayer player = (HockeyPlayer)p;
            Position position = player.Position;
            List<HockeyPlayer> playersAtPosition = new List<HockeyPlayer>();

            foreach (HockeyPlayer h in t.Roster)
                if (h.Position == position) playersAtPosition.Add(h);

            playersAtPosition = playersAtPosition.OrderByDescending(o => o.CurrentSkill).ToList();

            if (position == Position.G) starterCount = 1;
            else if (position == Position.W) starterCount = 3;
            else starterCount = 2;

            for (int i = 0; i < starterCount; i++)
            {
                if (player == playersAtPosition[i])
                {
                    starter = true;
                    return starter;
                }
            }

            return starter;
        }
        public bool IsSoccerStarter(Team t, Player p)
        {
            bool starter = false;
            SoccerPlayer player = (SoccerPlayer)p;
            Position position = player.Position;
            List<SoccerPlayer> playersAtPosition = new List<SoccerPlayer>();

            foreach (SoccerPlayer s in t.Roster)
                if (s.Position == position) playersAtPosition.Add(s);

            playersAtPosition = playersAtPosition.OrderByDescending(o => o.CurrentSkill).ToList();
            if (player == playersAtPosition[0]) starter = true;
            else starter = false;

            return starter;
        }
        #endregion
        #region Create Teams for Leagues
        public void CreateTeamsForLeagues(Random rnd)
        {
            CreateNBATeams(rnd);
            CreateMLBTeams(rnd);
            CreateNHLTeams(rnd);
            CreateNFLTeams(rnd);
            CreateMLSTeams(rnd);
        }
        public void CreateNBATeams(Random rnd)
        {
            NBA.TeamList.Add(new Team("Atlanta", "Hawks", "Eastern", "Southeast", "ATL", 40, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Boston", "Celtics", "Eastern", "Atlantic", "BOS", 60, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Brooklyn", "Nets", "Eastern", "Atlantic", "BKN", 62, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Charlotte", "Hornets", "Eastern", "Southeast", "CHA", 33, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Chicago", "Bulls", "Eastern", "Central", "CHI", 62, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Cleveland", "Cavaliers", "Eastern", "Central", "CLE", 30, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Dallas", "Mavericks", "Western", "Southwest", "DAL", 49, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Denver", "Nuggets", "Western", "Northwest", "DEN", 38, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Detroit", "Pistons", "Eastern", "Central", "DET", 33, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Golden State", "Warriors", "Western", "Pacific", "GSW", 66, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Houston", "Rockets", "Western", "Southwest", "HOU", 49, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Indiana", "Pacers", "Eastern", "Central", "IND", 43, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Los Angeles", "Clippers", "Western", "Pacific", "LAC", 72, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Los Angeles", "Lakers", "Western", "Pacific", "LAL", 75, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Memphis", "Grizzlies", "Western", "Southwest", "MEM", 40, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Miami", "Heat", "Eastern", "Southeast", "MIA", 69, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Milwaukee", "Bucks", "Eastern", "Central", "MIL", 46, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Minnesota", "Timberwolves", "Western", "Northwest", "MIN", 46, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("New Orleans", "Pelicans", "Western", "Southwest", "NOP", 51, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("New York", "Knicks", "Eastern", "Atlantic", "NYK", 82, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Oklahoma City", "Thunder", "Western", "Northwest", "OKC", 48, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Orlando", "Magic", "Eastern", "Southeast", "ORL", 52, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Philadelphia", "76ers", "Eastern", "Atlantic", "PHI", 58, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Phoenix", "Suns", "Western", "Pacific", "PHO", 42, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Portland", "Trail Blazers", "Western", "Northwest", "POR", 54, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Sacramento", "Kings", "Western", "Pacific", "SAC", 54, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("San Antonio", "Spurs", "Western", "Southwest", "SAS", 51, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Toronto", "Raptors", "Eastern", "Atlantic", "TOR", 47, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Utah", "Jazz", "Western", "Northwest", "UTA", 38, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Washington", "Wizards", "Eastern", "Southeast", "WAS", 47, rnd.Next(25, 76)));
        }
        public void CreateMLBTeams(Random rnd)
        {
            MLB.TeamList.Add(new Team("Arizona", "Diamondbacks", "ARI", 29, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Atlanta", "Braves", "ATL", 40, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Baltimore", "Orioles", "BAL", 45, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Boston", "Red Sox", "BOS", 72, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Chicago", "Cubs", "CHC", 71, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Chicago", "White Sox", "CHW", 61, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Cincinnati", "Reds", "CIN", 30, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Cleveland", "Indians", "CLE", 31, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Colorado", "Rockies", "COL", 29, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Detroit", "Tigers", "DET", 38, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Houston", "Astros", "HOU", 50, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Kansas City", "Royals", "KC", 48, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Los Angeles", "Angels", "LAA", 66, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Los Angeles", "Dodgers", "LAD", 69, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Miami", "Marlins", "MIA", 65, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Milwaukee", "Brewers", "MIL", 25, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Minnesota", "Twins", "MIN", 27, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("New York", "Mets", "NYM", 80, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("New York", "Yankees", "NYY", 85, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Oakland", "Athletics", "OAK", 33, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Philadelphia", "Phillies", "PHI", 58, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Pittsburgh", "Pirates", "PIT", 49, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("San Diego", "Padres", "SD", 31, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("San Francisco", "Giants", "SF", 58, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Seattle", "Mariners", "SEA", 47, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("St. Louis", "Cardinals", "STL", 57, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Tampa Bay", "Rays", "TB", 43, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Texas", "Rangers", "TEX", 55, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Toronto", "Blue Jays", "TOR", 52, rnd.Next(25, 76)));
            MLB.TeamList.Add(new Team("Washington", "Nationals", "WAS", 47, rnd.Next(25, 76)));
        }
        public void CreateNHLTeams(Random rnd)
        {
            NHL.TeamList.Add(new Team("Anaheim", "Ducks", "ANA", 60, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Arizona", "Coyotes", "ARI", 23, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Boston", "Bruins", "BOS", 85, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Buffalo", "Sabres", "BUF", 43, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Calgary", "Flames", "CAL", 44, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Carolina", "Hurricanes", "CAR", 38, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Chicago", "Blackhawks", "CHI", 66, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Colorado", "Avalanche", "COL", 44, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Columbus", "Blue Jackets", "COL", 45, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Dallas", "Stars", "DAL", 33, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Detroit", "Red Wings", "DET", 80, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Edmonton", "Oilers", "EDM", 66, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Florida", "Panthers", "FLA", 29, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Los Angeles", "Kings", "LAK", 71, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Minnesota", "Wild", "MIN", 61, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Montreal", "Canadiens", "MON", 85, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Nashville", "Predators", "NSH", 60, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("New Jersey", "Devils", "NJD", 73, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("New York", "Islanders", "NYI", 75, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("New York", "Rangers", "NYR", 79, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Ottawa", "Senators", "OTT", 67, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Philadelphia", "Flyers", "PHI", 58, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Pittsburgh", "Penguins", "PIT", 59, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("San Jose", "Sharks", "SJS", 61, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("St. Louis", "Blues", "STL", 57, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Tampa Bay", "Lightning", "TB", 59, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Toronto", "Maples Leafs", "TOR", 67, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Vancouver", "Canucks", "VAN", 65, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Vegas", "Golden Knights", "VEG", 60, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Washington", "Capitals", "WAS", 64, rnd.Next(25, 76)));
            NHL.TeamList.Add(new Team("Winnipeg", "Jets", "WIN", 46, rnd.Next(25, 76)));
        }
        public void CreateNFLTeams(Random rnd)
        {
            NFL.TeamList.Add(new Team("Arizona", "Cardinals", "ARI", 33, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Atlanta", "Falcons", "ATL", 44, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Baltimore", "Ravens", "BAL", 39, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Buffalo", "Bills", "BUF", 33, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Carolina", "Panthers", "CAR", 40, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Chicago", "Bears", "CHI", 57, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Cincinnati", "Bengals", "CIN", 30, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Cleveland", "Browns", "CLE", 31, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Dallas", "Cowboys", "DAL", 80, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Denver", "Broncos", "DEN", 41, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Detroit", "Lions", "DET", 38, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Green Bay", "Packers", "GB", 58, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Houston", "Texans", "HOU", 50, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Indianapolis", "Colts", "IND", 40, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Jacksonville", "Jaguars", "JAX", 31, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Kansas City", "Chiefs", "KC", 48, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Los Angeles", "Chargers", "LAC", 77, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Los Angeles", "Rams", "LAR", 77, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Miami", "Dolphins", "MIA", 85, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Minnesota", "Vikings", "MIN", 28, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("New England", "Patriots", "NE", 59, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("New Orleans", "Saints", "NO", 51, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("New York", "Giants", "NYG", 82, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("New York", "Jets", "NYJ", 82, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Oakland", "Raiders", "OAK", 73, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Philadelphia", "Eagles", "PHI", 58, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Pittsburgh", "Steelers", "PIT", 54, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("San Francisco", "49ers", "SF", 66, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Seattle", "Seahawks", "SEA", 47, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Tampa Bay", "Buccaneers", "TB", 36, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Tennessee", "Titans", "TEN", 48, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Washington", "Redskins", "WAS", 47, rnd.Next(25, 76)));
        }
        public void CreateMLSTeams(Random rnd)
        {
            MLS.TeamList.Add(new Team("Atlanta", "United FC", "ATL", 55, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Chicago", "Fire", "CHI", 45, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Cincinnati", "FC", "CIN", 33, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Colorado", "Rapids", "COL", 37, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Columbus", "Crew SC", "CLM", 39, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("D.C.", "United", "DCU", 41, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Dallas", "FC", "DAL", 43, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Houston", "Dynamo", "HOU", 41, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("LA", "Galaxy", "LAG", 66, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Los Angeles", "FC", "LAFC", 60, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Minnesota", "United FC", "MIN", 37, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Montreal", "Impact", "MON", 31, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("New England", "Revolution", "NE", 33, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("New York City", "FC", "NYCFC", 57, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("New York", "Red Bulls", "NYRB", 55, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Orlando City", "FC", "ORL", 44, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Philadelphia", "Union", "PHI", 31, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Portland", "Timbers", "POR", 62, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Salt Lake", "Real", "RSL", 32, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("San Jose", "Earthquakes", "SJ", 36, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Seattle", "Sounders FC", "SEA", 77, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Kansas City", "Sporting", "KC", 35, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Toronto", "FC", "TOR", 41, rnd.Next(25, 76)));
            MLS.TeamList.Add(new Team("Vancouver", "Whitecaps FC", "VAN", 29, rnd.Next(25, 76)));
        }
        #endregion

        public void CalculateWorldRankings()
        {
            foreach(Association association in Associations)
            {
                List<Player> playerList = association.PlayerList.OrderByDescending(o => o.CurrentSkill).ToList();
                for(int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].WorldRanking = i + 1;
                }
            }
        }
        public void AddLeaguesAndAssociationsToWorld()
        {
            //add NBA, NFL, MLB, NHL and MLS to World.Leagues
            Leagues.Add(NBA);
            Leagues.Add(NFL);
            Leagues.Add(MLB);
            Leagues.Add(NHL);
            Leagues.Add(MLS);
            //add PGA, ATP, WBA and UFC to World.Associations
            Associations.Add(PGA);
            Associations.Add(ATP);
            Associations.Add(WBA);
            Associations.Add(UFC);
        }

        #region Calendar - Set Month/Year
        public void HandleCalendar(Agency agency)
        {
            //add 1 to week number
            WeekNumber++;

            //check if month ends
            if (((WeekNumber == 5) && ((MonthNumber + 1) % 3 != 0)) || ((WeekNumber == 6) && ((MonthNumber + 1) % 3 == 0)))
            {
                SetNewMonth();
                PayPlayersMonthlySalary();
            }
        }
        private void SetNewMonth()
        {
            MonthNumber++;
            if (MonthNumber == 12)
            {
                SetNewYear();
            }
            MonthName = (Months)MonthNumber;
            WeekNumber = 1;
        }
        private void SetNewYear()
        {
            MonthNumber = 0;
            Year++;
            CreateAllEvents();
            IncreasePrizePool();
        }
        private void PayPlayersMonthlySalary()
        {
            foreach (League l in Leagues)
                if (l.InSeason)
                    foreach (Team t in l.TeamList)
                        foreach (Player p in t.Roster)
                        {
                            // all players getting paid monthly get their money
                            if (p.Contract.AgentPaySchedule == PaySchedule.Monthly)
                            {
                                p.CareerEarnings += p.Contract.MonthlySalary;
                                // if player is member of agency, agency gets paid too
                                if (p.MemberOfAgency)
                                {
                                    MainForm.agency.Money += Convert.ToInt32((double)p.Contract.MonthlySalary * (double)(p.Contract.AgentPercentage / 100));
                                    //find which Agent represents this client and give money to Agent
                                    MainForm.agency.FindAgent(p).CareerEarnings += Convert.ToInt32((double)p.Contract.MonthlySalary * (double)(p.Contract.AgentPercentage / 100));


                                    /*for (int i = 0; i < MainForm.agency.Agents.Count; i++)
                                    {
                                        int index = MainForm.agency.Agents[i].ClientList.FindIndex(o => (o.FullName == p.FullName) && (o.Id == p.Id) && (o.Sport == p.Sport));
                                        if (index >= 0) MainForm.agency.Agents[i].CareerEarnings += Convert.ToInt32((double)p.Contract.MonthlySalary * (double)(p.Contract.AgentPercentage / 100));
                                    }*/
                                }
                                    
                            }
                        }               
        }
        public void PayPlayersAnnualSalary(League l)
        {
            foreach (Team t in l.TeamList)
                foreach (Player p in t.Roster)
                {
                    // all players getting paid annually get their money
                    if (p.Contract.AgentPaySchedule == PaySchedule.Annually)
                    {
                        p.CareerEarnings += p.Contract.YearlySalary;
                        // if player is member of agency, agency gets paid too
                        if (p.MemberOfAgency)
                        {
                            MainForm.agency.Money += Convert.ToInt32((double)p.Contract.YearlySalary * (double)(p.Contract.AgentPercentage / 100));
                            //find which Agent represents this client and give money to Agent
                            MainForm.agency.FindAgent(p).CareerEarnings += Convert.ToInt32((double)p.Contract.YearlySalary * (double)(p.Contract.AgentPercentage / 100));
                        }
                    }
                }
            l.InSeason = false;
        }
        private void IncreasePrizePool()
        {
            foreach (Association a in Associations)
            {
                foreach (Event e in a.EventList)
                {
                    double inflationPercentage = (Year - 2000) * .0375;
                    e.PrizePool = (int)Math.Floor(e.PrizePool * (1 + inflationPercentage));
                }
            }
        }
        #endregion
        public bool CheckTestingWindow(Months month, Sports sport)
        {
            bool testingWindowOpen = false;
            if ((int)month == 0 && (sport.ToString() == "Basketball" || sport.ToString() == "Football"))
            {
                testingWindowOpen = true;
            }
            return testingWindowOpen;
        }
        #region Create Random First & Last Names

        public FirstName randomFirstName(Random rnd)
        {
            FirstName firstName;

            int firstNameNumber = rnd.Next(0, firstNameCount);
            firstName = (FirstName)firstNameNumber;

            return firstName;
        }
        public LastName randomLastName(Random rnd)
        {
            LastName lastName;

            int lastNameNumber = rnd.Next(0, lastNameCount);
            lastName = (LastName)lastNameNumber;

            return lastName;
        }

        #endregion

        public void CheckForEventsThisWeek()
        {
            EventsThisWeek.Clear();
            foreach (CalendarEvent e in Calendar.Events)
            {
                if (e.EventDate.Week == WeekNumber && e.EventDate.MonthName == MonthName) EventsThisWeek.Add(e);
            }
        }
    }
}
