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
using System.Xml.Linq;

namespace HotelHousekeepingApp
{
    public partial class FormRoom : Form
    {
        public FormRoom(Member mem)
        {
            InitializeComponent();
            this.member = mem;
        }
        BindingSource source;
        private Member member = new Member();
        IRoomRepository roomRepository = new RoomRepository();
        ITaskRepository taskRepository = new TaskRepository();
        private void DgvData_CellDoubleClick(object sender, EventArgs e)
        {
            FormRoomDetail formRoomDetail = new FormRoomDetail
            {
                Text = "Update Room",
                RoomInfo = GetRoomObject(),
                roomRepository = roomRepository,


            };
            if (formRoomDetail.ShowDialog() == DialogResult.OK)
            {
                source.Position = source.Count - 1;
                LoadAll();
            }
        }
        private Room GetRoomObject()
        {
            Room room = null;
            try
            {
                room = roomRepository.GetRoomByNumber(int.Parse(txtNumber.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Room");
            }
            return room;
        }
        private void LoadAll()
        {
            
                var rooms=roomRepository.GetRooms();
                try
                {
                    source = new BindingSource();
                    source.DataSource = rooms;
                   
                    txtNumber.DataBindings.Clear();
                    txtStatus.DataBindings.Clear();

                   
                    txtNumber.DataBindings.Add("Text", source, "RoomNumber");
                    txtStatus.DataBindings.Add("Text", source, "RoomStatus");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (rooms.Count() == 0)
                    {
                        ClearText();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Load room list");
                }
            }
        private void ClearText()
        {
           
            txtNumber.Text = string.Empty;
            txtStatus.Text = string.Empty;
        }

        private void FormRoom_Load(object sender, EventArgs e)
        {
            dgvData.CellDoubleClick += DgvData_CellDoubleClick;
            if (member.Role.Equals("housekeeping"))
            {
                btnSchedule.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboSearch.Text == "")
            {
                MessageBox.Show("Please choose type search", "Search Fail", MessageBoxButtons.OK);
            }
            else if (cboSearch.Text.Equals("By ID"))
            {
                LoadList("Search ID");
            }
            else if (cboSearch.Text.Equals("By Number"))
            {
                LoadList("Search Number");
            }
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
           

        }
        private void LoadList(string typelist)
        { 
            if (typelist.Equals("Filter"))
            {
                var rooms = roomRepository.GetRoomByStatus(cboFilter.Text);
                try
                {
                    source = new BindingSource();
                    source.DataSource = rooms;
                    
                    txtNumber.DataBindings.Clear();
                    txtStatus.DataBindings.Clear();

                    
                    txtNumber.DataBindings.Add("Text", source, "RoomNumber");
                    txtStatus.DataBindings.Add("Text", source, "RoomStatus");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (rooms.Count() == 0)
                    {
                        ClearText();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Load room list");
                }
            }
            else if (typelist.Equals("Search Number"))
            {
                
                try
                {
                    var room = roomRepository.GetRoomByNumber(int.Parse(txtSearch.Text));
                    source = new BindingSource();
                    source.DataSource = room;
                   
                    txtNumber.DataBindings.Clear();
                    txtStatus.DataBindings.Clear();

                    
                    txtNumber.DataBindings.Add("Text", source, "RoomNumber");
                    txtStatus.DataBindings.Add("Text", source, "RoomStatus");
                    dgvData.DataSource = null;
                    dgvData.DataSource = source;
                    if (room == null)
                    {
                        ClearText();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search room list");
                }
            }


        }

        private void btnFilter_Click_1(object sender, EventArgs e)
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

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            
                Room room = GetRoomObject();
                frmTaskDetails frmTaskDetails = new frmTaskDetails
                {
                    InsertOrUpdate = true,
                    Text = "Add a new task",                   
                    taskRepository = taskRepository,
                    RoomNumber = room.RoomNumber
                };
            if (frmTaskDetails.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Add a new task successfully", "Task Management", MessageBoxButtons.OK);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
 }

