using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsAgencyTycoon
{
    public partial class CalendarForm : Form
    {
        public Calendar c;
        public List<CalendarEvent> PlayerBirthdays = new List<CalendarEvent>();
        public List<CalendarEvent> LeagueYearBegins = new List<CalendarEvent>();
        public List<CalendarEvent> LeagueYearEnds = new List<CalendarEvent>();
        public List<CalendarEvent> AssociationEvents = new List<CalendarEvent>();
        public CalendarForm(Calendar calendar)
        {
            InitializeComponent();
            c = calendar;
            PopulateLists();
            PopulateFormLabels();
        }
        public void PopulateLists()
        {
            c.Events = c.Events.OrderBy(o => o.EventDate.MonthNumber).ThenBy(o => o.EventDate.Week).ToList();
            foreach (CalendarEvent e in c.Events)
            {
                if (e.EventType == CalendarEventType.PlayerBirthday) PlayerBirthdays.Add(e);
                else if (e.EventType == CalendarEventType.LeagueYearBegins) LeagueYearBegins.Add(e);
                else if (e.EventType == CalendarEventType.LeagueYearEnds) LeagueYearEnds.Add(e);
                else if (e.EventType == CalendarEventType.AssociationEvent) AssociationEvents.Add(e);
            }
        }
        public void PopulateFormLabels()
        {
            string birthdayList = "";
            foreach (CalendarEvent e in PlayerBirthdays)
            {
                birthdayList += "[" + e.Sport.ToString() + "] " + e.EventName + ": Month - " + e.EventDate.MonthName.ToString() + ", Week #" + e.EventDate.Week.ToString() + Environment.NewLine;
            }
            lblPlayerBirthdays.Text = birthdayList;

            string leagueStart = "";
            foreach (CalendarEvent e in LeagueYearBegins)
            {
                leagueStart += e.EventName + Environment.NewLine;
            }
            lblLeagueYearsStart.Text = leagueStart;

            string leagueEnd = "";
            foreach (CalendarEvent e in LeagueYearEnds)
            {
                leagueEnd += e.EventName + Environment.NewLine;
            }
            lblLeagueYearsEnd.Text = leagueEnd;

            string associationEvents = "";
            foreach (CalendarEvent e in AssociationEvents)
            {
                associationEvents += "[" + e.Sport.ToString() + "] " + e.EventName + Environment.NewLine;
            }
            lblAssociationEvents.Text = associationEvents;
        }
    }
}
