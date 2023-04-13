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
    public interface IFoodRepository
    {
       
        Task<bool> AddOrUpdateFood(Food food, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetAllFood<T>(IPagingParams pagingParams, Func<IQueryable<Food>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetFoodsByQuery<T>(FoodQuery foodQuery, IPagingParams pagingParams, Func<IQueryable<Food>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<bool> IsSlugFoodExisted(int foodId, string urlSlug, CancellationToken cancellationToken = default);
        Task<bool> DeleteFood(int foodId, CancellationToken cancellationToken = default);
        Task<Food> GetFoodById(int id, CancellationToken cancellationToken = default);
        Task<Food> GetFoodBySlug(string slug, CancellationToken cancellationToken = default);
        Task<IList<Food>> GetBestSellingFood(int limit, CancellationToken cancellationToken = default);

    }
}
