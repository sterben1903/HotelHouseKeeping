using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public class RoomRepository:IRoomRepository
    {
        public IEnumerable<Room> GetRooms() => RoomDAO.Instance.GetRoomList();
        public void Update(Room room) => RoomDAO.Instance.Update(room);
   
        public Room GetRoomByNumber(int number) => RoomDAO.Instance.GetRoomByNumber(number);
        public IEnumerable<Room> GetRoomByStatus(string status) => RoomDAO.Instance.GetRoomListByStatus(status);
        public IEnumerable<int> GetRoomNumbers() => RoomDAO.Instance.GetRoomNumbers();
  
    }
}
