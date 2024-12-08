using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace User_Story
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            string customdataSource = "D:\\Development\\ruberx\\source\\User_Story\\userstory_app\\User Story\\Resource\\UserStoriesDB.accdb";
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + customdataSource;
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

        public bool SendResetLink(string email)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Check if the email exists
                string query = "SELECT COUNT(1) FROM tbl_users WHERE Email = @Email";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        return false; // Email not found
                    }
                }

                // Generate token and expiration
                string token = Guid.NewGuid().ToString();
                DateTime expiry = DateTime.Now.AddHours(1);

                // Store token and expiry in the database
                query = "UPDATE tbl_users SET reset_token = @Token, reset_token_expiry = @Expiry WHERE Email = @Email";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@Expiry", expiry);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }

                // Send the email (this assumes you have an EmailHelper class)
                EmailHelper.SendEmail(email, "Password Reset", $"Use the following link to reset your password: https://yourapp.com/reset?token={token}");
                return true;
            }
        }

        // Step 2: Validate Token
        public bool ValidateResetToken(string token)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(1) FROM tbl_users WHERE reset_token = @Token AND reset_token_expiry > @Now";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@Now", DateTime.Now);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Step 3: Reset Password
        public bool ResetPassword(string token, string newPassword)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Check if token is valid
                if (!ValidateResetToken(token))
                {
                    return false; // Token invalid or expired
                }

                // Hash the new password
                string passwordHash = HashingHelper.HashPassword(newPassword);

                // Update the password and clear the reset token
                string query = "UPDATE tbl_users SET Password = @Password, reset_token = NULL, reset_token_expiry = NULL WHERE reset_token = @Token";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", passwordHash);
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

    }
}
