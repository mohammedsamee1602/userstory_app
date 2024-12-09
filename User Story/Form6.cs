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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string criteria = txtSearchCriteria.Text.Trim();
            string filter = cmbFilter.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(criteria) || string.IsNullOrEmpty(filter))
            {
                MessageBox.Show("Please enter search criteria and select a filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                List<Member> members = dbHelper.SearchMembers(criteria, filter);

                if (members.Count == 0)
                {
                    MessageBox.Show("No matching members found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridViewResults.DataSource = members;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
