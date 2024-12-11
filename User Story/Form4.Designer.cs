namespace User_Story
{
    partial class Form4
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
            this.BtnValidateToken = new System.Windows.Forms.Button();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnValidateToken
            // 
            this.BtnValidateToken.Location = new System.Drawing.Point(126, 110);
            this.BtnValidateToken.Name = "BtnValidateToken";
            this.BtnValidateToken.Size = new System.Drawing.Size(155, 23);
            this.BtnValidateToken.TabIndex = 22;
            this.BtnValidateToken.Text = "Validate token";
            this.BtnValidateToken.UseVisualStyleBackColor = true;
            this.BtnValidateToken.Click += new System.EventHandler(this.BtnValidateToken_Click);
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(126, 25);
            this.txtToken.Multiline = true;
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(155, 36);
            this.txtToken.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Token:";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 173);
            this.Controls.Add(this.BtnValidateToken);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.label2);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnValidateToken;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label2;
    }
}