﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Player
    {
        public int Id;
        public Sports Sport;
        public PlayerType PlayerType;
        public Team Team;
        public League League;
        public Position Position;

        //name variables
        public FirstName FirstName;
        public LastName LastName;
        public string FullName;

        //player skills
        public int CurrentSkill;
        public int PotentialSkill;

        //player attributes to determine which teams or contracts a player prefers
        public int Greed;
        public int Lifestyle;
        public int Loyalty;
        public int PlayForTitleContender;

        //contract && endorsements
        public Contract Contract;
        public Endorsement Endorsement;
        public bool WillingToNegotiate;
        public bool MemberOfAgency;

        //age and birthday
        public int Age;
        public Months BirthMonth;
        public int BirthWeek;
        public Date Birthday;

        //popularity by numbers and descriptions
        public int Popularity;
        public PopularityDescription PopularityDescription;
        public string PopularityString;

        //agencyHappiness by numbers and descriptions
        public int AgencyHappiness;
        public HappinessDescription AgencyHappinessDescription;
        public string AgencyHappinessString;

        //teamHappines by numbers and description
        public int TeamHappiness;
        public HappinessDescription TeamHappinessDescription;
        public string TeamHappinessString;

        public int CareerEarnings;
        public int WorldRanking;
        

        public Player(Random rnd, int id, Sports sport, FirstName firstName, LastName lastName, int currentSkill, int age, Months birthMonth, int birthWeek)
        {
            Id = id;
            Sport = sport;
            PlayerType = DeterminePlayerType(Sport);

            FirstName = firstName;
            LastName = lastName;
            FullName = firstName.ToString() + " " + lastName.ToString();

            CurrentSkill = currentSkill;
            PotentialSkill = AssignPotential(rnd, age, currentSkill);

            Greed = rnd.Next(0, 101);
            Lifestyle = rnd.Next(0, 101);
            Loyalty = rnd.Next(0, 101);
            PlayForTitleContender = rnd.Next(0, 101);

            WillingToNegotiate = true;
            MemberOfAgency = false;

            Age = age;
            BirthMonth = birthMonth;
            if (birthWeek == 5)
            {
                if (((int)BirthMonth + 1) % 3 == 0) BirthWeek = 5;
                else BirthWeek = 4;
            }
            else BirthWeek = birthWeek;
            Birthday = CreateBirthday(birthMonth, birthWeek);

            //CreatePlayerContract(rnd);

            Popularity = DeterminePopularity(CurrentSkill, PotentialSkill, Age);
            PopularityDescription = DescribePopularity(Popularity);
            PopularityString = EnumToString(PopularityDescription.ToString());

            //TeamHappiness = DetermineTeamHappiness(rnd);
            //TeamHappinessDescription = DescribeHappiness(TeamHappiness);
            //TeamHappinessString = EnumToString(TeamHappinessDescription.ToString());

            //AgencyHappiness = DetermineAgencyHappiness(rnd);
            //AgencyHappinessDescription = DescribeHappiness(AgencyHappiness);
            //AgencyHappinessString = EnumToString(AgencyHappinessDescription.ToString());

            CareerEarnings = 0;
        }

        public PlayerType DeterminePlayerType(Sports s)
        {
            if (s == Sports.Boxing || s == Sports.Golf || s == Sports.MMA || s == Sports.Tennis) return PlayerType.Individual;
            else return PlayerType.Team;
        }

        public int AssignPotential(Random rnd, int age, int currentSkill)
        {
            int potentialSkill = 0;
            int potential = 0;

            if (age <= 20) potential = rnd.Next(15, 41);
            else if (age <= 25) potential = rnd.Next(10, 36);
            else if (age <= 30) potential = rnd.Next(5, 26);
            else if (age <= 35) potential = rnd.Next(0, 7);

            potentialSkill = currentSkill + potential;
            if (potentialSkill > 100) potentialSkill = 100;

            return potentialSkill;
        }

        public Contract CreatePlayerContract(Random rnd)
        {
            Contract contract;
            int years = rnd.Next(1, 6);
            int signingBonus;
            int yearlySalary = DetermineYearlySalary(rnd);
            if (PlayerType == PlayerType.Team)
            {
                if (Sport == Sports.Football)
                {
                    // determine signing bonus of contract
                    signingBonus = DetermineSigningBonus(rnd, years, yearlySalary);
                    // calculate total value of contract
                    int totalValueOfContract = years * yearlySalary;
                    // remove signing bonus from total value of contract
                    totalValueOfContract -= signingBonus;
                    // recalculate yearlySalary
                    yearlySalary = totalValueOfContract / years;
                }

                else signingBonus = 0;

                int monthlySalary = Convert.ToInt32((double)yearlySalary / (double)League.MonthsInSeason);

                contract = new Contract(years, yearlySalary, monthlySalary, League.SeasonStart, League.SeasonEnd, signingBonus, DetermineAgentPercentage(rnd), DeterminePaySchedule(years));
                return contract;
            }
            else 
            {
                contract = null;
                return contract;
            }

            //return contract;
        }

        public double DetermineAgentPercentage(Random rnd)
        {
            double percentage = 0.00;
            int max = 1000;
            int min = 250;

            if (Sport == Sports.Basketball || Sport == Sports.Football) max = 300;
            else if (Sport == Sports.Baseball || Sport == Sports.Hockey || Sport == Sports.Soccer) max = 500;

            percentage = (double)rnd.Next(min, max + 1) / 100;

            return percentage;
        }

        public PaySchedule DeterminePaySchedule(int years)
        {
            PaySchedule paySchedule;

            if (years == 1) paySchedule = PaySchedule.Annually;
            else paySchedule = PaySchedule.Monthly;

            return paySchedule;
        }

        public int DetermineYearlySalary(Random rnd)
        {
            int YearlySalary = 0;
            int maxSalary = League.MaxSalary;
            int minSalary = League.MinSalary;
            int skillLevel = CurrentSkill;
            int maxMinDifference;

            if (Age <= 21)
            {
                skillLevel = PotentialSkill;
                maxSalary = maxSalary / 4;
            }
            else if (Age <= 25)
            {
                skillLevel = (CurrentSkill + PotentialSkill) / 2;
                maxSalary = maxSalary / 2;
                minSalary = minSalary * 2;
            }
            else if (Age <= 30) minSalary = minSalary * 3;
            else if (Age > 30) 
            {
                CurrentSkill = (int)Math.Round(CurrentSkill * 0.75);
                minSalary = minSalary * 5;
            }
            else
            {
                CurrentSkill = (int)Math.Round(CurrentSkill * 0.6);
                minSalary = minSalary * 5;
            }

            maxMinDifference = maxSalary - minSalary;

            if (CurrentSkill < 40) YearlySalary = minSalary;
            else if (CurrentSkill < 50) YearlySalary = rnd.Next(minSalary, (int)(minSalary * 5));
            else if (CurrentSkill < 60) YearlySalary = rnd.Next((int)(maxSalary * 0.5), maxSalary);
            else if (CurrentSkill < 70) YearlySalary = rnd.Next((int)(maxSalary * 0.6), maxSalary);
            else if (CurrentSkill < 80) YearlySalary = rnd.Next((int)(maxSalary * 0.75), maxSalary);
            else YearlySalary = maxSalary;

            return YearlySalary;
        }

        public int DetermineSigningBonus(Random rnd, int years, int YearlySalary)
        {
            int signingBonus = 0;
            double percentage = 0;

            if (years == 5) percentage = rnd.Next(10, 21) / 100;
            if (years == 4) percentage = rnd.Next(10, 16) / 100;
            if (years == 3) percentage = rnd.Next(7, 13) / 100;
            if (years == 2) percentage = rnd.Next(5, 11) / 100;
            if (years == 1) percentage = rnd.Next(5, 8) / 100;

            signingBonus = (int)((YearlySalary * years) * (percentage / 100));

            return signingBonus;
        }

        public Date CreateBirthday(Months month, int week)
        {
            Date birthday = new Date((int)month, month, week);
            return birthday;
        }

        public int DeterminePopularity(int currentSkill, int potentialSkill, int age)
        {
            int popularity = 0;

            popularity = currentSkill + (potentialSkill - currentSkill);
            if (age <= 21) popularity += 20;
            else if (age <= 24) popularity += 15;
            else if (age <= 27) popularity += 10;
            else if (age <= 29) popularity += 5;

            return popularity;
        }

        public PopularityDescription DescribePopularity(int pop)
        {
            PopularityDescription description;

            if (pop <= 25) description = PopularityDescription.Unknown;
            else if (pop <= 35) description = PopularityDescription.RelativeUnknown;
            else if (pop <= 45) description = PopularityDescription.Neutral;
            else if (pop <= 55) description = PopularityDescription.LocalFavorite;
            else if (pop <= 70) description = PopularityDescription.VeryPopular;
            else if (pop <= 85) description = PopularityDescription.ExtremelyPopular;
            else description = PopularityDescription.Superstar;

            return description;
        }

        //want to rewrite this to use players position on depth chart
        //and team's titleContender and marketValue variables
        public void DetermineTeamHappiness(Random rnd, bool isStarter)
        {
            int happiness = 0;
            int happinessChecks = 0;
            //bool marketLifestyleMatch = false;
            //bool starterCurrentSkillMatch = false;
            //bool teamPlayerTitleContenderMatch = false;

            //marketValue and lifestyle match?
            if (Lifestyle >= 60 && Team.MarketValue >= 50) happinessChecks++;

            //starter and currentSkill match?
            if (CurrentSkill >= 50 && isStarter) happinessChecks += 2;

            //team titleContender and PlayForTitleContender match?
            if (Team.TitleConteder >= 50 && PlayForTitleContender >= 65) happinessChecks++;

            if (happinessChecks == 0)
                happiness = rnd.Next(0, 50);
            else if (happinessChecks == 1)
                happiness = rnd.Next(15, 60);
            else if (happinessChecks == 2)
                happiness = rnd.Next(35, 70);
            else if (happinessChecks == 3)
                happiness = rnd.Next(50, 80);
            else if (happinessChecks == 4)
                happiness = rnd.Next(75, 101);

            TeamHappiness = happiness;

            TeamHappinessDescription = DescribeHappiness(TeamHappiness);
            TeamHappinessString = EnumToString(TeamHappinessDescription.ToString());
        }

        //want to rewrite this to use TeamHappiness, Contract status
        //and a little randomness
        public void DetermineAgencyHappiness(Random rnd, Contract contract)
        {
            int happiness;
            bool contentWithContract = true;

            if (Sport == Sports.Baseball || Sport == Sports.Basketball || Sport == Sports.Football || Sport == Sports.Hockey || Sport == Sports.Soccer)
            {
                if (Greed >= 65 && contract.YearlySalary < (League.MaxSalary * (PotentialSkill / 100))) contentWithContract = false;

                if (contentWithContract)
                    happiness = (rnd.Next(41, 100) + TeamHappiness) / 2;
                else happiness = (rnd.Next(0, 41) + TeamHappiness) / 2;
            }
            else if (Sport == Sports.Boxing || Sport == Sports.Golf || Sport == Sports.MMA || Sport == Sports.Tennis)
                happiness = rnd.Next(0, 100);
            else happiness = rnd.Next(0, 100);
            

            AgencyHappiness = happiness;

            AgencyHappinessDescription = DescribeHappiness(AgencyHappiness);
            AgencyHappinessString = EnumToString(AgencyHappinessDescription.ToString());
        }

        public HappinessDescription DescribeHappiness(int happy)
        {
            HappinessDescription description;

            if (happy <= 20) description = HappinessDescription.Disgruntled;
            else if (happy <= 40) description = HappinessDescription.Displeased;
            else if (happy <= 60) description = HappinessDescription.Neutral;
            else if (happy <= 80) description = HappinessDescription.Happy;
            else description = HappinessDescription.Ecstatic;

            return description;
        }

        public string EnumToString(string s)
        {
            s = Regex.Replace(s, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
            return s;
        }
    }
}
