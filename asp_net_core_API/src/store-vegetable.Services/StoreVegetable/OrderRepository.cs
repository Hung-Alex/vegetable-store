using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public class OrderRepository : IOrderRepository
    {
        public Task CreateOrder(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
