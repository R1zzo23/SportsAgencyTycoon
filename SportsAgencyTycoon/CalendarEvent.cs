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
        public string EventName;
        public Date EventDate;
        public int PlayerID;
        public Sports Sport;
        public int LoanRepaymentAmount;

        // constructor method for PlayerBirthdays
        public CalendarEvent(Player player)
        {
            EventType = CalendarEventType.PlayerBirthday;
            EventName = player.FullName + "'s Birthday";
            EventDate = player.Birthday;
            PlayerID = player.Id;
            Sport = player.Sport;
        }

        // constructor method for LoanRepayment
        public CalendarEvent(Date loanRepaymentDate, int loanRepaymentAmount)
        {
            EventType = CalendarEventType.LoanRepayment;
            EventName = "Agency Repays Loan of " + loanRepaymentAmount.ToString("C0"); 
            EventDate = loanRepaymentDate;
            LoanRepaymentAmount = loanRepaymentAmount;
        }

        // constructor method for AssociationEvent
        public CalendarEvent(Event e)
        {
            EventType = CalendarEventType.AssociationEvent;
            EventName = e.Year + " " + e.Name;
            EventDate = e.EventDate;
            Sport = e.Sport;
        }

        // constructor method for LeagueYearBegins
        public CalendarEvent(League l)
        {
            EventType = CalendarEventType.LeagueYearBegins;
            EventName = l.Abbreviation + " Year Begins";
            EventDate = l.SeasonStart;
            Sport = l.Sport;
        }

        // constrcutor method for LeagueYearBeings and LeagueYearEnds
        public CalendarEvent(League l, string s)
        {
            EventType = CalendarEventType.LeagueYearEnds;
            EventName = l.Abbreviation + " Year Ends";
            EventDate = l.SeasonEnd;
            Sport = l.Sport;
        }
    }
}
