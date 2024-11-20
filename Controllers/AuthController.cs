using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Models;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Services.Mappers;

namespace MicroTransation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly TokenMappers _token;

        public AuthController(AppDbContext appDbContext, TokenMappers token)
        {
            _appDbContext = appDbContext;
            _token = token;
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn (UserAuthDTO _authUser)
        {
            if (_authUser.Email == "" || _authUser.Password == "")
            {
                return BadRequest("Renseignez tout les champs");
            }

            var _user = _appDbContext.Users.FirstOrDefault(user => user.Email == _authUser.Email);
            
            if (_user == null)
            {
                return BadRequest("L'utilisateur n'as pas été trouvé");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(_authUser.Password, _user.Password);

            if (!validPassword)
            {
                return Unauthorized("Mot de passe ou nom d'utilisateur érroné");
            }

            var token = new AuthToken
            {
                emissionDate = DateTime.UtcNow,
                expirationDate = DateTime.UtcNow.AddDays(3),
                token = Guid.NewGuid().ToString(),
                user = _user,
            };

            _appDbContext.AuthTokens.Add(token);
            await _appDbContext.SaveChangesAsync();

            return Ok(token);
        }

        [HttpPost("create")]
        public User createUser(UserCreateDTO userDto)
        {
            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
            };

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.Password = hashPassword;

            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            return user;
        }
    }
}
