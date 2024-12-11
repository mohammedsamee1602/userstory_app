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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                bool isTokenSent = dbHelper.SendResetToken(email);


                if (isTokenSent)
                {
                    MessageBox.Show("A reset token has been sent to your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form4 validateToken = new Form4();
                    this.Hide();
                    validateToken.ShowDialog();

                    Form4 validateTokenPage = new Form4();
                    this.Hide();
                    validateTokenPage.ShowDialog();
                  


                }
                else
                {
                    MessageBox.Show("The email address is not registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Form2 loginPage = new Form2();
                    this.Hide();
                    loginPage.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Form4 validateTokenPage = new Form4();
                this.Hide();
                validateTokenPage.ShowDialog();
            }
        }

    }
}
