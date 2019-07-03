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
            this.lblTeamInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.lblRoster.Location = new System.Drawing.Point(4, 40);
            this.lblRoster.Name = "lblRoster";
            this.lblRoster.Size = new System.Drawing.Size(35, 13);
            this.lblRoster.TabIndex = 2;
            this.lblRoster.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTeamInfo);
            this.panel1.Controls.Add(this.lblRoster);
            this.panel1.Location = new System.Drawing.Point(13, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 519);
            this.panel1.TabIndex = 3;
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
            // TeamRosterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 632);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbTeamList);
            this.Controls.Add(this.cbLeagues);
            this.Name = "TeamRosterForm";
            this.Text = "Team Rosters";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLeagues;
        private System.Windows.Forms.ComboBox cbTeamList;
        private System.Windows.Forms.Label lblRoster;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTeamInfo;
    }
}