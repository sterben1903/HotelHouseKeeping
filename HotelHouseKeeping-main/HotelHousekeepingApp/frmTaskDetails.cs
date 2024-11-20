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
    public partial class frmTaskDetails : Form
    {
        public bool InsertOrUpdate { get; set; }
        public ITaskRepository taskRepository { get; set; }
        IMemberRepository memberRepository = new MemberRepository(); 
        public RoomTask taskInfo { get; set; }

        public int RoomNumber { get; set; }
        public frmTaskDetails()
        {
            InitializeComponent();
        }

        private void frmTaskDetails_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate)
            {
                lbTaskIDTag.Visible = false;
                cboTaskStatus.SelectedIndex = 0;
                cboTaskStatus.Enabled = false;
                if(RoomNumber != 0)
                {
                    txtRoomNumber.Text = RoomNumber.ToString();
                    txtRoomNumber.Enabled = false;
                    RoomNumber = 0;
                }
            }
            else
            {
                lbTaskID.Text = taskInfo.TaskID.ToString();
                txtMemberID.Text =  taskInfo.MemberID.ToString();
                txtMemberName.Text = taskInfo.MemberName.ToString();
                dtpDateCreate.Text = taskInfo.DateCreate.ToString();
                txtRoomNumber.Text = taskInfo.RoomNumber.ToString();
                cboTaskStatus.Text = taskInfo.TaskStatus.ToString();
                txtMemberID.Enabled = false;
                txtMemberName.Enabled = false;
                dtpDateCreate.Enabled = false;
                txtRoomNumber.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (InsertOrUpdate)
                {
                    var task = new RoomTask
                    {

                        MemberID = int.Parse(txtMemberID.Text),
                        MemberName = txtMemberName.Text,
                        DateCreate = DateTime.Parse(dtpDateCreate.Text),
                        RoomNumber = int.Parse(txtRoomNumber.Text),
                        TaskStatus = cboTaskStatus.Text
                    };
                    taskRepository.insert(task);
                }
                else
                {
                    var task = new RoomTask
                    {
                        TaskID = int.Parse(lbTaskID.Text),
                        MemberID = int.Parse(txtMemberID.Text),
                        MemberName = txtMemberName.Text,
                        DateCreate = DateTime.Parse(dtpDateCreate.Text),
                        RoomNumber = int.Parse(txtRoomNumber.Text),
                        TaskStatus = cboTaskStatus.Text
                    };
                    taskRepository.update(task);
                }
                
            }catch(Exception ex)
                {
                MessageBox.Show(ex.Message, InsertOrUpdate == true ? "Add new task" : "Update a task");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRoomID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Member member = memberRepository.GetMemberByID(int.Parse(txtMemberID.Text));
                if (member == null)
                {
                    MessageBox.Show("Member ID is not existed!", "Invalid infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    txtMemberName.Text = member.Name;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
