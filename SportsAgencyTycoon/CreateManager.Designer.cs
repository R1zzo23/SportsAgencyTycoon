namespace SportsAgencyTycoon
{
    partial class CreateManager
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.managerLastNameTextBox = new System.Windows.Forms.TextBox();
            this.managerFirstNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.agencyNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateManagerAndAgency = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create Your Manager and Agency";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.managerLastNameTextBox);
            this.groupBox1.Controls.Add(this.managerFirstNameTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 102);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manager Information";
            // 
            // managerLastNameTextBox
            // 
            this.managerLastNameTextBox.Location = new System.Drawing.Point(74, 49);
            this.managerLastNameTextBox.Name = "managerLastNameTextBox";
            this.managerLastNameTextBox.Size = new System.Drawing.Size(177, 20);
            this.managerLastNameTextBox.TabIndex = 3;
            // 
            // managerFirstNameTextBox
            // 
            this.managerFirstNameTextBox.Location = new System.Drawing.Point(74, 20);
            this.managerFirstNameTextBox.Name = "managerFirstNameTextBox";
            this.managerFirstNameTextBox.Size = new System.Drawing.Size(177, 20);
            this.managerFirstNameTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Last Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "First Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.agencyNameTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 67);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agency Information";
            // 
            // agencyNameTextBox
            // 
            this.agencyNameTextBox.Location = new System.Drawing.Point(90, 17);
            this.agencyNameTextBox.Name = "agencyNameTextBox";
            this.agencyNameTextBox.Size = new System.Drawing.Size(208, 20);
            this.agencyNameTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Agency Name:";
            // 
            // btnCreateManagerAndAgency
            // 
            this.btnCreateManagerAndAgency.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateManagerAndAgency.Location = new System.Drawing.Point(116, 225);
            this.btnCreateManagerAndAgency.Name = "btnCreateManagerAndAgency";
            this.btnCreateManagerAndAgency.Size = new System.Drawing.Size(75, 23);
            this.btnCreateManagerAndAgency.TabIndex = 3;
            this.btnCreateManagerAndAgency.Text = "Create";
            this.btnCreateManagerAndAgency.UseVisualStyleBackColor = true;
            this.btnCreateManagerAndAgency.Click += new System.EventHandler(this.btnCreateManagerAndAgency_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(330, 43);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(88, 13);
            this.infoLabel.TabIndex = 4;
            this.infoLabel.Text = "Information Label";
            // 
            // CreateManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 383);
            this.ControlBox = false;
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.btnCreateManagerAndAgency);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateManager";
            this.Text = "CreateManager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox managerLastNameTextBox;
        private System.Windows.Forms.TextBox managerFirstNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox agencyNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateManagerAndAgency;
        private System.Windows.Forms.Label infoLabel;
    }
}