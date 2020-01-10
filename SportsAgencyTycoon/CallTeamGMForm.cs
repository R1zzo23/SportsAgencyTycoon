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
            int agentSmoothTalk = _Agent.Negotiating + _Agent.Intelligence + _Relationship.Relationship / 2;
            GMLastResponse(agentSmoothTalk, "smooth");
        }

        private void BtnPowerPlay_Click(object sender, EventArgs e)
        {
            int agentPowerPlay = _Agent.IndustryPower + _Agent.Intelligence;
            int clientsInSport = 0;
            foreach (Player p in _Agent.ClientList)
                if (p.Sport == _Client.Sport)
                    clientsInSport++;
            for (int i = 0; i < clientsInSport; i++)
                agentPowerPlay += rnd.Next(1, 7);

            GMLastResponse(agentPowerPlay, "power");
        }

        private void BtnDemand_Click(object sender, EventArgs e)
        {
            int agentDemand = _Agent.Greed + _Agent.IndustryPower;
            foreach (Player p in _Agent.ClientList)
                if (p.Sport == _Client.Sport)
                    if (p.CurrentSkill > 60 || p.PotentialSkill > 70)
                        agentDemand += DiceRoll();

            GMLastResponse(agentDemand, "demand");
        }
        private void GMLastResponse(int agentResponse, string tone)
        {
            string gmLastResponse = "";

            if (GMCertainty > agentResponse)
            {
                if (tone == "smooth")
                {
                    gmLastResponse = "Listen, I know you're trying to do right by your client but it's just not going to happen right now.";
                    _Relationship.Relationship -= rnd.Next(1, 3);
                }
                else if (tone == "power")
                {
                    gmLastResponse = "Nice try but you don't have that type of pull around here.";
                    _Relationship.Relationship -= rnd.Next(2, 6);
                }
                else
                {
                    gmLastResponse = "You think you can come in here and make baseless demands like that?!? This won't work!";
                    _Relationship.Relationship -= rnd.Next(7, 16);
                }
            }
            else
            {
                int highRoll = 0;
                int numberOfRolls = (agentResponse - GMCertainty) / 10 + 1;
                for (int i = 0; i < numberOfRolls; i++)
                {
                    int roll = DiceRoll();
                    if (roll > highRoll) highRoll = roll;
                }
                if (highRoll >= 9)
                {
                    gmLastResponse = "Hmm, I think I can see what you're saying. I'll talk to Coach and have him make the switch.";
                    _Relationship.Relationship += rnd.Next(2, 6);
                }
                else
                {
                    if (tone == "smooth")
                    {
                        gmLastResponse = "I hear what you're saying, and appreciate your approach, but now is not the time.";
                        _Relationship.Relationship -= rnd.Next(0, 3);
                        gbRespondToPT.Visible = false;
                    }
                    else if (tone == "power")
                    {
                        gmLastResponse = "Well done on that power play, but it's not going to happen.";
                        _Relationship.Relationship -= rnd.Next(2, 6);
                    }
                    else
                    {
                        gmLastResponse = "I see someone's found their voice, huh? How cute. This meeting is over.";
                        _Relationship.Relationship -= rnd.Next(4, 11);
                    }
                }
            }
            lblGMTalk.Text = gmLastResponse;
            gbRespondToPT.Visible = false;
        }
        public int DiceRoll()
        {
            int firstDie = rnd.Next(1, 7);
            int secondDie = rnd.Next(1, 7);
            return firstDie + secondDie;
        }
    }
}
