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
    public partial class User : Form
    {
        BdayBuddyLinqDataContext bd = new BdayBuddyLinqDataContext();
        public User()
        {
            InitializeComponent();
            // Set the DateTimePicker to display only month and day
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM dd"; // Example: September 01
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Close();
            mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Search date  and display all data to datagridview 
            // Get the selected month and day from the DateTimePicker
            int selectedMonth = dateTimePicker1.Value.Month;
            int selectedDay = dateTimePicker1.Value.Day;

            // Query the database to find records that match the selected month and day
            var searchData = from c in bd.ctables
                             where c.dateOfBirth.Month == selectedMonth && c.dateOfBirth.Day == selectedDay
                             select new
                             {
                                 ID = c.IndexNumber,
                                 Name = c.name,
                                 DateOfBirth = c.dateOfBirth
                             };

            // Bind the search results to the DataGridView
            dataGridViewUser.DataSource = searchData.ToList();

            // If no records are found, show a message
            if (!searchData.Any())
            {
                MessageBox.Show("No records found for the selected month and day.", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    }

