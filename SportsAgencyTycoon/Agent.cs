﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsAgencyTycoon
{
    public class Agent
    {
        public string First;
        public string Last;
        public int Salary;
        public int Negotiating;
        public int Greed;
        public int IndustryPower;
        public int Intelligence;
        public int Level;
        public Roles Role;
        public List<Client> ClientList;
        public int ClientCount;
        public List<Licenses> LicensesHeld;
        public bool HasAppliedForLicense;
        public Licenses AppliedLicense;
        public int LicenseTestPrep;
        public bool BeingTrainedForTest;

        public Agent(string firstName, string lastName, int salary, int negotiatingRating, int greedRating, int industryPowerRating, int intelligence, int levelRating, Roles role)
        {
            First = firstName;
            Last = lastName;
            Salary = salary;
            Negotiating = negotiatingRating;
            Greed = greedRating;
            IndustryPower = industryPowerRating;
            Intelligence = intelligence;
            Level = levelRating;
            Role = role;
            ClientList = new List<Client>();
            LicensesHeld = new List<Licenses>();
            BeingTrainedForTest = false;
            AppliedLicense = null;
            HasAppliedForLicense = false;
        }

        public void AddClient(Client client)
        {
            ClientList.Add(client);
            ClientCount = ClientList.Count();
        }
    }

    public enum Roles
    {
        Manager,
        Agent,
        Marketer
    }
}
