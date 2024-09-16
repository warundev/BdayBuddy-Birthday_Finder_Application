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
    public partial class LoginForm : Form
    {
        BdayBuddyLinqDataContext Bd = new BdayBuddyLinqDataContext();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SignTable signTable = new SignTable();
            string name = txtUserName.Text;
            string password = txtPassword.Text;

            var user = Bd.SignTables.Where(u => u.name == txtUserName.Text && u.password == txtPassword.Text);

            if(user!= null)
            {
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.Show();
                
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void lblSignUp_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.Show();
            this.Hide();
        }
    }
}
