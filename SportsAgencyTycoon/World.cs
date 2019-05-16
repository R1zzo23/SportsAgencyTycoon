using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class World
    {
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
        public World()
        {
            AvailableLicenses = new List<Licenses>();
            AvailableClients = new List<Client>();
            AvailableAgents = new List<Agent>();
            Leagues = new List<League>();
            Associations = new List<Association>();
            
            Year = 2000;
            MonthNumber = 5;
            MonthName = Months.June;
            WeekNumber = 1;
        }

        public void InitializeLicenses()
        {
            Licenses basketballLicense = new Licenses(Sports.Basketball, 250, 1250, Months.July, 7);
            Licenses footballLicense = new Licenses(Sports.Football, 2500, 1650, Months.January, 1);
            AvailableLicenses.Add(basketballLicense);
            AvailableLicenses.Add(footballLicense);
        }
        public void CreateNewClients(Agency agency, Licenses license)
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
            CreateLeagues();
            CreateAssociations();
            CreateAllEvents();
            CreateAthletesForAssociations();
            CreateTeamsForLeagues();
            AddLeaguesAndAssociationsToWorld();
        }
        public void CreateLeagues()
        {
            Random rnd = new Random();
            NBA = new League(Sports.Basketball, "National Basketball Association", "NBA", rnd.Next(45, 100), new Date(10, Months.October, 4), new Date(6, Months.June, 2), 40000000, 582180);
            MLB = new League(Sports.Baseball, "Major League Baseball", "MLB", rnd.Next(30, 70), new Date(4, Months.April, 1), new Date(10, Months.October, 4), 35000000, 555000);
            NFL = new League(Sports.Football, "National Football League", "NFL", rnd.Next(50, 100), new Date(8, Months.August, 1), new Date(2, Months.February, 1), 30000000, 495000);
            NHL = new League(Sports.Hockey, "National Hockey League", "NHL", rnd.Next(30, 55), new Date(10, Months.October, 1), new Date(6, Months.June, 2), 9500000, 650000);
            MLS = new League(Sports.Soccer, "Major League Soccer", "MLS", rnd.Next(15, 50), new Date(3, Months.March, 2), new Date(12, Months.December, 2), 7000000, 56250);
        }
        public void CreateAssociations()
        {
            Random rnd = new Random();
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
            PGA.EventList.Add(new Event("World Golf Championships", "Mexico City, MEX", 10250000, 100, 70, new Date(2, Months.February, 3)));
            PGA.EventList.Add(new Event("THE PLAYERS Championship", "Ponte Vedra Beach,  FL", 6700000, 75, 50, new Date(3, Months.March, 2)));
            PGA.EventList.Add(new Event("Masters Tournament", "Augusta, GA", 11500000, 60, 50, new Date(4, Months.April, 2)));
            PGA.EventList.Add(new Event("PGA Championship", "Farmingdale, NY", 11000000, 100, 70, new Date(5, Months.May, 3)));
            PGA.EventList.Add(new Event("U.S. Open", "Pebble Beach, CA", 12000000, 100, 70, new Date(6, Months.June, 2)));
            PGA.EventList.Add(new Event("The Open Championship", "Portrush, NIR", 11000000, 85, 60, new Date(7, Months.July, 7)));
            PGA.EventList.Add(new Event("THE NORTHERN TRUST", "Jersey City, NJ", 9250000, 100, 70, new Date(8, Months.August, 1)));
            PGA.EventList.Add(new Event("BMW Championship", "Medinah, IL", 9250000, 100, 70, new Date(8, Months.August, 3)));
            PGA.EventList.Add(new Event("TOUR Championship", "Atlanta, GA", 9000000, 100, 70, new Date(8, Months.August, 4)));

        }
        public void CreateATPEvents()
        {
            ATP.EventList.Add(new Event("Australian Open", "Melbourne, AUS", 15000000, 64, 64, new Date(1, Months.January, 3)));
            ATP.EventList.Add(new Event("French Open", "Paris, FR", 20000000, 64, 64, new Date(5, Months.May, 4)));
            ATP.EventList.Add(new Event("Wimbledon", "London, EN", 38000000, 100, 100, new Date(6, Months.June, 5)));
            ATP.EventList.Add(new Event("US Open", "New York City, NY", 40000000, 100, 100, new Date(9, Months.September, 1)));
        }
        public void CreateWBAEvents()
        {
            WBA.EventList.Add(new Event("Up-And-Comers Showcase", "Las Vegas, NV", 15000000, 30, 30, new Date(1, Months.January, 1)));
            WBA.EventList.Add(new Event("NYC Prize Fights", "New York City, NY", 35000000, 12, 12, new Date(5, Months.May, 2)));
            WBA.EventList.Add(new Event("Top Rank Boxing Premiere", "Los Angeles, CA", 29000000, 10, 10, new Date(9, Months.September, 3)));
            WBA.EventList.Add(new Event("King of the Ring", "Las Vegas, NV", 50000000, 40, 40, new Date(11, Months.November, 4)));
        }
        public void CreateUFCEvents()
        {
            UFC.EventList.Add(new Event("Bloodied & Bruised", "Chicago, IL", 2500000, 10, 10, new Date(2, Months.February, 3)));
            UFC.EventList.Add(new Event("King Kong of the Octagon", "Atlantic City, NJ", 50, 50, 6000000, new Date(6, Months.June, 1)));
            UFC.EventList.Add(new Event("Elite Title Showdown", "Las Vegas, NV", 8, 8, 3250000, new Date(10, Months.October, 2)));
        }
        #endregion
        #region Create Athletes for Associations
        public void CreateAthletesForAssociations()
        {
            CreateGolfers();
            CreateTennisPlayers();
            CreateBoxers();
            CreateMMAFighters();
        }
        public void CreateGolfers()
        {
            Random rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                PGA.PlayerList.Add(new Player(Sports.Golf, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 65)));
            }
        }
        public void CreateTennisPlayers()
        {
            Random rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                ATP.PlayerList.Add(new Player(Sports.Tennis, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(18, 35)));
            }
        }
        public void CreateBoxers()
        {
            Random rnd = new Random();
            for (var i = 0; i < 40; i++)
            {
                WBA.PlayerList.Add(new Player(Sports.Boxing, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(16, 34)));
            }
        }
        public void CreateMMAFighters()
        {
            Random rnd = new Random();
            for (var i = 0; i < 50; i++)
            {
                UFC.PlayerList.Add(new Player(Sports.MMA, randomFirstName(rnd), randomLastName(rnd), rnd.Next(25, 76), rnd.Next(17, 31)));
            }
        }
        #endregion
        #region Create Teams for Leagues
        public void CreateTeamsForLeagues()
        {
            CreateNBATeams();
            CreateMLBTeams();
            CreateNHLTeams();
            CreateNFLTeams();
            CreateMLSTeams();
        }
        public void CreateNBATeams()
        {
            Random rnd = new Random();
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
        public void CreateMLBTeams()
        {
            Random rnd = new Random();
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
        public void CreateNHLTeams()
        {
            Random rnd = new Random();
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
        public void CreateNFLTeams()
        {
            Random rnd = new Random();
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
        public void CreateMLSTeams()
        {
            Random rnd = new Random();
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
        public void HandleCalendar()
        {
            //add 1 to week number
            WeekNumber++;

            //check if month ends
            if (((WeekNumber == 5) && ((MonthNumber + 1) % 3 != 0)) || ((WeekNumber == 6) && ((MonthNumber + 1) % 3 == 0)))
            {
                SetNewMonth();
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
    }
}
