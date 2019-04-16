namespace SportsAgencyTycoon
{
    partial class AgencyForm
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
            this.agencyNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clientCountLabel = new System.Windows.Forms.Label();
            this.agentCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moneyLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.industryInfluenceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // agencyNameLabel
            // 
            this.agencyNameLabel.AutoSize = true;
            this.agencyNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agencyNameLabel.Location = new System.Drawing.Point(260, 9);
            this.agencyNameLabel.Name = "agencyNameLabel";
            this.agencyNameLabel.Size = new System.Drawing.Size(229, 37);
            this.agencyNameLabel.TabIndex = 0;
            this.agencyNameLabel.Text = "Agency Name";
            this.agencyNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "# of Agents:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "# of Clients:";
            // 
            // clientCountLabel
            // 
            this.clientCountLabel.AutoSize = true;
            this.clientCountLabel.Location = new System.Drawing.Point(493, 54);
            this.clientCountLabel.Name = "clientCountLabel";
            this.clientCountLabel.Size = new System.Drawing.Size(13, 13);
            this.clientCountLabel.TabIndex = 3;
            this.clientCountLabel.Text = "0";
            // 
            // agentCountLabel
            // 
            this.agentCountLabel.AutoSize = true;
            this.agentCountLabel.Location = new System.Drawing.Point(310, 54);
            this.agentCountLabel.Name = "agentCountLabel";
            this.agentCountLabel.Size = new System.Drawing.Size(13, 13);
            this.agentCountLabel.TabIndex = 4;
            this.agentCountLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Money:";
            // 
            // moneyLabel
            // 
            this.moneyLabel.AutoSize = true;
            this.moneyLabel.Location = new System.Drawing.Point(61, 54);
            this.moneyLabel.Name = "moneyLabel";
            this.moneyLabel.Size = new System.Drawing.Size(13, 13);
            this.moneyLabel.TabIndex = 6;
            this.moneyLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(606, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Industry Influence:";
            // 
            // industryInfluenceLabel
            // 
            this.industryInfluenceLabel.AutoSize = true;
            this.industryInfluenceLabel.Location = new System.Drawing.Point(706, 54);
            this.industryInfluenceLabel.Name = "industryInfluenceLabel";
            this.industryInfluenceLabel.Size = new System.Drawing.Size(36, 13);
            this.industryInfluenceLabel.TabIndex = 8;
            this.industryInfluenceLabel.Text = "0/100";
            // 
            // AgencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.industryInfluenceLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.moneyLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.agentCountLabel);
            this.Controls.Add(this.clientCountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.agencyNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgencyForm";
            this.Text = "AgencyForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label agencyNameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label clientCountLabel;
        private System.Windows.Forms.Label agentCountLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label moneyLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label industryInfluenceLabel;
    }
}