using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing
{
    public class ServiceDetailDAO:BaseDAL
    {
        private static ServiceDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceDetailDAO() { }
        public static ServiceDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<ServiceDetail> GetRoomServicesByRoomNumber(int roomNumber)
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from ServiceDetail where RoomNumber = @RoomNumber";
            var roomServices = new List<ServiceDetail>();
            try
            {
                var param = dataProvider.CreateParameter("@RoomNumber", 4, roomNumber, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    roomServices.Add(new ServiceDetail
                    {
                        ServiceDetailID = dataReader.GetInt32(0),
                        ServiceID = dataReader.GetInt32(1),
                        RoomNumber = dataReader.GetInt32(2),
                        Quantity = dataReader.GetInt32(3),
                        Price = dataReader.GetDecimal(4),
                        CreateDate = dataReader.GetDateTime(5)
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
            return roomServices;
        }
        public void Addnew(ServiceDetail serviceDetail)
        {
            try
            {
                ServiceDetail s = GetRoomSerivceByServiceDetailID(serviceDetail.ServiceDetailID);
                if (s == null)
                {
                    string SQLInsert = "insert ServiceDetail values (@ServiceID, @RoomNumber,@Quantity,@Price,@CreateDate)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ServiceID", 20, serviceDetail.ServiceID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@RoomNumber", 20, serviceDetail.RoomNumber, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Quantity", 20, serviceDetail.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Price", 20, serviceDetail.Price, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@CreateDate", 20, serviceDetail.CreateDate, DbType.DateTime));
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
        public ServiceDetail GetRoomSerivceByServiceDetailID(int ServiceDetailID)
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from ServiceDetail where ServiceDetailID = @ServiceDetailID";
            ServiceDetail roomService = null;
            try
            {
                var param = dataProvider.CreateParameter("@ServiceDetailID", 4, ServiceDetailID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    roomService = new ServiceDetail
                    {
                        ServiceDetailID = dataReader.GetInt32(0),
                        ServiceID = dataReader.GetInt32(1),
                        RoomNumber = dataReader.GetInt32(2),
                        Quantity = dataReader.GetInt32(3),
                        Price = dataReader.GetDecimal(4),
                        CreateDate = dataReader.GetDateTime(5)
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
            return roomService;
        }
        public void Update(ServiceDetail serviceDetail)
        {
            try
            {
                ServiceDetail s = GetRoomSerivceByServiceDetailID(serviceDetail.ServiceDetailID);
                if (s != null)
                {
                    string SQLUpdate = "update ServiceDetail set ServiceID = @ServiceID, RoomNumber=@RoomNumber, Quantity=@Quantity,Price=@Price, CreateDate=@CreateDate where ServiceDetailID = @ServiceDetailID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ServiceID", 4, serviceDetail.ServiceID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@RoomNumber", 4, serviceDetail.RoomNumber, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Quantity", 4, serviceDetail.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Price", 15, serviceDetail.Price, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@CreateDate", 20, serviceDetail.CreateDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter("@ServiceDetailID", 4, serviceDetail.ServiceDetailID, DbType.Int32));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Room service is not existed!");
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
        public void Remove(int serviceDetailID)
        {
            try
            {
                
                    ServiceDetail s = GetRoomSerivceByServiceDetailID(serviceDetailID);
                    if (s != null)
                    {
                        string SQLDelete = "Delete ServiceDetail where ServiceDetailID = @ServiceDetailID";
                        var param = dataProvider.CreateParameter("@ServiceDetailID", 4, serviceDetailID, DbType.Int32);
                        dataProvider.Delete(SQLDelete, CommandType.Text, param);
                    }
                    else
                    {
                        throw new Exception("Room service does not already exist");
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
        public IEnumerable<ServiceDetail> GetAllRoomService()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from ServiceDetail ";
            var roomServices = new List<ServiceDetail>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    roomServices.Add(new ServiceDetail
                    {
                        ServiceDetailID = dataReader.GetInt32(0),
                        ServiceID = dataReader.GetInt32(1),
                        RoomNumber = dataReader.GetInt32(2),
                        Quantity = dataReader.GetInt32(3),
                        Price = dataReader.GetDecimal(4),
                        CreateDate = dataReader.GetDateTime(5)
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
            return roomServices;
        }
        public decimal GetTotal(int roomNumber)
        {
            IDataReader dataReader = null;
            string SQLSelect = "select sum([Quantity]*[Price])\r\nfrom [dbo].[ServiceDetail]\r\nwhere [RoomNumber] = @RoomNumber";
            decimal total = 0;
            try
            {
                var param = dataProvider.CreateParameter("@RoomNumber", 4, roomNumber, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    if (dataReader.GetDecimal(0) != null)
                    {
                        total = dataReader.GetDecimal(0);
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
            return total;
        }
        public void GetBill(int roomNumber)
        {
            try
            {

                
                
                    string SQLDelete = "Delete ServiceDetail where RoomNumber = @RoomNumber";
                    var param = dataProvider.CreateParameter("@RoomNumber", 4, roomNumber, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                

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
