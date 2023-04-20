using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrder(int cartId, CancellationToken cancellationToken = default);
        

    }
}
