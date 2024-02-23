using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class taskController : ControllerBase
{
    int userId;
    ITaskService TaskService;
    public taskController(ITaskService TaskService, int userId)
    {
        this.TaskService = TaskService;
        this.userId=userId;
    }

    // [HttpGet]
    // public ActionResult<List<TheTask>> GetAll()=>
    //     TaskService.GetAll();
    

    // [HttpGet("{id}")]
    // public ActionResult<TheTask> Get(int id)
    // {
    //     var task = TaskService.Get(id);
    //     if (task == null)
    //         return NotFound();
    //     return task;
    // }

   //מחזיר את כל המשימות של משתמש מסויים
   [HttpGet("/api/todo")]
    public ActionResult<List<TheTask>> Get()
    {
        var tasks = TaskService.Get(this.userId).ToList();
        if (tasks.Count == 0)
        {
            return NotFound();
        }
        
        return tasks;
    }

    //שליפה של משימה מסויימת של משתמש מסויים
    [HttpGet("/api/todo/{taskId}")]
    public ActionResult<TheTask> Get(int taskId)
    {
        var task = TaskService.Get(taskId,this.userId);
        if (task == null)
            return NotFound();
        return task;
    }
    
    
    [HttpPost("/api/todo")]
    public IActionResult Create(TheTask task)
    {
        TaskService.Add(this.userId,task);
        return CreatedAtAction(nameof(Create), new {id=task.Id}, task);


    }
    [HttpPut("/api/todo/{taskId}")]
    public IActionResult Update(int taskId, TheTask task)
    {
        if (taskId != task.Id)
            return BadRequest();

        var existingTask = TaskService.Get(taskId);
        if (existingTask is null)
            return  NotFound();

        TaskService.Update(task);

        return NoContent();
    }

    [HttpDelete("/api/todo/{taskId} ")]
    public IActionResult Delete(int taskId)
    {
        var task = TaskService.Get(taskId);
        if (task is null)
            return  NotFound();

        TaskService.Delete(taskId,this.userId);
        return Content(TaskService.Count.ToString());
    }
    
}

}

