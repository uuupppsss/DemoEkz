using DemoEkzApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoEkzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserServiceController : ControllerBase
    {
        readonly User05Context context;
        public UserServiceController(User05Context context)
        {
            this.context = context;
        }

        [HttpPost("CreateNewUser")]
        public async Task<ActionResult> CreateNewUser(User user)
        {
            if (user == null)
                return BadRequest("Invalid user");
            user.IsAutorized = false;
            context.Users.Add(user);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("FirstAutorization")]
        public async Task<ActionResult> FirstAutorization(User user, string newPassword)
        {
            if (user == null)
                return BadRequest("Invalid user");
            User createdUser = context.Users.FirstOrDefault(x => x.Login == user.Login);
            if (createdUser == null)
                return BadRequest("User not found");
            if (createdUser.Password == user.Password)
            {
                createdUser.Password = newPassword;
                createdUser.IsAutorized = true;
                context.Users.Update(createdUser);
                context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest("Password mismatching");
        }

        
    }
}
