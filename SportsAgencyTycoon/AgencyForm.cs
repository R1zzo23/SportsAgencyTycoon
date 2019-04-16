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
    public partial class AgencyForm : Form
    {
        public AgencyForm(Agency agency)
        {
            InitializeComponent();
            agencyNameLabel.Text = agency.Name;
            moneyLabel.Text = agency.Money.ToString("C");
            industryInfluenceLabel.Text = agency.IndustryInfluence.ToString() + "/100";
            clientCountLabel.Text = agency.ClientCount.ToString();
            agentCountLabel.Text = agency.AgentCount.ToString();
        }
    }
}
