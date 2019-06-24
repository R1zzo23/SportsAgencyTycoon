using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsAgencyTycoon
{
    public class Calendar
    {
        public MainForm MainForm;
        public List<CalendarEvent> Events = new List<CalendarEvent>();

        public Calendar (MainForm form)
        {
            MainForm = form;
        }
        public void AddCalendarEvent(CalendarEvent e)
        {
            Events.Add(e);
        }
    }
}
