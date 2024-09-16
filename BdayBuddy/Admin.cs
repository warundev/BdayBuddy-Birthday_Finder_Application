using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BdayBuddy
{
    public partial class Admin : Form
    {
        BdayBuddyLinqDataContext bd = new BdayBuddyLinqDataContext();
        public Admin()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            dataGridViewAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Close();
            mainForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ctable ctable = new ctable();
            ctable.IndexNumber = txtIndexNumber.Text;
            ctable.name = txtStName.Text;
            ctable.dateOfBirth = dateTimePicker1.Value;

            bd.ctables.InsertOnSubmit(ctable);
            bd.SubmitChanges();
            MessageBox.Show("Inserting Successfully","Successful",MessageBoxButtons.OK, MessageBoxIcon.Information);

            //List<ctable> show = bd.ctables.All();
            LoadData();




        }
        private void LoadData()
        {
            var data = from c in bd.ctables
                       select new
                       {
                           ID = c.IndexNumber,
                           Name = c.name,
                           DateOfBirth = c.dateOfBirth
                       };

            dataGridViewAdmin.DataSource = data.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ctable> allRec = bd.ctables.ToList();
            dataGridViewAdmin.DataSource= allRec;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIndexNumber.Text= string.Empty;
            txtStName.Text= string.Empty;
            dateTimePicker1.Text= string.Empty;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string indexNumberToSearch = txtIndexNumber.Text.Trim();

            if (string.IsNullOrEmpty(indexNumberToSearch))
            {
                MessageBox.Show("Please enter an Index Number to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Search for the row with the matching IndexNumber
            DataGridViewRow foundRow = null;
            foreach (DataGridViewRow row in dataGridViewAdmin.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == indexNumberToSearch)
                {
                    foundRow = row;
                    break;
                }
            }

            if (foundRow != null)
            {
                // Select the row
                foundRow.Selected = true;

                // Proceed to delete the record
                var recToDelete = bd.ctables.FirstOrDefault(t => t.IndexNumber == indexNumberToSearch);

                if (recToDelete != null)
                {
                    try
                    {
                        bd.ctables.DeleteOnSubmit(recToDelete);
                        bd.SubmitChanges();

                        List<ctable> showtb = bd.ctables.ToList();
                        dataGridViewAdmin.DataSource = showtb;
                        MessageBox.Show("Record Deleted Successfully..!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Record not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Index Number not found in the DataGridView.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string idToUpdate = txtIndexNumber.Text;

            var recToUpdate = bd.ctables.SingleOrDefault(t => t.IndexNumber == idToUpdate);

            if (recToUpdate != null)
            {
                try
                {
                    bd.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, recToUpdate);

                    recToUpdate.name = txtStName.Text;
                    recToUpdate.dateOfBirth = dateTimePicker1.Value;

                    bd.SubmitChanges();
                    LoadData();

                    MessageBox.Show("Record Updated Successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Data.Linq.ChangeConflictException ex)
                {
                    MessageBox.Show("Unable to update record. It may have been modified or deleted by another user.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Record not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //seach Funtion
            // Get the IndexNumber from the search text box (assuming you have a text box for input)
            string searchIndexNumber = txtIndexNumber.Text;

            if (!string.IsNullOrEmpty(searchIndexNumber))
            {
                // Perform the search based on IndexNumber
                var searchResult = from c in bd.ctables
                                   where c.IndexNumber == searchIndexNumber
                                   select new
                                   {
                                       ID = c.IndexNumber,
                                       Name = c.name,
                                       DateOfBirth = c.dateOfBirth
                                   };

                // Check if any results were found
                if (searchResult.Any())
                {
                    dataGridViewAdmin.DataSource = searchResult.ToList();
                }
                else
                {
                    MessageBox.Show("No record found with the given Index Number!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter an Index Number to search!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
