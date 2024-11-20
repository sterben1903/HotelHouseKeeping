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
    public partial class FormTask : Form
    {
        BindingSource source;
        private RoomTask task = new RoomTask();
        ITaskRepository taskRepository = new TaskRepository();
        public Member member { get; set; }
        public FormTask(Member member)
        {
            InitializeComponent();
            this.member = member;
        }

        private void FormTask_Load(object sender, EventArgs e)
        {
            if (member.Role.Equals("housekeeper"))
            {
                btnCreate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnCreate.Enabled = true;
                btnDelete.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadTaskList()
        {
            var tasks = taskRepository.getTasks();
            try
            {
                if (member.Role.Equals("housekeeper"))
                {
                    tasks = taskRepository.getTasksByMemberID(member.MemberID);
                    
                }
                source = new BindingSource();
                source.DataSource = tasks;
                txtTaskID.DataBindings.Clear();
                txtRoomNumber.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtDateCreate.DataBindings.Clear();
                txtTaskStatus.DataBindings.Clear();
                txtMemberID.DataBindings.Clear();

                txtTaskID.DataBindings.Add("Text", source, "TaskID");
                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtDateCreate.DataBindings.Add("Text", source, "DateCreate");
                txtTaskStatus.DataBindings.Add("Text", source, "TaskStatus");
                txtRoomNumber.DataBindings.Add("Text", source, "RoomNumber");
                dgvData.DataSource = tasks;
                dgvData.DataSource = source;
                if (tasks.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else if (member.Role.Equals("manager")) { 
                    btnDelete.Enabled = true;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load task list");
            }
        }
        public void ClearText()
        {
            txtRoomNumber.Text = String.Empty;
            txtTaskID.Text = String.Empty;
            txtMemberID.Text = String.Empty;
            txtMemberName.Text = String.Empty;
            txtTaskStatus.Text = String.Empty;
            txtDateCreate.Text = String.Empty;
        }
        public RoomTask GetTaskObject()
        {
            RoomTask task = null;
            try
            {
                task = taskRepository.getTaskByID(int.Parse(txtTaskID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Task");
            }
            return task;
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmTaskDetails frmTaskDetails = new frmTaskDetails
            {
                Text = "Update a task",
                InsertOrUpdate = false,
                taskInfo = GetTaskObject(),
                taskRepository = taskRepository,

            };
            if(frmTaskDetails.ShowDialog() == DialogResult.OK)
            {
                LoadTaskList();
                source.Position = source.Position - 1;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTaskList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d;
                d = MessageBox.Show("Do you really want to delete it?", "Task Management - Delete Task", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(d == DialogResult.OK)
                {
                    var task = GetTaskObject();
                    taskRepository.delete(task.TaskID);
                }
                LoadTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmTaskDetails frmTaskDetails = new frmTaskDetails
            {
                InsertOrUpdate = true,
                Text = "Add a new task",
                taskRepository = taskRepository
            };
            if(frmTaskDetails.ShowDialog() == DialogResult.OK)
            {
                LoadTaskList();
                source.Position = source.Position - 1;
            }
        }
    }
}
