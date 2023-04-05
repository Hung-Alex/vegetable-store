using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.UserService
{
    public interface IUserRepository
    {
        Task<UserToken> GenerateToKen(User user, CancellationToken cancellation = default);
        Task<User> GetUserById(int id,CancellationToken cancellation=default);
        Task<User> CreateUser(CancellationToken cancellation=default);
        Task<User> GetUserByName(string name,CancellationToken cancellation=default);
            
    }
}
