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
    public partial class frmServiceDetails : Form
    {
        public bool InsertOrUpdate { get; set; }
        public IServiceRepository serviceRepository { get; set; }
        public Service serviceInfo { get; set; }
        public frmServiceDetails()
        {
            InitializeComponent();
        }

        private void frmServiceDetails_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate)
            {
                lbID.Visible = false;

            }
            else
            {
                if (serviceInfo.Name.Equals("Giat,ui quan ao"))
                {
                    txtServiceName.Enabled = false;
                    txtQuantity.Enabled = false;
                }
                lbServiceID.Text = serviceInfo.ServiceID.ToString();
                txtServiceName.Text = serviceInfo.Name;
                txtPrice.Text = serviceInfo.Price.ToString();
                txtQuantity.Text = serviceInfo.Quantity.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (InsertOrUpdate)
                {
                    var service = new Service
                    {
                        Name = txtServiceName.Text,
                        Price = decimal.Parse(txtPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text)
                    };
                    serviceRepository.insert(service);
                }
                else if (int.Parse(txtQuantity.Text) >= 0)
                {
                    var service = new Service
                    {
                        ServiceID = int.Parse(lbServiceID.Text),
                        Name = txtServiceName.Text,
                        Price = decimal.Parse(txtPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text)
                    };
                    serviceRepository.update(service);
                }
                else
                {
                    MessageBox.Show("Quantity must greater than 0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == true ? "Add new service" : "Update a service");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
