using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public interface IServiceRepository
    {
        void insert(Service service);
        void update(Service service);
        void delete(int serviceID);
        IEnumerable<Service> getServices();
        Service getServiceByID(int serviceID);

    }
}
