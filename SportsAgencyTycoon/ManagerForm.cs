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
    public partial class ManagerForm : Form
    {
        public ManagerForm(Agent manager, Agency agency)
        {
            InitializeComponent();
            managerNameLabel.Text = manager.First + " " + manager.Last;
            memberOfAgencyLabel.Text = "Member of " + agency.Name;
            greedLabel.Text = manager.Greed.ToString();
            industryPowerLabel.Text = manager.IndustryPower.ToString();
            levelLabel.Text = manager.Level.ToString();
            negotiatingLabel.Text = manager.Negotiating.ToString();

        }
        
    }
}
