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
    public partial class CreateManager : Form
    {
        public CreateManager()
        {
            InitializeComponent();
        }

        private void btnCreateManagerAndAgency_Click(object sender, EventArgs e)
        {
            MainForm form1 = (MainForm)this.MdiParent;
            form1.agency = new Agency(agencyNameTextBox.Text, 1000000, 1);
            form1.myManager = new Agent(managerFirstNameTextBox.Text,
                                      managerLastNameTextBox.Text,
                                      10, 10, 10, 1, Roles.Manager);
            form1.agency.AddAgent(form1.myManager);
            infoLabel.Text = "Information Label" + Environment.NewLine +
                form1.myManager.First + " " + form1.myManager.Last + Environment.NewLine + "Role: " +
                form1.myManager.Role.ToString() + Environment.NewLine +
                "Negotiating: " + form1.myManager.Negotiating + Environment.NewLine +
                "Greed: " + form1.myManager.Greed + Environment.NewLine +
                "Industry Power: " + form1.myManager.IndustryPower + Environment.NewLine +
                "Level: " + form1.myManager.Level + Environment.NewLine +
                "Agency Name: " + form1.agency.Name + Environment.NewLine +
                "Agent Count: " + form1.agency.AgentCount + Environment.NewLine +
                "Client Count: " + form1.agency.ClientCount + Environment.NewLine +
                "Cash: " + form1.agency.Money.ToString("C");
            this.Close();
        }
    }
}
