using MicroTransation.Data;
using MicroTransation.Models;
using MicroTransation.DTOs;
using MicroTransation.Repositories;
using MicroTransation.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MicroTransation.Repositories
{
    {
        private readonly AppDbContext _appDbContext;

        public ItemsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Item> GetById(int id)
        {
            return _appDbContext.Items.FirstOrDefault(Item => Item.Id == id);
        }
        public async Task<IEnumerable<Item>> GetAll()
        {
            return _appDbContext.Items.ToList();
        }

        public async Task<Item> Create(Item item)
        {
            try
            {
                await _appDbContext.Items.AddAsync(item);
                await _appDbContext.SaveChangesAsync();

                return item;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); };
        }

        public async Task<Item> Update(Item item)
        {
            try
            {
                _appDbContext.Items.Update(item);
                await _appDbContext.SaveChangesAsync();

                return item;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); };
        }

        public async Task<Item> Delete(Item item)
        {
            try
            {
                _appDbContext.Items.Remove(item);
                await _appDbContext.SaveChangesAsync();

                return item;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); };
        }

    }
}
