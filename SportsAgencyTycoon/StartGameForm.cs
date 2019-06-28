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
    public partial class StartGameForm : Form
    {
        private string _AgencyName;
        public string AgencyName
        {
            get { return _AgencyName; }
        }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
        }
        private int _TeamIndex;
        public int TeamIndex
        {
            get { return _TeamIndex; }
        }
        private int _IndividualIndex;
        public int IndividualIndex
        {
            get { return _IndividualIndex; }
        }

        public StartGameForm()
        {
            InitializeComponent();
        }

        private void btnCreateAgencyAndAgent_Click(object sender, EventArgs e)
        {
            //check if all text boxes are filled
            if (txtAgencyName.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "")
                MessageBox.Show("Make sure you've given an agency name and first and last names for your agent!");
            else
            {
                // set AgencyName, FirstName and LastName
                _AgencyName = txtAgencyName.Text.Trim();
                _FirstName = txtFirstName.Text.Trim();
                _LastName = txtLastName.Text.Trim();
                // check if any TeamSports radio buttons are selected
                if (!radioBaseball.Checked && !radioBasketball.Checked && !radioFootball.Checked && !radioHockey.Checked && !radioSoccer.Checked && !radioRandomTeam.Checked)
                    MessageBox.Show("Please select a team sport to be licensed in!");
                else
                {
                    // assign TeamIndex
                    if (radioBaseball.Checked) _TeamIndex = 0;
                    else if (radioBasketball.Checked) _TeamIndex = 1;
                    else if (radioFootball.Checked) _TeamIndex = 2;
                    else if (radioHockey.Checked) _TeamIndex = 3;
                    else if (radioSoccer.Checked) _TeamIndex = 4;
                    else if (radioRandomTeam.Checked) _TeamIndex = 5;

                    // check if any IndividualSports radio buttons are selected
                    if (!radioBoxing.Checked && !radioGolf.Checked && !radioTennis.Checked && !radioUFC.Checked && !radioRandomIndividual.Checked)
                        MessageBox.Show("Please select an individual sport to be licensed in!");
                    else
                    {
                        if (radioBoxing.Checked) _IndividualIndex = 0;
                        else if (radioGolf.Checked) _IndividualIndex = 1;
                        else if (radioTennis.Checked) _IndividualIndex = 2;
                        else if (radioUFC.Checked) _IndividualIndex = 3;
                        else if (radioRandomIndividual.Checked) _IndividualIndex = 4;

                        this.Close();
                    }
                }
            }
        }
    }
}
