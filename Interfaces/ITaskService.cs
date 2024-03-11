using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using System.Collections.Generic;

namespace myTask.Interfaces
{
    public interface ITaskService
    {
        List<TheTask> GetAll(int userId);
        TheTask Get(int taskId,int userId);
        void Add(int userId,TheTask myTask);
        void Update(TheTask myTask);
        void Delete(int taskId,int userId);
        void DeleteByUserId(int userId);
        // ActionResult<List<TheTask>> GetAll(int v);
        // ActionResult<TheTask> Get(int taskId, string id);
        // void Add(string id, TheTask task);
        // void Delete(int taskId, string id);
        // void Delete(int taskId, int userId);
        int Count {get;}
    }
}