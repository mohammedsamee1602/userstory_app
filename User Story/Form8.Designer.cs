namespace User_Story
{
    partial class Form8
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
            this.listBoxRecommendations = new System.Windows.Forms.ListBox();
            this.btnAcceptRecommendation = new System.Windows.Forms.Button();
            this.Recommendation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtActivity = new System.Windows.Forms.DataGridView();
            this.dtRecommendation = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRecommendation)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxRecommendations
            // 
            this.listBoxRecommendations.FormattingEnabled = true;
            this.listBoxRecommendations.Location = new System.Drawing.Point(41, 62);
            this.listBoxRecommendations.Name = "listBoxRecommendations";
            this.listBoxRecommendations.Size = new System.Drawing.Size(263, 147);
            this.listBoxRecommendations.TabIndex = 0;
            // 
            // btnAcceptRecommendation
            // 
            this.btnAcceptRecommendation.Location = new System.Drawing.Point(321, 124);
            this.btnAcceptRecommendation.Name = "btnAcceptRecommendation";
            this.btnAcceptRecommendation.Size = new System.Drawing.Size(263, 23);
            this.btnAcceptRecommendation.TabIndex = 1;
            this.btnAcceptRecommendation.Text = "Accept Recommendaitons";
            this.btnAcceptRecommendation.UseVisualStyleBackColor = true;
            this.btnAcceptRecommendation.Click += new System.EventHandler(this.btnAcceptRecommendation_Click);
            // 
            // Recommendation
            // 
            this.Recommendation.AutoSize = true;
            this.Recommendation.Location = new System.Drawing.Point(41, 28);
            this.Recommendation.Name = "Recommendation";
            this.Recommendation.Size = new System.Drawing.Size(90, 13);
            this.Recommendation.TabIndex = 2;
            this.Recommendation.Text = "Recommendation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Activity Data";
            // 
            // dtActivity
            // 
            this.dtActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtActivity.Location = new System.Drawing.Point(446, 254);
            this.dtActivity.Name = "dtActivity";
            this.dtActivity.Size = new System.Drawing.Size(387, 150);
            this.dtActivity.TabIndex = 4;
            // 
            // dtRecommendation
            // 
            this.dtRecommendation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtRecommendation.Location = new System.Drawing.Point(41, 254);
            this.dtRecommendation.Name = "dtRecommendation";
            this.dtRecommendation.Size = new System.Drawing.Size(387, 150);
            this.dtRecommendation.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Recommendation Data";
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtRecommendation);
            this.Controls.Add(this.dtActivity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Recommendation);
            this.Controls.Add(this.btnAcceptRecommendation);
            this.Controls.Add(this.listBoxRecommendations);
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recomendation System";
            this.Load += new System.EventHandler(this.Form8_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRecommendation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxRecommendations;
        private System.Windows.Forms.Button btnAcceptRecommendation;
        private System.Windows.Forms.Label Recommendation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtActivity;
        private System.Windows.Forms.DataGridView dtRecommendation;
        private System.Windows.Forms.Label label2;
    }
}