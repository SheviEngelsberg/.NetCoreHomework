using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class userController : ControllerBase
{
    IUserService userService;
    public userController(IUserService userService)
    {
        this.userService = userService;
// this.userId=///

    }

//     [HttpGet]
//     public ActionResult<List<theTask>> GetAll()=>
//         TaskService.GetAll();
    

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var user = userService.Get(id);
        if (user == null)
            return NotFound();
        return user;
    }


    [HttpPost]
    public IActionResult Create(User user)
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
        public IActionResult Delete(int id)
        {
            var user = userService.Get(id);
            if (user is null)
                return  NotFound();

            userService.Delete(id);

            return Content(userService.Count.ToString());
        }
    
}

}
