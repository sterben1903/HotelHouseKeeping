using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class RoomTask
    {
        public int TaskID { get; set; }
        public int MemberID { get; set; }
        public int RoomNumber { get; set; }
        public DateTime DateCreate { get; set; }
        public string MemberName { get; set; }
        public string TaskStatus { get; set; }
    }
}
