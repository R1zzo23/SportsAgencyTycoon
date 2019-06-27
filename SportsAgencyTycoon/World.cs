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
        public List<Licenses> AvailableLicenses;
        public List<Client> AvailableClients;
        public List<Agent> AvailableAgents;
        public List<League> Leagues;
        public List<Association> Associations;
        public League NBA;
        public League MLB;
        public League NFL;
        public League NHL;
        public League MLS;
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
            AvailableClients = new List<Client>();
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

        public void InitializeLicenses()
        {
            Licenses basketballLicense = new Licenses(Sports.Basketball, 250, 1250, Months.July, 7);
            Licenses footballLicense = new Licenses(Sports.Football, 2500, 1650, Months.January, 1);
            AvailableLicenses.Add(basketballLicense);
            AvailableLicenses.Add(footballLicense);
        }
        /*public void CreateNewClients(Agency agency, Licenses license)
        {
            Random rnd = new Random();
            int numberNewClients = HowManyNewClients(agency.IndustryInfluence);
            for (int i = 0; i < numberNewClients; i++)
            {
                Client client = new Client(
                    randomFirstName(rnd).ToString(),
                    randomLastName(rnd).ToString(),
                    rnd.Next(18, 35),
                    rnd.Next(1, 5),
                    rnd.Next(5, 9),
                    rnd.Next(0, 75),
                    100,
                    0,
                    license.Sport);
                AvailableClients.Add(client);
            }
        }*/
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
            Random rnd = new Random();
            CreateLeagues(rnd);
            CreateAssociations(rnd);
            CreateAllEvents();
            CreateAthletesForAssociations(rnd);
            CreateTeamsForLeagues(rnd);
            AddLeaguesAndAssociationsToWorld();
            CreatePlayersForTeams(rnd);
            CreateCalendarEventsForLeagueStartAndEnd();
            CreatePlayerBirthdayCalendarEvents();
            CalculateWorldRankings();
        }
        public void CreateLeagues(Random rnd)
        {
            NBA = new League(Sports.Basketball, "National Basketball Association", "NBA", rnd.Next(45, 100), new Date(10, Months.October, 4), new Date(6, Months.June, 2), 40000000, 582180);
            MLB = new League(Sports.Baseball, "Major League Baseball", "MLB", rnd.Next(30, 70), new Date(4, Months.April, 1), new Date(10, Months.October, 4), 35000000, 555000);
            NFL = new League(Sports.Football, "National Football League", "NFL", rnd.Next(50, 100), new Date(8, Months.August, 1), new Date(2, Months.February, 1), 30000000, 495000);
            NHL = new League(Sports.Hockey, "National Hockey League", "NHL", rnd.Next(30, 55), new Date(10, Months.October, 1), new Date(6, Months.June, 2), 9500000, 650000);
            MLS = new League(Sports.Soccer, "Major League Soccer", "MLS", rnd.Next(15, 50), new Date(3, Months.March, 2), new Date(12, Months.December, 2), 7000000, 56250);
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
            CreateCalendarEventsForAssociationEvents();
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
            foreach (Event e in PGA.EventList) Calendar.AddCalendarEvent(new CalendarEvent(e));
            foreach (Event e in ATP.EventList) Calendar.AddCalendarEvent(new CalendarEvent(e));
            foreach (Event e in WBA.EventList) Calendar.AddCalendarEvent(new CalendarEvent(e));
            foreach (Event e in UFC.EventList) Calendar.AddCalendarEvent(new CalendarEvent(e));
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
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.PG));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 1, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.PG));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 2, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SG));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 3, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SG));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 4, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SF));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 5, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SF));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 6, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.PF));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 7, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.PF));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 8, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.CE));
                NBA.TeamList[i].Roster.Add(new BasketballPlayer(i * 10 + 9, Sports.Basketball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.CE));
            }
        }
        public void CreateFootballPlayers(Random rnd)
        {
            for (int i = 0; i < NFL.TeamList.Count; i++)
            {
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.QB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 1, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.QB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 2, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.RB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 3, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.RB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 4, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.FB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 5, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.FB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 6, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.WR));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 7, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.WR));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 8, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.TE));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 9, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.TE));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 10, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 11, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 12, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OG));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 13, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OG));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 14, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OT));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 15, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OT));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 16, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.DE));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 17, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.DE));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 18, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.DE));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 19, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.DE));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 20, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.DT));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 21, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.DT));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 22, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.LB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 23, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.LB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 24, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.LB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 25, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.LB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 26, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.CB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 27, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.CB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 28, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.CB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 29, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.CB));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 30, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SS));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 31, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SS));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 32, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.FS));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 33, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.FS));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 34, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.K));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 35, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.P));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 36, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.WR));
                NFL.TeamList[i].Roster.Add(new FootballPlayer(i * 38 + 37, Sports.Football, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(21, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.WR));
            }
        }
        public void CreateHockeyPlayers(Random rnd)
        {
            for (int i = 0; i < NHL.TeamList.Count; i++)
            {
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.G));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 1, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.G));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 2, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 3, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 4, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 5, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 6, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 7, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 8, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 9, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 10, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 11, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 12, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 13, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 14, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 15, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.W));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 16, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 17, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
                NHL.TeamList[i].Roster.Add(new HockeyPlayer(i * 19 + 18, Sports.Hockey, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 38), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
            }
        }
        public void CreateBaseballPlayers(Random rnd)
        {
            for (int i = 0; i < MLB.TeamList.Count; i++)
            {
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 1, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.C));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 2, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 3, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 4, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 5, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 6, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 7, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 8, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 9, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.INF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 10, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 11, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 12, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 13, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.OF));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 14, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 15, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 16, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 17, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.SP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 18, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.RP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 19, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.RP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 20, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.RP));
                MLB.TeamList[i].Roster.Add(new BaseballPlayer(i * 22 + 21, Sports.Baseball, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 37), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.RP));
            }
        }
        public void CreateSoccerPlayers(Random rnd)
        {
            for (int i = 0; i < MLS.TeamList.Count; i++)
            {
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.GK));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 1, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.GK));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 2, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 3, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 4, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 5, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 6, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.D));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 7, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.MID));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 8, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.MID));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 9, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.MID));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 10, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.MID));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 11, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.MID));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 12, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.F));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 13, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.F));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 14, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.F));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 15, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.F));
                MLS.TeamList[i].Roster.Add(new SoccerPlayer(i * 17 + 16, Sports.Soccer, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 33), (Months)rnd.Next(0, 12), rnd.Next(1, 6), Position.F));
            }
        }
        public void CreateGolfers(Random rnd)
        {
            for (var i = 0; i < 144; i++)
            {
                PGA.PlayerList.Add(new Golfer(i, Sports.Golf, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 65), (Months)rnd.Next(0,12), rnd.Next(1, 6)));
            }
        }
        public void CreateTennisPlayers(Random rnd)
        {
            for (var i = 0; i < 128; i++)
            {
                ATP.PlayerList.Add(new TennisPlayer(i, Sports.Tennis, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 35), (Months)rnd.Next(0, 12), rnd.Next(1, 6)));
            }
        }
        public void CreateBoxers(Random rnd)
        {
            for (var i = 0; i < 40; i++)
            {
                WBA.PlayerList.Add(new Boxer(i, Sports.Boxing, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 34), (Months)rnd.Next(0, 12), rnd.Next(1, 6)));
            }
        }
        public void CreateMMAFighters(Random rnd)
        {
            for (var i = 0; i < 50; i++)
            {
                UFC.PlayerList.Add(new MMAFighter(i, Sports.MMA, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(17, 31), (Months)rnd.Next(0, 12), rnd.Next(1, 6)));
            }
        }
        public void CreatePlayerBirthdayCalendarEvents()
        {
            foreach (Player p in PGA.PlayerList) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Player p in ATP.PlayerList) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Player p in WBA.PlayerList) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Player p in UFC.PlayerList) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Team t in NBA.TeamList)
                foreach (Player p in t.Roster) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Team t in NFL.TeamList)
                foreach (Player p in t.Roster) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Team t in MLB.TeamList)
                foreach (Player p in t.Roster) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Team t in NHL.TeamList)
                foreach (Player p in t.Roster) Calendar.AddCalendarEvent(new CalendarEvent(p));
            foreach (Team t in MLS.TeamList)
                foreach (Player p in t.Roster) Calendar.AddCalendarEvent(new CalendarEvent(p));
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
            NBA.TeamList.Add(new Team("Atlanta", "Hawks", "ATL", 40, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Boston", "Celtics", "BOS", 60, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Brooklyn", "Nets", "BKN", 62, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Charlotte", "Hornets", "CHA", 33, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Chicago", "Bulls", "CHI", 62, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Cleveland", "Cavaliers", "CLE", 30, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Dallas", "Mavericks", "DAL", 49, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Denver", "Nuggets", "DEN", 38, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Detroit", "Pistons", "DET", 33, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Golden State", "Warriors", "GSW", 66, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Houston", "Rockets", "HOU", 49, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Indiana", "Pacers", "IND", 43, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Los Angeles", "Clippers", "LAC", 72, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Los Angeles", "Lakers", "LAL", 75, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Memphis", "Grizzlies", "MEM", 40, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Miami", "Heat", "MIA", 69, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Milwaukee", "Bucks", "MIL", 46, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Minnesota", "Timberwolves", "MIN", 46, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("New Orleans", "Pelicans", "NOP", 51, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("New York", "Knicks", "NYK", 82, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Oklahoma City", "Thunder", "OKC", 48, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Orlando", "Magic", "ORL", 52, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Philadelphia", "76ers", "PHI", 58, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Phoenix", "Suns", "PHO", 42, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Portland", "Trail Blazers", "POR", 54, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Sacramento", "Kings", "SAC", 54, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("San Antonio", "Spurs", "SAS", 51, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Toronto", "Raptors", "TOR", 47, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Utah", "Jazz", "UTA", 38, rnd.Next(25, 76)));
            NBA.TeamList.Add(new Team("Washington", "Wizards", "WAS", 47, rnd.Next(25, 76)));
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
                List<Player> playerList = association.PlayerList.OrderByDescending(o => o.SkillLevel).ToList();
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
            }

            //check for birthdays
            /*foreach (Association a in Associations)
            {
                foreach (Player p in a.PlayerList)
                {
                    if (p.BirthMonth == MonthName && p.BirthWeek == WeekNumber) p.Age++;
                }
            }
            foreach (Client c in agency.Clients)
            {
                if (c.BirthMonth == MonthName && c.BirthWeek == WeekNumber) c.Age++;
            }*/
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
            /*foreach (Association a in Associations)
            {
                foreach (Event e in a.EventList)
                {
                    if (e.EventDate.Week == WeekNumber && e.EventDate.MonthName == MonthName && e.Year == Year)
                    {
                        EventsThisWeek.Add(e);
                    }
                }
            }*/
        }
    }
}
