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
    public partial class Dashboard : Form
    {
        BdayBuddyLinqDataContext bd = new BdayBuddyLinqDataContext();
        public Dashboard()
        {
            InitializeComponent();
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            UpdateStudentCount();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            //show student count realtime update
            UpdateStudentCount();
        }

        private void UpdateStudentCount()
        {
            // Get the total count of students
            int studentCount = bd.ctables.Count();

            // Update lblStatus with the count of students
            lblStatus.Text = $"{studentCount}";
        }
    }
}
