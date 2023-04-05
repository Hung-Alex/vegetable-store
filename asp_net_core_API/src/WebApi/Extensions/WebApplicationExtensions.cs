using Microsoft.EntityFrameworkCore;
using store_vegetable.Data.Context;
using System.Runtime.CompilerServices;
using WebApi.Middwares;

namespace WebApi.Extensions
{
    public static  class WebApplicationExtensions
    {
        public static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreVegetableDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VegetableStoreDb")));
            //add authencation
            builder.Services.AddAuthentication()
                            .AddScheme<TokenAuthenticationSchemeOptions, TokenAuthenticationSchemeHandler>("Tokens",
                            opts => { }
    );



            return builder;
        }
        public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy.WithOrigins("http://example.com",
                                                              "http://www.contoso.com")
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                                      });
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

        public static WebApplication SetupRequestPieLines(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors(MyAllowSpecificOrigins);

            return app;
        }

    }
}
