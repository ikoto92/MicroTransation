using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Models;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Services.Mappers;

namespace MicroTransation.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]

        public  async Task<IActionResult> SignIn (UserAuthDTO _authUser)

        public IActionResult SignIn (UserAuthDTO _authUser)

        {

            if(_authUser.Email==""|| _authUser.Password == "")
            {
               return BadRequest("renseigner les champs");
            }
           
            var user = _appDbContext.Users.FirstOrDefault(user => user.Email == _authUser.Email);

            if (user == null) 
            {

                return BadRequest("L'utilisateur n'a pas été trouvé !"); 

                return BadRequest("re"); 

            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(_authUser.Password, user.Password);

            if (!validPassword)
            {
                throw new Exception("mot de passe erronné");
            };

            var token = new AuthToken
            {
                emissionDate = DateTime.Now,
                expirationDate = DateTime.Now.AddDays(3),
                token = Guid.NewGuid().ToString(),
                user = user,
            };

            _appDbContext.AuthTokens.Add(token);

            //await _appDbContext.SaveChangesAsync();

           
            var guid = Guid.NewGuid().ToString();

            return Ok(_token.GetTokenGet(token));
        }
    }
}
