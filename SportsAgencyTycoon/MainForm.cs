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
    public partial class MainForm : Form
    {
        public Agency agency;
        public Agent myManager;
        public MainForm()
        {
            InitializeComponent();
            CreateManager createManagerForm;
            createManagerForm = new CreateManager();
            createManagerForm.MdiParent = this;
            createManagerForm.Show();
        }

        public void HideAllOpenForms()
        {
            //FormCollection fc = Application.OpenForms;
            //foreach (Form frm in fc)
            //{
            //    frm.Hide();
            //}
        }

        AgencyForm agencyForm;

        private void agencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HideAllOpenForms();
            if (agencyForm == null)
            {
                agencyForm = new AgencyForm(agency);
                agencyForm.MdiParent = this;
                agencyForm.Show();
            }
            else
            {
                agencyForm.Activate();
            }
        }

        ManagerForm managerForm;

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HideAllOpenForms();
            if (managerForm == null)
            {
                managerForm = new ManagerForm(myManager, agency);
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
