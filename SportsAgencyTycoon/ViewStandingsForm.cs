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
    public partial class ViewStandingsForm : Form
    {
        public World World;
        List<Team> EasternConference = new List<Team>();
        List<Team> WesternConference = new List<Team>();
        List<Team> AtlanticDivision = new List<Team>();
        List<Team> CentralDivision = new List<Team>();
        List<Team> SoutheastDivision = new List<Team>();
        List<Team> NorthwestDivision = new List<Team>();
        List<Team> SouthwestDivision = new List<Team>();
        List<Team> PacificDivision = new List<Team>();
        public ViewStandingsForm(World world)
        {
            InitializeComponent();
            World = world;
            FillLeagueComboBox();
        }

        private void FillLeagueComboBox()
        {
            cbLeagues.Items.Clear();

            string leagueName;

            foreach (League league in World.Leagues)
            {
                leagueName = league.Name;
                cbLeagues.Items.Add(leagueName);
            }
        }

        private void CbLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            League selectedLeague = World.Leagues[cbLeagues.SelectedIndex];
            if (selectedLeague.Sport == Sports.Basketball)
            {
                DisplayBasketballStandings(selectedLeague);
            }
        }

        private void DisplayBasketballStandings(League l)
        {
            EasternConference.Clear();
            WesternConference.Clear();
            AtlanticDivision.Clear();
            CentralDivision.Clear();
            SoutheastDivision.Clear();
            NorthwestDivision.Clear();
            SouthwestDivision.Clear();
            PacificDivision.Clear();

            foreach (Team t in l.TeamList)
            {
                if (t.Conference == "Eastern") EasternConference.Add(t);
                else WesternConference.Add(t);
                if (t.Division == "Atlantic") AtlanticDivision.Add(t);
                else if (t.Division == "Central") CentralDivision.Add(t);
                else if (t.Division == "Southeast") SoutheastDivision.Add(t);
                else if (t.Division == "Northwest") NorthwestDivision.Add(t);
                else if (t.Division == "Southwest") SouthwestDivision.Add(t);
                else if (t.Division == "Pacific") PacificDivision.Add(t);
            }

            EasternConference = EasternConference.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            WesternConference = WesternConference.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            AtlanticDivision = AtlanticDivision.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            CentralDivision = CentralDivision.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            SoutheastDivision = SoutheastDivision.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NorthwestDivision = NorthwestDivision.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            SouthwestDivision = SouthwestDivision.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            PacificDivision = PacificDivision.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();

            lblEastConference.Text = "Eastern Conference Standings:";
            lblWestConference.Text = "Western Conference Standings:";
            lblEastDivision1.Text = "Atlantic Divisions Standings:";
            lblEastDivision2.Text = "Central Division Standings:";
            lblEastDivision3.Text = "Southeast Division Standings:";
            lblWestDivision1.Text = "Northwest Division Standings:";
            lblWestDivision2.Text = "Southwest Division Standings:";
            lblWestDivision3.Text = "Pacific Division Standings:";

            for (int i = 0; i < EasternConference.Count; i++)
            {
                lblEastConference.Text += Environment.NewLine + EasternConference[i].Abbreviation + " " + EasternConference[i].Wins + "W - " + EasternConference[i].Losses + "L";
                lblWestConference.Text += Environment.NewLine + WesternConference[i].Abbreviation + " " + WesternConference[i].Wins + "W - " + WesternConference[i].Losses + "L";
            }
            for (int j = 0; j < AtlanticDivision.Count; j++)
            {
                lblEastDivision1.Text += Environment.NewLine + AtlanticDivision[j].Abbreviation + " " + AtlanticDivision[j].Wins + "W - " + AtlanticDivision[j].Losses + "L";
                lblEastDivision2.Text += Environment.NewLine + CentralDivision[j].Abbreviation + " " + CentralDivision[j].Wins + "W - " + CentralDivision[j].Losses + "L";
                lblEastDivision3.Text += Environment.NewLine + SoutheastDivision[j].Abbreviation + " " + SoutheastDivision[j].Wins + "W - " + SoutheastDivision[j].Losses + "L";
                lblWestDivision1.Text += Environment.NewLine + NorthwestDivision[j].Abbreviation + " " + NorthwestDivision[j].Wins + "W - " + NorthwestDivision[j].Losses + "L";
                lblWestDivision2.Text += Environment.NewLine + SouthwestDivision[j].Abbreviation + " " + SouthwestDivision[j].Wins + "W - " + SouthwestDivision[j].Losses + "L";
                lblWestDivision3.Text += Environment.NewLine + PacificDivision[j].Abbreviation + " " + PacificDivision[j].Wins + "W - " + PacificDivision[j].Losses + "L";
            }
        }
    }
}
