using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Story
{
    public partial class Form7 : Form
    {
        private string connectionString;
        public Form7()
        {
            InitializeComponent();
                  
            DatabaseHelper databaseHelper = new DatabaseHelper();
            connectionString = databaseHelper.returnConString();
            
        }

        private void LoadEvents()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT EventName FROM tbl_events";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbEvent.Items.Add(reader["EventName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading events: " + ex.Message);
            }
        }


        private void btnViewAttendance_Click(object sender, EventArgs e)
        {
            string selectedEvent = cmbEvent.SelectedItem?.ToString();
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;


            MessageBox.Show(fromDate.ToString());
            MessageBox.Show(toDate.ToString());

            if (string.IsNullOrEmpty(selectedEvent))
            {
                MessageBox.Show("Please select an event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT UserName, UserEmail, AttendanceDate 
                             FROM tbl_attendance a 
                             INNER JOIN tbl_events e ON a.EventID = e.EventID 
                             WHERE e.EventName = @EventName AND AttendanceDate BETWEEN @FromDate AND @ToDate";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EventName", selectedEvent);
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);

                        DataTable dt = new DataTable();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);

                        dataGridViewAttendance.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving attendance: " + ex.Message);
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            if (dataGridViewAttendance.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Save Attendance Report"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Write header
                        foreach (DataGridViewColumn column in dataGridViewAttendance.Columns)
                        {
                            sw.Write(column.HeaderText + ",");
                        }
                        sw.WriteLine();

                        // Write rows
                        foreach (DataGridViewRow row in dataGridViewAttendance.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                sw.Write(cell.Value?.ToString() + ",");
                            }
                            sw.WriteLine();
                        }
                    }
                    MessageBox.Show("Data exported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting data: " + ex.Message);
                }
            }
        }

    }
}
