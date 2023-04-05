using Microsoft.EntityFrameworkCore;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.UserService
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreVegetableDbContext _context;
        public UserRepository( StoreVegetableDbContext context)
        {
            _context= context;
        }
        public Task<User> CreateUser(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserToken> GenerateToKen(User user, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellation = default)
        {
            var userQuery =_context.Set<User>().Where(x => x.ID == id);
            return await userQuery.SingleOrDefaultAsync(cancellation);
        }

        public async Task<User> GetUserByName(string name, CancellationToken cancellation = default)
        {
            var userQuery = _context.Set<User>().Where(x => x.Name == name);
            return await userQuery.SingleOrDefaultAsync(cancellation);
        }
    }
}
