using BusinessObject;
using DataAccessing.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelHousekeepingApp
{
    public partial class FormMemberDetail : Form
    {
        public string rolelogin;
        private bool CheckRole()
        {
            if (rolelogin.Equals("manager")) { return true; }
            return false;
        }
        public FormMemberDetail()
        {
            InitializeComponent();
        }
        public IMemberRepository memberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public Member MemberInfo { get; set; }
        private void FormMemberDetail_Load(object sender, EventArgs e)
        {
            if (CheckRole() == false) { cboRole.Enabled = false; }
            if (InsertOrUpdate == true)
            {
                txtID.Text = MemberInfo.MemberID.ToString();
                txtID.Visible = false;
                txtEmail.Text = MemberInfo.Email;
                txtPassword.Text = MemberInfo.Password;
                txtConfirmPass.Text = MemberInfo.Password;
                txtName.Text = MemberInfo.Name;
                txtAddress.Text = MemberInfo.Address;
                txtPhone.Text = MemberInfo.Phone;
                cboRole.Text = MemberInfo.Role;
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        public bool isValidEmail()
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(txtEmail.Text))
                return (true);
            else
                return (false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(txtConfirmPass.Text))
            {
                if (txtPhone.Text.Length == 10)
                {
                    if (isValidEmail())
                    {
                        try
                        {
                            string tempphone;
                            if (txtPhone.Text == "")
                            {
                                tempphone = MemberInfo.Phone;
                            }
                            else tempphone = txtPhone.Text;
                            var member = new Member
                            {
                                MemberID = int.Parse(txtID.Text),
                                Email = txtEmail.Text,
                                Password = txtPassword.Text,
                                Name = txtName.Text,
                                Address = txtAddress.Text,
                                Phone = tempphone,
                                Role = cboRole.Text
                            };
                            if (InsertOrUpdate == false)
                            {
                                memberRepository.Insert(member);
                                MessageBox.Show("Add successful", "Add Member");
                            }
                            else
                            {
                                memberRepository.Update(member);
                                MessageBox.Show("Update successful", "Update Member");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add new member" : "Update member");
                        }
                    }
                    else MessageBox.Show("Invalid Email, Please try again", "Save Fail", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Invalid Phone, Please try again", "Save Fail", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Password are different, Please try again", "Save Fail", MessageBoxButtons.OK);

        }
        private bool IsNumber(string val)
        {
            if (val != "")
                return Regex.IsMatch(val, @"^[0-9]\d*\.?[0]*$");
            else return true;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (IsNumber(txtPhone.Text) != true)
            {
                MessageBox.Show("Member Phone wrong format,Please try again", "Update Fail");
                txtPhone.Text = "";
            }
        }
    }
}
