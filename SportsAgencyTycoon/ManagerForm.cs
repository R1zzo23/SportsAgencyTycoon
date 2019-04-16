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
        public ManagerForm()
        {
            InitializeComponent();
            Form1 form1 = (Form1)this.MdiParent;
            Agent manager = form1.myManager;
            managerNameLabel.Text = manager.First + " " + manager.Last;
            greedLabel.Text = manager.Greed.ToString();
            industryPowerLabel.Text = manager.IndustryPower.ToString();
            levelLabel.Text = manager.Level.ToString();
            negotiatingLabel.Text = manager.Negotiating.ToString();

        }
        
    }
}
