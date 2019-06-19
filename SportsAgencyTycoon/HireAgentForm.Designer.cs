namespace SportsAgencyTycoon
{
    partial class HireAgentForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.declineButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is a modal.";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(149, 125);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(85, 22);
            this.acceptButton.TabIndex = 1;
            this.acceptButton.Text = "Green Check";
            this.acceptButton.UseVisualStyleBackColor = true;
            // 
            // declineButton
            // 
            this.declineButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.declineButton.Location = new System.Drawing.Point(250, 124);
            this.declineButton.Name = "declineButton";
            this.declineButton.Size = new System.Drawing.Size(75, 23);
            this.declineButton.TabIndex = 2;
            this.declineButton.Text = "Red X";
            this.declineButton.UseVisualStyleBackColor = true;
            // 
            // HireAgentForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.declineButton;
            this.ClientSize = new System.Drawing.Size(461, 233);
            this.ControlBox = false;
            this.Controls.Add(this.declineButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HireAgentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hire Agent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button declineButton;
    }
}