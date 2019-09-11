namespace SportsAgencyTycoon
{
    partial class TeamRosterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbLeagues = new System.Windows.Forms.ComboBox();
            this.cbTeamList = new System.Windows.Forms.ComboBox();
            this.lblRoster = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAwards = new System.Windows.Forms.Label();
            this.lblTeamInfo = new System.Windows.Forms.Label();
            this.cbTeamRoster = new System.Windows.Forms.ComboBox();
            this.lblYearlySalary = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAgentPercent = new System.Windows.Forms.Label();
            this.lblAgentPaid = new System.Windows.Forms.Label();
            this.lblYearsLeft = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblSkillLevel = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblAgencyHappiness = new System.Windows.Forms.Label();
            this.lblTeamHappiness = new System.Windows.Forms.Label();
            this.lblPlayForTitle = new System.Windows.Forms.Label();
            this.lblLoyalty = new System.Windows.Forms.Label();
            this.lblLifestyle = new System.Windows.Forms.Label();
            this.lblGreed = new System.Windows.Forms.Label();
            this.lblPopularity = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLeagues
            // 
            this.cbLeagues.FormattingEnabled = true;
            this.cbLeagues.Location = new System.Drawing.Point(13, 13);
            this.cbLeagues.Name = "cbLeagues";
            this.cbLeagues.Size = new System.Drawing.Size(240, 21);
            this.cbLeagues.TabIndex = 0;
            this.cbLeagues.SelectedIndexChanged += new System.EventHandler(this.cbLeagues_SelectedIndexChanged);
            // 
            // cbTeamList
            // 
            this.cbTeamList.FormattingEnabled = true;
            this.cbTeamList.Location = new System.Drawing.Point(259, 13);
            this.cbTeamList.Name = "cbTeamList";
            this.cbTeamList.Size = new System.Drawing.Size(240, 21);
            this.cbTeamList.TabIndex = 1;
            this.cbTeamList.SelectedIndexChanged += new System.EventHandler(this.cbTeamList_SelectedIndexChanged);
            // 
            // lblRoster
            // 
            this.lblRoster.AutoSize = true;
            this.lblRoster.Location = new System.Drawing.Point(4, 175);
            this.lblRoster.Name = "lblRoster";
            this.lblRoster.Size = new System.Drawing.Size(35, 13);
            this.lblRoster.TabIndex = 2;
            this.lblRoster.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAwards);
            this.panel1.Controls.Add(this.lblTeamInfo);
            this.panel1.Controls.Add(this.lblRoster);
            this.panel1.Location = new System.Drawing.Point(13, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 519);
            this.panel1.TabIndex = 3;
            // 
            // lblAwards
            // 
            this.lblAwards.AutoSize = true;
            this.lblAwards.Location = new System.Drawing.Point(6, 48);
            this.lblAwards.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAwards.Name = "lblAwards";
            this.lblAwards.Size = new System.Drawing.Size(35, 13);
            this.lblAwards.TabIndex = 4;
            this.lblAwards.Text = "label1";
            // 
            // lblTeamInfo
            // 
            this.lblTeamInfo.AutoSize = true;
            this.lblTeamInfo.Location = new System.Drawing.Point(4, 12);
            this.lblTeamInfo.Name = "lblTeamInfo";
            this.lblTeamInfo.Size = new System.Drawing.Size(35, 13);
            this.lblTeamInfo.TabIndex = 3;
            this.lblTeamInfo.Text = "label1";
            // 
            // cbTeamRoster
            // 
            this.cbTeamRoster.FormattingEnabled = true;
            this.cbTeamRoster.Location = new System.Drawing.Point(505, 13);
            this.cbTeamRoster.Name = "cbTeamRoster";
            this.cbTeamRoster.Size = new System.Drawing.Size(240, 21);
            this.cbTeamRoster.TabIndex = 4;
            this.cbTeamRoster.SelectedIndexChanged += new System.EventHandler(this.cbTeamRoster_SelectedIndexChanged);
            // 
            // lblYearlySalary
            // 
            this.lblYearlySalary.AutoSize = true;
            this.lblYearlySalary.Location = new System.Drawing.Point(6, 22);
            this.lblYearlySalary.Name = "lblYearlySalary";
            this.lblYearlySalary.Size = new System.Drawing.Size(71, 13);
            this.lblYearlySalary.TabIndex = 4;
            this.lblYearlySalary.Text = "Yearly Salary:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAgentPercent);
            this.groupBox1.Controls.Add(this.lblAgentPaid);
            this.groupBox1.Controls.Add(this.lblYearsLeft);
            this.groupBox1.Controls.Add(this.lblYearlySalary);
            this.groupBox1.Location = new System.Drawing.Point(505, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 125);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contract Info";
            // 
            // lblAgentPercent
            // 
            this.lblAgentPercent.AutoSize = true;
            this.lblAgentPercent.Location = new System.Drawing.Point(6, 94);
            this.lblAgentPercent.Name = "lblAgentPercent";
            this.lblAgentPercent.Size = new System.Drawing.Size(96, 13);
            this.lblAgentPercent.TabIndex = 7;
            this.lblAgentPercent.Text = "Agent Percentage:";
            // 
            // lblAgentPaid
            // 
            this.lblAgentPaid.AutoSize = true;
            this.lblAgentPaid.Location = new System.Drawing.Point(6, 71);
            this.lblAgentPaid.Name = "lblAgentPaid";
            this.lblAgentPaid.Size = new System.Drawing.Size(65, 13);
            this.lblAgentPaid.TabIndex = 6;
            this.lblAgentPaid.Text = "Agent Paid: ";
            // 
            // lblYearsLeft
            // 
            this.lblYearsLeft.AutoSize = true;
            this.lblYearsLeft.Location = new System.Drawing.Point(6, 46);
            this.lblYearsLeft.Name = "lblYearsLeft";
            this.lblYearsLeft.Size = new System.Drawing.Size(58, 13);
            this.lblYearsLeft.TabIndex = 5;
            this.lblYearsLeft.Text = "Years Left:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblPosition);
            this.groupBox2.Controls.Add(this.lblSkillLevel);
            this.groupBox2.Controls.Add(this.lblAge);
            this.groupBox2.Controls.Add(this.lblFullName);
            this.groupBox2.Location = new System.Drawing.Point(505, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 123);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player Info";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(6, 16);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(47, 13);
            this.lblPosition.TabIndex = 10;
            this.lblPosition.Text = "Position:";
            // 
            // lblSkillLevel
            // 
            this.lblSkillLevel.AutoSize = true;
            this.lblSkillLevel.Location = new System.Drawing.Point(6, 89);
            this.lblSkillLevel.Name = "lblSkillLevel";
            this.lblSkillLevel.Size = new System.Drawing.Size(61, 13);
            this.lblSkillLevel.TabIndex = 9;
            this.lblSkillLevel.Text = "Skill Level: ";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(6, 64);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 8;
            this.lblAge.Text = "Age:";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(6, 40);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(71, 13);
            this.lblFullName.TabIndex = 7;
            this.lblFullName.Text = "Yearly Salary:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblAgencyHappiness);
            this.groupBox3.Controls.Add(this.lblTeamHappiness);
            this.groupBox3.Controls.Add(this.lblPlayForTitle);
            this.groupBox3.Controls.Add(this.lblLoyalty);
            this.groupBox3.Controls.Add(this.lblLifestyle);
            this.groupBox3.Controls.Add(this.lblGreed);
            this.groupBox3.Controls.Add(this.lblPopularity);
            this.groupBox3.Location = new System.Drawing.Point(505, 300);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 201);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Player Emotions";
            // 
            // lblAgencyHappiness
            // 
            this.lblAgencyHappiness.AutoSize = true;
            this.lblAgencyHappiness.Location = new System.Drawing.Point(6, 173);
            this.lblAgencyHappiness.Name = "lblAgencyHappiness";
            this.lblAgencyHappiness.Size = new System.Drawing.Size(99, 13);
            this.lblAgencyHappiness.TabIndex = 10;
            this.lblAgencyHappiness.Text = "Agency Happiness:";
            // 
            // lblTeamHappiness
            // 
            this.lblTeamHappiness.AutoSize = true;
            this.lblTeamHappiness.Location = new System.Drawing.Point(6, 148);
            this.lblTeamHappiness.Name = "lblTeamHappiness";
            this.lblTeamHappiness.Size = new System.Drawing.Size(90, 13);
            this.lblTeamHappiness.TabIndex = 9;
            this.lblTeamHappiness.Text = "Team Happiness:";
            // 
            // lblPlayForTitle
            // 
            this.lblPlayForTitle.AutoSize = true;
            this.lblPlayForTitle.Location = new System.Drawing.Point(6, 124);
            this.lblPlayForTitle.Name = "lblPlayForTitle";
            this.lblPlayForTitle.Size = new System.Drawing.Size(68, 13);
            this.lblPlayForTitle.TabIndex = 8;
            this.lblPlayForTitle.Text = "Play for Title:";
            // 
            // lblLoyalty
            // 
            this.lblLoyalty.AutoSize = true;
            this.lblLoyalty.Location = new System.Drawing.Point(6, 99);
            this.lblLoyalty.Name = "lblLoyalty";
            this.lblLoyalty.Size = new System.Drawing.Size(43, 13);
            this.lblLoyalty.TabIndex = 7;
            this.lblLoyalty.Text = "Loyalty:";
            // 
            // lblLifestyle
            // 
            this.lblLifestyle.AutoSize = true;
            this.lblLifestyle.Location = new System.Drawing.Point(6, 71);
            this.lblLifestyle.Name = "lblLifestyle";
            this.lblLifestyle.Size = new System.Drawing.Size(51, 13);
            this.lblLifestyle.TabIndex = 6;
            this.lblLifestyle.Text = "Lifestyle: ";
            // 
            // lblGreed
            // 
            this.lblGreed.AutoSize = true;
            this.lblGreed.Location = new System.Drawing.Point(6, 46);
            this.lblGreed.Name = "lblGreed";
            this.lblGreed.Size = new System.Drawing.Size(42, 13);
            this.lblGreed.TabIndex = 5;
            this.lblGreed.Text = "Greed: ";
            // 
            // lblPopularity
            // 
            this.lblPopularity.AutoSize = true;
            this.lblPopularity.Location = new System.Drawing.Point(6, 22);
            this.lblPopularity.Name = "lblPopularity";
            this.lblPopularity.Size = new System.Drawing.Size(56, 13);
            this.lblPopularity.TabIndex = 4;
            this.lblPopularity.Text = "Popularity:";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(751, 52);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(35, 13);
            this.lblStats.TabIndex = 8;
            this.lblStats.Text = "label1";
            // 
            // TeamRosterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 578);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbTeamRoster);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbTeamList);
            this.Controls.Add(this.cbLeagues);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TeamRosterForm";
            this.Text = "Team Rosters";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLeagues;
        private System.Windows.Forms.ComboBox cbTeamList;
        private System.Windows.Forms.Label lblRoster;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTeamInfo;
        private System.Windows.Forms.ComboBox cbTeamRoster;
        private System.Windows.Forms.Label lblYearlySalary;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAgentPaid;
        private System.Windows.Forms.Label lblYearsLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSkillLevel;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblLifestyle;
        private System.Windows.Forms.Label lblGreed;
        private System.Windows.Forms.Label lblPopularity;
        private System.Windows.Forms.Label lblPlayForTitle;
        private System.Windows.Forms.Label lblLoyalty;
        private System.Windows.Forms.Label lblAgencyHappiness;
        private System.Windows.Forms.Label lblTeamHappiness;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblAgentPercent;
        private System.Windows.Forms.Label lblAwards;
        private System.Windows.Forms.Label lblStats;
    }
}