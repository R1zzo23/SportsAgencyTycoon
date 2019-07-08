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
        public PaySchedule AgentPaySchedule;

        public Contract(int years, int yearlSalary, Date startDate, Date endDate, int signingBonus, PaySchedule agentPaySchedule)
        {
            Years = years;
            YearlySalary = yearlSalary;
            StartDate = startDate;
            EndDate = endDate;
            SigningBonus = signingBonus;
            AgentPaySchedule = agentPaySchedule;
        }
    }
}
