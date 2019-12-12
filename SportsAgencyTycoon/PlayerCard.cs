﻿using System;
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
    public partial class PlayerCard : Form
    {
        private Player p;
        public PlayerCard(Player selectedClient)
        {
            InitializeComponent();
            p = selectedClient;
            FillLabels();
        }
        public void FillLabels()
        {
            lblName.Text = p.FullName;
            lblBehavior.Text = p.BehaviorDescription;
            lblComposure.Text = p.ComposureString;
            lblGreed.Text = p.GreedString;
            lblLeadership.Text = p.LeadershipString;
            lblWorkEthic.Text = p.WorkEthicString;
        }
    }
}
