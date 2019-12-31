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
            this.btnPlayingTime = new System.Windows.Forms.Button();
            this.btnDemandTrade = new System.Windows.Forms.Button();
            this.btnPersonalRequest = new System.Windows.Forms.Button();
            this.btnContract = new System.Windows.Forms.Button();
            this.btnTeammateIssue = new System.Windows.Forms.Button();
            this.lblTitleContender = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGMTalk
            // 
            this.lblGMTalk.AutoSize = true;
            this.lblGMTalk.Location = new System.Drawing.Point(26, 108);
            this.lblGMTalk.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblGMTalk.Name = "lblGMTalk";
            this.lblGMTalk.Size = new System.Drawing.Size(70, 25);
            this.lblGMTalk.TabIndex = 0;
            this.lblGMTalk.Text = "label1";
            // 
            // btnPlayingTime
            // 
            this.btnPlayingTime.Location = new System.Drawing.Point(31, 226);
            this.btnPlayingTime.Name = "btnPlayingTime";
            this.btnPlayingTime.Size = new System.Drawing.Size(197, 47);
            this.btnPlayingTime.TabIndex = 1;
            this.btnPlayingTime.Text = "Playing Time";
            this.btnPlayingTime.UseVisualStyleBackColor = true;
            this.btnPlayingTime.Click += new System.EventHandler(this.BtnPlayingTime_Click);
            // 
            // btnDemandTrade
            // 
            this.btnDemandTrade.Location = new System.Drawing.Point(31, 438);
            this.btnDemandTrade.Name = "btnDemandTrade";
            this.btnDemandTrade.Size = new System.Drawing.Size(197, 47);
            this.btnDemandTrade.TabIndex = 2;
            this.btnDemandTrade.Text = "Demand Trade";
            this.btnDemandTrade.UseVisualStyleBackColor = true;
            // 
            // btnPersonalRequest
            // 
            this.btnPersonalRequest.Location = new System.Drawing.Point(31, 385);
            this.btnPersonalRequest.Name = "btnPersonalRequest";
            this.btnPersonalRequest.Size = new System.Drawing.Size(197, 47);
            this.btnPersonalRequest.TabIndex = 3;
            this.btnPersonalRequest.Text = "Personal Request";
            this.btnPersonalRequest.UseVisualStyleBackColor = true;
            // 
            // btnContract
            // 
            this.btnContract.Location = new System.Drawing.Point(31, 332);
            this.btnContract.Name = "btnContract";
            this.btnContract.Size = new System.Drawing.Size(197, 47);
            this.btnContract.TabIndex = 4;
            this.btnContract.Text = "Contract";
            this.btnContract.UseVisualStyleBackColor = true;
            // 
            // btnTeammateIssue
            // 
            this.btnTeammateIssue.Location = new System.Drawing.Point(31, 279);
            this.btnTeammateIssue.Name = "btnTeammateIssue";
            this.btnTeammateIssue.Size = new System.Drawing.Size(197, 47);
            this.btnTeammateIssue.TabIndex = 5;
            this.btnTeammateIssue.Text = "Teammate Issue";
            this.btnTeammateIssue.UseVisualStyleBackColor = true;
            // 
            // lblTitleContender
            // 
            this.lblTitleContender.AutoSize = true;
            this.lblTitleContender.Location = new System.Drawing.Point(31, 13);
            this.lblTitleContender.Name = "lblTitleContender";
            this.lblTitleContender.Size = new System.Drawing.Size(165, 25);
            this.lblTitleContender.TabIndex = 6;
            this.lblTitleContender.Text = "Title Contender:";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(299, 13);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(93, 25);
            this.lblRecord.TabIndex = 7;
            this.lblRecord.Text = "Record: ";
            // 
            // CallTeamGMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.lblTitleContender);
            this.Controls.Add(this.btnTeammateIssue);
            this.Controls.Add(this.btnContract);
            this.Controls.Add(this.btnPersonalRequest);
            this.Controls.Add(this.btnDemandTrade);
            this.Controls.Add(this.btnPlayingTime);
            this.Controls.Add(this.lblGMTalk);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "CallTeamGMForm";
            this.Text = "Call Team GM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGMTalk;
        private System.Windows.Forms.Button btnPlayingTime;
        private System.Windows.Forms.Button btnDemandTrade;
        private System.Windows.Forms.Button btnPersonalRequest;
        private System.Windows.Forms.Button btnContract;
        private System.Windows.Forms.Button btnTeammateIssue;
        private System.Windows.Forms.Label lblTitleContender;
        private System.Windows.Forms.Label lblRecord;
    }
}