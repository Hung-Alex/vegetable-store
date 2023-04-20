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

    }
}
