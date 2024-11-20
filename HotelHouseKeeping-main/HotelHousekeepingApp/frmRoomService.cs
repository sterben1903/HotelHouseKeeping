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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace HotelHousekeepingApp
{
    public partial class frmRoomService : Form
    {
        BindingSource source = null;
        ServiceDetail serviceDetail = new ServiceDetail();
        IRoomServiceRepository roomServiceRepository = new RoomServiceRepository();
        IRoomRepository roomRepository = new RoomRepository();
        IServiceRepository serviceRepository = new ServiceRepository();
        bool isFilter = false;
        public frmRoomService()
        {
            InitializeComponent();
        }

        private void frmRoomService_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            var listRoomNumber = roomRepository.GetRoomNumbers();
            foreach(var roomNumber in listRoomNumber)
            {
                cboRoomNumber.Items.Add(roomNumber.ToString());
            }
            
        }
        private void LoadRoomServiceList()
        {
            var roomServices = roomServiceRepository.GetRoomServices();
            try
            {
                if (isFilter)
                {
                    roomServices = roomServiceRepository.GetRoomServicesByRoomNumber(int.Parse(cboRoomNumber.Text));
                    if (roomServices.Count() != 0)
                        txtTotal.Text = roomServiceRepository.GetTotal(int.Parse(cboRoomNumber.Text)).ToString();
                    else
                        txtTotal.Text = String.Empty;
                    isFilter = false;
                }
                source = new BindingSource();
                source.DataSource = roomServices;
                dgvData.DataSource = roomServices;
                dgvData.DataSource = source;
                 
               if (roomServices.Count() == 0)
                {
                    btnDelete.Enabled = false;
                }
                else btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load task list");
            }
        }
        
        public ServiceDetail GetRoomServiceObject()
        {
            ServiceDetail serviceDetail = null;
            try
            {
                DataGridViewRow row = dgvData.CurrentRow;
                string id = row.Cells[0].Value.ToString();
                serviceDetail = roomServiceRepository.GetRoomServiceByServiceDetailID(int.Parse(id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Room Service");
            }
            return serviceDetail;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRoomServiceList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d;
                d = MessageBox.Show("Do you really want to delete it?", "Room Service Management - Delete RoomService", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (d == DialogResult.OK)
                {
                    var roomService = GetRoomServiceObject();
                    Service service = serviceRepository.getServiceByID(roomService.ServiceID);
                    service.Quantity += roomService.Quantity;
                    serviceRepository.update(service);
                    roomServiceRepository.delete(roomService.ServiceDetailID);
                }
                isFilter = true;
                LoadRoomServiceList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            frmRoomServiceDetail frmRoomServiceDetail = new frmRoomServiceDetail
            {
                Text = "Add a new service detail",
                InsertOrUpdate = true,
                
                selectedRoomNumber = int.Parse(cboRoomNumber.Text)
            };
            if(frmRoomServiceDetail.ShowDialog() == DialogResult.OK)
            {
                isFilter = true;
                LoadRoomServiceList();
                source.Position = source.Position - 1;
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmRoomServiceDetail frmRoomServiceDetail = new frmRoomServiceDetail
            {
                Text = "Update a service Detail",
                InsertOrUpdate = false,
                serviceDetailInfo = GetRoomServiceObject()
            };
            if(frmRoomServiceDetail.ShowDialog() == DialogResult.OK)
            {
                isFilter=true;
                LoadRoomServiceList();
                source.Position = source.Position - 1;
            }
        }

        

        private void cboRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            isFilter = true;
            LoadRoomServiceList();
        }

        private void btnGetBill_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d;
                d = MessageBox.Show("The total of this room is :" + roomServiceRepository.GetTotal(int.Parse(cboRoomNumber.Text))+"\n Are you sure to get bill?","RoomServiceManagement - Get bill",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(d == DialogResult.OK)
                {
                    roomServiceRepository.GetBill(int.Parse(cboRoomNumber.Text));
                    txtTotal.Text = String.Empty;
                }
                isFilter = true;
                LoadRoomServiceList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
