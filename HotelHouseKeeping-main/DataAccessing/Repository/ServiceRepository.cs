using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public class ServiceRepository:IServiceRepository
    {
        public void insert(Service service) => ServiceDAO.Instance.Addnew(service);
        public void update(Service service) => ServiceDAO.Instance.Update(service);
        public void delete(int serviceID) => ServiceDAO.Instance.Remove(serviceID);
        public IEnumerable<Service> getServices() => ServiceDAO.Instance.getServiceList();
        public Service getServiceByID(int serviceID) => ServiceDAO.Instance.getServiceByID(serviceID);
    }
}
