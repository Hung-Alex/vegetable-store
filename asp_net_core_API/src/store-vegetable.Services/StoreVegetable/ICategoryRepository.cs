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
    public interface ICategoryRepository
    {
        Task<IList<Categories>> GetAllCategories(CancellationToken cancellationToken=default);

        Task<IPagedList<T>> GetFoodsByCategorySlug<T>(FoodQuery foodQuery,IPagingParams pagingParams, Func<IQueryable<Food>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<bool> IsCategorySlugExistedAsync(int categoryId,string urlSlug,CancellationToken cancellationToken=default);
        Task<bool> DeleteCategory(int categoryId, CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdateCategory(Categories category,CancellationToken cancellationToken=default);
        Task<Categories> GetCategoryById(int id, CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetCategorysByQuery<T>(CategoryQuery categoryQuery , IPagingParams pagingParams, Func<IQueryable<Categories>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);

    }
}
