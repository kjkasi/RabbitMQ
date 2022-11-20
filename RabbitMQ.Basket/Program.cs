using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Basket.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

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

builder.Services.AddSingleton(() =>
{
    Console.WriteLine("123");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BasketContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    var factory = new ConnectionFactory()
    {
        HostName = "rabbitmq"
    };

    var connection = factory.CreateConnection();
    var channel = connection.CreateModel();

    channel.ExchangeDeclare(
        exchange: "direct_logs",
        type: "direct"
    );

    var queueName = channel.QueueDeclare(queue: "").QueueName;
    channel.QueueBind(queue: queueName, exchange: "direct_logs", routingKey: "info");
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = JsonSerializer.Deserialize<CatalogCreate>(body) ;
        var routingKey = ea.RoutingKey;
        Console.WriteLine($" [x] Received '{routingKey}':(Id:'{message?.Id}', Name:'{message?.Name}', Desc:'{message?.Description}', Price:'{message?.Price}')");
    };
    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
