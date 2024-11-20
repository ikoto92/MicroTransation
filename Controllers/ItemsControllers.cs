using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.Models;

namespace MicroTransation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsControllers : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ItemsControllers(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IEnumerable<Item> GetAllItem ()
        {
            return _appDbContext.Items;
        
        }
    }
}
