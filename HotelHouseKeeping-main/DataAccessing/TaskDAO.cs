using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.Data.SqlClient;

namespace DataAccessing
{
    public class TaskDAO:BaseDAL
    {
        private static TaskDAO instance = null;
        private static readonly object instanceLock = new object();
        private TaskDAO() { }
        public static TaskDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new TaskDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<RoomTask> GetTaskList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Task";
            var tasks = new List<RoomTask>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    tasks.Add(new RoomTask
                    {
                        TaskID = dataReader.GetInt32(0),
                        MemberID = dataReader.GetInt32(1),
                        RoomNumber = dataReader.GetInt32(2),
                        DateCreate = dataReader.GetDateTime(3),
                        MemberName = dataReader.GetString(4),
                        TaskStatus = dataReader.GetString(5)
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
            return tasks;
        }
        public RoomTask GetTaskByID(int taskID)
        {
            IDataReader dataReader = null;
            String SQLSelect = "Select * from Task where TaskID = @TaskID";
            RoomTask task = null;
            try
            {
                var param = dataProvider.CreateParameter("@TaskID", 4, taskID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read()) {
                    task = new RoomTask
                    {
                        TaskID = dataReader.GetInt32(0),
                        MemberID = dataReader.GetInt32(1),
                        RoomNumber = dataReader.GetInt32(2),
                        DateCreate = dataReader.GetDateTime(3),
                        MemberName = dataReader.GetString(4),
                        TaskStatus = dataReader.GetString(5)
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
            return task;
        }
        public void AddNew(RoomTask task)
        {
            try
            {
                RoomTask t = GetTaskByID(task.TaskID);
                if (t == null)
                {
                    string SQLInsert = "insert Task values (@MemberID, @RoomNumber,@DateCreate,@MemberName,@TaskStatus)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, task.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@RoomNumber", 4, task.RoomNumber, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@DateCreate", 20, task.DateCreate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter("@MemberName", 20, task.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@TaskStatus", 10, task.TaskStatus, DbType.String));
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
        public void Update(RoomTask task)
        {
            try
            {
                RoomTask t = GetTaskByID(task.TaskID);
                if(t != null)
                {
                    string SQLUpdate = "update Task set MemberID = @MemberID, RoomNumber = @RoomNumber, DateCreate= @DateCreate, MemberName =@MemberName, TaskStatus = @TaskStatus where TaskID = @TaskID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, task.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@RoomNumber", 4, task.RoomNumber, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@DateCreate", 20, task.DateCreate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter("@MemberName", 20, task.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@TaskStatus", 10, task.TaskStatus, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@TaskID", 10, task.TaskID, DbType.Int32));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Task does not already exist");
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
        public void Remove(int taskID)
        {
            try
            {
                RoomTask t = GetTaskByID(taskID);
                if (t != null)
                {
                    string SQLDelete = "Delete Task where TaskID = @TaskID";
                    var param = dataProvider.CreateParameter("@TaskID", 4, taskID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("Task does not already exist");
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
        public IEnumerable<RoomTask> getTasksByMemberID(int memberID)
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Task where MemberID = @MemberID";
            var tasks = new List<RoomTask>();
            try
            {
                var param = dataProvider.CreateParameter("@MemberID", 4, memberID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    tasks.Add(new RoomTask
                    {
                        TaskID = dataReader.GetInt32(0),
                        MemberID = dataReader.GetInt32(1),
                        RoomNumber = dataReader.GetInt32(2),
                        DateCreate = dataReader.GetDateTime(3),
                        MemberName = dataReader.GetString(4),
                        TaskStatus = dataReader.GetString(5)
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
            return tasks;
        }
    }
}
