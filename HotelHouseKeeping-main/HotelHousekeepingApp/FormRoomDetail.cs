using BusinessObject;
using DataAccessing.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HotelHousekeepingApp
{
    public partial class FormRoomDetail : Form
    {
        public FormRoomDetail()
        {
            InitializeComponent();
        }
        public IRoomRepository roomRepository = new RoomRepository();
        public Room RoomInfo { get; set; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormRoomDetail_Load(object sender, EventArgs e)
        {

            txtNumber.Text = RoomInfo.RoomNumber.ToString();
            cboStatus.Text = RoomInfo.RoomStatus;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var room = new Room
                {

                    RoomNumber = int.Parse(txtNumber.Text),
                    RoomStatus = cboStatus.Text
                };

                roomRepository.Update(room);
                MessageBox.Show("Update successful", "Update Room");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool IsNumber(string val)
        {
            if (val != "")
                return Regex.IsMatch(val, @"^[0-9]\d*\.?[0]*$");
            else return true;
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            if (IsNumber(txtNumber.Text) != true)
            {
                MessageBox.Show("Room Number wrong format,Please try again", "Update Fail");
                txtNumber.Text = "";
            }
        }
    }
}
