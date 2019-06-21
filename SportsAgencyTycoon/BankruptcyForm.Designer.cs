namespace SportsAgencyTycoon
{
    partial class BankruptcyForm
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
            this.lblBankruptcy = new System.Windows.Forms.Label();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.btnBankrupt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBankruptcy
            // 
            this.lblBankruptcy.AutoSize = true;
            this.lblBankruptcy.Location = new System.Drawing.Point(13, 13);
            this.lblBankruptcy.MaximumSize = new System.Drawing.Size(200, 0);
            this.lblBankruptcy.Name = "lblBankruptcy";
            this.lblBankruptcy.Size = new System.Drawing.Size(35, 13);
            this.lblBankruptcy.TabIndex = 0;
            this.lblBankruptcy.Text = "label1";
            // 
            // btnBorrow
            // 
            this.btnBorrow.Location = new System.Drawing.Point(12, 92);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(81, 23);
            this.btnBorrow.TabIndex = 1;
            this.btnBorrow.Text = "Borrow $200k";
            this.btnBorrow.UseVisualStyleBackColor = true;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnBankrupt
            // 
            this.btnBankrupt.Location = new System.Drawing.Point(115, 92);
            this.btnBankrupt.Name = "btnBankrupt";
            this.btnBankrupt.Size = new System.Drawing.Size(92, 23);
            this.btnBankrupt.TabIndex = 2;
            this.btnBankrupt.Text = "File Bankruptcy";
            this.btnBankrupt.UseVisualStyleBackColor = true;
            this.btnBankrupt.Click += new System.EventHandler(this.btnBankrupt_Click);
            // 
            // BankruptcyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 125);
            this.ControlBox = false;
            this.Controls.Add(this.btnBankrupt);
            this.Controls.Add(this.btnBorrow);
            this.Controls.Add(this.lblBankruptcy);
            this.Name = "BankruptcyForm";
            this.Text = "Bankruptcy Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBankruptcy;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnBankrupt;
    }
}