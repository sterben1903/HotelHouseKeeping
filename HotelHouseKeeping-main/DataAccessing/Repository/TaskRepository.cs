using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public class TaskRepository:ITaskRepository
    {
        public RoomTask getTaskByID(int taskID) => TaskDAO.Instance.GetTaskByID(taskID);
        public void insert(RoomTask task) => TaskDAO.Instance.AddNew(task);
        public void update(RoomTask task) => TaskDAO.Instance.Update(task);
        public void delete(int taskID) => TaskDAO.Instance.Remove(taskID);
        public IEnumerable<RoomTask> getTasks() => TaskDAO.Instance.GetTaskList();
        public IEnumerable<RoomTask> getTasksByMemberID(int memberID) => TaskDAO.Instance.getTasksByMemberID(memberID);
    }
}
