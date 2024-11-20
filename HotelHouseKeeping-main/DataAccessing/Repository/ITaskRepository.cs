using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessing.Repository
{
    public interface ITaskRepository
    {
        void insert(RoomTask task);
        void update(RoomTask task);
        void delete(int taskID);
        IEnumerable<RoomTask> getTasks();
        RoomTask getTaskByID(int taskID);
        IEnumerable<RoomTask> getTasksByMemberID(int memberID);

    }
}
