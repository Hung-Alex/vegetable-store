using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using store_vegetable.Data.Context;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Design;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Extensions;
using store_vegetable.Data.Seeders;
using WebApi.Mapsters;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
{
    builder.ConfigureServices()
        .ConfigureCors()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster();
}


var app = builder.Build();
{
    app.SetupRequestPieLines();
    app.UseDataSeeder();
    app.MapCategoryEnpoints();
    
    app.Run();
}





