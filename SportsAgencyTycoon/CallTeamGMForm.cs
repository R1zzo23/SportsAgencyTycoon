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
    public partial class CallTeamGMForm : Form
    {
        private Random rnd;
        private Agent _Agent;
        private Player _Client;
        private Team _Team;
        private World world;
        private RelationshipWithTeam _Relationship;
        public CallTeamGMForm(Random r, Agent a, Player p, Team t, World w)
        {
            InitializeComponent();
            rnd = r;
            _Agent = a;
            _Client = p;
            _Team = t;
            world = w;
            FindAgentTeamRelationship();
            InitialGMTalk();
        }
        private void FindAgentTeamRelationship()
        {
            _Relationship = _Agent.RelationshipsWithTeams[_Agent.RelationshipsWithTeams.FindIndex(o => o.Team == _Team)];
            Console.WriteLine("Relationship #: " + _Relationship.Relationship);
        }
        private void InitialGMTalk()
        {
            Console.WriteLine("First: " + _Agent.First + ", Last: " + _Agent.Last + ", Full: " + _Agent.FullName);
            if (_Relationship.Relationship >= 75)
                lblGMTalk.Text = "Hey " + _Agent.FullName + "! Great to see you again!" + Environment.NewLine + "You know I'm always willing to help so what can I do for you today?";
            else if (_Relationship.Relationship >= 35)
                lblGMTalk.Text = _Agent.FullName + ", what do I owe this phone call to?";
            else lblGMTalk.Text = "You again? Better make this quick and don't waste my time!";
        }
    }
}
