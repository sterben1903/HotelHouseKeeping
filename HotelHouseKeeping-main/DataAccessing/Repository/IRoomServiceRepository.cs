using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public interface IRoomServiceRepository
    {
        void insert(ServiceDetail serviceDetail);
        void update(ServiceDetail serviceDetail);
        void delete(int serviceDetailID);

        IEnumerable<ServiceDetail> GetRoomServicesByRoomNumber(int roomNumber);
        IEnumerable<ServiceDetail> GetRoomServices();
        ServiceDetail GetRoomServiceByServiceDetailID(int serviceDetailID);
        decimal GetTotal(int roomNumber);
        void GetBill(int roomNumber);
    }
}
