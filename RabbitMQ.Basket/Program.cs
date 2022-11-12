using Microsoft.EntityFrameworkCore;
using RabbitMQ.Basket.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region DbSetup
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddDbContext<BasketContext>(opt =>
    opt.UseSqlServer("Server=sqldata;Initial Catalog=BasketDb;User Id=sa;Password=Pass@word;TrustServerCertificate=True"));
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
