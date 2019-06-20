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
        private int _FundsSpent;
        public int FundsSpent {
            get { return _FundsSpent; }
        }
        private string _AgentType;
        public string AgentType
        {
            get { return _AgentType; }
        }
        public AgentSearch()
        {
            InitializeComponent();
        }

        private void btn25kAgentSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(25000);
        }

        private void btn50kAgentSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(50000);
        }

        private void btn100kAgentSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(100000);
        }

        private void btn250kAgentSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(250000);
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
