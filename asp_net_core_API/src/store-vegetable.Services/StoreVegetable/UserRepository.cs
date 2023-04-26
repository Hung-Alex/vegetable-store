using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using store_vegetable.Core.Contracts;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using store_vegetable.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Services.Extensions;

namespace store_vegetable.Services.StoreVegetable
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreVegetableDbContext _context;
        public UserRepository(StoreVegetableDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(int id)
        {
            var query = _context.Set<User>().Include(x => x.UserToken);
            var user= await query.FirstOrDefaultAsync(x=>x.Id==id);
            return user;

        }

        public async Task<IPagedList<T>> GetPagedUserList<T>(UserQuery userQuery, IPagingParams pagingParams, Func<IQueryable<User>, IQueryable<T>> map, CancellationToken cancellationToken = default)
        {
            var Users = map(FilterUser(userQuery));
            return await Users.ToPagedListAsync(pagingParams, cancellationToken);
        }
        private IQueryable<User> FilterUser(UserQuery userQuery)
        {
            IQueryable<User> query = _context.Set<User>();
            if (!string.IsNullOrEmpty(userQuery.KeyWord))
            {
                query = query.Where(x => x.Name.Contains(userQuery.KeyWord));
            }
            return query;
        }
        public async Task<User> GetUserByUserName(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Name == username);
        }

        public async Task<bool> IsUserNameExisted(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>().AnyAsync(x => x.Name == username);
        }

        public async Task<User> Login(string username, string password, CancellationToken cancellationToken = default)
        {
            var user =_context.Set<User>().FirstOrDefault(x=>x.Name==username);
            if (user == null)
            {
                return null;
            }
            var decodePassword = user.Password.DecodeFrom64();
            if (password != decodePassword)
            {
                return null;
            }
            return user;
        }

        public async Task<bool> Register(string username, string password, string comfirmPassword, CancellationToken cancellationToken = default)
        {
            var user = _context.Set<User>().FirstOrDefault(x => x.Name == username);
            if (user != null)
            {
                return false;
            }
            if (password != comfirmPassword)
            {
                return false;
            }
            _context.Add(new User() { Name = username, Password = password.EncodePasswordToBase64() });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveUser(int id, CancellationToken cancellationToken = default)
        {
            var user= await _context.Set<User>().FindAsync(id);
            if (user==null)
            {
                return false;
            }
            _context.Remove(user);
            return await _context.SaveChangesAsync()>0;   
        }

        public  async Task<User> SetRole(int id, string role)
        {
            var user = await _context.Set<User>().FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.Role=role;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUser(User user, CancellationToken cancellationToken = default)
        {
            if (user==null)
            {
                return false;
            }
            
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
