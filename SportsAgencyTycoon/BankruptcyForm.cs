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
    public partial class BankruptcyForm : Form
    {
        private Agency myAgency;
        private bool _AcceptedLoan;
        public bool AcceptedLoan
        {
            get { return _AcceptedLoan; }
        }
        public BankruptcyForm(Agency agency)
        {
            InitializeComponent();
            myAgency = agency;
        }
        public void DisplayInformation()
        {
            lblBankruptcy.Text = "Your agency - " + myAgency.Name + " - is in danger of going bankrupt. The bank is willing to loan you $200k but in a year's time you need to pay us back $350k. What do you think?";
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            _AcceptedLoan = true;
            this.Close();
        }

        private void btnBankrupt_Click(object sender, EventArgs e)
        {
            _AcceptedLoan = false;
            MessageBox.Show("Game Over!");
            this.Close();
        }
    }
}
