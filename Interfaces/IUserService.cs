using myTask.Interfaces;
using myTask.Models;
using System.Collections.Generic;

namespace myTask.Interfaces
{
    public interface IUserService
    {
        void Delete(int id);
        user Get(int id);
        void Add(user user);
        int Count {get;}
    }
}