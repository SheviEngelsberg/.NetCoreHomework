using myTask.Models;
using System.Collections.Generic;

namespace myTask.Interfaces
{
    public interface ITaskService
    {
        List<TheTask> Get(int userId);
        TheTask Get(int taskId,int userId);
        void Add(int userId,TheTask myTask);
        void Update(TheTask myTask);
        void Delete(int taskId,int userId);
        int Count {get;}
    }
}