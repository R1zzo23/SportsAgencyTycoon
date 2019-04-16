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
    public partial class Form1 : Form
    {
        public Agency agency;
        public Agent myManager;
        public Form1()
        {
            InitializeComponent();
        }

        CreateManager createManagerForm;

        private void managerAndAgentToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (createManagerForm == null)
            {
                createManagerForm = new CreateManager();
                createManagerForm.MdiParent = this;
                createManagerForm.Show();
            }
            else
            {
                createManagerForm.Activate();
            }
        }

        private void agencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        ManagerForm managerForm;

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (managerForm == null)
            {
                managerForm = new ManagerForm();
                managerForm.MdiParent = this;
                managerForm.Show();
            }
            else
            {
                managerForm.Activate();
            }
        }
    }
}
