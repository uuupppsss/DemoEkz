using DemoEkzApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public async Task<ActionResult> CreateNewUser(UserDTO user)
        {
            if (user == null)
                return BadRequest("Invalid user");
            Роли user_role=context.Ролиs.FirstOrDefault(r=>r.Id==user.RoleId);
            User user_to_create = new User()
            {
                Login = user.Login,
                Password = user.Password,
                RoleId = user.RoleId,
                IsAutorized=false,
                Role=user_role
            };
            context.Users.Add(user_to_create);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("FirstAutorization")]
        public async Task<ActionResult> FirstAutorization(UserDTO user, string newPassword)
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
                await context.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest("Password mismatching");
        }

        
    }
}
