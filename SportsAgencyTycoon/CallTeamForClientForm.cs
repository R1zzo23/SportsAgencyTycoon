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
    public partial class CallTeamForClientForm : Form
    {
        private Agent _Agent;
        private Player _Client;
        private League _League;

        public CallTeamForClientForm(Agent agent, Player client, League league)
        {
            InitializeComponent();
            _Agent = agent;
            _Client = client;
            _League = league;
            lblLeagueName.Text = _League.Name;
            PopulateTeamList();
        }

        public void PopulateTeamList()
        {
            foreach (Team t in _League.TeamList)
            {
                string team = t.City + " " + t.Mascot;
                cbTeamList.Items.Add(team);
            }
        }
    }
}
