using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;
using Microsoft.AspNetCore.Authorization;
using myTask.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Diagnostics;

namespace myTask.Controllers{
    [ApiController]
[Route("/api/user")]
public class userController : ControllerBase
{
    IUserService userService;
    ITaskService taskService;

        public object UserService { get; private set; }

        public userController(IUserService userService,ITaskService taskService)
    {
        this.userService = userService;
        this.taskService = taskService;

    }

     [HttpGet("/api/Allusers")]
     [Authorize(Policy="Admin")]
     public ActionResult<List<User>> GetAll()=>
         userService.GetAll();
    
    //שליפת משתמש לפי מזהה
    [HttpGet]
    [Authorize(Policy="User")]

    public ActionResult<User> Get()
    {
        var userId = Convert.ToInt32(User.FindFirst("Id").Value);
        var user = userService.Get(userId);
        if (user == null)
            return NotFound();
        return user;
    }


    [HttpPost]
    [Authorize(Policy="Admin")]

    public IActionResult Create(User user)
    {
        if(user is null)
            return BadRequest("user is null");
        userService.Add(user);
        return CreatedAtAction(nameof(Create), new {id=user.Id}, user);

    }
        [HttpDelete]
        [Route("{userId}")]
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

    [HttpPut]
        [Authorize(Policy = "User")]

        public IActionResult Update([FromBody]User user)
        {
<<<<<<< HEAD
            var userId = Convert.ToInt32(User.FindFirst("Id").Value);
            var userType = User.FindAll("Type").Select(x => x.Value.ToLower()).Skip(1).FirstOrDefault() ?? User.FindFirst("Type").Value.ToLower();
=======
   Console.WriteLine("lll");
            var userId = Convert.ToInt32(User.FindFirst("Id").Value);
            var userType = User.FindFirst("Type").Value.ToLower();
>>>>>>> f60610f55743aa05ae11f63d39f1e7b599d52a3c
            Console.WriteLine(userType);
            var existingUser = userService.Get(userId);
            if (existingUser is null)
                return NotFound();
            user.Id = userId;
            user.Type=userType;
            userService.Update(user);

            return NoContent();
        }}

}
