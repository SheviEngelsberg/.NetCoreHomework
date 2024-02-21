using myTask.Models;
using System.Collections.Generic;

namespace myTask.Interfaces
{
    public interface ITaskService
    {
        List<TheTask> GetAll();
        TheTask Get(int id);
        TheTask Get(int id, string userName);
        List<TheTask> Get(string userName);
        void Add(TheTask myTask);
        void Add(string userName,TheTask myTask);
        void Delete(int id);
        void Update(TheTask myTask);
        int Count {get;}
    }
}