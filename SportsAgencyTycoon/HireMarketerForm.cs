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
    public partial class HireMarketerForm : Form
    {
        private int _MarketerApplicants;
        private int _MarketerLevel;
        private List<Marketer> marketers = new List<Marketer>();
        private Marketer _HiredMarketer;
        public Marketer HiredMarketer
        {
            get { return _HiredMarketer; }
        }
        private int _FundsSpent;
        public int FundsSpent
        {
            get { return _FundsSpent; }
        }
        private string _MarketerType;
        public string MarketerType
        {
            get { return _MarketerType; }
        }
        public HireMarketerForm(int i, string s)
        {
            InitializeComponent();
            _FundsSpent = i;
            _MarketerType = s;

        }
        public void HowManyMarketers()
        {
            if (_FundsSpent == 25000)
            {
                _MarketerApplicants = 2;
                _MarketerLevel = 1;
            }
            else if (_FundsSpent == 50000 || _FundsSpent == 100000)
            { 
                _MarketerApplicants = 3;
                _MarketerLevel = 2;
            }
            else
            {
                _MarketerApplicants = 3;
                _MarketerLevel = 3;
            }
        }
        public void CreateApplicants(World world, Random rnd)
        {
            List<int> ratings = new List<int>();

            for (int i = 0; i < _MarketerApplicants; i++)
            {
                ratings.Clear();
                ratings.Add(DetermineCreativity(_MarketerType, rnd));
                ratings.Add(DetermineOutreach(_MarketerType, rnd));
                ratings.Add(DetermineStrategy(_MarketerType, rnd));
                ratings.Add(DetermineSalary(_MarketerLevel, _MarketerType, rnd, ratings));
                marketers.Add(new Marketer(
                    world.randomFirstName(rnd).ToString(),
                    world.randomLastName(rnd).ToString(),
                    ratings[3],
                    ratings[0],
                    ratings[1],
                    ratings[2],
                    _MarketerLevel,
                    Roles.Marketer));
            }
            DisplayApplicantInformation();
        }
        private void DisplayApplicantInformation()
        {
            Marketer m1 = marketers[0];
            Marketer m2 = marketers[1];
            radioApplicant1.Text = m1.First + " " + m1.Last + " (LVL " + _MarketerLevel + ")";
            radioApplicant2.Text = m2.First + " " + m2.Last + " (LVL " + _MarketerLevel + ")";
            lblAgent1.Text = m1.Salary.ToString("C0") + "/month | OUTRCH: " + m1.Outreach.ToString() + " | CREATVTY: " + m1.Creativity.ToString() + " | STRAT: " + m1.Strategy.ToString();
            lblAgent2.Text = m2.Salary.ToString("C0") + "/month | OUTRCH: " + m2.Outreach.ToString() + " | CREATVTY: " + m2.Creativity.ToString() + " | STRAT: " + m2.Strategy.ToString();
            if (marketers.Count < 3)
            {
                radioApplicant3.Enabled = false;
                radioApplicant3.Text = "";
                lblAgent3.Text = "";
            }
            else
            {
                Marketer m3 = marketers[2];
                radioApplicant3.Text = m3.First + " " + m3.Last + " (LVL " + _MarketerLevel + ")";
                lblAgent3.Text = m3.Salary.ToString("C0") + "/month | OUTRCH: " + m3.Outreach.ToString() + " | CREATVTY: " + m3.Creativity.ToString() + " | STRAT: " + m3.Strategy.ToString();
            }
        }
        public int DetermineSalary(int level, string type, Random rnd, List<int> ratings)
        {
            int salary = 8000;
            double multiplier = 0;

            int sum = 0;
            foreach (int i in ratings) sum += i;

            int ratingsDifference = sum - 60;
            salary += ratingsDifference * 500;

            if (_MarketerType != "BalancedMarketer") multiplier = 0.2;

            salary = (int)(salary * (1 + multiplier));

            return salary;
        }
        private int DetermineRating(string marketerType, bool marketerSpecialty, Random rnd)
        {
            int rating = 0;

            rating = rnd.Next(15, 22);

            // agentyType of PlayersAgent gets small boost in all categories
            if (marketerType == "BalancedMarketer")
            {
                rating += 2;
            }
            // marketerTypes other than PlayersAgent gets larger boost in their 
            // main category and small nerf in all other categories
            else
            {
                if (_MarketerLevel == 1)
                {
                    if (marketerSpecialty) rating += 5;
                    else rating -= 2;
                }
                else if (_MarketerLevel == 2)
                {
                    if (marketerSpecialty) rating += 7;
                    else rating += 1;
                }
                else if (_MarketerLevel == 3)
                {
                    if (marketerSpecialty) rating += 12;
                    else rating += 5;
                }
            }

            return rating;
        }
        private int DetermineCreativity(string marketerType, Random rnd)
        {
            int rating = 0;
            bool marketerSpecialty;

            if (marketerType == "CreativeVisionary") marketerSpecialty = true;
            else marketerSpecialty = false;

            rating = DetermineRating(marketerType, marketerSpecialty, rnd);

            return rating;
        }
        private int DetermineOutreach(string marketerType, Random rnd)
        {
            int rating = 0;
            bool marketerSpecialty;

            if (marketerType == "OutreachPro") marketerSpecialty = true;
            else marketerSpecialty = false;

            rating = DetermineRating(marketerType, marketerSpecialty, rnd);

            return rating;
        }
        private int DetermineStrategy(string marketerType, Random rnd)
        {
            int rating = 0;
            bool marketerSpecialty;

            if (marketerType == "StrategicPlanner") marketerSpecialty = true;
            else marketerSpecialty = false;

            rating = DetermineRating(marketerType, marketerSpecialty, rnd);

            return rating;
        }

        private void BtnHireSelected_Click(object sender, EventArgs e)
        {
            if (radioApplicant1.Checked)
            {
                _HiredMarketer = marketers[0];
                this.Close();
            }
            else if (radioApplicant2.Checked)
            {
                _HiredMarketer = marketers[1];
                this.Close();
            }
            else if (radioApplicant3.Checked)
            {
                _HiredMarketer = marketers[2];
                this.Close();
            }
            else MessageBox.Show("You must select one of the applicants before hiring.");
        }

        private void BtnPassOnHiring_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
