using System;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string passwordHash = HashingHelper.HashPassword(password);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email and Password are required.");
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            int userId = db.GetUserIdIfValid(email, passwordHash); // New method to validate and get UserId

            if (userId > 0)
            {
                // Save the UserId to tbl_loggedIn
                if (db.SaveLoggedInUser(userId))
                {
                    MessageBox.Show("Login successful!");

                    // Navigate to the next form
                    Form6 searchPage = new Form6();
                    this.Hide();
                    searchPage.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error saving login session. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }


        private void btnSignup_Click(object sender, EventArgs e)
        {
            Form1 signup = new Form1();
            this.Hide();
            signup.ShowDialog();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Form3 resetpass = new Form3();
            this.Hide();
            resetpass.ShowDialog();
        }
    }
}
