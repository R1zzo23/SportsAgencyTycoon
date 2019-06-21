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
    public partial class AgentSearch : Form
    {
        private Agency myAgency;
        private int _FundsSpent;
        public int FundsSpent {
            get { return _FundsSpent; }
        }
        private string _AgentType;
        public string AgentType
        {
            get { return _AgentType; }
        }
        public AgentSearch(Agency agency)
        {
            InitializeComponent();
            myAgency = agency;

        }
        private bool AgencyHasEnoughMoney(int i)
        {
            bool enoughMoney;

            if (i > myAgency.Money) enoughMoney = false;
            else enoughMoney = true;

            return enoughMoney;
        }
        private void btn25kAgentSearch_Click(object sender, EventArgs e)
        {
            if (AgencyHasEnoughMoney(25000)) UpdateAndClose(25000);
            else MessageBox.Show("Agency doesn't have enough funds!");
        }

        private void btn50kAgentSearch_Click(object sender, EventArgs e)
        {
            if (AgencyHasEnoughMoney(50000)) UpdateAndClose(50000);
            else MessageBox.Show("Agency doesn't have enough funds!");
        }

        private void btn100kAgentSearch_Click(object sender, EventArgs e)
        {
            if (AgencyHasEnoughMoney(100000)) UpdateAndClose(100000);
            else MessageBox.Show("Agency doesn't have enough funds!");
        }

        private void btn250kAgentSearch_Click(object sender, EventArgs e)
        {
            if (AgencyHasEnoughMoney(250000)) UpdateAndClose(250000);
            else MessageBox.Show("Agency doesn't have enough funds!");
        }
        private void UpdateAndClose(int i)
        {
            if (radioIndustryBuff.Checked) _AgentType = "IndustryBuff";
            else if (radioPlayersAgent.Checked) _AgentType = "PlayersAgent";
            else if (radioSmoothTalker.Checked) _AgentType = "SmoothTalker";
            else if (radioSportsShark.Checked) _AgentType = "SportsShark";
            _FundsSpent = i;
            this.Close();
        }
    }
}
