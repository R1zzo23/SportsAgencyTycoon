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
    public partial class HireAgentForm : Form
    {
        private int _AgentApplicants;
        private int _AgentLevel;
        private List<Agent> agents = new List<Agent>();
        private int _FundsSpent;
        public int FundsSpent
        {
            get { return _FundsSpent; }
        }
        private string _AgentType;
        public string AgentType
        {
            get { return _AgentType; }
        }

        public HireAgentForm()
        {
            InitializeComponent();
        }
        public void SetFundsSpent(int i, string s)
        {
            _FundsSpent = i;
            HowManyAgents();
        }
        public void HowManyAgents()
        {
            if (_FundsSpent == 25000)
            {
                _AgentApplicants = 2;
                _AgentLevel = 1;
            }
            else if (_FundsSpent == 50000 || _FundsSpent == 100000)
            {
                _AgentApplicants = 3;
                _AgentLevel = 2;
            }
            else
            {
                _AgentApplicants = 3;
                _AgentLevel = 3;
            }
        }
        public void CreateApplicants(World world, Random rnd)
        {
            for (int i = 0; i < _AgentApplicants; i++)
            {
                agents.Add(new Agent(world.randomFirstName(rnd).ToString(), world.randomLastName(rnd).ToString(), DetermineSalary(_AgentLevel, _AgentType, rnd), 0, 0, 0, 0, _AgentLevel, Roles.Agent));
            }
            DisplayApplicantInformation();
        }
        public int DetermineSalary(int level, string type, Random rnd)
        {
            int salary = 0;
            int multiplier = 0;

            if (_AgentType != "PlayersAgent") multiplier = 2;

            multiplier += _AgentLevel;

            salary = rnd.Next(5, 13) * multiplier * 1000;

            return salary;
        }
        private void DisplayApplicantInformation()
        {
            Agent a1 = agents[0];
            Agent a2 = agents[1];
            radioApplicant1.Text = a1.First + " " + a1.Last + " (LVL " + _AgentLevel + ")";
            radioApplicant2.Text = a2.First + " " + a2.Last + " (LVL " + _AgentLevel + ")";
            lblAgent1.Text = a1.Salary.ToString("C0") + "/month | NEG: " + a1.Negotiating.ToString() + " | GRD: " + a1.Greed.ToString() + " | POW: " + a1.IndustryPower.ToString() + " | IQ: " + a1.Intelligence.ToString();
            lblAgent2.Text = a2.Salary.ToString("C0") + "/month | NEG: " + a2.Negotiating.ToString() + " | GRD: " + a2.Greed.ToString() + " | POW: " + a2.IndustryPower.ToString() + " | IQ: " + a2.Intelligence.ToString();
            if (agents.Count < 3)
            {
                radioApplicant3.Enabled = false;
                radioApplicant3.Text = "";
                lblAgent3.Text = "";
            }
            else
            {
                Agent a3 = agents[2];
                radioApplicant3.Text = a3.First + " " + a3.Last + " (LVL " + _AgentLevel + ")";
                lblAgent3.Text = "Applicant #3's Info";
            }
        }
    }
}
