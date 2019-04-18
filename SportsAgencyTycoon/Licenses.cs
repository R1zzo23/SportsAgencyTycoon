using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Licenses
    {
        public Sports Sport;
        public int ApplicationFee;
        public int YearlyDues;
        public Months MonthOfRenewal;
        public int MonthOfRenewalNumber;

        public Licenses(Sports sport, int applicationFee, int yearlyDues, Months monthOfRenewal, int monthOfRenewalNumber)
        {
            Sport = sport;
            ApplicationFee = applicationFee;
            YearlyDues = yearlyDues;
            MonthOfRenewal = monthOfRenewal;
            MonthOfRenewalNumber = monthOfRenewalNumber;
        }

        public void InitializeLicenses()
        {

        }
    }
}
