using MarketCheckout.Application.Services;
using MarketCheckout.Application.Services.Interface;
using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckout.Infrastructure.Persistence.Data;
using MarketCheckoutApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using static MarketCheckout.Infrastructure.Persistence.Data.MarketCheckoutDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Order Processing Service API",
        Version = "v1",
        Description = "API for processing e-commerce orders and determining CD allocation"
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
builder.Services.AddScoped<IBaseRepository<ItemCart>, BaseRepository<ItemCart>>();
builder.Services.AddScoped<IBaseRepository<Cart>, BaseRepository<Cart>>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IItemCartService, ItemCartService>();
builder.Services.AddHttpClient("ProductDummy", client => { client.BaseAddress = new Uri("https://dummyjson.com/products/");});

builder.Services.AddDbContext<MarketCheckoutDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MarketCheckout API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
