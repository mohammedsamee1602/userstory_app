namespace User_Story
{
    partial class Form7
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
            this.cmbEvent = new System.Windows.Forms.ComboBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewAttendance = new System.Windows.Forms.DataGridView();
            this.btnViewAttendance = new System.Windows.Forms.Button();
            this.btnExportToCSV = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbEvent
            // 
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Location = new System.Drawing.Point(50, 23);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(121, 21);
            this.cmbEvent.TabIndex = 0;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(194, 23);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // dataGridViewAttendance
            // 
            this.dataGridViewAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttendance.Location = new System.Drawing.Point(41, 73);
            this.dataGridViewAttendance.Name = "dataGridViewAttendance";
            this.dataGridViewAttendance.Size = new System.Drawing.Size(722, 253);
            this.dataGridViewAttendance.TabIndex = 2;
            // 
            // btnViewAttendance
            // 
            this.btnViewAttendance.Location = new System.Drawing.Point(644, 23);
            this.btnViewAttendance.Name = "btnViewAttendance";
            this.btnViewAttendance.Size = new System.Drawing.Size(119, 23);
            this.btnViewAttendance.TabIndex = 3;
            this.btnViewAttendance.Text = "View Attendance";
            this.btnViewAttendance.UseVisualStyleBackColor = true;
            this.btnViewAttendance.Click += new System.EventHandler(this.btnViewAttendance_Click);
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.Location = new System.Drawing.Point(315, 354);
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(200, 23);
            this.btnExportToCSV.TabIndex = 4;
            this.btnExportToCSV.Text = "Export";
            this.btnExportToCSV.UseVisualStyleBackColor = true;
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(414, 23);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 20);
            this.dtpToDate.TabIndex = 5;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 404);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.btnExportToCSV);
            this.Controls.Add(this.btnViewAttendance);
            this.Controls.Add(this.dataGridViewAttendance);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.cmbEvent);
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance";
            this.Load += new System.EventHandler(this.Form7_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttendance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEvent;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DataGridView dataGridViewAttendance;
        private System.Windows.Forms.Button btnViewAttendance;
        private System.Windows.Forms.Button btnExportToCSV;
        private System.Windows.Forms.DateTimePicker dtpToDate;
    }
}