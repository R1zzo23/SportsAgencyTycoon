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
            // NegotiatePercentageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 300);
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
    }
}