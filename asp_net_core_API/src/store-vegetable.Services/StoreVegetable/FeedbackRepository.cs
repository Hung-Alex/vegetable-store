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

namespace store_vegetable.Services.StoreVegetable
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly StoreVegetableDbContext _context;
        public FeedbackRepository(StoreVegetableDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateFeedback(Feedback category, CancellationToken cancellationToken = default)
        {
            if (category.Id > 0)
            {

                _context.Update(category);
            }
            else
            {
                _context.Add(category);
            }
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteFeedback(int feedbackId, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<Feedback>().FindAsync(feedbackId);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IList<Feedback>> GetAllFeedbacks(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Feedback>().ToListAsync();// null or  is not null
        }

        public async Task<Feedback> GetFeedbackById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Feedback>().FindAsync(id, cancellationToken);
        }

        public async Task<IPagedList<T>> GetFeedbacksBySlug<T>(FeedbackQuery feedQuery, IPagingParams pagingParams, Func<IQueryable<Feedback>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterFood(feedQuery));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<bool> IsFeedbackSlugExistedAsync(int feedbackId, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Categories>().AnyAsync(x => x.Id != feedbackId && x.UrlSlug == urlSlug);
        }

        public async Task<bool> SetStatusFeedback(Feedback feedback, CancellationToken cancellationToken = default)
        {
            feedback.Status = !feedback.Status;
            _context.Update(feedback);
            return await _context.SaveChangesAsync() > 0;
        }

        private IQueryable<Feedback> FilterFood(FeedbackQuery feedbackQuery)
        {
            IQueryable<Feedback> feedbacks = _context
                .Set<Feedback>();


            if (feedbackQuery.Status)
            {
                feedbacks = feedbacks.Where(x => x.Status == feedbackQuery.Status);
            }
            if (!String.IsNullOrWhiteSpace(feedbackQuery.Title))
            {
                feedbacks = feedbacks.Where(x => x.Title.Contains(feedbackQuery.Title));
            }
            if (!String.IsNullOrWhiteSpace(feedbackQuery.UrlSlug))
            {
                feedbacks = feedbacks.Where(x => x.UrlSlug == feedbackQuery.UrlSlug);
            }
            return feedbacks;
        }
    }
}


