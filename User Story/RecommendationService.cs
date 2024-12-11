using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace User_Story
{
    public class RecommendationService
    {
        private string connectionString;

        public RecommendationService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GenerateRecommendations(int userId)
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
    }

}
