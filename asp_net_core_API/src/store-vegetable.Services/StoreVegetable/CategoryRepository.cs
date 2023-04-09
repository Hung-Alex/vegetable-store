using Microsoft.EntityFrameworkCore;
using store_vegetable.Core.Contracts;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Services.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace store_vegetable.Services.StoreVegetable
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreVegetableDbContext _context;
        public CategoryRepository(StoreVegetableDbContext  context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateCategory(Categories category, CancellationToken cancellationToken = default)
        {
            if (category.Id>0)
            {
                _context.Update(category);
            }
            else
            {
                _context.Add(category);
            }
            return await _context.SaveChangesAsync()>0;   
        }

        public async Task<bool> DeleteCategory(int categoryId, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<Categories>().FindAsync(categoryId);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }

        public async Task<IList<Categories>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await _context.Set<Categories>().ToListAsync();// null or  is not null

                
        }

        public async Task<IPagedList<T>> GetFoodsByCategorySlug<T>(FoodQuery foodQuery, IPagingParams pagingParams, Func<IQueryable<Food>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterFood(foodQuery));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<bool> IsCategorySlugExistedAsync(int categoryId, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Categories>().AnyAsync(x => x.Id != categoryId && x.UrlSlug == urlSlug);
        }

       
        private  IQueryable<Food> FilterFood(FoodQuery foodQuery)
        {
            IQueryable<Food> foods = _context
                .Set<Food>()
                .Include(x => x.Categories);

            if (!String.IsNullOrWhiteSpace(foodQuery.CategorySlug))
            {
                foods = foods.Where(x => x.Categories.UrlSlug == foodQuery.CategorySlug);
            }
            return   foods;
        }

       
    }
}
