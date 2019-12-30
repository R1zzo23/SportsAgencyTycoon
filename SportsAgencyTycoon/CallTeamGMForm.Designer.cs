namespace SportsAgencyTycoon
{
    partial class CallTeamGMForm
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
            this.lblGMTalk = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGMTalk
            // 
            this.lblGMTalk.AutoSize = true;
            this.lblGMTalk.Location = new System.Drawing.Point(13, 13);
            this.lblGMTalk.Name = "lblGMTalk";
            this.lblGMTalk.Size = new System.Drawing.Size(35, 13);
            this.lblGMTalk.TabIndex = 0;
            this.lblGMTalk.Text = "label1";
            // 
            // CallTeamGMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblGMTalk);
            this.Name = "CallTeamGMForm";
            this.Text = "Call Team GM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGMTalk;
    }
}