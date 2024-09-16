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
    public partial class SignupForm : Form
    {
        BdayBuddyLinqDataContext bd = new BdayBuddyLinqDataContext();
        public SignupForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            SignTable st = new SignTable();
            st.name = txtUserName.Text;
            st.password = txtPassword.Text;

            bd.SignTables.InsertOnSubmit(st);
            bd.SubmitChanges();
            MessageBox.Show("Account created Successfully..!", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
