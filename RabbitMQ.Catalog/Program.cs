using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Catalog.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region DbSetup
builder.Services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
builder.Services.AddDbContext<CatalogContext>(opt => 
    {
        opt.UseSqlServer("Server=sqldata;Initial Catalog=CatalogDb;User Id=sa;Password=Pass@word;TrustServerCertificate=True",
            sqlServerOptionsAction: sqlopt =>
            {
                sqlopt.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlopt.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
            });
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
