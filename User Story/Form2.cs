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
            if (db.LoginUser(email, passwordHash))
            {
                MessageBox.Show("Login successful!");
                // Proceed to the main application
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }
    }
}
