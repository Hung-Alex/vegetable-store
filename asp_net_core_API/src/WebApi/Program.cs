using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using store_vegetable.Data.Context;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Design;

using store_vegetable.Services.UserService;
using store_vegetable.Services.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.ConfigureServices()
        .ConfigureCors()
        .ConfigureSwaggerOpenApi();
}

var app = builder.Build();
{
    app.SetupRequestPieLines();
    app.Run();
}





