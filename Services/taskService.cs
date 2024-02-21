using myTask.Interfaces;
using myTask.Models;
namespace myTask.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;

public class taskService : ITaskService
{
    List<TheTask> tasks {get;}
    
    private string fileName ="tasks.json";
    public taskService(IWebHostEnvironment  webHost)
    {
        this.fileName =Path.Combine(webHost.ContentRootPath,"Data" ,"tasks.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            tasks = JsonSerializer.Deserialize<List<TheTask>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });  
        }
    }
    
    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(tasks));
    }

    // שליפת כל המשימות 
    public  List<TheTask> GetAll() => tasks;

    //שליפת כל המשימות של משתמש מסויים
    public List<TheTask> Get(string UserName)
    {
        List<TheTask> userTasks=new List<TheTask>();
        userTasks=tasks.Where(t=>t.UserName==UserName).ToList();
        return userTasks;
    }

    // שליפה של משימה מסויימת לפי ID
    public  TheTask Get(int id) 
    {
        return tasks.FirstOrDefault(p => p.Id == id);
    }

    //שליפה של משימה מסויימת של משתמש מסויים
    public TheTask Get(int id, string userName)
    {
        return tasks.FirstOrDefault(t=> t.Id==id&&t.UserName==userName);
    }

    //הוספת משימה
    public void Add(TheTask newTask)
    {
        newTask.Id = tasks.Count()+1;
        tasks.Add(newTask);
        saveToFile();
    }
    //הוספת משימה מסויימת למשתמש מסויים
    public void Add(string userName, TheTask newTask)
    {
        newTask.UserName=userName;
        newTask.Id = tasks.Count()+1;
        tasks.Add(userName,newTask);
        saveToFile();
    }
    // מחיקת משימה
    public void Delete(int id)
    {
        var task = Get(id);
        if (task is null)
            return;

        tasks.Remove(task);
        saveToFile();
    }

    //עידכון משימה
    public void Update(TheTask task)
    {
        var index = tasks.FindIndex(p => p.Id == task.Id);
        if (index == -1)
            return;

        tasks[index] = task;
        saveToFile();
    }

    //מספר המשימות
    public int Count => tasks.Count();
}