﻿using MicroTransation.DTOs;
using MicroTransation.Models;

namespace MicroTransation.Services.Mappers
{
    public class TokenMappers
    {
        public TokenDTO GetTokenGet(AuthToken token)
        {
            return  new TokenDTO
            {
                emissionDate = token.emissionDate,
                expirationDate = token.expirationDate,
                Token = token.token,

                UserAuth = new UserAuthDTO
                {
            
                    Email = token.user.Name,
                    Password = token.user.Email,
                }
            };

        }
    }
}
