using BusinessObject;
using DataAccessing.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace HotelHousekeepingApp
{
    public partial class FormService : Form
    {
        BindingSource source;
        private Service service = new Service();
        IServiceRepository serviceRepository = new ServiceRepository();
        private Member member = new Member();
        IMemberRepository memberRepository = new MemberRepository();
        public FormService()
        {
            InitializeComponent();
        }

        private void txtServiceID_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormService_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
        }

        private void DgvData_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadServiceList()
        {
            var services = serviceRepository.getServices(); 
            try
            {
                source = new BindingSource();
                source.DataSource = services;
                dgvData.DataSource = services;
                dgvData.DataSource = source;
                dgvData.Columns["Status"].Visible = false;
                if (services.Count() == 0)
                {
                    
                    btnDelete.Enabled = false;
                }
                else btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load service list");
            }
        }
        
        private Service GetServiceObject()
        {
            Service service = null;
            try
            {
                DataGridViewRow row = dgvData.CurrentRow;
                string id = row.Cells[0].Value.ToString();
                service = serviceRepository.getServiceByID(int.Parse(id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Service");
            }
            return service;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadServiceList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var task = GetServiceObject();
                if(!task.Name.Equals("Giat,ui quan ao"))
                {
                    DialogResult d;
                    d = MessageBox.Show("Do you really want to delete it?", "Service Management - Delete Service", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (d == DialogResult.OK)
                    {
                        serviceRepository.delete(task.ServiceID);
                    }
                    LoadServiceList();
                }
                else
                {
                    MessageBox.Show("Can not delete this service!", "Service Management - Delete Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Service");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmServiceDetails frmServiceDetails = new frmServiceDetails
            {
                InsertOrUpdate = true,
                Text = "Add a new service",
                serviceRepository = serviceRepository
            };
            if(frmServiceDetails.ShowDialog() == DialogResult.OK)
            {
                LoadServiceList();
                source.Position = source.Position - 1;
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmServiceDetails frmServiceDetails = new frmServiceDetails
            {
                Text = "Update a service",
                InsertOrUpdate = false,
                serviceInfo = GetServiceObject(),
                serviceRepository = serviceRepository
            };
            if (frmServiceDetails.ShowDialog() == DialogResult.OK)
            {
                LoadServiceList();
                source.Position = source.Position - 1;
            }
        }

        private void btnAddRoomService_Click(object sender, EventArgs e)
        {
            frmRoomService frmRoomService = new frmRoomService();
            frmRoomService.Show();
            this.Hide();
        }

        private void btnViewService_Click(object sender, EventArgs e)
        {
            frmRoomService frmRoomService = new frmRoomService();
            frmRoomService.Show();
            this.Hide();
        }
    }
}
