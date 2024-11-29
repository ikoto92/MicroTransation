using MicroTransation.Data;
using MicroTransation.Models;
using MicroTransation.DTOs;
using MicroTransation.Repositories;
using MicroTransation.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MicroTransation.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _appDbContext;
        private readonly TokenMappers _token;

        public UserRepository(AppDbContext appDbContext, TokenMappers token)
        {
            _appDbContext = appDbContext;
            _token = token;

        }
        public async Task<User> GetById(int id)
        {
            return _appDbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public async Task<IEnumerable<User>> GetAll() 
        {
            return _appDbContext.Users.ToList();
        }

        public async Task<User> Create(User user)
        {
            try
            {
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); };
        }

        public async Task<User> Update(User user) 
        {
            try
            {
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); };
        }

        public async Task<User> Delete(User user) 
        {
            try
            {
                _appDbContext.Users.Remove(user);
                await _appDbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); };
        }

        public async Task AddToken(AuthToken token)
        {
            try
            {
                await _appDbContext.AuthTokens.AddAsync(token);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du token : " + ex.Message, ex);
            }
        }

    }
}

