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
        private PlayingTimeDiscussion PlayingTimeDiscussion;
        private int GMCertainty;
        private bool GMAgreed;
        public CallTeamGMForm(Random r, Agent a, Player p, Team t, World w)
        {
            InitializeComponent();
            rnd = r;
            _Agent = a;
            _Client = p;
            _Team = t;
            world = w;
            PlayingTimeDiscussion = new PlayingTimeDiscussion(rnd, world, _Team, _Client.League, _Client);
            FillTeamInfo();
            FindAgentTeamRelationship();
            InitialGMTalk();
            if (_Client.DepthChart == 1) btnPlayingTime.Enabled = false;
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

            message = PlayingTimeDiscussion.GMResponse();
            GMCertainty = PlayingTimeDiscussion.OutputGMCertainty();
            GMAgreed = PlayingTimeDiscussion.OutputGMAgreed();

            lblGMTalk.Text = message;
            Console.WriteLine("GMCertainty = " + GMCertainty);
            if (!GMAgreed)
                gbRespondToPT.Visible = true;
            else
            {
                int playingTimePowerIndex = _Agent.Achievements.FindIndex(o => o.Name == "Playing Time Power");
                if (playingTimePowerIndex < 0)
                    _Agent.AddAchievementToAgent(world.GlobalAchievements[world.GlobalAchievements.FindIndex(o => o.Name == "Playing Time Power")]);
            }
        }

        private void BtnSmoothTalk_Click(object sender, EventArgs e)
        {
            int agentSmoothTalk = _Agent.Negotiating + _Agent.Intelligence;

        }

        private void BtnPowerPlay_Click(object sender, EventArgs e)
        {

        }

        private void BtnDemand_Click(object sender, EventArgs e)
        {

        }
        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
