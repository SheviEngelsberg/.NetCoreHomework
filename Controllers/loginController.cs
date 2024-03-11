
using Microsoft.AspNetCore.Mvc;
using myTask.Models;
using myTask.Interfaces;
using myTask.Services;
using System.Security.Claims;

namespace myTask.Controllers{
    [ApiController]
[Route("[controller]")]
public class loginController : ControllerBase
{
    IUserService userService;
    

    public loginController(IUserService userService){
        this.userService=userService;
    }
    public ActionResult<string> login([FromBody] User user){
        User myUser=this.userService.GetAll().FirstOrDefault(u=>u.Name==user.Name&&u.Password==user.Password);
        if(myUser==null)
            return Unauthorized();
        var claims = new List<Claim>
        {
            new Claim("Type",myUser.Type),
            new Claim("Id",myUser.Id.ToString())
        };

        var token= tokenService.GetToken(claims);
        
        return new OkObjectResult(tokenService.WriteToken(token));


            
    } 
}

}

