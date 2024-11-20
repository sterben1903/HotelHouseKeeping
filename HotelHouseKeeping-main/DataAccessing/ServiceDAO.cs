using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing
{
    public class ServiceDAO:BaseDAL
    {
        private static ServiceDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceDAO() { }
        public static ServiceDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Service> getServiceList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "select [ServiceID],[Name],[Price],[Quantity] from [dbo].[Service] where [Status] = 1";
            var services = new List<Service>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    services.Add(new Service
                    {
                        ServiceID = dataReader.GetInt32(0),
                        Name = dataReader.GetString(1),
                        Price = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3)
                        //Status = 1
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return services;
        }
        public Service getServiceByID(int serviceID)
        {
            IDataReader dataReader = null;
            String SQLSelect = "Select [ServiceID],[Name],[Price],[Quantity] from Service where ServiceID = @ServiceID and [Status] = 1";
            Service service = null;
            try
            {
                var param = dataProvider.CreateParameter("@ServiceID", 4, serviceID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    service = new Service
                    {
                        ServiceID = dataReader.GetInt32(0),
                        Name = dataReader.GetString(1),
                        Price = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Status = 1
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return service;
        }
        public void Addnew(Service service)
        {
            try
            {
                Service s = getServiceByID(service.ServiceID);
                if(s == null)
                {
                    string SQLInsert = "insert Service values ( @Name,@Price,@Quantity,1)";
                    var parameters = new List<SqlParameter>();
                    
                    parameters.Add(dataProvider.CreateParameter("@Name", 20, service.Name, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Price", 15, service.Price, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@Quantity", 5, service.Quantity, DbType.Int32));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void Update(Service service)
        {
            try
            {
                Service s = getServiceByID(service.ServiceID);
                if (s != null)
                {
                    string SQLUpdate = "update Service set Name =@Name, Price = @Price ,Quantity = @Quantity where ServiceID = @ServiceID and [Status] = 1";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@Name", 20, service.Name, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Price", 15, service.Price, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@Quantity", 5, service.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ServiceID", 4, service.ServiceID, DbType.Int32));

                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Service does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void Remove(int serviceID)
        {
            try
            {
                Service s = getServiceByID(serviceID);
                if(s != null)
                {
                    string SQLDelete = "update [dbo].[Service] set [Status] = 0 where [ServiceID]  =@ServiceID";
                    var param = dataProvider.CreateParameter("@ServiceID", 4, serviceID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("Service does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
