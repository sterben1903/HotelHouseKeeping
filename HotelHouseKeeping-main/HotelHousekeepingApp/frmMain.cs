using BusinessObject;
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
    public partial class frmMain : Form
    {
        private Form activeForm;
        private Member member = new Member();
        public frmMain(Member mem)
        {
            InitializeComponent();
            this.member = mem;
        }
        private void OpenChildForm(Form childForm,object btnsender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle= FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbhome.Text = childForm.Text;
        }
        private void btnMember_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormMember(member),sender);
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTask(member), sender);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormRoom(member), sender);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormService(), sender);
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

       private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* DialogResult d;
            d = MessageBox.Show("Do you really want to exit?", "Sales Management",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (d == DialogResult.Cancel)
            {
                e.Cancel = true;
            }*/
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin fl = new frmLogin();
            fl.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
