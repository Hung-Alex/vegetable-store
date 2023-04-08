﻿using Azure;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Context;
using store_vegetable.Data.Extensions;
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
            var users=AddUsers();
            var categories=  AddCategories();
            var carts = AddCarts(users);
            var orders = AddOrders(users);
           
        }
        private IList<Cart> AddCarts(IList<User> users)
        {
            var carts = new List<Cart>();
            foreach (var user in users)
            {
                carts.Add(new Cart() { UserId = user.Id });
            }
            _dbContext.AddRange(carts);
            _dbContext.SaveChanges();
            return carts;
        }
        private IList<Order> AddOrders(IList<User> users)
        {
            var orders = new List<Order>();
            foreach (var user in users)
            {
                orders.Add(new Order() 
                { 
                    UserId = user.Id ,
                    Address="Phù Đổng Thiên Vương",
                    Phone="113",
                    Total=400000,

                });
            }
            _dbContext.AddRange(orders);
            _dbContext.SaveChanges();
            return orders;
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
            var categories = new List<Categories>()
            {
                new()
                { 
                    Name="Rau Củ Quả",
                    Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Rau Củ Quả".GenerateSlug(),

                },
                 new()
                {
                    Name="Trái Cây",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Trái Cây".GenerateSlug(),

                },
                  new()
                {
                    Name="Thịt, Trứng ,Cá",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Thịt, Trứng ,Cá".GenerateSlug(),

                },
                   new()
                {
                    Name="Hạt ,Ngũ Cốc ,Hạt",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Hạt ,Ngũ Cốc ,Hạt".GenerateSlug(),

                },
                    new()
                {
                    Name="Sản Phẩm Từ Sữa",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Sản Phẩm Từ Sữa".GenerateSlug(),

                },
                     new()
                {
                    Name="Đồ Uống",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Đồ Uống".GenerateSlug(),

                },
                      new()
                {
                    Name="Thực Phẩm Dinh Dưỡng",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Thực Phẩm Dinh Dưỡng".GenerateSlug(),

                },
                       new()
                {
                    Name="Sốt Gia Vị",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Sốt Gia Vị".GenerateSlug(),

                },
                        new()
                {
                    Name="Đặc Sản Đà Lạt",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Đặc Sản Đà Lạt".GenerateSlug(),

                },
                         new()
                {
                    Name="Giỏ Quả ,Combo",
                     Description="Lorem Ipsum " +
                    "is simply dummy text of the " +
                    "printing and typesetting industry. " +
                    "Lorem Ipsum has been the industry's standard " +
                    "dummy text ever since the 1500s, when an unknown " +
                    "printer took a galley of type and scrambled it to make" +
                    " a type specimen book.",
                    UrlSlug="Giỏ Quả ,Combo".GenerateSlug(),

                },
            };
            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();
            return categories;
        }

        private IList<User> AddUsers() 
        {
            var Users = new List<User>()
            {
                new()
                { 
                    Name="daicakhu7",
                    Password="hungprovip".EncodePasswordToBase64(),               
                },
                new()
                {
                    Name="daicakhu8",
                    Password="hungprovip".EncodePasswordToBase64(),
                },new()
                {
                    Name="daicakhu9",
                    Password="hungprovip".EncodePasswordToBase64(),
                },new()
                {
                    Name="daicakhu10",
                    Password="hungprovip".EncodePasswordToBase64(),
                },new()
                {
                    Name="daicakhu11",
                    Password="hungprovip".EncodePasswordToBase64(),
                },new()
                {
                    Name="daicakhu12",
                    Password="hungprovip".EncodePasswordToBase64(),
                },
                new()
                {
                    Name="Admin",
                    Password="hungprovip".EncodePasswordToBase64(),
                    Role="admin"
                },
            }; 
            _dbContext.AddRange(Users);
            _dbContext.SaveChanges();
            return Users;
        }
        private IList<Food> AddFoods()
        {

            throw new Exception();
        }
    }
}
