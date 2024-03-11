using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;
using Microsoft.AspNetCore.Authorization;
using myTask.Services;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class userController : ControllerBase
{
    IUserService userService;
    ITaskService taskService;
    public userController(IUserService userService,ITaskService taskService)
    {
        this.userService = userService;
        this.taskService = taskService;

    }

     [HttpGet]
     [Authorize(Policy="Admin")]
     public ActionResult<List<User>> GetAll()=>
         userService.GetAll();
    
    //שליפת משתמש לפי מזהה
    [HttpGet("/api/user")]
    [Authorize(Policy="User")]

    public ActionResult<User> Get()
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var user = userService.Get(userId);
        if (user == null)
            return NotFound();
        return user;
    }


    [HttpPost("/api/user")]
    [Authorize(Policy="Admin")]

    public IActionResult Create(User user)
    {
        if(user is null)
            return BadRequest("user is null");
        userService.Add(user);
        return CreatedAtAction(nameof(Create), new {id=user.Id}, user);

    }


        [HttpDelete("/api/user/{userId}")]
        [Authorize(Policy="Admin")]
        public IActionResult Delete(int userId)
        {
            var user = userService.Get(userId);
            if (user is null)
                return  NotFound();

            userService.Delete(userId);
            taskService.DeleteByUserId(userId);

            return Content(userService.Count.ToString());
        }

    
}

}
