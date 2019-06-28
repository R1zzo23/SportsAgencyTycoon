namespace SportsAgencyTycoon
{
    partial class StartGameForm
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
            this.txtAgencyName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioRandomIndividual = new System.Windows.Forms.RadioButton();
            this.radioUFC = new System.Windows.Forms.RadioButton();
            this.radioBoxing = new System.Windows.Forms.RadioButton();
            this.radioTennis = new System.Windows.Forms.RadioButton();
            this.radioGolf = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioRandomTeam = new System.Windows.Forms.RadioButton();
            this.radioSoccer = new System.Windows.Forms.RadioButton();
            this.radioHockey = new System.Windows.Forms.RadioButton();
            this.radioFootball = new System.Windows.Forms.RadioButton();
            this.radioBasketball = new System.Windows.Forms.RadioButton();
            this.radioBaseball = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateAgencyAndAgent = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agency Name:";
            // 
            // txtAgencyName
            // 
            this.txtAgencyName.Location = new System.Drawing.Point(96, 6);
            this.txtAgencyName.Name = "txtAgencyName";
            this.txtAgencyName.Size = new System.Drawing.Size(264, 20);
            this.txtAgencyName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 274);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customize Your Agent";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioRandomIndividual);
            this.groupBox3.Controls.Add(this.radioUFC);
            this.groupBox3.Controls.Add(this.radioBoxing);
            this.groupBox3.Controls.Add(this.radioTennis);
            this.groupBox3.Controls.Add(this.radioGolf);
            this.groupBox3.Location = new System.Drawing.Point(147, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 166);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Individual Sports";
            // 
            // radioRandomIndividual
            // 
            this.radioRandomIndividual.AutoSize = true;
            this.radioRandomIndividual.Location = new System.Drawing.Point(7, 120);
            this.radioRandomIndividual.Name = "radioRandomIndividual";
            this.radioRandomIndividual.Size = new System.Drawing.Size(65, 17);
            this.radioRandomIndividual.TabIndex = 9;
            this.radioRandomIndividual.TabStop = true;
            this.radioRandomIndividual.Text = "Random";
            this.radioRandomIndividual.UseVisualStyleBackColor = true;
            // 
            // radioUFC
            // 
            this.radioUFC.AutoSize = true;
            this.radioUFC.Location = new System.Drawing.Point(7, 95);
            this.radioUFC.Name = "radioUFC";
            this.radioUFC.Size = new System.Drawing.Size(103, 17);
            this.radioUFC.TabIndex = 8;
            this.radioUFC.TabStop = true;
            this.radioUFC.Text = "Ultimate Fighting";
            this.radioUFC.UseVisualStyleBackColor = true;
            // 
            // radioBoxing
            // 
            this.radioBoxing.AutoSize = true;
            this.radioBoxing.Location = new System.Drawing.Point(7, 20);
            this.radioBoxing.Name = "radioBoxing";
            this.radioBoxing.Size = new System.Drawing.Size(57, 17);
            this.radioBoxing.TabIndex = 5;
            this.radioBoxing.TabStop = true;
            this.radioBoxing.Text = "Boxing";
            this.radioBoxing.UseVisualStyleBackColor = true;
            // 
            // radioTennis
            // 
            this.radioTennis.AutoSize = true;
            this.radioTennis.Location = new System.Drawing.Point(7, 70);
            this.radioTennis.Name = "radioTennis";
            this.radioTennis.Size = new System.Drawing.Size(57, 17);
            this.radioTennis.TabIndex = 7;
            this.radioTennis.TabStop = true;
            this.radioTennis.Text = "Tennis";
            this.radioTennis.UseVisualStyleBackColor = true;
            // 
            // radioGolf
            // 
            this.radioGolf.AutoSize = true;
            this.radioGolf.Location = new System.Drawing.Point(7, 45);
            this.radioGolf.Name = "radioGolf";
            this.radioGolf.Size = new System.Drawing.Size(44, 17);
            this.radioGolf.TabIndex = 6;
            this.radioGolf.TabStop = true;
            this.radioGolf.Text = "Golf";
            this.radioGolf.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioRandomTeam);
            this.groupBox2.Controls.Add(this.radioSoccer);
            this.groupBox2.Controls.Add(this.radioHockey);
            this.groupBox2.Controls.Add(this.radioFootball);
            this.groupBox2.Controls.Add(this.radioBasketball);
            this.groupBox2.Controls.Add(this.radioBaseball);
            this.groupBox2.Location = new System.Drawing.Point(10, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 166);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Team Sports";
            // 
            // radioRandomTeam
            // 
            this.radioRandomTeam.AutoSize = true;
            this.radioRandomTeam.Location = new System.Drawing.Point(7, 145);
            this.radioRandomTeam.Name = "radioRandomTeam";
            this.radioRandomTeam.Size = new System.Drawing.Size(65, 17);
            this.radioRandomTeam.TabIndex = 5;
            this.radioRandomTeam.TabStop = true;
            this.radioRandomTeam.Text = "Random";
            this.radioRandomTeam.UseVisualStyleBackColor = true;
            // 
            // radioSoccer
            // 
            this.radioSoccer.AutoSize = true;
            this.radioSoccer.Location = new System.Drawing.Point(7, 120);
            this.radioSoccer.Name = "radioSoccer";
            this.radioSoccer.Size = new System.Drawing.Size(59, 17);
            this.radioSoccer.TabIndex = 4;
            this.radioSoccer.TabStop = true;
            this.radioSoccer.Text = "Soccer";
            this.radioSoccer.UseVisualStyleBackColor = true;
            // 
            // radioHockey
            // 
            this.radioHockey.AutoSize = true;
            this.radioHockey.Location = new System.Drawing.Point(7, 95);
            this.radioHockey.Name = "radioHockey";
            this.radioHockey.Size = new System.Drawing.Size(62, 17);
            this.radioHockey.TabIndex = 3;
            this.radioHockey.TabStop = true;
            this.radioHockey.Text = "Hockey";
            this.radioHockey.UseVisualStyleBackColor = true;
            // 
            // radioFootball
            // 
            this.radioFootball.AutoSize = true;
            this.radioFootball.Location = new System.Drawing.Point(7, 70);
            this.radioFootball.Name = "radioFootball";
            this.radioFootball.Size = new System.Drawing.Size(62, 17);
            this.radioFootball.TabIndex = 2;
            this.radioFootball.TabStop = true;
            this.radioFootball.Text = "Football";
            this.radioFootball.UseVisualStyleBackColor = true;
            // 
            // radioBasketball
            // 
            this.radioBasketball.AutoSize = true;
            this.radioBasketball.Location = new System.Drawing.Point(7, 45);
            this.radioBasketball.Name = "radioBasketball";
            this.radioBasketball.Size = new System.Drawing.Size(74, 17);
            this.radioBasketball.TabIndex = 1;
            this.radioBasketball.TabStop = true;
            this.radioBasketball.Text = "Basketball";
            this.radioBasketball.UseVisualStyleBackColor = true;
            // 
            // radioBaseball
            // 
            this.radioBaseball.AutoSize = true;
            this.radioBaseball.Location = new System.Drawing.Point(7, 20);
            this.radioBaseball.Name = "radioBaseball";
            this.radioBaseball.Size = new System.Drawing.Size(65, 17);
            this.radioBaseball.TabIndex = 0;
            this.radioBaseball.TabStop = true;
            this.radioBaseball.Text = "Baseball";
            this.radioBaseball.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Choose one license from each category:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(73, 51);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(191, 20);
            this.txtLastName.TabIndex = 3;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(73, 24);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(191, 20);
            this.txtFirstName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Last Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "First Name:";
            // 
            // btnCreateAgencyAndAgent
            // 
            this.btnCreateAgencyAndAgent.Location = new System.Drawing.Point(113, 324);
            this.btnCreateAgencyAndAgent.Name = "btnCreateAgencyAndAgent";
            this.btnCreateAgencyAndAgent.Size = new System.Drawing.Size(158, 23);
            this.btnCreateAgencyAndAgent.TabIndex = 3;
            this.btnCreateAgencyAndAgent.Text = "Create Agency and Agent";
            this.btnCreateAgencyAndAgent.UseVisualStyleBackColor = true;
            this.btnCreateAgencyAndAgent.Click += new System.EventHandler(this.btnCreateAgencyAndAgent_Click);
            // 
            // StartGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 362);
            this.Controls.Add(this.btnCreateAgencyAndAgent);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtAgencyName);
            this.Controls.Add(this.label1);
            this.Name = "StartGameForm";
            this.Text = "#BuildYourDynasty";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAgencyName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioRandomIndividual;
        private System.Windows.Forms.RadioButton radioUFC;
        private System.Windows.Forms.RadioButton radioBoxing;
        private System.Windows.Forms.RadioButton radioTennis;
        private System.Windows.Forms.RadioButton radioGolf;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioRandomTeam;
        private System.Windows.Forms.RadioButton radioSoccer;
        private System.Windows.Forms.RadioButton radioHockey;
        private System.Windows.Forms.RadioButton radioFootball;
        private System.Windows.Forms.RadioButton radioBasketball;
        private System.Windows.Forms.RadioButton radioBaseball;
        private System.Windows.Forms.Button btnCreateAgencyAndAgent;
    }
}