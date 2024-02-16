using myTask.Interfaces;
using myTask.Models;
namespace myTask.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;

public class theTaskService : ITaskService
{
    List<theTask> tasks {get;}
    
    private string fileName ="task.json";
    public theTaskService(IWebHostEnvironment  webHost)
    {
        this.fileName =Path.Combine(webHost.ContentRootPath,"Data" ,"task.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            tasks = JsonSerializer.Deserialize<List<theTask>>(jsonFile.ReadToEnd(),
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

    //שליפת כל המשימות 
    public  List<theTask> GetAll() => tasks;

    //שליפה של משימה מסויימת לפי ID
    public  theTask Get(int id) 
    {
        return tasks.FirstOrDefault(p => p.Id == id);
    }

    //הוספת משימה
    public void Add(theTask newTask)
    {
        newTask.Id = tasks.Count()+1;
        tasks.Add(newTask);
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
    public void Update(theTask task)
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