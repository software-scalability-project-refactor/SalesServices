using Microsoft.EntityFrameworkCore;
using Npgsql;
using SalesService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    options.UseNpgsql(connectionString);
});

var app = builder.Build();
