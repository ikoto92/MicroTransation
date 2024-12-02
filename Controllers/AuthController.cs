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
                    return BadRequest("Renseignez tous les champs.");

                var user = (await _userRepository.GetAll()).FirstOrDefault(u => u.Email == _authUser.Email);
                if (user == null)
                    return BadRequest("L'utilisateur n'a pas été trouvé.");

                if (!BCrypt.Net.BCrypt.Verify(_authUser.Password, user.Password))
                    return Unauthorized("Mot de passe ou nom d'utilisateur erroné.");

                var token = new AuthToken
                {
                    emissionDate = DateTime.UtcNow,
                    expirationDate = DateTime.UtcNow.AddDays(3),
                    token = Guid.NewGuid().ToString(),
                    user = user
                };

                if (_userRepository is ITokenRepository tokenRepository)
                {
                    await tokenRepository.AddToken(token);
                }
                else
                {
                    throw new InvalidOperationException("Le repository ne supporte pas les tokens.");
                }
                return Ok(new { emission = token.emissionDate, expiration = token.expirationDate, token = token.token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
