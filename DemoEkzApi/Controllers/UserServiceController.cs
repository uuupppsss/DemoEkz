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
            User user1 = context.Users.FirstOrDefault(u => u.Login == user.Login);
            if (user1 != null)
                return BadRequest("User already exist");
            User user_to_create = new User()
            {
                Login = user.Login,
                Password = user.Password,
                RoleId = user.RoleId,
                IsAutorized = false,
                Role = context.Ролиs.FirstOrDefault(r => r.Id == user.RoleId)
            };
            if (user_to_create.Role == null)
                return BadRequest("Invalid data");
            context.Users.Add(user_to_create);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("FirstAutorization")]
        public async Task<ActionResult<UserDTO>> FirstAutorization(UserDTO user, string newPassword)
        {
            if (user == null)
                return BadRequest("Invalid user");
            User createdUser = context.Users.FirstOrDefault(x => x.Login == user.Login);
            if (createdUser == null)
                return NotFound("User not found");
            if (createdUser.Password == user.Password)
            {
                createdUser.Password = newPassword;
                createdUser.IsAutorized = true;
                context.Users.Update(createdUser);
                await context.SaveChangesAsync();
                return Ok(new UserDTO()
                {
                    Id=createdUser.Id,
                    Login=createdUser.Login,
                    Password=createdUser.Password,
                    RoleId=createdUser.RoleId,
                    IsAutorized=true
                });
            }
            else
                return BadRequest("Password mismatching");
        }

        [HttpGet("Autorization")]
        public async Task<ActionResult<UserDTO>> Autorization(UserDTO user)
        {
            if (user == null)
                return BadRequest("Invalid user_login");
            User createdUser = context.Users.FirstOrDefault(x => x.Login == user.Login);
            if (createdUser == null)
                return NotFound("User not found");
            if (createdUser.Password == user.Password)
            {
                return Ok(createdUser);
            }
            else
                return BadRequest("Password mismatching");
        }

        [HttpGet("IfUserAutorized")]
        public async Task<ActionResult<bool>> IfUserAutorized(string user_login)
        {
            if(user_login == null)
                return BadRequest("Invalid user");
            User createdUser = context.Users.FirstOrDefault(x => x.Login == user_login);
            if (createdUser == null)
                return NotFound("User not found");
            return Ok(createdUser.IsAutorized);
        }

        [HttpGet("IfUserExist")]
        public async Task<ActionResult<bool>> IfUserExist(string user_login)
        {
            if (user_login == null)
                return BadRequest("Invalid user");
            User createdUser = context.Users.FirstOrDefault(y => y.Login == user_login);
            if (createdUser == null)
                return Ok(false);
            else return Ok(true);
        }

        [HttpGet("IfPasswordMatch")]
        public async Task<ActionResult<bool>> IfPasswordMatch(UserDTO user)
        {
            if (user == null)
                return BadRequest("Invalid user");
            User createdUser = context.Users.FirstOrDefault(y => y.Login == user.Login);
            if (createdUser == null)
                return NotFound();
            if (user.Password == createdUser.Password)
                return Ok(true);
            else return Ok(false);
        }

        [HttpGet("RemoveUser")]
        public async Task<ActionResult<bool>> RemoveUser(int user_id)
        {
            if (user_id == 0) return BadRequest("Invalid data");
            User createdUser = context.Users.FirstOrDefault(x => x.Id == user_id);
            if (createdUser==null) return NotFound();
            context.Users.Remove(createdUser);
            await context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
