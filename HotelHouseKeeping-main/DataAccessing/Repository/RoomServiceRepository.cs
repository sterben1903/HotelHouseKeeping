using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public class RoomServiceRepository:IRoomServiceRepository
    {
        public void insert(ServiceDetail serviceDetail) => ServiceDetailDAO.Instance.Addnew(serviceDetail);
        public void update(ServiceDetail serviceDetail) => ServiceDetailDAO.Instance.Update(serviceDetail);
        public void delete(int serviceDetailID) => ServiceDetailDAO.Instance.Remove(serviceDetailID);
        public IEnumerable<ServiceDetail> GetRoomServicesByRoomNumber(int roomNumber) => ServiceDetailDAO.Instance.GetRoomServicesByRoomNumber(roomNumber);
        public IEnumerable<ServiceDetail> GetRoomServices() => ServiceDetailDAO.Instance.GetAllRoomService();
        public ServiceDetail GetRoomServiceByServiceDetailID(int serviceDetailID) => ServiceDetailDAO.Instance.GetRoomSerivceByServiceDetailID(serviceDetailID);
        public decimal GetTotal(int roomNumber) => ServiceDetailDAO.Instance.GetTotal(roomNumber);
        public void GetBill(int roomNumber) => ServiceDetailDAO.Instance.GetBill(roomNumber);
    }
}
