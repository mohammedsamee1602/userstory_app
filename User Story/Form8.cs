using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace User_Story
{
    public partial class Form8 : Form
    {
        private string connectionString;

        public Form8()
        {
            InitializeComponent();
            DatabaseHelper databaseHelper = new DatabaseHelper();
            connectionString = databaseHelper.returnConString();
        }

        private void LoadRecommendations(int userId)
        {
            List<string> recommendations = GenerateRecommendations(userId);
            listBoxRecommendations.Items.Clear(); // Clear existing items

            if (recommendations.Count > 0)
            {
                foreach (var recommendation in recommendations)
                {
                    listBoxRecommendations.Items.Add(recommendation); // Add each recommendation to the listbox
                }
            }
            else
            {
                listBoxRecommendations.Items.Add("No recommendations available."); // Display a message if no recommendations
            }
        }

        private List<string> GenerateRecommendations(int userId)
        {
            List<string> recommendations = new List<string>();
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    // Fetch user event history
                    string query = @"SELECT EventID FROM tbl_user_history WHERE UserID = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", userId);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int eventId = reader.GetInt32(0);
                            // Example logic: generate recommendation based on event
                            recommendations.Add($"Recommended activity for event {eventId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating recommendations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return recommendations;
        }

        public int GetCurrentLoggedInUserId()
        {
            int userId = 0;

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT TOP 1 UserId FROM tbl_loggedIn ORDER BY ID DESC";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving current user ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return userId;
        }

        private void btnAcceptRecommendation_Click(object sender, EventArgs e)
        {
            if (listBoxRecommendations.SelectedItem != null)
            {
                string selectedRecommendation = listBoxRecommendations.SelectedItem.ToString();
                string recommendation = listBoxRecommendations.SelectedIndex.ToString();
                string action = "Accepted"; // Set the action to "Accepted" when the recommendation is accepted
                MessageBox.Show($"You accepted: {selectedRecommendation}", "Recommendation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Log user action
                LogUserAction(recommendation, action);  // Pass both recommendation and action
            
            }


        }


        private void LogUserAction(string recommendation, string action)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO tbl_user_history (UserID, EventID, InteractionDate, InteractionType) 
                              VALUES (?, ?, ?, ?)";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", GetCurrentLoggedInUserId());  // UserId
                        cmd.Parameters.AddWithValue("?", recommendation);              // InteractionType (recommendation description)
                        cmd.Parameters.AddWithValue("?", DateTime.Now.ToShortDateString());// InteractionDate
                        cmd.Parameters.AddWithValue("?", action);                      // Action Type (e.g., "Accepted", "Viewed")
                        cmd.ExecuteNonQuery();
                    }
                }
                PopulateUserHistoryGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error logging action: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PopulateRecommendationsGrid()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Define the query to fetch data from the tbl_recommendations
                    string query = "SELECT RecommendationID, RecommendationText, CreatedDate, IsActive FROM tbl_recommendations";

                    // Create a DataAdapter to fetch data
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conn);

                    // Create a DataTable to hold the data
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the fetched data
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dtRecommendation.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating recommendations grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateUserHistoryGrid()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Define the query to fetch data from tbl_user_history
                    string query = "SELECT HistoryID, UserID, EventID, InteractionDate, InteractionType FROM tbl_user_history";

                    // Create a DataAdapter to fetch data
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conn);

                    // Create a DataTable to hold the data
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the fetched data
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dtActivity.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating user history grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form8_Load(object sender, EventArgs e)
        {
            LoadRecommendations(GetCurrentLoggedInUserId());
            PopulateRecommendationsGrid();
            PopulateUserHistoryGrid();
        }
    }
}
