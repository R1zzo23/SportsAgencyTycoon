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
            FillTeamInfo();
            FindAgentTeamRelationship();
            InitialGMTalk();
        }
        private void FillTeamInfo()
        {
            if (_Client.Sport == Sports.Soccer)
                lblRecord.Text = "Record: " + _Team.Wins + "-" + _Team.Losses + "-" + _Team.Ties;
            else if (_Client.Sport == Sports.Hockey)
                lblRecord.Text = "Record: " + _Team.Wins + "-" + _Team.Losses + "-" + _Team.OTLosses;
            else
                lblRecord.Text = "Record: " + _Team.Wins + "-" + _Team.Losses;
            lblTitleContender.Text = "Title Contender: " + _Team.TitleConteder;
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

        private void BtnPlayingTime_Click(object sender, EventArgs e)
        {
            string message = "";
            if (_Client.DepthChart == 1)
            {
                message = _Client.FirstName + ", you're already the starter. What more do you want?";
                _Client.TeamHappiness -= rnd.Next(1, 4);
                _Relationship.Relationship -= rnd.Next(1, 6);
            }
            else if (_Client.DepthChart == 2)
            {
                Player starter = _Team.Roster.Find(o => (o.Position == _Client.Position) && (o.DepthChart == 1));
                if (starter.CurrentSkill < _Client.CurrentSkill)
                {
                    if (starter.PotentialSkill > _Client.PotentialSkill)
                        message = starter.FullName + " is someone we value very much and need to see his game blossom.";
                    else if (_Team.TitleConteder < 40)
                        message = "It's a rebuilding year and " + starter.FullName + " is a young player we want to see play.";
                    else if (_Team.TitleConteder < 60)
                        message = "I completely hear your argument and think we can have Coach find more minutes for you.";
                    else
                        message = "We need to push for the playoffs. Let's have Coach elevate you on the depth chart.";
                }
                else
                {
                    if (starter.PotentialSkill > _Client.PotentialSkill)
                        message = starter.FullName + " is just a better player. Period.";
                    else
                        message = "We do see some promise in your client.";
                }
            }
            else
            {
                if (_Team.TitleConteder < 50)
                {
                    if (_Team.Wins < _Team.Losses)
                        message = "Maybe we need to shake some things up to keep some hope this season.";
                    else
                        message = "We've got a good thing going right now, let's not do anything drastic!";
                }
                else
                {
                    if (_Team.Wins < _Team.Losses)
                        message = "We are a better team than our record shows so we will stick with the current depth chart.";
                    else
                        message = "Why would we change the rotation? Because your guy isn't playing enough?";
                }
            }
            lblGMTalk.Text = message;
        }
    }
}
