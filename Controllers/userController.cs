using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class userController : ControllerBase
{
    IUserService userService;
    long userId;
    public userController(IUserService userService)
    {
        this.userService = userService;
// this.userId=///

    }

//     [HttpGet]
//     public ActionResult<List<theTask>> GetAll()=>
//         TaskService.GetAll();
    

    [HttpGet("{id}")]
    public ActionResult<user> Get(int userId)
    {
        var user = userService.Get(userId);
        if (user == null)
            return NotFound();
        return user;
    }

    [HttpPost]
    public IActionResult Create(user user)
    {
        userService.Add(user);
        return CreatedAtAction(nameof(Create), new {id=user.Id}, user);

    }
    
//     [HttpPut("{id}")]
//     public IActionResult Update(int id, theTask task)
//         {
//             if (id != task.Id)
//                 return BadRequest();

//             var existingTask = TaskService.Get(id);
//             if (existingTask is null)
//                 return  NotFound();

//             TaskService.Update(task);

//             return NoContent();
//         }

        [HttpDelete("{id}")]
        public IActionResult Delete(int userId)
        {
            var user = userService.Get(userId);
            if (user is null)
                return  NotFound();

            userService.Delete(userId);

            return Content(userService.Count.ToString());
        }
    
}

}
