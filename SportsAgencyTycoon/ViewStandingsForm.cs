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
        List<List<Team>> FootballConferencesAndDivisions = new List<List<Team>>();
        List<Team> AFC = new List<Team>();
        List<Team> NFC = new List<Team>();
        List<Team> AFCEast = new List<Team>();
        List<Team> AFCNorth = new List<Team>();
        List<Team> AFCSouth = new List<Team>();
        List<Team> AFCWest = new List<Team>();
        List<Team> NFCEast = new List<Team>();
        List<Team> NFCNorth = new List<Team>();
        List<Team> NFCSouth = new List<Team>();
        List<Team> NFCWest = new List<Team>();
        List<Team> AmericanLeague = new List<Team>();
        List<Team> NationalLeague = new List<Team>();
        List<Team> ALEast = new List<Team>();
        List<Team> ALCentral = new List<Team>();
        List<Team> ALWest = new List<Team>();
        List<Team> NLEast = new List<Team>();
        List<Team> NLCentral = new List<Team>();
        List<Team> NLWest = new List<Team>();
        List<Team> MetropolitanDivision = new List<Team>();
        
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
            else if (selectedLeague.Sport == Sports.Football)
            {
                DisplayFootballStandings(selectedLeague);
            }
            else if (selectedLeague.Sport == Sports.Baseball)
            {
                DisplayBaseballStandings(selectedLeague);
            }
            else if (selectedLeague.Sport == Sports.Hockey)
            {
                DisplayHockeyStandings(selectedLeague);
            }
        }
        private void FillFootballLists(League l)
        {
            AFC.Clear();
            NFC.Clear();
            AFCEast.Clear();
            NFCEast.Clear();
            AFCNorth.Clear();
            NFCNorth.Clear();
            AFCSouth.Clear();
            NFCSouth.Clear();
            AFCWest.Clear();
            NFCWest.Clear();

            foreach (Team t in l.TeamList)
            {
                if (t.Conference == "AFC")
                {
                    AFC.Add(t);
                    if (t.Division == "East") AFCEast.Add(t);
                    else if (t.Division == "North") AFCNorth.Add(t);
                    else if (t.Division == "South") AFCSouth.Add(t);
                    else if (t.Division == "West") AFCWest.Add(t);
                }
                else if (t.Conference == "NFC")
                {
                    NFC.Add(t);
                    if (t.Division == "East") NFCEast.Add(t);
                    else if (t.Division == "North") NFCNorth.Add(t);
                    else if (t.Division == "South") NFCSouth.Add(t);
                    else if (t.Division == "West") NFCWest.Add(t);
                }
            }

            FootballConferencesAndDivisions.Add(AFC);
            FootballConferencesAndDivisions.Add(NFC);
            FootballConferencesAndDivisions.Add(AFCEast);
            FootballConferencesAndDivisions.Add(AFCNorth);
            FootballConferencesAndDivisions.Add(AFCSouth);
            FootballConferencesAndDivisions.Add(AFCWest);
            FootballConferencesAndDivisions.Add(NFCEast);
            FootballConferencesAndDivisions.Add(NFCNorth);
            FootballConferencesAndDivisions.Add(NFCSouth);
            FootballConferencesAndDivisions.Add(NFCWest);
        }

        private void DisplayFootballStandings(League l)
        {
            FillFootballLists(l);
            OrderFootballListsByWinsAndTitleContender();
            lblEastConference.Text = "AFC Standings:";
            lblWestConference.Text = "NFC Standings:";
            lblEastDivision1.Text = "AFC East Standings:";
            lblEastDivision2.Text = "AFC North Standings:";
            lblEastDivision3.Text = "AFC South Standings:";
            lblEastDivision4.Text = "AFC West Standings";
            lblWestDivision1.Text = "NFC East Standings:";
            lblWestDivision2.Text = "NFC North Standings:";
            lblWestDivision3.Text = "NFC South Standings:";
            lblWestDivision4.Text = "NFC West Standings:";

            for (int i = 0; i < AFC.Count; i++)
            {
                lblEastConference.Text += Environment.NewLine + AFC[i].Abbreviation + " " + AFC[i].Wins + "W - " + AFC[i].Losses + "L";
                lblWestConference.Text += Environment.NewLine + NFC[i].Abbreviation + " " + NFC[i].Wins + "W - " + NFC[i].Losses + "L";
            }
            for (int j = 0; j < AFCEast.Count; j++)
            {
                lblEastDivision1.Text += Environment.NewLine + AFCEast[j].Abbreviation + " " + AFCEast[j].Wins + "W - " + AFCEast[j].Losses + "L";
                lblEastDivision2.Text += Environment.NewLine + AFCNorth[j].Abbreviation + " " + AFCNorth[j].Wins + "W - " + AFCNorth[j].Losses + "L";
                lblEastDivision3.Text += Environment.NewLine + AFCSouth[j].Abbreviation + " " + AFCSouth[j].Wins + "W - " + AFCSouth[j].Losses + "L";
                lblEastDivision4.Text += Environment.NewLine + AFCWest[j].Abbreviation + " " + AFCWest[j].Wins + "W - " + AFCWest[j].Losses + "L";
                lblWestDivision1.Text += Environment.NewLine + NFCEast[j].Abbreviation + " " + NFCEast[j].Wins + "W - " + NFCEast[j].Losses + "L";
                lblWestDivision2.Text += Environment.NewLine + NFCNorth[j].Abbreviation + " " + NFCNorth[j].Wins + "W - " + NFCNorth[j].Losses + "L";
                lblWestDivision3.Text += Environment.NewLine + NFCSouth[j].Abbreviation + " " + NFCSouth[j].Wins + "W - " + NFCSouth[j].Losses + "L";
                lblWestDivision4.Text += Environment.NewLine + NFCWest[j].Abbreviation + " " + NFCWest[j].Wins + "W - " + NFCWest[j].Losses + "L";
            }
        }

        private void OrderFootballListsByWinsAndTitleContender()
        {
            AFC = AFC.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NFC = NFC.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            AFCEast = AFCEast.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            AFCNorth = AFCNorth.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            AFCSouth = AFCSouth.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            AFCWest = AFCWest.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NFCEast = NFCEast.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NFCNorth = NFCNorth.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NFCSouth = NFCSouth.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NFCWest = NFCWest.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
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
        private void DisplayHockeyStandings(League l)
        {
            EasternConference.Clear();
            WesternConference.Clear();
            AtlanticDivision.Clear();
            CentralDivision.Clear();
            MetropolitanDivision.Clear();
            PacificDivision.Clear();

            foreach (Team t in l.TeamList)
            {
                if (t.Conference == "Eastern")
                {
                    EasternConference.Add(t);
                    if (t.Division == "Atlantic") AtlanticDivision.Add(t);
                    else MetropolitanDivision.Add(t);
                }
                else
                {
                    WesternConference.Add(t);
                    if (t.Division == "Pacific") PacificDivision.Add(t);
                    else CentralDivision.Add(t);
                }
            }

            EasternConference = EasternConference.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
            WesternConference = WesternConference.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
            AtlanticDivision = AtlanticDivision.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
            MetropolitanDivision = MetropolitanDivision.OrderByDescending(o => o.Points).ThenBy(o => o.Wins).ToList();
            CentralDivision = CentralDivision.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();
            PacificDivision = PacificDivision.OrderByDescending(o => o.Points).ThenByDescending(o => o.Wins).ToList();

            lblEastConference.Text = "Eastern Conference Standings:";
            lblWestConference.Text = "Western Conference Standings:";
            lblEastDivision1.Text = "Atlantic Divisions Standings:";
            lblEastDivision2.Text = "";
            lblEastDivision3.Text = "Metropolitan Division Standings:";
            lblEastDivision4.Text = "";
            lblWestDivision1.Text = "Central Division Standings:";
            lblWestDivision2.Text = "";
            lblWestDivision3.Text = "Pacific Division Standings:";
            lblWestDivision4.Text = "";

            for (int i = 0; i < WesternConference.Count; i++)
            {
                lblEastConference.Text += Environment.NewLine + EasternConference[i].Abbreviation + ": " + EasternConference[i].Points + " points (" + EasternConference[i].Wins + "-" + EasternConference[i].Losses + "-" + EasternConference[i].OTLosses + ")";
                lblWestConference.Text += Environment.NewLine + WesternConference[i].Abbreviation + ": " + WesternConference[i].Points + " points (" + WesternConference[i].Wins + "-" + WesternConference[i].Losses + "-" + WesternConference[i].OTLosses + ")";
            }
            lblEastConference.Text += Environment.NewLine + EasternConference[15].Abbreviation + ": " + EasternConference[15].Points + " point (" + EasternConference[15].Wins + "-" + EasternConference[15].Losses + "-" + EasternConference[15].OTLosses + ")";
            for (int j = 0; j < AtlanticDivision.Count; j++)
            {
                lblEastDivision1.Text += Environment.NewLine + AtlanticDivision[j].Abbreviation + ": " + AtlanticDivision[j].Points + " points (" + AtlanticDivision[j].Wins + "-" + AtlanticDivision[j].Losses + "-" + AtlanticDivision[j].OTLosses + ")";
                //lblEastDivision2.Text += Environment.NewLine + CentralDivision[j].Abbreviation + " " + CentralDivision[j].Wins + "W - " + CentralDivision[j].Losses + "L";
                lblEastDivision3.Text += Environment.NewLine + MetropolitanDivision[j].Abbreviation + ": " + MetropolitanDivision[j].Points + " points (" + MetropolitanDivision[j].Wins + "-" + MetropolitanDivision[j].Losses + "-" + MetropolitanDivision[j].OTLosses + ")";
                //lblWestDivision2.Text += Environment.NewLine + SouthwestDivision[j].Abbreviation + " " + SouthwestDivision[j].Wins + "W - " + SouthwestDivision[j].Losses + "L";
                lblWestDivision3.Text += Environment.NewLine + PacificDivision[j].Abbreviation + ": " + PacificDivision[j].Points + " points (" + PacificDivision[j].Wins + "-" + PacificDivision[j].Losses + "-" + PacificDivision[j].OTLosses + ")";
            }
            for (int x = 0; x < CentralDivision.Count; x++)
                lblWestDivision1.Text += Environment.NewLine + CentralDivision[x].Abbreviation + ": " + CentralDivision[x].Points + " points (" + CentralDivision[x].Wins + "-" + CentralDivision[x].Losses + "-" + CentralDivision[x].OTLosses + ")";
        }
        private void DisplayBaseballStandings(League l)
        {
            AmericanLeague.Clear();
            NationalLeague.Clear();
            ALEast.Clear();
            ALCentral.Clear();
            ALWest.Clear();
            NLEast.Clear();
            NLCentral.Clear();
            NLWest.Clear();

            foreach (Team t in l.TeamList)
                if (t.Conference == "AL")
                {
                    AmericanLeague.Add(t);
                    if (t.Division == "East") ALEast.Add(t);
                    else if (t.Division == "Central") ALCentral.Add(t);
                    else ALWest.Add(t);
                }
                else
                {
                    NationalLeague.Add(t);
                    if (t.Division == "East") NLEast.Add(t);
                    else if (t.Division == "Central") NLCentral.Add(t);
                    else NLWest.Add(t);
                }

            AmericanLeague = AmericanLeague.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NationalLeague = NationalLeague.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            ALEast = ALEast.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            ALCentral = ALCentral.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            ALWest = ALWest.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NLEast = NLEast.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NLCentral = NLCentral.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();
            NLWest = NLWest.OrderByDescending(o => o.Wins).ThenByDescending(o => o.TitleConteder).ToList();

            lblEastConference.Text = "American League Standings:";
            lblWestConference.Text = "National League Standings:";
            lblEastDivision1.Text = "AL East Standings:";
            lblEastDivision2.Text = "AL Central Standings:";
            lblEastDivision3.Text = "AL West Standings:";
            lblWestDivision1.Text = "NL East Standings:";
            lblWestDivision2.Text = "NL Central Standings:";
            lblWestDivision3.Text = "NL West Standings:";

            for (int i = 0; i < AmericanLeague.Count; i++)
            {
                lblEastConference.Text += Environment.NewLine + AmericanLeague[i].Abbreviation + " " + AmericanLeague[i].Wins + "W - " + AmericanLeague[i].Losses + "L";
                lblWestConference.Text += Environment.NewLine + NationalLeague[i].Abbreviation + " " + NationalLeague[i].Wins + "W - " + NationalLeague[i].Losses + "L";
            }
            for (int j = 0; j < ALEast.Count; j++)
            {
                lblEastDivision1.Text += Environment.NewLine + ALEast[j].Abbreviation + " " + ALEast[j].Wins + "W - " + ALEast[j].Losses + "L";
                lblEastDivision2.Text += Environment.NewLine + ALCentral[j].Abbreviation + " " + ALCentral[j].Wins + "W - " + ALCentral[j].Losses + "L";
                lblEastDivision3.Text += Environment.NewLine + ALWest[j].Abbreviation + " " + ALWest[j].Wins + "W - " + ALWest[j].Losses + "L";
                lblWestDivision1.Text += Environment.NewLine + NLEast[j].Abbreviation + " " + NLEast[j].Wins + "W - " + NLEast[j].Losses + "L";
                lblWestDivision2.Text += Environment.NewLine + NLCentral[j].Abbreviation + " " + NLCentral[j].Wins + "W - " + NLCentral[j].Losses + "L";
                lblWestDivision3.Text += Environment.NewLine + NLWest[j].Abbreviation + " " + NLWest[j].Wins + "W - " + NLWest[j].Losses + "L";
            }
        }
    }
}
