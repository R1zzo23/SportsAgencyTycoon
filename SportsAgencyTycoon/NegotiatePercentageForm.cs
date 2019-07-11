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
    public partial class NegotiatePercentageForm : Form
    {
        private Agent _Agent;
        private Player _Client;
        private double _Percentage;
        public Double Percentage
        {
            get { return _Percentage; }
        }

        public NegotiatePercentageForm(Agent agent, Player client)
        {
            InitializeComponent();
            _Agent = agent;
            _Client = client;
        }

        //only accept decimals in the text box
        private void txtYourAskingPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtYourAskingPercent.Text + ch, out x))
            {
                e.Handled = true;
            }
        }
    }
}
