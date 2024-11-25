using DemoEkzApi.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace DemoEkzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private User[] _users;
        private User05Context context;
        public AuthController(User05Context _context)
        {
            this.context = _context;
            _users = context.Users.ToArray();
        }

        [HttpGet]
        public ActionResult<ResponseTokenAndRole> Login(string login, string password)
        {
            User? user=_users.FirstOrDefault(x => x.Login == login&&x.Password==password);
            if (user == null) return Unauthorized();
            string role = user.Role.Название;
            int id=user.Id;

            List<Claim> claims = new()
            {
                new Claim(ClaimValueTypes.Integer32, id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    //кладём полезную нагрузку
                    claims: claims,
                    //устанавливаем время жизни токена 2 минуты
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new ResponseTokenAndRole
            {
                Token = token,
                Role = role
            });
        }
    }
}
