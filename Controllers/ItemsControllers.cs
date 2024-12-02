using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.Models;
using MicroTransation.Repositories;

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
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _itemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var item = await _itemRepository.GetById(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(Item UpdateItem)
        {
            try
            {
                await _itemRepository.Delete(UpdateItem);

                return Ok("Delete done !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
