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
        public string PlayerName;
        public Date EventDate;
        public int PlayerID;
        public int EventID;
        public Sports Sport;
        public int LoanRepaymentAmount;
        public int Year;

        // constructor method for PlayerBirthdays
        public CalendarEvent(Player player)
        {
            EventType = CalendarEventType.PlayerBirthday;
            EventName = player.FullName + "'s Birthday";
            PlayerName = player.FullName;
            EventDate = player.Birthday;
            PlayerID = player.Id;
            Sport = player.Sport;
        }

        // constructor method for ClientBirthday
        public CalendarEvent(Client client)
        {
            EventType = CalendarEventType.ClientBirthday;
            EventName = client.FullName + "'s Birthday";
            PlayerName = client.FullName;
            EventDate = client.Birthday;
            PlayerID = client.Id;
            Sport = client.Sport;
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
            EventID = e.Id;
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

        // constructor method for Progression/Regression for leagues
        public CalendarEvent(string s, League l)
        {
            EventType = CalendarEventType.ProgressionRegression;
            EventName = l.Abbreviation + " Progression/Regression";
            EventDate = new Date(l.SeasonStart.MonthNumber - 1, l.SeasonStart.MonthName - 1,  l.SeasonStart.Week);
            Sport = l.Sport;
        }

        // constructor method for Progression/Regression for Associations
        public CalendarEvent(Association a)
        {
            EventType = CalendarEventType.ProgressionRegression;
            EventName = a.Abbreviation + " Progression/Regression";
            EventDate = new Date(11, Months.December, 1);
            Sport = a.Sport;
        }
    }
}
