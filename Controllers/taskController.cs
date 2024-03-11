using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class taskController : ControllerBase
{
    ITaskService TaskService;
    public taskController(ITaskService TaskService)
    {
        this.TaskService = TaskService;
    }

    
   //מחזיר את כל המשימות של משתמש מסויים
   [HttpGet("/api/todo")]
   [Authorize(Policy="User"),Authorize(Policy="Admin")]
    public ActionResult<List<TheTask>> GetAll()
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var tasks = TaskService.GetAll(userId);
        if (tasks==null)
        {
            return NotFound();
        }
            return tasks;
    }

    //שליפה של משימה מסויימת של משתמש מסויים
    [HttpGet("/api/todo/{taskId}")]
    [Authorize(Policy="User")]

    public ActionResult<TheTask> Get(int taskId)
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var task = TaskService.Get(taskId,userId);
        if (task == null)
            return NotFound();
        return task;
    }
    
    
    [HttpPost("/api/todo")]
    [Authorize(Policy="User")]

    public IActionResult Create(TheTask task)
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        TaskService.Add(userId,task);
        return CreatedAtAction(nameof(Create), new {id=task.Id}, task);


    }
    [HttpPut("/api/todo/{taskId}")]
    [Authorize(Policy="User")]

    public IActionResult Update(int taskId, TheTask task)
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        if (taskId != task.Id)
            return BadRequest();

        var existingTask = TaskService.Get(taskId,userId);
        if (existingTask is null)
            return  NotFound();

        TaskService.Update(task);

        return NoContent();
    }

    [HttpDelete("/api/todo/{taskId} ")]
    [Authorize(Policy="User")]

    public IActionResult Delete(int taskId)
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var task = TaskService.Get(taskId,userId);
        if (task is null)
            return  NotFound();

        TaskService.Delete(taskId,userId);
        return Content(TaskService.Count.ToString());
    }
    
}

}

