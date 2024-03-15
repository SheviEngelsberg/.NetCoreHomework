using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace myTask.Controllers{
    [ApiController]
[Route("/api/todo")]
public class taskController : ControllerBase
{
    ITaskService TaskService;
    public taskController(ITaskService TaskService)
    {
        this.TaskService = TaskService;
    }

    
   //מחזיר את כל המשימות של משתמש מסויים
   [HttpGet]
   [Authorize(Policy="User")]
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
    [HttpGet("/{taskId}")]
    [Authorize(Policy="User")]

    public ActionResult<TheTask> Get(int taskId)
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var task = TaskService.Get(taskId,userId);
        if (task == null)
            return NotFound();
        return task;
    }
    
    
    [HttpPost]
    [Authorize(Policy="User")]

    public IActionResult Create(TheTask task)
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        TaskService.Add(userId,task);
        task.UserId=userId;
        return CreatedAtAction(nameof(Create), new {id=task.Id}, task);


    }
    [HttpPut("/{taskId}")]
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
        task.UserId=userId;
        
        return NoContent();
    }

    [HttpDelete("/{taskId}")]
    [Authorize(Policy="User")]
    public IActionResult Delete(int taskId)
    {
        Console.WriteLine("llll"+taskId);
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var task = TaskService.Get(taskId,userId);
        if (task is null)
            return  NotFound();

        TaskService.Delete(taskId,userId);
        return NoContent();
    }
    
}

}

