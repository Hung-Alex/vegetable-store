using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using store_vegetable.Data.Context;
using store_vegetable.Data.Seeders;
using store_vegetable.Services.Media;
using store_vegetable.Services.StoreVegetable;
using System.Runtime.CompilerServices;
using FluentValidation;
using WebApi.Models;
using WebApi.Validations;
using WebApi.Authencation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Design;
using WebApi.JwtToken;
using WebApi.Settings;
using Microsoft.Extensions.Configuration;
using MailKit;
using WebApi.Mail;

namespace WebApi.Extensions
{
    public static  class WebApplicationExtensions
    {
       
        public static  WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreVegetableDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VegetableStoreDb")));
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IMediaManager, LocalFileSystemMediaManager>();
            builder.Services.AddScoped<IFoodRepository, FoodRepository>();
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserTokenRepository,UserTokenRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IJwtTokenRepository, JwtTokenRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();




            builder.Services.AddAuthentication(option =>
            {

            })
                .AddScheme<TokenAuthenticationSchemeOptions, AuthenticationService>("Admin", opst =>
                {
                    opst.Role = "admin";
                })
            .AddScheme<TokenAuthenticationSchemeOptions, AuthenticationService>("User", opst =>
             {
                 opst.Role = "user";
             })
            .AddScheme<TokenAuthenticationSchemeOptions, AuthenticationService>("Logout", opst =>
             {
                 opst.Role = null;
             });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.AddAuthenticationSchemes("Admin")
                          .RequireAuthenticatedUser()
                          .Build(); 
                });
                options.AddPolicy("User", policy =>
                {
                    policy.AddAuthenticationSchemes("User")
                          .RequireAuthenticatedUser()
                          .Build();
                });
                options.AddPolicy("Logout", policy =>
                {
                    policy.AddAuthenticationSchemes("Logout")
                          .RequireAuthenticatedUser()
                          .Build();
                });
            });





            return builder;
        }
        public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app)
        {
            using var scope=app.ApplicationServices.CreateScope();
            try
            {
                scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .Initialize();
            }
            catch (Exception ex)
            {

                scope.ServiceProvider
                    .GetRequiredService<ILogger<Program>>()
                    .LogError(ex, "could not insert data into database");
            }
            return app;
        }
        public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
        {

            builder.Services
                    .AddCors(options =>
                    {
                        options.AddPolicy("storevegetable", policyBuider => policyBuider
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod()
                );
                    });
            return builder;
        }
        public static WebApplicationBuilder ConfigureSwaggerOpenApi(this WebApplicationBuilder builder)
        {
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }
        public static WebApplicationBuilder ConfigureMailService(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<Mail.IMailService, Mail.MailService>();
            return builder;
        }
        public static WebApplication SetupRequestPieLines(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.MapControllers();
            app.UseCors("storevegetable");

            return app;
        }

    }
}
