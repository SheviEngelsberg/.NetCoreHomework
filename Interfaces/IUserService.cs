using myTask.Models;
using System.Collections.Generic;

namespace myTask.Interfaces
{
    public interface IUserService
    {
        //שליפת משתמש לפי מזהה
        User Get(int userId);
        List<User> GetAll();
        void Add(User user); 

        void Delete(int id);
        int Count { get; } // Added missing semicolon
    }
}
