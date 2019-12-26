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
        public double ClientsAcceptancePercent
        {
            get { return _ClientsAcceptancePercent; }
        }
        private int MaxPercent;
        private int MinPercent = 2;
        private double ClientAsk;
        private int _NumberOfAgentOffers = 0;
        public int NumberOfAgentOffers
        {
            get { return _NumberOfAgentOffers;  }
        }
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
            int oneQuarterRange = ((MaxPercent * 100 - MinPercent * 100) / 4);
            int firstQuarter = (MinPercent * 100) + (oneQuarterRange);
            int secondQuarter = (MinPercent * 100) + (oneQuarterRange * 2);
            int thirdQuarter = (MinPercent * 100) + (oneQuarterRange * 3);

            if (_Client.Greed >= 80) acceptAt = rnd.Next((MinPercent * 100), firstQuarter);
            else if (_Client.Greed >= 60) acceptAt = rnd.Next(firstQuarter, secondQuarter);
            else if (_Client.Greed >= 40) acceptAt = rnd.Next(secondQuarter, thirdQuarter);
            else acceptAt = rnd.Next(thirdQuarter, (MaxPercent * 100));

            acceptance = Convert.ToDouble((double)acceptAt / 100);

            Console.WriteLine("Client will accept at " + acceptance + "%.");

            return acceptance;
        }

        private double ClientAsksFor(Random rnd, double min, double accepted)
        {
            double askingPrice = min;
            if (accepted < min) accepted = min + 0.1;
            askingPrice = (double)rnd.Next((int)(min * 100), (int)(accepted * 100)) / 100;
            ClientAsk = askingPrice;

            return askingPrice;
        }

        private void ClientConsidersOffer(double d)
        {
            double agentDifference = d - _ClientsAcceptancePercent;
            double clientDifference = _ClientsAcceptancePercent - ClientAsk;
            Console.WriteLine("agentDifference: " + agentDifference);
            Console.WriteLine("clientDifference: " + clientDifference);
            Console.WriteLine("clientAsk: " + ClientAsk);
            Console.WriteLine("ClientAcceptancePercent: " + _ClientsAcceptancePercent);
            
            if (d <= _ClientsAcceptancePercent)
            {
                MessageBox.Show("You have yourself a deal!");
                
                _Percentage = d;
                this.Close();
            }
                
            else
            {
                if (_ClientMood <= 0)
                {
                    MessageBox.Show("These negotiations are done! I will not be signing with your agency!");
                    _Percentage = 0;
                    this.Close();
                }
                else if (_ClientMood < 20)
                {
                    MessageBox.Show("I'm not happy with how these negotiations are going. This is the percentage that will get the deal done.");
                    NumberOfCounterOffers += 3;
                    _ClientsAcceptancePercent = ClientAsk;
                    txtYourAskingPercent.Text = ClientAsk.ToString();
                    _ClientMood = 0;
                }
                else 
                {
                    if (clientDifference < agentDifference / 2)
                    {
                        MessageBox.Show("You've got to do MUCH better than that! Take a look at my counter offer.");
                        NumberOfCounterOffers += 2;
                    }
                    else
                    {
                        MessageBox.Show("That's more value than I'm willing to part with. How about this?");
                        NumberOfCounterOffers++;
                    }
                    CalculateNewMood(agentDifference, clientDifference, NumberOfCounterOffers);
                    txtYourAskingPercent.Text = ClientAsksFor(rnd, ClientAsk, _ClientsAcceptancePercent).ToString();
                    if (_ClientMood <= 0) _ClientMood = 1;
                }
                                
                lblClientMood.Text = _ClientMood.ToString();
                
            }        
        }

        private void CalculateNewMood(double agentDifference, double clientDifference, int counterOffers)
        {
            if (agentDifference < clientDifference) _ClientMood += rnd.Next(-7, 1) * counterOffers;
            else _ClientMood += rnd.Next(-20, 1) * counterOffers;
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
            _NumberOfAgentOffers++;
            ClientConsidersOffer(Convert.ToDouble(txtYourAskingPercent.Text));
        }

        private void btnEndNegotiations_Click(object sender, EventArgs e)
        {
            _Percentage = -1;
            this.Close();
        }
    }
}
