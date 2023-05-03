using store_vegetable.Core.Contracts;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public interface IUserRepository
    {
        Task<User> Login(string username, string password,CancellationToken cancellationToken=default);
        Task<bool> Register(string username, string password, string comfirmPassword, CancellationToken cancellationToken = default);
        Task<bool> IsUserNameExisted(string username, CancellationToken cancellationToken = default);
        Task<bool> RemoveUser(int id, CancellationToken cancellationToken = default);
        Task<User> SetRole(int id, string role);
        Task<User> GetById(int id);
        Task<User> GetUserByUserName(string username, CancellationToken cancellationToken=default);

        Task<IPagedList<T>> GetPagedUserList<T>(UserQuery userQuery, IPagingParams pagingParams, Func<IQueryable<User>, IQueryable<T>> map, CancellationToken cancellationToken = default);

        Task<bool> UpdateUser(User user, CancellationToken cancellationToken = default);
        

    }
}
