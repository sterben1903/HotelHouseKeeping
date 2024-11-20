using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public interface IRoomRepository
    {
        public IEnumerable<Room> GetRooms();
        public void Update(Room room);
        public Room GetRoomByNumber(int number);
        public IEnumerable<Room> GetRoomByStatus(string status);
        public IEnumerable<int> GetRoomNumbers();
    }
}
