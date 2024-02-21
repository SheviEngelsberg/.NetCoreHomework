using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class taskController : ControllerBase
{
    ITaskService TaskService;
    public taskController(ITaskService TaskService)
    {
        this.TaskService = TaskService;
// this.userId=///

    }

    [HttpGet]
    public ActionResult<List<TheTask>> GetAll()=>
        TaskService.GetAll();
    

    [HttpGet("{id}")]
    public ActionResult<TheTask> Get(int id)
    {
        var task = TaskService.Get(id);
        if (task == null)
            return NotFound();
        return task;
    }
   
    [HttpGet("task/{id}/user/{userName}")]
    public ActionResult<TheTask> Get(int id, string userName)
    {
        var task = TaskService.Get(id,userName);
        if (task == null)
            return NotFound();
        return task;
    }
    
    [HttpGet("allTasksOFuserName/{UserName}")]
public ActionResult<List<TheTask>> Get(string UserName)
{
    var tasks = TaskService.Get(UserName).ToList();
    if (tasks.Count == 0)
    {
        return NotFound();
    }
    
    return tasks;
}

    [HttpPost]
    public IActionResult Create(TheTask task)
    {
        TaskService.Add(task);
        return CreatedAtAction(nameof(Create), new {id=task.Id}, task);


    }
    [HttpPost]
    public IActionResult Create(string userName,TheTask task)
    {
        TaskService.Add(userName,task);
        return CreatedAtAction(nameof(Create), new {id=task.Id}, task);


    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, TheTask task)
        {
            if (id != task.Id)
                return BadRequest();

            var existingTask = TaskService.Get(id);
            if (existingTask is null)
                return  NotFound();

            TaskService.Update(task);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = TaskService.Get(id);
            if (task is null)
                return  NotFound();

            TaskService.Delete(id);

            return Content(TaskService.Count.ToString());
        }
    
}

}

