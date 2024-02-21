using myTask.Models;
using System.Collections.Generic;

namespace myTask.Interfaces
{
    public interface IUserService
    {
        User Get(int id); // Corrected capitalization of 'User'
        void Add(User user); // Corrected capitalization of 'User'

        void Delete(int id);
        int Count { get; } // Added missing semicolon
    }
}
