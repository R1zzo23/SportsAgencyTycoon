namespace SportsAgencyTycoon
{
    partial class ManagerForm
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
            this.managerNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.negotiatingLabel = new System.Windows.Forms.Label();
            this.industryPowerLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.greedLabel = new System.Windows.Forms.Label();
            this.memberOfAgencyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // managerNameLabel
            // 
            this.managerNameLabel.AutoSize = true;
            this.managerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managerNameLabel.Location = new System.Drawing.Point(269, 9);
            this.managerNameLabel.Name = "managerNameLabel";
            this.managerNameLabel.Size = new System.Drawing.Size(250, 37);
            this.managerNameLabel.TabIndex = 0;
            this.managerNameLabel.Text = "Manager Name";
            this.managerNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "NEG:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "LEV:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "POW:";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(13, 88);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(34, 13);
            this.label.TabIndex = 4;
            this.label.Text = "GRD:";
            // 
            // negotiatingLabel
            // 
            this.negotiatingLabel.AutoSize = true;
            this.negotiatingLabel.Location = new System.Drawing.Point(52, 64);
            this.negotiatingLabel.Name = "negotiatingLabel";
            this.negotiatingLabel.Size = new System.Drawing.Size(13, 13);
            this.negotiatingLabel.TabIndex = 5;
            this.negotiatingLabel.Text = "0";
            // 
            // industryPowerLabel
            // 
            this.industryPowerLabel.AutoSize = true;
            this.industryPowerLabel.Location = new System.Drawing.Point(52, 115);
            this.industryPowerLabel.Name = "industryPowerLabel";
            this.industryPowerLabel.Size = new System.Drawing.Size(13, 13);
            this.industryPowerLabel.TabIndex = 6;
            this.industryPowerLabel.Text = "0";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(52, 142);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(13, 13);
            this.levelLabel.TabIndex = 7;
            this.levelLabel.Text = "0";
            // 
            // greedLabel
            // 
            this.greedLabel.AutoSize = true;
            this.greedLabel.Location = new System.Drawing.Point(53, 88);
            this.greedLabel.Name = "greedLabel";
            this.greedLabel.Size = new System.Drawing.Size(13, 13);
            this.greedLabel.TabIndex = 8;
            this.greedLabel.Text = "0";
            // 
            // memberOfAgencyLabel
            // 
            this.memberOfAgencyLabel.AutoSize = true;
            this.memberOfAgencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.memberOfAgencyLabel.Location = new System.Drawing.Point(283, 46);
            this.memberOfAgencyLabel.Name = "memberOfAgencyLabel";
            this.memberOfAgencyLabel.Size = new System.Drawing.Size(210, 17);
            this.memberOfAgencyLabel.TabIndex = 9;
            this.memberOfAgencyLabel.Text = "Member of [Agency Name Here]";
            this.memberOfAgencyLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.memberOfAgencyLabel);
            this.Controls.Add(this.greedLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.industryPowerLabel);
            this.Controls.Add(this.negotiatingLabel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.managerNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagerForm";
            this.Text = "ManagerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label managerNameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label negotiatingLabel;
        private System.Windows.Forms.Label industryPowerLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label greedLabel;
        private System.Windows.Forms.Label memberOfAgencyLabel;
    }
}