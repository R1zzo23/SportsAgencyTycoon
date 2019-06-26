namespace SportsAgencyTycoon
{
    partial class CalendarForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPlayerBirthdays = new System.Windows.Forms.Label();
            this.lblAssociationEvents = new System.Windows.Forms.Label();
            this.lblLeagueYearsEnd = new System.Windows.Forms.Label();
            this.lblLeagueYearsStart = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblLeagueYearsStart);
            this.panel1.Controls.Add(this.lblLeagueYearsEnd);
            this.panel1.Controls.Add(this.lblAssociationEvents);
            this.panel1.Controls.Add(this.lblPlayerBirthdays);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(919, 425);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Birthdays:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "League Years Start:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(582, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Assocation Events:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "League Years End:";
            // 
            // lblPlayerBirthdays
            // 
            this.lblPlayerBirthdays.AutoSize = true;
            this.lblPlayerBirthdays.Location = new System.Drawing.Point(4, 31);
            this.lblPlayerBirthdays.Name = "lblPlayerBirthdays";
            this.lblPlayerBirthdays.Size = new System.Drawing.Size(35, 13);
            this.lblPlayerBirthdays.TabIndex = 4;
            this.lblPlayerBirthdays.Text = "label5";
            // 
            // lblAssociationEvents
            // 
            this.lblAssociationEvents.AutoSize = true;
            this.lblAssociationEvents.Location = new System.Drawing.Point(582, 31);
            this.lblAssociationEvents.Name = "lblAssociationEvents";
            this.lblAssociationEvents.Size = new System.Drawing.Size(35, 13);
            this.lblAssociationEvents.TabIndex = 5;
            this.lblAssociationEvents.Text = "label6";
            // 
            // lblLeagueYearsEnd
            // 
            this.lblLeagueYearsEnd.AutoSize = true;
            this.lblLeagueYearsEnd.Location = new System.Drawing.Point(386, 31);
            this.lblLeagueYearsEnd.Name = "lblLeagueYearsEnd";
            this.lblLeagueYearsEnd.Size = new System.Drawing.Size(35, 13);
            this.lblLeagueYearsEnd.TabIndex = 6;
            this.lblLeagueYearsEnd.Text = "label7";
            // 
            // lblLeagueYearsStart
            // 
            this.lblLeagueYearsStart.AutoSize = true;
            this.lblLeagueYearsStart.Location = new System.Drawing.Point(179, 31);
            this.lblLeagueYearsStart.Name = "lblLeagueYearsStart";
            this.lblLeagueYearsStart.Size = new System.Drawing.Size(35, 13);
            this.lblLeagueYearsStart.TabIndex = 7;
            this.lblLeagueYearsStart.Text = "label8";
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 450);
            this.Controls.Add(this.panel1);
            this.Name = "CalendarForm";
            this.Text = "List of Calendar Events";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLeagueYearsStart;
        private System.Windows.Forms.Label lblLeagueYearsEnd;
        private System.Windows.Forms.Label lblAssociationEvents;
        private System.Windows.Forms.Label lblPlayerBirthdays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}