using myTask.Interfaces;
using myTask.Models;
namespace myTask.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;

public class userService : IUserService
{
    List<User> users {get;}
    
    private string fileName ="users.json";
    public userService(IWebHostEnvironment  webHost)
    {
        this.fileName =Path.Combine(webHost.ContentRootPath,"Data" ,"users.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
           
            
        }
       
    }
    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(users));
    }
    // public  List<theTask> GetAll() => tasks;

    //שליפת משתמש לפי ID
    public  User Get(int userId) 
    {
        return users.FirstOrDefault(p => p.Id == userId);
    }
    //שליפת כל המשתמשים הקיימים
    public List<User> GetAll() => users;

    //הוספת משתמש
    public void Add(User newUser)
    {
        newUser.Id = users.Count()+1;
        users.Add(newUser);
        saveToFile();
    }
    
    //מחיקת משתמש לפי ID
    public void Delete(int userId)
    {
        var user = Get(userId);
        if (user is null)
            return;

        users.Remove(user);
        saveToFile();
    }

    // public void Update(theTask task)
    //     {
    //         var index = tasks.FindIndex(p => p.Id == task.Id);
    //         if (index == -1)
    //             return;

    //         tasks[index] = task;
    //         saveToFile();
    //     }
    public int Count => users.Count();

    // private string GetDebuggerDisplay()
    // {
    //     return ToString();
    // }
}


