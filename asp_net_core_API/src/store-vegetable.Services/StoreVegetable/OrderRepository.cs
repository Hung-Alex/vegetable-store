using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Org.BouncyCastle.Asn1.Esf;
using store_vegetable.Core.Contracts;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Services.Extensions;

namespace store_vegetable.Services.StoreVegetable
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreVegetableDbContext _context;
        public OrderRepository(StoreVegetableDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrder(int userId, int cartId, Order order, CancellationToken cancellationToken = default)
        {
            order.UserId= userId;
            _context.Add(order);
            await _context.SaveChangesAsync();
            var orderItems = await TakeCartItemToOrder(order.Id, cartId);
            var total = orderItems.Sum(x => x.Quantity * x.Price);
            order.Total = total;
            order.OrderDate = DateTime.Now;
            var orderItemsList = await TakeCartItemToOrder(order.Id, cartId);
            _context.AddRange(orderItemsList);
            return await _context.SaveChangesAsync()>0;
        }
        private async Task<IList<OrderItem>>  TakeCartItemToOrder(int OrderId,int cartId) {
            var query = _context.Set<CartItem>()
                .Include(x=>x.Food)
                .Where(x => x.CartId == cartId)
                .Select(x=>new OrderItem
            {
                OrderId=OrderId,
                FoodId=x.FoodId,
                Quantity=x.Quantity,
                Price=x.Food.Price,
            });

            return await query.ToListAsync<OrderItem>();
        }

        public async Task<IPagedList<T>> GetPagedListOrder<T>(OrderQuery orderQuery, IPagingParams pagingParams,Func<IQueryable<Order>, IQueryable<T>> map ,CancellationToken cancellationToken = default)
        {
            IQueryable<T> pageList = map(FilterOrder(orderQuery));

            return  await pageList.ToPagedListAsync(pagingParams, cancellationToken);
                
        }
        
        private IQueryable<Order> FilterOrder(OrderQuery orderQuery)
        {
            IQueryable<Order> query = _context.Set<Order>().Include(x=>x.Items).ThenInclude(x=>x.Food);
            if (!string.IsNullOrEmpty(orderQuery.Address))
            {
                query.Where(x => x.Address.Contains(orderQuery.Address));
            }
            query.Where(x => orderQuery.Status ? x.Status == orderQuery.Status : x.Status == orderQuery.Status);

            return query;
        }
        public async Task<bool> SetOrderStatus(int idOrder, CancellationToken cancellationToken = default)
        {
            var order = await _context.Set<Order>().FindAsync(idOrder);

            if (order == null) { return false; }
            else
            {
                order.Status = !order.Status;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteOrderById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Order>().Where(x => x.Id == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<Order> GetOrderById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Order>().FindAsync(id);
            
        }
    }
}
