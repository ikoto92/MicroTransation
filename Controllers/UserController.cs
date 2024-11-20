using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Models;

namespace MicroTransation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public User getUer(int id)
        {
            return _appDbContext.Users.FirstOrDefault((user) => user.Id == id);
        }  
    }
}
