using Azure;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly StoreVegetableDbContext _dbContext;
        public DataSeeder(StoreVegetableDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Foods.Any())
            {
                return;
            }
            AddFeedbacks();
           
        }

        
        /// <summary>
        /// add feedbacks in database
        /// </summary>
        /// <returns></returns>
        private IList<Feedback> AddFeedbacks()
        {

            var feedbacks = new List<Feedback>()
            {
                new(){
                    Title="doi tien vi mua ban lang xet",
                    Description="ko biet noi gi luon chi la de test",
                    Email="2015595@dlu.edu.com",
                    UrlSlug="doi-tien-vi-mua-ban-lang-xet-1",
                    Meta="Lorem Ipsum is simply dummy text of " +
                    "the printing and typesetting industry. Lorem " +
                    "Ipsum has been the industry's standard dummy text " +
                    "ever since the 1500s, when an unknown printer took a galley " +
                    "of type and scrambled it to make a type specimen book. It has" +
                    " survived not only five centuries, but also the leap into electronic ",
                    ShippingDate=DateTime.Now,
                    Status=false
                },
                    new(){
                    Title="doi tien vi mua ban lang xet",
                    Description="ko biet noi gi luon chi la de test",
                    Email="2015595@dlu.edu.com",
                    UrlSlug="doi-tien-vi-mua-ban-lang-xet-2",
                    Meta="Lorem Ipsum is simply dummy text of " +
                    "the printing and typesetting industry. Lorem " +
                    "Ipsum has been the industry's standard dummy text " +
                    "ever since the 1500s, when an unknown printer took a galley " +
                    "of type and scrambled it to make a type specimen book. It has" +
                    " survived not only five centuries, but also the leap into electronic ",
                    ShippingDate=DateTime.Now,
                    Status=false
                },    new(){
                    Title="doi tien vi mua ban lang xet",
                    Description="ko biet noi gi luon chi la de test",
                    Email="2015595@dlu.edu.com",
                    UrlSlug="doi-tien-vi-mua-ban-lang-xet-3",
                    Meta="Lorem Ipsum is simply dummy text of " +
                    "the printing and typesetting industry. Lorem " +
                    "Ipsum has been the industry's standard dummy text " +
                    "ever since the 1500s, when an unknown printer took a galley " +
                    "of type and scrambled it to make a type specimen book. It has" +
                    " survived not only five centuries, but also the leap into electronic ",
                    ShippingDate=DateTime.Now,
                    Status=false
                },    new(){
                    Title="doi tien vi mua ban lang xet",
                    Description="ko biet noi gi luon chi la de test",
                    Email="2015595@dlu.edu.com",
                    UrlSlug="doi-tien-vi-mua-ban-lang-xet-4",
                    Meta="Lorem Ipsum is simply dummy text of " +
                    "the printing and typesetting industry. Lorem " +
                    "Ipsum has been the industry's standard dummy text " +
                    "ever since the 1500s, when an unknown printer took a galley " +
                    "of type and scrambled it to make a type specimen book. It has" +
                    " survived not only five centuries, but also the leap into electronic ",
                    ShippingDate=DateTime.Now,
                    Status=true
                },    new(){
                    Title="doi tien vi mua ban lang xet",
                    Description="ko biet noi gi luon chi la de test",
                    Email="2015595@dlu.edu.com",
                    UrlSlug="doi-tien-vi-mua-ban-lang-xet-5",
                    Meta="Lorem Ipsum is simply dummy text of " +
                    "the printing and typesetting industry. Lorem " +
                    "Ipsum has been the industry's standard dummy text " +
                    "ever since the 1500s, when an unknown printer took a galley " +
                    "of type and scrambled it to make a type specimen book. It has" +
                    " survived not only five centuries, but also the leap into electronic ",
                    ShippingDate=DateTime.Now,
                    Status=true
                },


            };
            _dbContext.AddRange(feedbacks);
            _dbContext.SaveChanges();
            return feedbacks;
        }

        private IList<Categories> AddCategories()
        {
            throw new Exception();
        }

        private IList<Food> AddFoods()
        {

            throw new Exception();
        }
    }
}
