using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.StoreVegetable
{
    public interface IOrderRepository
    {
        Task CreateOrder(int userId, CancellationToken cancellationToken = default);

    }
}
