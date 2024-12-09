﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Story
{
    public partial class Form5 : Form
    {
        private string resetToken;
        public Form5(string token)
        {
            InitializeComponent();
            resetToken = token;
        }

        private void BtnRenewPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim(); // Assuming there's a TextBox named txtNewPassword
            string confirmPassword = txtConfirmPassword.Text.Trim(); // Assuming there's a TextBox named txtConfirmPassword

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter and confirm your new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                bool isPasswordReset = dbHelper.ResetPassword(resetToken, newPassword);

                if (isPasswordReset)
                {
                    MessageBox.Show("Your password has been successfully reset. You can now log in with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close Form6 after successful password reset
                }
                else
                {
                    MessageBox.Show("Failed to reset password. The token may be invalid or expired.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}