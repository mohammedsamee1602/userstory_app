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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void BtnValidateToken_Click(object sender, EventArgs e)
        {
            string token = txtToken.Text.Trim(); 

            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Please enter the reset token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                bool isValid = dbHelper.ValidateResetToken(token);

                if (isValid)
                {
                    MessageBox.Show("Token is valid. You may now reset your password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    Form5 form5 = new Form5(token); // Pass the token to Form6
                    form5.Show(); // Open Form5 for password renewal
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid or expired token. Please request a new one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
