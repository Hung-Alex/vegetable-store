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
    public interface IOrderRepository
    {
        Task<bool> CreateOrder(int userId,int cartId,Order order, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedListOrder<T>(OrderQuery orderQuery, IPagingParams pagingParams,Func< IQueryable<Order>, IQueryable<T>> map, CancellationToken cancellationToken = default);

        Task<bool> SetOrderStatus(int idOrder, CancellationToken cancellationToken = default);

        Task<bool> DeleteOrderById(int id,CancellationToken cancellationToken=default);
        Task<Order> GetOrderById(int id, CancellationToken cancellationToken = default);
       
        

    }
}
