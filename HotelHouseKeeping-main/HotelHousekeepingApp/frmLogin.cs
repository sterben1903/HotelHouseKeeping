using DataAccessing.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelHousekeepingApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        IMemberRepository memberRepository = new MemberRepository();
   

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            var member = memberRepository.GetMemberByEmail(txtEmail.Text);
            if (member == null)
            {
                MessageBox.Show("Login fail, Please try again", "Login Status", MessageBoxButtons.OK);
            }
            else if (member != null && txtPassword.Text.Equals(member.Password))
            {
                if(member.Role.Equals("manager"))
                MessageBox.Show("Login Success - Manager", "Login Status", MessageBoxButtons.OK);
                else if(member.Role.Equals("housekeeper"))
                    MessageBox.Show("Login Success - HouseKeeper", "Login Status", MessageBoxButtons.OK);


                frmMain main = new frmMain(member);
                this.Hide();
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show("Login fail, Please try again", "Login Status", MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
