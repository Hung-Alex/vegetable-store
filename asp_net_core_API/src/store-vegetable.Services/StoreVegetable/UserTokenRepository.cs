﻿using Microsoft.EntityFrameworkCore;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly StoreVegetableDbContext _context;
        public UserTokenRepository(StoreVegetableDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckTokenIsExisted(int userId, string token)
        {
            var user = await _context.Set<UserToken>().FirstOrDefaultAsync(x=>x.UserId==userId);
            if (user == null) return false;
            if (String.IsNullOrEmpty(user.Token))
            {
                return false;
            }
            
            return true;
        }

        public async Task<bool> SetStatusAccount(int userId, bool status, CancellationToken cancellationToken = default)
        {
            var user = await _context.Set<UserToken>().FirstOrDefaultAsync(x => x.UserId == userId);
            if (user==null)
            {
                return false;
            }
            user.Status = status;

            return  await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserToken> AddOrUpdateUserToken(int userId, string Token, CancellationToken cancellationToken = default)
        {
            var user = await _context.Set<UserToken>().FirstOrDefaultAsync(x => x.UserId == userId);
            if (user==null)
            {
                user = new UserToken()
                {
                    UserId = userId,
                    Token = Token,
                    Status = true,
                    Expired=DateTime.Now
                

                };
                _context.Add(user);
            }
            else
            {
                user.Token = Token;
                user.Status = true;
              
            }
            
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
