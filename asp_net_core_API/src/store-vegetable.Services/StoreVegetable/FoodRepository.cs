using Microsoft.EntityFrameworkCore;
using store_vegetable.Core.Contracts;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Services.Extensions;

namespace store_vegetable.Services.StoreVegetable
{
    public class FoodRepository : IFoodRepository
    {
        private readonly StoreVegetableDbContext _context;
        public FoodRepository(StoreVegetableDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateFood(Food food, CancellationToken cancellationToken = default)
        {
            if (food.Id>0)
            {
                _context.Update(food);
            }
            else
            {
                _context.Add(food);
            }
            return await _context.SaveChangesAsync()>0;
        }


        private IQueryable<Food> FilterFood(FoodQuery foodQuery)
        {
            IQueryable<Food> foods = _context
                .Set<Food>()
                .Include(x => x.Categories);

            if (!String.IsNullOrWhiteSpace(foodQuery.CategorySlug))
            {
                foods = foods.Where(x => x.Categories.UrlSlug == foodQuery.CategorySlug);
            }
            if (!String.IsNullOrWhiteSpace(foodQuery.Keyword))
            {
                foods = foods.Where(x => x.Categories.UrlSlug.Contains(foodQuery.Keyword));
            }
            if (!String.IsNullOrWhiteSpace(foodQuery.UrlSlug))
            {
                foods = foods.Where(x => x.UrlSlug == foodQuery.UrlSlug);
            }
            return foods;
        }
        public  async Task<bool> DeleteFood(int foodId, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<Food>().FindAsync(foodId);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IPagedList<T>> GetAllFood<T>(IPagingParams pagingParams, Func<IQueryable<Food>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterFood(null));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<Food> GetFoodById(int id, CancellationToken cancellationToken = default)
        {
            var food = await _context.Set<Food>().FindAsync(id, cancellationToken);
            return food;
        }

        public async Task<Food> GetFoodBySlug(string slug, CancellationToken cancellationToken = default)
        {
            var food= await _context.Set<Food>().FirstOrDefaultAsync(x=>x.UrlSlug==slug,cancellationToken);
            return food;
        }

        public async Task<IPagedList<T>> GetFoodsByQuery<T>(FoodQuery foodQuery, IPagingParams pagingParams, Func<IQueryable<Food>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterFood(foodQuery));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<bool> IsSlugFoodExisted(int foodId, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Food>().AnyAsync(x => x.Id != foodId && x.UrlSlug == urlSlug);
        }

        public  async Task<IList<Food>> GetBestSellingFood(int limit, CancellationToken cancellationToken = default)
        {
            IQueryable<OrderItem> query = _context.Set<OrderItem>().Include(x=>x.Food);
            var foods = query.GroupBy(x => new { x.FoodId }).Select(x => new
            {
                foodId = x.Key,
                total = x.Sum(x => x.Quantity),
                food=x.Select(x=>x.Food).FirstOrDefault(),

            })
                .OrderByDescending(x => x.total).Select(x=>x.food).Take(limit);
            return foods.ToList();

        }
    }
}
