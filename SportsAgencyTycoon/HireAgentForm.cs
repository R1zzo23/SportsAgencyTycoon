﻿using System;
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
        private Agent _HiredAgent;
        public Agent HiredAgent
        {
            get { return _HiredAgent; }
        }
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

        public HireAgentForm(int i, string s)
        {
            InitializeComponent();
            _FundsSpent = i;
            _AgentType = s;
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
            List<int> ratings = new List<int>();

            for (int i = 0; i < _AgentApplicants; i++)
            {
                ratings.Clear();
                ratings.Add(DetermineNegotiating(_AgentType, rnd));
                ratings.Add(DetermineGreed(_AgentType, rnd));
                ratings.Add(DeterminePower(_AgentType, rnd));
                ratings.Add(DetermineIntelligence(_AgentType, rnd));
                ratings.Add(DetermineSalary(_AgentLevel, _AgentType, rnd, ratings));
                agents.Add(new Agent(
                    world.randomFirstName(rnd).ToString(), 
                    world.randomLastName(rnd).ToString(), 
                    ratings[4],
                    ratings[0],
                    ratings[1],
                    ratings[2],
                    ratings[3],
                    _AgentLevel, 
                    Roles.Agent));
            }
            DisplayApplicantInformation();
        }
        public int DetermineSalary(int level, string type, Random rnd, List<int> ratings)
        {
            int salary = 8000;
            double multiplier = 0;

            int sum = 0;
            foreach (int i in ratings) sum += i;

            int ratingsDifference = sum - 80;
            salary += ratingsDifference * 500;

            if (_AgentType != "PlayersAgent") multiplier = 0.2;

            salary = (int)(salary * (1 + multiplier));

            return salary;
        }
        public int DetermineRating(string agentType, bool agentSpecialty, Random rnd)
        {
            int rating = 0;

            rating = rnd.Next(15, 22);

            // agentyType of PlayersAgent gets small boost in all categories
            if (agentType == "PlayersAgent")
            {
                rating += 2;
            }
            // agentTypes other than PlayersAgent gets larger boost in their 
            // main category and small nerf in all other categories
            else
            {
                if (_AgentLevel == 1)
                {
                    if (agentSpecialty) rating += 5;
                    else rating -= 2;
                }
                else if (_AgentLevel == 2)
                {
                    if (agentSpecialty) rating += 7;
                    else rating += 1;
                }
                else if (_AgentLevel == 3)
                {
                    if (agentSpecialty) rating += 12;
                    else rating += 5;
                }
            }
            
            return rating;
        }
        public int DetermineNegotiating(string agentType, Random rnd)
        {
            int rating = 0;
            bool agentSpecialty;

            if (agentType == "SmoothTalker" || agentType == "SportsShark") agentSpecialty = true;
            else agentSpecialty = false;

            rating = DetermineRating(agentType, agentSpecialty, rnd);

            return rating;
        }
        public int DetermineGreed(string agentType, Random rnd)
        {
            int rating = 0;
            bool agentSpecialty;

            if (agentType == "SportsShark") agentSpecialty = true;
            else agentSpecialty = false;

            rating = DetermineRating(agentType, agentSpecialty, rnd);

            return rating;
        }
        public int DeterminePower(string agentType, Random rnd)
        {
            int rating = 0;
            bool agentSpecialty;

            if (agentType == "IndustryBuff" || agentType == "SportsShark") agentSpecialty = true;
            else agentSpecialty = false;

            rating = DetermineRating(agentType, agentSpecialty, rnd);

            return rating;
        }
        public int DetermineIntelligence(string agentType, Random rnd)
        {
            int rating = 0;
            bool agentSpecialty;

            if (agentType == "IndustryBuff" || agentType == "SmoothTalker") agentSpecialty = true;
            else agentSpecialty = false;

            rating = DetermineRating(agentType, agentSpecialty, rnd);

            return rating;
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
                lblAgent3.Text = a3.Salary.ToString("C0") + "/month | NEG: " + a3.Negotiating.ToString() + " | GRD: " + a3.Greed.ToString() + " | POW: " + a3.IndustryPower.ToString() + " | IQ: " + a3.Intelligence.ToString();
            }
        }

        private void btnPassOnHiring_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHireSelected_Click(object sender, EventArgs e)
        {
            if (radioApplicant1.Checked)
            {
                _HiredAgent = agents[0];
                this.Close();
            }
            else if (radioApplicant2.Checked)
            {
                _HiredAgent = agents[1];
                this.Close();
            }
            else if (radioApplicant3.Checked)
            {
                _HiredAgent = agents[2];
                this.Close();
            }
            else MessageBox.Show("You must select one of the applicants before hiring.");
        }
    }
}
