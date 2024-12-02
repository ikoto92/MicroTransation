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
        public async Task<IActionResult> UpdateUser(UserUpdate userUpdateDto)
        {
            try
            {
                var existingUser = await _userRepository.GetById(userUpdateDto.Id);

                if (existingUser == null)

                    return NotFound("User not found");
               
                existingUser.Email = userUpdateDto.Email ?? existingUser.Email;
                existingUser.Name = userUpdateDto.Name ?? existingUser.Name;
                existingUser.Password = string.IsNullOrEmpty(userUpdateDto.Password)
                    ? existingUser.Password

                    : BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);

                return Ok(await _userRepository.Update(existingUser));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateName")]
        public async Task<IActionResult> UpdateUserName(UserUpdateName userUpdateDto)
        {
            try
            {
                var existingUser = await _userRepository.GetById(userUpdateDto.Id);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                existingUser.Name = userUpdateDto.Name;

                var updatedUser = await _userRepository.Update(existingUser);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateEmail")]
        public async Task<IActionResult> UpdateUserEmail(UserUpdateEmail userUpdateDto)
        {
            try
            {
                var existingUser = await _userRepository.GetById(userUpdateDto.Id);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                existingUser.Email = userUpdateDto.Email;

                var updatedUser = await _userRepository.Update(existingUser);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdateUserPassword(UserUpdatePassword userUpdateDto)
        {
            try
            {
                var existingUser = await _userRepository.GetById(userUpdateDto.Id);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);

                var updatedUser = await _userRepository.Update(existingUser);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
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
