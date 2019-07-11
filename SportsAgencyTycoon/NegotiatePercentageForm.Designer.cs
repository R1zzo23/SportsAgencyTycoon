namespace SportsAgencyTycoon
{
    partial class NegotiatePercentageForm
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
            this.txtYourAskingPercent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClientGreed = new System.Windows.Forms.Label();
            this.lblAgentGreed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAgentNegotiating = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblClientMood = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMakeOffer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMaxPercent = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMinPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtYourAskingPercent
            // 
            this.txtYourAskingPercent.Location = new System.Drawing.Point(97, 144);
            this.txtYourAskingPercent.Name = "txtYourAskingPercent";
            this.txtYourAskingPercent.Size = new System.Drawing.Size(58, 20);
            this.txtYourAskingPercent.TabIndex = 0;
            this.txtYourAskingPercent.Text = "2.00";
            this.txtYourAskingPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYourAskingPercent_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your Asking %:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Client\'s Greed:";
            // 
            // lblClientGreed
            // 
            this.lblClientGreed.AutoSize = true;
            this.lblClientGreed.Location = new System.Drawing.Point(95, 13);
            this.lblClientGreed.Name = "lblClientGreed";
            this.lblClientGreed.Size = new System.Drawing.Size(0, 13);
            this.lblClientGreed.TabIndex = 3;
            // 
            // lblAgentGreed
            // 
            this.lblAgentGreed.AutoSize = true;
            this.lblAgentGreed.Location = new System.Drawing.Point(95, 39);
            this.lblAgentGreed.Name = "lblAgentGreed";
            this.lblAgentGreed.Size = new System.Drawing.Size(0, 13);
            this.lblAgentGreed.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Agent\'s Greed:";
            // 
            // lblAgentNegotiating
            // 
            this.lblAgentNegotiating.AutoSize = true;
            this.lblAgentNegotiating.Location = new System.Drawing.Point(95, 63);
            this.lblAgentNegotiating.Name = "lblAgentNegotiating";
            this.lblAgentNegotiating.Size = new System.Drawing.Size(0, 13);
            this.lblAgentNegotiating.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Agent\'s Neg:";
            // 
            // lblClientMood
            // 
            this.lblClientMood.AutoSize = true;
            this.lblClientMood.Location = new System.Drawing.Point(208, 13);
            this.lblClientMood.Name = "lblClientMood";
            this.lblClientMood.Size = new System.Drawing.Size(0, 13);
            this.lblClientMood.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Client\'s Mood:";
            // 
            // btnMakeOffer
            // 
            this.btnMakeOffer.Location = new System.Drawing.Point(161, 142);
            this.btnMakeOffer.Name = "btnMakeOffer";
            this.btnMakeOffer.Size = new System.Drawing.Size(75, 23);
            this.btnMakeOffer.TabIndex = 10;
            this.btnMakeOffer.Text = "Make Offer";
            this.btnMakeOffer.UseVisualStyleBackColor = true;
            this.btnMakeOffer.Click += new System.EventHandler(this.btnMakeOffer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Minimum:";
            // 
            // lblMaxPercent
            // 
            this.lblMaxPercent.AutoSize = true;
            this.lblMaxPercent.Location = new System.Drawing.Point(190, 173);
            this.lblMaxPercent.Name = "lblMaxPercent";
            this.lblMaxPercent.Size = new System.Drawing.Size(51, 13);
            this.lblMaxPercent.TabIndex = 12;
            this.lblMaxPercent.Text = "Minimum:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Maximum:";
            // 
            // lblMinPercent
            // 
            this.lblMinPercent.AutoSize = true;
            this.lblMinPercent.Location = new System.Drawing.Point(73, 173);
            this.lblMinPercent.Name = "lblMinPercent";
            this.lblMinPercent.Size = new System.Drawing.Size(51, 13);
            this.lblMinPercent.TabIndex = 14;
            this.lblMinPercent.Text = "Minimum:";
            // 
            // NegotiatePercentageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 300);
            this.Controls.Add(this.lblMinPercent);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblMaxPercent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMakeOffer);
            this.Controls.Add(this.lblClientMood);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAgentNegotiating);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAgentGreed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblClientGreed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtYourAskingPercent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NegotiatePercentageForm";
            this.Text = "Negotiate Agent Commission";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtYourAskingPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClientGreed;
        private System.Windows.Forms.Label lblAgentGreed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAgentNegotiating;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblClientMood;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMakeOffer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMaxPercent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMinPercent;
    }
}