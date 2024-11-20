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
    public partial class frmRoomServiceDetail : Form
    {
        public bool InsertOrUpdate { get; set; }
        public IRoomServiceRepository roomServiceRepository = new RoomServiceRepository();
        public ServiceDetail serviceDetailInfo { get; set; }
        public IServiceRepository serviceRepository = new ServiceRepository();
        public IRoomRepository roomRepository = new RoomRepository();
        public int selectedRoomNumber { get; set; }
        public int serviceQuantity { get; set; }
        public frmRoomServiceDetail()
        {
            InitializeComponent();
        }

        private void frmRoomServiceDetail_Load(object sender, EventArgs e)
        {
            var listRoom = roomRepository.GetRoomNumbers();
            cboRoomNumber.Enabled = false;
            foreach(var room in listRoom)
            {
                cboRoomNumber.Items.Add(room);
            }
            if (InsertOrUpdate)
            {
                if(selectedRoomNumber != 0)
                {
                    cboRoomNumber.Text = selectedRoomNumber.ToString();
                    cboRoomNumber.Enabled = false;
                    selectedRoomNumber = 0;
                }
                lbServiceDetailID.Visible = false;
                labelServiceDetailID.Visible = false;
                dtpDateCreate.Value = DateTime.Now;
            }
            else
            {
                
                txtServiceID.Enabled = false;
                labelServiceDetailID.Text = serviceDetailInfo.ServiceDetailID.ToString();
                txtServiceID.Text = serviceDetailInfo.ServiceID.ToString();
                cboRoomNumber.Text = serviceDetailInfo.RoomNumber.ToString();
                txtQuantity.Text = serviceDetailInfo.Quantity.ToString();
                txtPrice.Text = serviceDetailInfo.Price.ToString();
                dtpDateCreate.Value = serviceDetailInfo.CreateDate;
                Service service = serviceRepository.getServiceByID(int.Parse(txtServiceID.Text));
                if(!service.Name.Equals("Giat,ui quan ao"))
                service.Quantity += serviceDetailInfo.Quantity;
                serviceQuantity = serviceDetailInfo.Quantity;
                txtServiceName.Text = service.Name;
                serviceRepository.update(service);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (InsertOrUpdate)
                {
                    var serviceDetail = new ServiceDetail
                    {
                        ServiceID = int.Parse(txtServiceID.Text),
                        RoomNumber = int.Parse(cboRoomNumber.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        Price = decimal.Parse(txtPrice.Text),
                        CreateDate = dtpDateCreate.Value
                    };                    
                    roomServiceRepository.insert(serviceDetail);
                    Service service = serviceRepository.getServiceByID(int.Parse(txtServiceID.Text));
                    if (!service.Name.Equals("Giat,ui quan ao"))
                    service.Quantity -= int.Parse(txtQuantity.Text);
                    serviceRepository.update(service);
                }
                else
                {
                    var serviceDetail = new ServiceDetail
                    {
                        ServiceDetailID = int.Parse(labelServiceDetailID.Text),
                        ServiceID = int.Parse(txtServiceID.Text),
                        RoomNumber = int.Parse(cboRoomNumber.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        Price = decimal.Parse(txtPrice.Text),
                        CreateDate = dtpDateCreate.Value
                    };

                    Service service = serviceRepository.getServiceByID(int.Parse(txtServiceID.Text));
                    if (!service.Name.Equals("Giat,ui quan ao"))
                    {
                        if (service.Quantity < int.Parse(txtQuantity.Text))
                        {
                            MessageBox.Show("The quantity is not enough!", "Invalid information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            service.Quantity -= serviceQuantity;
                            serviceRepository.update(service);
                        }
                        else if (service.Quantity == 0)
                        {
                            MessageBox.Show("Service is out of stock!", "Invalid information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            service.Quantity -= serviceQuantity;
                            serviceRepository.update(service);
                        }
                        else if (serviceQuantity == int.Parse(txtQuantity.Text))
                        {
                            service.Quantity -= serviceQuantity;
                            serviceRepository.update(service);
                        }
                        else
                        {
                            service.Quantity -= int.Parse(txtQuantity.Text);
                            serviceRepository.update(service);
                            roomServiceRepository.update(serviceDetail);
                        }
                    }
                        
                    
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == true ? "Add new room service" : "Update a room service");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            if(serviceQuantity == int.Parse(txtQuantity.Text))
            {
                Service service = serviceRepository.getServiceByID(int.Parse(txtServiceID.Text));
                if (!service.Name.Equals("Giat,ui quan ao"))
                    service.Quantity -= serviceQuantity;
                serviceRepository.update(service);
            }
            this.Close();
        }

        private void txtServiceID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                    Service service = serviceRepository.getServiceByID(int.Parse(txtServiceID.Text));
                
                    if (service == null)
                    {
                        MessageBox.Show("Service ID is not existed!", "Invalid infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (service.Quantity == 0 && !service.Name.Equals("Giat,ui quan ao"))
                    {
                        MessageBox.Show("Service is out of stock!", "Invalid infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        txtPrice.Text = service.Price.ToString();
                        txtServiceName.Text = service.Name;
                    }
                
                    
                
                
            }
           catch(Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void txtServiceName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (InsertOrUpdate) {
                    Service service = serviceRepository.getServiceByID(int.Parse(txtServiceID.Text));
                    if (!service.Name.Equals("Giat,ui quan ao"))
                    {
                        if (service.Quantity < int.Parse(txtQuantity.Text))
                        {
                            MessageBox.Show("The quantity is not enough!", "Invalid information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                        
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
