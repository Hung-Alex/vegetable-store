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
    public interface ICartRepository
    {
        
        Task<bool> AddItemInCartAsync(CartItem Item,CancellationToken cancellationToken=default);
        Task<bool> RemoveItemInCartAsync(int cartId, int foodId,CancellationToken cancellationToken = default);
        Task<bool> RemoveAllItemInCartAsync(int id,CancellationToken cancellationToken = default);
        Task<bool> CartHasEmptyAsync(int id,CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetCartAsync<T>(CancellationToken cancellationToken = default);
        Task<Cart> CreateCartAsync(int userId,CancellationToken cancellationToken = default);
        Task<Cart> GetCartByUserIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CartItem> ItemIsExitedInCart(int id,int cartId, CancellationToken cancellationToken = default);
        Task<bool> UpdateCartItem(CartItem cartItem, CancellationToken cancellationToken = default);
        Task<IPagedList<CartItemDto>> GetAllItemInCart(int cartId, IPagingParams pagingParams, CancellationToken cancellationToken = default);
    }
}
