using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using store_vegetable.Core.Contracts;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public class CartRepository : ICartRepository
    {
        private readonly StoreVegetableDbContext _context;
        public CartRepository(StoreVegetableDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemInCartAsync(CartItem Item, CancellationToken cancellationToken = default)
        {
            if (Item==null)
            {
                return false;
            }
            _context.Add(Item);
            return await _context.SaveChangesAsync()>0;
            
        }

        public async Task<bool> CartHasEmptyAsync(int id, CancellationToken cancellationToken = default)
        {
           return _context.Set<CartItem>().Where(x=>x.CartId==id).IsNullOrEmpty();
        }

        public async Task<Cart> CreateCartAsync(int userId, CancellationToken cancellationToken = default)
        {
            var cart = new Cart() { UserId = userId };
            _context.Add(cart);

            return await _context.SaveChangesAsync() > 0 ? cart : null;


        }

        public Task<IPagedList<T>> GetCartAsync<T>(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByUserIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Cart>().FirstOrDefaultAsync(x => x.UserId == id);
        }

      

        public async Task<bool> RemoveAllItemInCartAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<OrderItem>().Where(x => x.OrderId == id).ExecuteDeleteAsync() > 0; 
        }

        public async Task<bool> RemoveItemInCartAsync(int cartId, int foodId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<CartItem>().Where(x => x.FoodId==foodId&&x.CartId==cartId).ExecuteDeleteAsync()>0;
        }
    }
}
