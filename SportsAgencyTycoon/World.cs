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
        public void CreateNewClients(Agency agency)
        {
            Random rnd = new Random();
            int numberNewClients = HowManyNewClients(agency.IndustryInfluence);
            for (int i = 0; i < numberNewClients; i++)
            {
                //Client client = new Client(randomFirstName(), randomLastName(), rnd.Next(28, 66), )
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

        public FirstName randomFirstName()
        {
            FirstName firstName;

            Random rnd = new Random();
            int firstNameNumber = rnd.Next(0, firstNameCount);
            firstName = (FirstName)firstNameNumber;

            return firstName;
        }
        public LastName randomLastName()
        {
            LastName lastName;

            Random rnd = new Random();
            int lastNameNumber = rnd.Next(0, lastNameCount);
            lastName = (LastName)lastNameNumber;

            return lastName;
        }

        #endregion
    }

    public enum Months
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    public enum FirstName
    {
        Liam,
        Noah,
        William,
        James,
        Logan,
        Benjamin,
        Mason,
        Elijah,
        Oliver,
        Jacob,
        Lucas,
        Michael,
        Alexander,
        Ethan,
        Daniel,
        Matthew,
        Aiden,
        Henry,
        Joseph,
        Jackson,
        Samuel,
        Sebastian,
        David,
        Carter,
        Wyatt,
        Jayden,
        John,
        Owen,
        Dylan,
        Luke,
        Gabriel,
        Anthony,
        Isaac,
        Grayson,
        Jack,
        Julian,
        Levi,
        Christopher,
        Joshua,
        Andrew,
        Lincoln,
        Mateo,
        Ryan,
        Jaxon,
        Nathan,
        Aaron,
        Isaiah,
        Thomas,
        Charles,
        Caleb,
        Josiah,
        Christian,
        Hunter,
        Eli,
        Jonathan,
        Connor,
        Landon,
        Adrian,
        Asher,
        Cameron,
        Leo,
        Theodore,
        Jeremiah,
        Hudson,
        Robert,
        Easton,
        Nolan,
        Nicholas,
        Ezra,
        Colton,
        Angel,
        Brayden,
        Jordan,
        Dominic,
        Austin,
        Ian,
        Adam,
        Elias,
        Jaxson,
        Greyson,
        Jose,
        Ezekiel,
        Carson,
        Evan,
        Maverick,
        Bryson,
        Jace,
        Cooper,
        Xavier,
        Parker,
        Roman,
        Jason,
        Santiago,
        Chase,
        Sawyer,
        Gavin,
        Leonardo,
        Kayden,
        Ayden,
        Jameson,
        Kevin,
        Bentley,
        Zachary,
        Everett,
        Axel,
        Tyler,
        Micah,
        Vincent,
        Weston,
        Miles,
        Wesley,
        Nathaniel,
        Harrison,
        Brandon,
        Cole,
        Declan,
        Luis,
        Braxton,
        Damian,
        Silas,
        Tristan,
        Ryder,
        Bennett,
        George,
        Emmett,
        Justin,
        Kai,
        Max,
        Diego,
        Luca,
        Ryker,
        Carlos,
        Maxwell,
        Kingston,
        Ivan,
        Maddox,
        Juan,
        Ashton,
        Jayce,
        Rowan,
        Kaiden,
        Giovanni,
        Eric,
        Jesus,
        Calvin,
        Abel,
        King,
        Camden,
        Amir,
        Blake,
        Alex,
        Brody,
        Malachi,
        Emmanuel,
        Jonah,
        Beau,
        Jude,
        Antonio,
        Alan,
        Elliott,
        Elliot,
        Waylon,
        Xander,
        Timothy,
        Victor,
        Bryce,
        Finn,
        Brantley,
        Edward,
        Abraham,
        Patrick,
        Grant,
        Karter,
        Hayden,
        Richard,
        Miguel,
        Joel,
        Gael,
        Tucker,
        Rhett,
        Avery,
        Steven,
        Graham,
        Kaleb,
        Jasper,
        Jesse,
        Matteo,
        Dean,
        Zayden,
        Preston,
        August,
        Oscar,
        Jeremy,
        Alejandro,
        Marcus,
        Dawson,
        Lorenzo,
        Messiah,
        Zion,
        Maximus
    }
    public enum LastName
    {
        Smith,
        Johnson,
        Williams,
        Jones,
        Brown,
        Davis,
        Miller,
        Wilson,
        Moore,
        Taylor,
        Anderson,
        Thomas,
        Jackson,
        White,
        Harris,
        Martin,
        Thompson,
        Garcia,
        Martinez,
        Robinson,
        Clark,
        Rodriguez,
        Lewis,
        Lee,
        Walker,
        Hall,
        Allen,
        Young,
        Hernandez,
        King,
        Wright,
        Lopez,
        Hill,
        Scott,
        Green,
        Adams,
        Baker,
        Gonzalez,
        Nelson,
        Carter,
        Mitchell,
        Perez,
        Roberts,
        Turner,
        Phillips,
        Campbell,
        Parker,
        Evans,
        Edwards,
        Collins,
        Stewart,
        Sanchez,
        Morris,
        Rogers,
        Reed,
        Cook,
        Morgan,
        Bell,
        Murphy,
        Bailey,
        Rivera,
        Cooper,
        Richardson,
        Cox,
        Howard,
        Ward,
        Torres,
        Peterson,
        Gray,
        Ramirez,
        James,
        Watson,
        Brooks,
        Kelly,
        Sanders,
        Price,
        Bennett,
        Wood,
        Barnes,
        Ross,
        Henderson,
        Coleman,
        Jenkins,
        Perry,
        Powell,
        Long,
        Patterson,
        Hughes,
        Flores,
        Washington,
        Butler,
        Simmons,
        Foster,
        Gonzales,
        Bryant,
        Alexander,
        Russell,
        Griffin,
        Diaz,
        Hayes,
        Myers,
        Ford,
        Hamilton,
        Graham,
        Sullivan,
        Wallace,
        Woods,
        Cole,
        West,
        Jordan,
        Owens,
        Reynolds,
        Fisher,
        Ellis,
        Harrison,
        Gibson,
        Mcdonald,
        Cruz,
        Marshall,
        Ortiz,
        Gomez,
        Murray,
        Freeman,
        Wells,
        Webb,
        Simpson,
        Stevens,
        Tucker,
        Porter,
        Hunter,
        Hicks,
        Crawford,
        Henry,
        Boyd,
        Mason,
        Morales,
        Kennedy,
        Warren,
        Dixon,
        Ramos,
        Reyes,
        Burns,
        Gordon,
        Shaw,
        Holmes,
        Rice,
        Robertson,
        Hunt,
        Black,
        Daniels,
        Palmer,
        Mills,
        Nichols,
        Grant,
        Knight,
        Ferguson,
        Rose,
        Stone,
        Hawkins,
        Dunn,
        Perkins,
        Hudson,
        Spencer,
        Gardner,
        Stephens,
        Payne,
        Pierce,
        Berry,
        Matthews,
        Arnold,
        Wagner,
        Willis,
        Ray,
        Watkins,
        Olson,
        Carroll,
        Duncan,
        Snyder,
        Hart,
        Cunningham,
        Bradley,
        Lane,
        Andrews,
        Ruiz,
        Harper,
        Fox,
        Riley,
        Armstrong,
        Carpenter,
        Weaver,
        Greene,
        Lawrence,
        Elliott,
        Chavez,
        Sims,
        Austin,
        Peters,
        Kelley,
        Franklin,
        Lawson,
        Fields,
        Gutierrez,
        Ryan,
        Schmidt,
        Carr,
        Vasquez,
        Castillo,
        Wheeler,
        Chapman,
        Oliver,
        Montgomery,
        Richards,
        Williamson,
        Johnston
    }
}
