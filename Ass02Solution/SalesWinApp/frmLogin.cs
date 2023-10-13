using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmLogin : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            MemberObject loginMember = memberRepository.Login(Email, Password);
            bool isAdmin = memberRepository.IsAdmin(Email, Password);
            if (loginMember != null)
            {
                MessageBox.Show("Login successfully", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain frmMain = null;
                if (isAdmin)
                {
                    frmMain = new frmMain
                    {
                        loginMember = loginMember
                    };
                    frmMain.Closed += (s, args) => this.Close();
                    this.Hide();
                    frmMain.Show();
                }
                else
                {

                }
            }
            else
            {
                if (MessageBox.Show("Login failed!!", "Login", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
