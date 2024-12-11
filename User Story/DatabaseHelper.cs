using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace User_Story
{

    public class Member
    {
        public string MemberID { get; set; }
        public string FullName { get; set; }
        public string MembershipType { get; set; }
        public List<string> Interests { get; set; }
    }

    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            string customdataSource = "D:\\Development\\ruberx\\source\\User_Story\\userstory_app\\User Story\\Resource\\UserStoriesDB.accdb";
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + customdataSource;
        }


        public List<Member> SearchMembers(string criteria, string filter)
        {
            List<Member> members = new List<Member>();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "";

                switch (filter.ToLower())
                {
                    case "name":
                        query = "SELECT * FROM tbl_members WHERE fullName LIKE @Criteria";
                        break;
                    case "membership type":
                        query = "SELECT * FROM tbl_members WHERE membershipType LIKE @Criteria";
                        break;
                    case "interests":
                        query = "SELECT * FROM tbl_members WHERE interests LIKE @Criteria";
                        break;
                    default:
                        throw new ArgumentException("Invalid filter");
                }

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Criteria", "%" + criteria + "%");

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            members.Add(new Member
                            {
                                MemberID = reader["memberID"].ToString(),
                                FullName = reader["fullName"].ToString(),
                                MembershipType = reader["membershipType"].ToString(),
                                Interests = reader["interests"].ToString().Split(',').ToList()
                            });
                        }
                    }
                }
            }

            return members;
        }


        public bool RegisterUser(string username, string email, string passwordHash)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tbl_users (Username, Useremail, Userpassword) VALUES (@Username, @Email, @Password)";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", passwordHash);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (OleDbException ex)
                    {
                        MessageBox.Show(ex.Message);
                        if (ex.Message.Contains("duplicate value"))
                            throw new Exception("Username or Email already exists.");
                        throw;
                    }
                }
            }
        }

        public bool LoginUser(string email, string passwordHash)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM tbl_users WHERE Useremail=@Email AND Userpassword=@Password";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", passwordHash);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool Login(string email, string password)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Retrieve the hashed password from the database
                string query = "SELECT Password FROM tbl_users WHERE Useremail = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    string storedHash = cmd.ExecuteScalar()?.ToString();
                    if (string.IsNullOrEmpty(storedHash))
                    {
                        return false; // Email not found
                    }

                    // Compare the stored hash with the entered password
                    return HashingHelper.VerifyPassword(password, storedHash);
                }
            }
        }


        // Step 1: Send Reset Token
        public bool SendResetToken(string email)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Check if the email exists
                string query = "SELECT COUNT(1) FROM tbl_users WHERE Useremail = @Email";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        return false; // Email not found
                    }
                }



                // Generate token
                string token = GenerateResetToken();
                DateTime expiry = DateTime.Now.AddMinutes(30); // Token valid for 30 minutes

                // Store token and expiration in the database
                query = "UPDATE tbl_users SET ResetToken = @Token, ResetTokenExpiry = @Expiry WHERE Useremail = @Email";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@Expiry", expiry);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }

                // Save token to a .txt file
                try
                {
                    string filePath = "ResetTokenLog.txt";
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, true))
                    {
                        writer.WriteLine($"Email: {email}, Token: {token}, Generated At: {DateTime.Now}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to log token to file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                // Send the token via email
                string subject = "Password Reset Token";
                string body = $"Your password reset token is: {token}\n\nThis token is valid for 30 minutes.";
                EmailHelper.SendEmail(email, subject, body);
                return true;
            }
        }


        // Step 2: Validate Token
        public bool ValidateResetToken(string token)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(1) FROM tbl_users WHERE ResetToken = @Token AND ResetTokenExpiry > @Now";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@Now", DateTime.Now);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Step 3: Reset Password
        public bool ResetPassword(string resetToken, string hashedPassword)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Check if the token is valid and not expired
                string query = "SELECT COUNT(1) FROM tbl_users WHERE ResetToken = ? AND ResetTokenExpiry > ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", resetToken);
                    cmd.Parameters.AddWithValue("@Expiry", DateTime.Now);

                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        return false; // Token invalid or expired
                    }
                }

                // Update the password and clear the reset token
                query = "UPDATE tbl_users SET Userpassword = ?, ResetToken = NULL, ResetTokenExpiry = NULL WHERE ResetToken = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Token", resetToken);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }


        // Helper method to generate a random reset token
        private string GenerateResetToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
