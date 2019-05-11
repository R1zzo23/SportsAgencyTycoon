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
            WBA = new Association(Sports.Boxing, "World Boxing Assocition", "WBA", rnd.Next(10, 40));
            UFC = new Association(Sports.MMA, "Ultimate Fighting Championship", "UFC", rnd.Next(30, 60));
        }
        #endregion
        #region Create Events for Associations
        public void CreatePGAEvents()
        {

        }
        public void CreateATPEvents()
        {

        }
        public void CreateWBAEvents()
        {

        }
        public void CreateUFCEvents()
        {

        }
        #endregion
        #region Create Athletes for Associations
        public void CreateGolfers()
        {

        }
        public void CreateTennisPlayers()
        {

        }
        public void CreateBoxers()
        {

        }
        public void CreateMMAFighters()
        {

        }
        #endregion
        #region Create Teams for Leagues
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

        }
        public void CreateNHLTeams()
        {

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
            NFL.TeamList.Add(new Team("Arizona", "Cardinals", "ARI", 33, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Seattle", "Seahawks", "SEA", 47, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Tampa Bay", "Buccaneers", "TB", 36, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Tennessee", "Titans", "TEN", 48, rnd.Next(25, 76)));
            NFL.TeamList.Add(new Team("Washington", "Redskins", "WAS", 47, rnd.Next(25, 76)));

        }
        public void CreateMLSTeams()
        {

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
