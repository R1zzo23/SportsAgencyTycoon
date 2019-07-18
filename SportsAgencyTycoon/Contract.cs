using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Contract
    {
        public int Years;
        public int YearlySalary;
        public Date StartDate;
        public Date EndDate;
        public int SigningBonus;  //only for NFL players
        public int MonthlySalary;
        public double AgentPercentage;
        public PaySchedule AgentPaySchedule;

        public Contract(int years, int yearlySalary, int monthlySalary, Date startDate, Date endDate, int signingBonus, double agentPercentage, PaySchedule agentPaySchedule)
        {
            Years = years;
            YearlySalary = yearlySalary;
            MonthlySalary = monthlySalary;
            StartDate = startDate;
            EndDate = endDate;
            SigningBonus = signingBonus;
            AgentPercentage = agentPercentage;
            AgentPaySchedule = agentPaySchedule;
        }
    }
}
