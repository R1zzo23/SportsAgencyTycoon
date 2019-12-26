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
            this.btnHireSelected = new System.Windows.Forms.Button();
            this.btnPassOnHiring = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAgent3 = new System.Windows.Forms.Label();
            this.lblAgent2 = new System.Windows.Forms.Label();
            this.lblAgent1 = new System.Windows.Forms.Label();
            this.radioApplicant3 = new System.Windows.Forms.RadioButton();
            this.radioApplicant2 = new System.Windows.Forms.RadioButton();
            this.radioApplicant1 = new System.Windows.Forms.RadioButton();
            this.lblA3Licenses = new System.Windows.Forms.Label();
            this.lblA1Licenses = new System.Windows.Forms.Label();
            this.lblA2Licenses = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHireSelected
            // 
            this.btnHireSelected.Location = new System.Drawing.Point(49, 243);
            this.btnHireSelected.Name = "btnHireSelected";
            this.btnHireSelected.Size = new System.Drawing.Size(85, 22);
            this.btnHireSelected.TabIndex = 1;
            this.btnHireSelected.Text = "Hire Selected";
            this.btnHireSelected.UseVisualStyleBackColor = true;
            this.btnHireSelected.Click += new System.EventHandler(this.btnHireSelected_Click);
            // 
            // btnPassOnHiring
            // 
            this.btnPassOnHiring.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPassOnHiring.Location = new System.Drawing.Point(189, 243);
            this.btnPassOnHiring.Name = "btnPassOnHiring";
            this.btnPassOnHiring.Size = new System.Drawing.Size(92, 23);
            this.btnPassOnHiring.TabIndex = 2;
            this.btnPassOnHiring.Text = "Pass On Hiring";
            this.btnPassOnHiring.UseVisualStyleBackColor = true;
            this.btnPassOnHiring.Click += new System.EventHandler(this.btnPassOnHiring_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblA2Licenses);
            this.groupBox1.Controls.Add(this.lblA1Licenses);
            this.groupBox1.Controls.Add(this.lblA3Licenses);
            this.groupBox1.Controls.Add(this.lblAgent3);
            this.groupBox1.Controls.Add(this.lblAgent2);
            this.groupBox1.Controls.Add(this.lblAgent1);
            this.groupBox1.Controls.Add(this.radioApplicant3);
            this.groupBox1.Controls.Add(this.radioApplicant2);
            this.groupBox1.Controls.Add(this.radioApplicant1);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 232);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Applicants";
            // 
            // lblAgent3
            // 
            this.lblAgent3.AutoSize = true;
            this.lblAgent3.Location = new System.Drawing.Point(10, 182);
            this.lblAgent3.Name = "lblAgent3";
            this.lblAgent3.Size = new System.Drawing.Size(35, 13);
            this.lblAgent3.TabIndex = 5;
            this.lblAgent3.Text = "label3";
            // 
            // lblAgent2
            // 
            this.lblAgent2.AutoSize = true;
            this.lblAgent2.Location = new System.Drawing.Point(10, 108);
            this.lblAgent2.Name = "lblAgent2";
            this.lblAgent2.Size = new System.Drawing.Size(35, 13);
            this.lblAgent2.TabIndex = 4;
            this.lblAgent2.Text = "label2";
            // 
            // lblAgent1
            // 
            this.lblAgent1.AutoSize = true;
            this.lblAgent1.Location = new System.Drawing.Point(7, 44);
            this.lblAgent1.Name = "lblAgent1";
            this.lblAgent1.Size = new System.Drawing.Size(35, 13);
            this.lblAgent1.TabIndex = 3;
            this.lblAgent1.Text = "label1";
            // 
            // radioApplicant3
            // 
            this.radioApplicant3.AutoSize = true;
            this.radioApplicant3.Location = new System.Drawing.Point(10, 158);
            this.radioApplicant3.Name = "radioApplicant3";
            this.radioApplicant3.Size = new System.Drawing.Size(85, 17);
            this.radioApplicant3.TabIndex = 2;
            this.radioApplicant3.TabStop = true;
            this.radioApplicant3.Text = "radioButton3";
            this.radioApplicant3.UseVisualStyleBackColor = true;
            // 
            // radioApplicant2
            // 
            this.radioApplicant2.AutoSize = true;
            this.radioApplicant2.Location = new System.Drawing.Point(8, 84);
            this.radioApplicant2.Name = "radioApplicant2";
            this.radioApplicant2.Size = new System.Drawing.Size(85, 17);
            this.radioApplicant2.TabIndex = 1;
            this.radioApplicant2.TabStop = true;
            this.radioApplicant2.Text = "radioButton2";
            this.radioApplicant2.UseVisualStyleBackColor = true;
            // 
            // radioApplicant1
            // 
            this.radioApplicant1.AutoSize = true;
            this.radioApplicant1.Location = new System.Drawing.Point(8, 20);
            this.radioApplicant1.Name = "radioApplicant1";
            this.radioApplicant1.Size = new System.Drawing.Size(85, 17);
            this.radioApplicant1.TabIndex = 0;
            this.radioApplicant1.TabStop = true;
            this.radioApplicant1.Text = "radioButton1";
            this.radioApplicant1.UseVisualStyleBackColor = true;
            // 
            // lblA3Licenses
            // 
            this.lblA3Licenses.AutoSize = true;
            this.lblA3Licenses.Location = new System.Drawing.Point(10, 206);
            this.lblA3Licenses.Name = "lblA3Licenses";
            this.lblA3Licenses.Size = new System.Drawing.Size(35, 13);
            this.lblA3Licenses.TabIndex = 6;
            this.lblA3Licenses.Text = "label1";
            // 
            // lblA1Licenses
            // 
            this.lblA1Licenses.AutoSize = true;
            this.lblA1Licenses.Location = new System.Drawing.Point(7, 67);
            this.lblA1Licenses.Name = "lblA1Licenses";
            this.lblA1Licenses.Size = new System.Drawing.Size(35, 13);
            this.lblA1Licenses.TabIndex = 7;
            this.lblA1Licenses.Text = "label2";
            // 
            // lblA2Licenses
            // 
            this.lblA2Licenses.AutoSize = true;
            this.lblA2Licenses.Location = new System.Drawing.Point(10, 131);
            this.lblA2Licenses.Name = "lblA2Licenses";
            this.lblA2Licenses.Size = new System.Drawing.Size(35, 13);
            this.lblA2Licenses.TabIndex = 8;
            this.lblA2Licenses.Text = "label3";
            // 
            // HireAgentForm
            // 
            this.AcceptButton = this.btnHireSelected;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnPassOnHiring;
            this.ClientSize = new System.Drawing.Size(338, 277);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPassOnHiring);
            this.Controls.Add(this.btnHireSelected);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HireAgentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hire Agent";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnHireSelected;
        private System.Windows.Forms.Button btnPassOnHiring;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioApplicant3;
        private System.Windows.Forms.RadioButton radioApplicant2;
        private System.Windows.Forms.RadioButton radioApplicant1;
        private System.Windows.Forms.Label lblAgent3;
        private System.Windows.Forms.Label lblAgent2;
        private System.Windows.Forms.Label lblAgent1;
        private System.Windows.Forms.Label lblA2Licenses;
        private System.Windows.Forms.Label lblA1Licenses;
        private System.Windows.Forms.Label lblA3Licenses;
    }
}