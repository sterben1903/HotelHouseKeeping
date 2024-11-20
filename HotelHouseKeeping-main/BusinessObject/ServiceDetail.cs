using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ServiceDetail
    {
        public int ServiceDetailID { get; set; }
        public int ServiceID { get; set; }
        public int RoomNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
