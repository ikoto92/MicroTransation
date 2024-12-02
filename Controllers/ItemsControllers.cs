using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Models;

namespace MicroTransation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsControllers : ControllerBase
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemsControllers(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IEnumerable<Item> GetAllItem ()
        {
            return _appDbContext.Items;
        }
    }
}
