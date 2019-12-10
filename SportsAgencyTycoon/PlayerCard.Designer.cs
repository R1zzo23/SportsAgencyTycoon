namespace SportsAgencyTycoon
{
    partial class PlayerCard
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblBehavior = new System.Windows.Forms.Label();
            this.lblComposure = new System.Windows.Forms.Label();
            this.lblGreed = new System.Windows.Forms.Label();
            this.lblLeadership = new System.Windows.Forms.Label();
            this.lblWorkEthic = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // lblBehavior
            // 
            this.lblBehavior.AutoSize = true;
            this.lblBehavior.Location = new System.Drawing.Point(16, 59);
            this.lblBehavior.Name = "lblBehavior";
            this.lblBehavior.Size = new System.Drawing.Size(35, 13);
            this.lblBehavior.TabIndex = 1;
            this.lblBehavior.Text = "label1";
            // 
            // lblComposure
            // 
            this.lblComposure.AutoSize = true;
            this.lblComposure.Location = new System.Drawing.Point(16, 87);
            this.lblComposure.Name = "lblComposure";
            this.lblComposure.Size = new System.Drawing.Size(35, 13);
            this.lblComposure.TabIndex = 2;
            this.lblComposure.Text = "label2";
            // 
            // lblGreed
            // 
            this.lblGreed.AutoSize = true;
            this.lblGreed.Location = new System.Drawing.Point(16, 117);
            this.lblGreed.Name = "lblGreed";
            this.lblGreed.Size = new System.Drawing.Size(35, 13);
            this.lblGreed.TabIndex = 3;
            this.lblGreed.Text = "label3";
            // 
            // lblLeadership
            // 
            this.lblLeadership.AutoSize = true;
            this.lblLeadership.Location = new System.Drawing.Point(16, 146);
            this.lblLeadership.Name = "lblLeadership";
            this.lblLeadership.Size = new System.Drawing.Size(35, 13);
            this.lblLeadership.TabIndex = 4;
            this.lblLeadership.Text = "label4";
            // 
            // lblWorkEthic
            // 
            this.lblWorkEthic.AutoSize = true;
            this.lblWorkEthic.Location = new System.Drawing.Point(16, 171);
            this.lblWorkEthic.Name = "lblWorkEthic";
            this.lblWorkEthic.Size = new System.Drawing.Size(35, 13);
            this.lblWorkEthic.TabIndex = 5;
            this.lblWorkEthic.Text = "label5";
            // 
            // PlayerCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblWorkEthic);
            this.Controls.Add(this.lblLeadership);
            this.Controls.Add(this.lblGreed);
            this.Controls.Add(this.lblComposure);
            this.Controls.Add(this.lblBehavior);
            this.Controls.Add(this.lblName);
            this.Name = "PlayerCard";
            this.Text = "PlayerCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBehavior;
        private System.Windows.Forms.Label lblComposure;
        private System.Windows.Forms.Label lblGreed;
        private System.Windows.Forms.Label lblLeadership;
        private System.Windows.Forms.Label lblWorkEthic;
    }
}