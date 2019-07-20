namespace SportsAgencyTycoon
{
    partial class CallTeamForClientForm
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
            this.lblLeagueName = new System.Windows.Forms.Label();
            this.cbTeamList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblLeagueName
            // 
            this.lblLeagueName.AutoSize = true;
            this.lblLeagueName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeagueName.Location = new System.Drawing.Point(13, 13);
            this.lblLeagueName.Name = "lblLeagueName";
            this.lblLeagueName.Size = new System.Drawing.Size(57, 20);
            this.lblLeagueName.TabIndex = 0;
            this.lblLeagueName.Text = "label1";
            // 
            // cbTeamList
            // 
            this.cbTeamList.FormattingEnabled = true;
            this.cbTeamList.Location = new System.Drawing.Point(17, 37);
            this.cbTeamList.Name = "cbTeamList";
            this.cbTeamList.Size = new System.Drawing.Size(222, 21);
            this.cbTeamList.TabIndex = 1;
            // 
            // CallTeamForClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbTeamList);
            this.Controls.Add(this.lblLeagueName);
            this.Name = "CallTeamForClientForm";
            this.Text = "Let\'s Find You A Deal!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLeagueName;
        private System.Windows.Forms.ComboBox cbTeamList;
    }
}