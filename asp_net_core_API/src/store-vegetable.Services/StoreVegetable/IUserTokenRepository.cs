using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public interface IUserTokenRepository
    {
        Task<bool> CheckTokenIsExisted(int userId,string token);
        Task<UserToken> AddOrUpdateUserToken(int userId, string Token, CancellationToken cancellationToken = default);
        Task<bool> SetStatusAccount(int userId,bool status, CancellationToken cancellationToken = default);
        

    }
}
