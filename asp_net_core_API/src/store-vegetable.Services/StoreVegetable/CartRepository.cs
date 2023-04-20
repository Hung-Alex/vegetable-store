using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using store_vegetable.Core.Contracts;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Services.Extensions;

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
            if (Item == null)
            {
                return false;
            }
            _context.Add(Item);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> CartHasEmptyAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Set<CartItem>().Where(x => x.CartId == id).IsNullOrEmpty();
        }

        public async Task<Cart> CreateCartAsync(int userId, CancellationToken cancellationToken = default)
        {
            var cart = new Cart() { UserId = userId };
            _context.Add(cart);

            return await _context.SaveChangesAsync() > 0 ? cart : null;


        }

        public async Task<IPagedList<CartItemDto>> GetAllItemInCart(int cartId, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            IQueryable<CartItem> query = _context.Set<CartItem>().Include(x => x.Food).Where(x => x.CartId == cartId);
            var items = await query
                .GroupBy(x => new { x.CartId, x.FoodId })
                .Select(x => new CartItemDto
                {
                    Id = x.Key.FoodId,
                    Image = x.Select(x => x.Food).FirstOrDefault().Image,
                    Name = x.Select(x => x.Food).FirstOrDefault().Name,
                    UrlSlug = x.Select(x => x.Food).FirstOrDefault().UrlSlug,
                    Price = x.Select(x => x.Food).FirstOrDefault().Price,
                    Quantity = x.Sum(x => x.Quantity)

                }).ToPagedListAsync(pagingParams, cancellationToken);
            return items;




        }

        public Task<IPagedList<T>> GetCartAsync<T>(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByUserIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Cart>().FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<CartItem> ItemIsExitedInCart(int id,int cartId ,CancellationToken cancellationToken = default)
        {
            return await _context.Set<CartItem>().FirstOrDefaultAsync(x => x.FoodId == id&&x.CartId==cartId,cancellationToken);

            
        }

        public async Task<bool> RemoveAllItemInCartAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<CartItem>().Where(x => x.CartId == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> RemoveItemInCartAsync(int cartId, int foodId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<CartItem>().Where(x => x.FoodId == foodId && x.CartId == cartId).ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> UpdateCartItem(CartItem cartItem, CancellationToken cancellationToken = default)
        {
            _context.Update(cartItem);
            return  await _context.SaveChangesAsync()>0;
        }
    }
}
