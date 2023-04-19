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
    public interface IFeedbackRepository
    {
        Task<IList<Feedback>> GetAllFeedbacks(CancellationToken cancellationToken = default);
        Task<IPagedList<T>> GetFeedbacksBySlug<T>(FeedbackQuery feedQuery, IPagingParams pagingParams, Func<IQueryable<Feedback>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
        Task<bool> IsFeedbackSlugExistedAsync(int feedbackId, string urlSlug, CancellationToken cancellationToken = default);
        Task<bool> DeleteFeedback(int feedbackId, CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdateFeedback(Feedback feedback, CancellationToken cancellationToken = default);
        Task<Feedback> GetFeedbackById(int id, CancellationToken cancellationToken = default);
        Task<bool> SetStatusFeedback(Feedback feedback, CancellationToken cancellationToken = default); 
    }
}
