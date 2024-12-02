using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.DTOs;
using MicroTransation.Models;
using MicroTransation.Repositories;
using static MicroTransation.DTOs.ItemDTO;

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

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateUser(ItemCreateDTO ItemDto)
        {
            try
            {
                var item = new Item
                {
                    Name = ItemDto.Name,
                    Description = ItemDto.Description,
                    price = ItemDto.price,
                    
                };

                await _itemRepository.Create(item);

                return CreatedAtAction(nameof(CreateUser), new { id = item.Id }, item);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem(ItemUpdateDTO itemUpdateDto)
        {
            try
            {
                var existingItem = await _itemRepository.GetById(itemUpdateDto.Id);

                if (existingItem == null)
                    return NotFound("Item not found");

                if (!string.IsNullOrEmpty(itemUpdateDto.Name))
                    existingItem.Name = itemUpdateDto.Name;

                if (!string.IsNullOrEmpty(itemUpdateDto.Description))
                    existingItem.Description = itemUpdateDto.Description;

                if (itemUpdateDto.price != 0) 
                    existingItem.price = itemUpdateDto.price;

                var updatedItem = await _itemRepository.Update(existingItem);
                
                return Ok(updatedItem);

            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateItemName")]
        public async Task<IActionResult> UpdateItemName(ItemUpdateNameDTO itemUpdateDto)
        {
            try
            {
                var existingItem = await _itemRepository.GetById(itemUpdateDto.Id);

                if (existingItem == null)
                {
                    return NotFound("User not found");
                }

                existingItem.Name = itemUpdateDto.Name;

                var updatedUser = await _itemRepository.Update(existingItem);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateItemDesciption")]
        public async Task<IActionResult> UpdateItemDescription(ItemUpdateDescriptionDTO itemUpdateDto)
        {
            try
            {
                var existingItem = await _itemRepository.GetById(itemUpdateDto.Id);

                if (existingItem == null)
                {
                    return NotFound("User not found");
                }

                existingItem.Description = itemUpdateDto.Description;

                var updatedUser = await _itemRepository.Update(existingItem);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateItemPrice")]
        public async Task<IActionResult> UpdateItemPrice(ItemUpdatepriceDTO itemUpdateDto)
        {
            try
            {
                var existingItem = await _itemRepository.GetById(itemUpdateDto.Id);

                if (existingItem == null)
                {
                    return NotFound("User not found");
                }

                existingItem.price = itemUpdateDto.price;

                var updatedUser = await _itemRepository.Update(existingItem);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
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
