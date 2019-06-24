using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class CalendarEvent
    {
        public CalendarEventType EventType;
        public Date EventDate;
        public int PlayerID;
        public Sports Sport;
        public int LoanRepaymentAmount;

        // constructor method for PlayerBirthdays
        public CalendarEvent(Player player)
        {
            EventType = CalendarEventType.PlayerBirthday;
            EventDate = player.Birthday;
            PlayerID = player.Id;
            Sport = player.Sport;
        }

        // constructor method for LoanRepayment
        public CalendarEvent(Date loanRepaymentDate, int loanRepaymentAmount)
        {
            EventType = CalendarEventType.LoanRepayment;
            EventDate = loanRepaymentDate;
            LoanRepaymentAmount = loanRepaymentAmount;
        }

        // constructor method for AssociationEvent
        public CalendarEvent(Event e)
        {
            EventType = CalendarEventType.AssociationEvent;
            EventDate = e.EventDate;
            Sport = e.Sport;
        }
    }
}
