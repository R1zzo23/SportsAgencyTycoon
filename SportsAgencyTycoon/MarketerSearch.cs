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
    public partial class MarketerSearch : Form
    {
        private Agency _Agency;
        private string _MarketerType;
        public string MarketerType
        {
            get { return _MarketerType; }
        }
        private Random rnd;
        private int _FundsSpent = 0;
        public int FundsSpent
        {
            get { return _FundsSpent; }
        }
        public MarketerSearch(Random r, Agency agency)
        {
            InitializeComponent();
            _Agency = agency;
            rnd = r;

            CheckAgencyFunds();

            FillLabels();
        }
        private void FillLabels()
        {

        }
        private void CheckAgencyFunds()
        {
            if (_Agency.Money < 25000)
            {
                btn250kMarketerSearch.Enabled = false;
                btn100kMarketerSearch.Enabled = false;
                btn50kMarketerSearch.Enabled = false;
                btn25kMarketerSearch.Enabled = false;
            }
            else if (_Agency.Money < 50000)
            {
                btn250kMarketerSearch.Enabled = false;
                btn100kMarketerSearch.Enabled = false;
                btn50kMarketerSearch.Enabled = false;
            }
            else if (_Agency.Money < 100000)
            {
                btn250kMarketerSearch.Enabled = false;
                btn100kMarketerSearch.Enabled = false;
            }
            else if (_Agency.Money < 250000)
                btn250kMarketerSearch.Enabled = false;
        }

        private void Btn25kMarketerSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(25000);
        }

        private void Btn50kMarketerSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(50000);
        }

        private void Btn100kMarketerSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(100000);
        }

        private void Btn250kMarketerSearch_Click(object sender, EventArgs e)
        {
            UpdateAndClose(250000);
        }
        private void UpdateAndClose(int i)
        {
            if (radioOutreachPro.Checked) _MarketerType = "OutreachPro";
            else if (radioStrategicPlanner.Checked) _MarketerType = "StratigcPlanner";
            else if (radioCreativeVisionary.Checked) _MarketerType = "CreativeVisionary";
            else if (radioBalancedMarketer.Checked) _MarketerType = "BalancedMarketer";
            _FundsSpent = i;
            this.Close();
        }
    }
}
