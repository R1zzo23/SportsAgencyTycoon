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
        Random rnd;
        private Agent _Agent;
        private Player _Client;
        private int _ClientMood;
        private double _ClientsCurrentAgentPercent;
        private double _ClientsAcceptancePercent;
        private int MaxPercent;
        private int MinPercent = 2;
        private double ClientAsk;
        private int NumberOfCounterOffers = 0;
        private double _Percentage;
        public Double Percentage
        {
            get { return _Percentage; }
        }

        public NegotiatePercentageForm(Random r, Agent agent, Player client)
        {
            InitializeComponent();
            rnd = r;
            _Agent = agent;
            _Client = client;
            _ClientMood = 100 - _Client.AgencyHappiness;
            if(_Client.Contract != null)
                _ClientsCurrentAgentPercent = _Client.Contract.AgentPercentage;
            DetermineMaxPercent();
            _ClientsAcceptancePercent = ClientsAcceptanceNumber();
            FillLabels();
        }

        private void DetermineMaxPercent()
        {
            if (_Client.Sport == Sports.Basketball || _Client.Sport == Sports.Football) MaxPercent = 3;
            else if (_Client.Sport == Sports.Baseball || _Client.Sport == Sports.Hockey || _Client.Sport == Sports.Soccer) MaxPercent = 5;
            else MaxPercent = 10;
        }

        private void FillLabels()
        {
            lblAgentGreed.Text = _Agent.Greed.ToString();
            lblAgentNegotiating.Text = _Agent.Negotiating.ToString();
            lblClientGreed.Text = _Client.Greed.ToString();
            lblClientMood.Text = _ClientMood.ToString();
            lblMinPercent.Text = ((double)MinPercent / 100).ToString("P");
            lblMaxPercent.Text = ((double)MaxPercent / 100).ToString("P");
            txtYourAskingPercent.Text = ClientAsksFor(rnd, MinPercent, _ClientsAcceptancePercent).ToString();
        }

        private double ClientsAcceptanceNumber()
        {
            int acceptAt;
            double acceptance;
            int minMaxDifference = MaxPercent - MinPercent;

            if (_Client.Greed >= 80) acceptAt = rnd.Next((MinPercent * 100), (MinPercent * (100 + (100 - _Client.Greed))));
            else if (_Client.Greed >= 60) acceptAt = rnd.Next((MinPercent * (100 + (100 - _Client.Greed))), (MinPercent * 100 + minMaxDifference * 100));
            else if (_Client.Greed >= 40) acceptAt = rnd.Next((MinPercent * (100 + (100 - _Client.Greed / 2))), (minMaxDifference * 100));
            else acceptAt = rnd.Next((minMaxDifference * 100), (MaxPercent * 100));

            acceptance = Convert.ToDouble((double)acceptAt / 100);

            Console.WriteLine("Client will accept at " + acceptance + "%.");

            return acceptance;
        }

        private double ClientAsksFor(Random rnd, double min, double accepted, int _ClientMood)
        {
            double askingPrice = min;

            askingPrice = (double)rnd.Next((int)(min * 100), (int)(accepted * 100)) / 100;

            return askingPrice;
        }

        private void ClientConsidersOffer(double d)
        {
            double agentDifference = d - ClientAsk;
            double clientDifference = _ClientsAcceptancePercent - ClientAsk;
            if (d <= _ClientsAcceptancePercent) MessageBox.Show("You have yourself a deal!");
            else
            {
                if (_ClientsAcceptancePercent > d * 2)
                {
                    MessageBox.Show("You've got to do MUCH better than that! Take a look at my counter offer.");
                    NumberOfCounterOffers += 2;
                }
                else
                {
                    MessageBox.Show("That's more value than I'm willing to part with. How about this?");
                    NumberOfCounterOffers++;
                }
                CalculateNewMood(agentDifference, clientDifference);
                
            }                
        }

        private int CalculateNewMood(double agentDifference, double clientDifference)
        {
            int newMood = 0;




            return newMood;
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

        private void btnMakeOffer_Click(object sender, EventArgs e)
        {
            ClientConsidersOffer(Convert.ToDouble(txtYourAskingPercent.Text));
        }
    }
}
