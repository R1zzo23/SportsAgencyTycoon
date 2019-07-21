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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitleContender = new System.Windows.Forms.Label();
            this.lblMarketValue = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
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
            this.cbTeamList.SelectedIndexChanged += new System.EventHandler(this.cbTeamList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Age";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Skill";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contract";
            // 
            // lblTitleContender
            // 
            this.lblTitleContender.AutoSize = true;
            this.lblTitleContender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleContender.Location = new System.Drawing.Point(264, 35);
            this.lblTitleContender.Name = "lblTitleContender";
            this.lblTitleContender.Size = new System.Drawing.Size(142, 20);
            this.lblTitleContender.TabIndex = 3;
            this.lblTitleContender.Text = "Title Contender: ";
            // 
            // lblMarketValue
            // 
            this.lblMarketValue.AutoSize = true;
            this.lblMarketValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketValue.Location = new System.Drawing.Point(505, 35);
            this.lblMarketValue.Name = "lblMarketValue";
            this.lblMarketValue.Size = new System.Drawing.Size(120, 20);
            this.lblMarketValue.TabIndex = 4;
            this.lblMarketValue.Text = "Market Value:";
            // 
            // CallTeamForClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMarketValue);
            this.Controls.Add(this.lblTitleContender);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbTeamList);
            this.Controls.Add(this.lblLeagueName);
            this.Name = "CallTeamForClientForm";
            this.Text = "Let\'s Find You A Deal!";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLeagueName;
        private System.Windows.Forms.ComboBox cbTeamList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitleContender;
        private System.Windows.Forms.Label lblMarketValue;
    }
}