using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Endorsement
    {
        public Company Company;
        public EndorsementType EndorsementType;
        public int Years;
        public int TotalValue;
        public int MonthlyInstallment;
        public double AgentPercentage;

        public Endorsement(Company company, EndorsementType type, int years, int totalValue, double agentPercentage)
        {
            Company = company;
            EndorsementType = type;
            Years = years;
            TotalValue = totalValue;
            AgentPercentage = agentPercentage;
            MonthlyInstallment = CalculateMonthlyInstallment(TotalValue, Years);
        }

        private int CalculateMonthlyInstallment(int cash, int years)
        {
            int money = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(cash) / Convert.ToDouble(years * 12)));
            return money;
        }
    }
}
