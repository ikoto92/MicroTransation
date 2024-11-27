using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Models;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Services.Mappers;
using MicroTransation.Repositories;

namespace MicroTransation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly TokenMappers _token;

        public AuthController(IRepository<User> userRepository, TokenMappers token)
        {
            _userRepository = userRepository;
            _token = token;
        }


        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn(UserAuthDTO _authUser)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(_authUser.Email) || string.IsNullOrWhiteSpace(_authUser.Password))
                {
                    return BadRequest("Renseignez tous les champs.");
                }

                var user = _userRepository.GetAll()
                                          .Result
                                          .FirstOrDefault(u => u.Email == _authUser.Email);

                if (user == null)
                {
                    return BadRequest("L'utilisateur n'a pas été trouvé.");
                }

                // Vérification du mot de passe
                bool validPassword = BCrypt.Net.BCrypt.Verify(_authUser.Password, user.Password);
                if (!validPassword)
                {
                    return Unauthorized("Mot de passe ou nom d'utilisateur erroné.");
                }
          
                var token = new AuthToken
                {
                    emissionDate = DateTime.UtcNow,
                    expirationDate = DateTime.UtcNow.AddDays(3),
                    token = Guid.NewGuid().ToString(),
                    user = user
                };
              
                var dbContext = (AppDbContext)_userRepository; 
                dbContext.AuthTokens.Add(token);
                await dbContext.SaveChangesAsync();

                return Ok(new
                {
                    Token = token.token,
                    Expiration = token.expirationDate
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserCreateDTO userDto)
        {
            try
            {             
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password) 
                };
                await _userRepository.Create(user);

                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
