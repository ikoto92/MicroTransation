using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Models;
using MicroTransation.Services.Mappers;
using MicroTransation.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MicroTransation.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepository.GetAll());
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(User UpdateUser)
        {
            try
            {
                return Ok(await _userRepository.Update(UpdateUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(User UpdateUser)
        {
            try
            {
                await _userRepository.Delete(UpdateUser);

                return Ok("Delete done !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
