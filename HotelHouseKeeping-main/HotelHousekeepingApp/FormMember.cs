using BusinessObject;
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
    public partial class FormMember : Form
    {
        BindingSource source;
        private Member member = new Member();
        IMemberRepository memberRepository = new MemberRepository();
        public FormMember(Member mem)
        {
            InitializeComponent();
            this.member = mem;
        }
        private void DgvData_CellDoubleClick(object sender, EventArgs e)
        {
            FormMemberDetail formMemberDetail = new FormMemberDetail
            {
                Text = "Update Member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                memberRepository = memberRepository,
                rolelogin = member.Role,

            };
            if (formMemberDetail.ShowDialog() == DialogResult.OK)
            {
                source.Position = source.Count - 1;
                LoadAll();
            }
        }
        private Member GetMemberObject()
        {
            Member member = null;
            try
            {
                member = memberRepository.GetMemberByID(int.Parse(txtID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member");
            }
            return member;
        }
        

        private void LoadAll()
        {
            if (member.Role.Equals("manager"))
            {
                var members = memberRepository.GetMembers();
                try
                {
                    source = new BindingSource();
                    source.DataSource = members;
                    txtID.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    txtAddress.DataBindings.Clear();
                    txtRole.DataBindings.Clear();

                    txtID.DataBindings.Add("Text", source, "MemberID");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtName.DataBindings.Add("Text", source, "Name");
                    txtAddress.DataBindings.Add("Text", source, "Address");
                    txtPhone.DataBindings.Add("Text", source, "Phone");
                    txtRole.DataBindings.Add("Text", source, "Role");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else btnDelete.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Load member list");
                }
            }
            else
            {
                var members = memberRepository.GetMemberByEmail(member.Email);
                try
                {
                    source = new BindingSource();
                    source.DataSource = members;
                    txtID.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    txtAddress.DataBindings.Clear();
                    txtRole.DataBindings.Clear();

                    txtID.DataBindings.Add("Text", source, "MemberID");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtName.DataBindings.Add("Text", source, "Name");
                    txtAddress.DataBindings.Add("Text", source, "Address");
                    txtPhone.DataBindings.Add("Text", source, "Phone");
                    txtRole.DataBindings.Add("Text", source, "Role");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Load member list");
                }
            }

         
        }
        private void ClearText()
        {
            txtID.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtRole.Text = string.Empty;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormMemberDetail frmmemberdetail = new FormMemberDetail()
            {
                Text = "Add Member",
                InsertOrUpdate = false,
                memberRepository = memberRepository,
                rolelogin = member.Role,
            };
            if (frmmemberdetail.ShowDialog() == DialogResult.OK)
            {
                LoadAll();
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.Delete(member.MemberID);
                LoadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }
        private void FormMember_Load_1(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dgvData.CellDoubleClick += DgvData_CellDoubleClick;
            if (member.Role.Equals("manager"))
            {
            }
            else
            {
                btnSearch.Enabled = false;
                btnFilter.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboSearch.Text == "")
            {
                MessageBox.Show("Please choose type search", "Search Fail", MessageBoxButtons.OK);
            }else if(cboSearch.Text.Equals("By ID"))
            {
                LoadList("Search ID");
            }else if(cboSearch.Text.Equals("By Name"))
            {
                LoadList("Search Name");
            }
        }
        private void LoadList(string typelist)
        {
            if (typelist.Equals("Search Name")) { 
                var members = memberRepository.GetMembersByName(txtSearch.Text);
                try
                {
                    source = new BindingSource();
                    source.DataSource = members;
                    txtID.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    txtAddress.DataBindings.Clear();
                    txtRole.DataBindings.Clear();

                    txtID.DataBindings.Add("Text", source, "MemberID");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtName.DataBindings.Add("Text", source, "Name");
                    txtAddress.DataBindings.Add("Text", source, "Address");
                    txtPhone.DataBindings.Add("Text", source, "Phone");
                    txtRole.DataBindings.Add("Text", source, "Role");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else btnDelete.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search member list");
                }
            }
            else if(typelist.Equals("Filter"))
            {
                var members = memberRepository.GetMembersByRole(cboFilter.Text);
                try
                {
                    source = new BindingSource();
                    source.DataSource = members;
                    txtID.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    txtAddress.DataBindings.Clear();
                    txtRole.DataBindings.Clear();

                    txtID.DataBindings.Add("Text", source, "MemberID");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtName.DataBindings.Add("Text", source, "Name");
                    txtAddress.DataBindings.Add("Text", source, "Address");
                    txtPhone.DataBindings.Add("Text", source, "Phone");
                    txtRole.DataBindings.Add("Text", source, "Role");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else btnDelete.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Filter member list");
                }
            }
            else if(typelist.Equals("Search ID"))
            {
                
                try
                {
                    var members = memberRepository.GetMemberByID(int.Parse(txtSearch.Text));
                    source = new BindingSource();
                    source.DataSource = members;
                    txtID.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    txtAddress.DataBindings.Clear();
                    txtRole.DataBindings.Clear();

                    txtID.DataBindings.Add("Text", source, "MemberID");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtName.DataBindings.Add("Text", source, "Name");
                    txtAddress.DataBindings.Add("Text", source, "Address");
                    txtPhone.DataBindings.Add("Text", source, "Phone");
                    txtRole.DataBindings.Add("Text", source, "Role");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (members==null)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else btnDelete.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search member list");
                }
            }
            
           
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cboFilter.Text == "")
            {
                MessageBox.Show("Please choose type filter", "Filter Fail", MessageBoxButtons.OK);
            }
            else 
            {
                LoadList("Filter");
            }
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
