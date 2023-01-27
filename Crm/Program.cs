using Crm.Application.BackgroundServices;
using Crm.Domain.People;
using EasyNetQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEasyNetQ("host=localhost;virtualHost=EasyNetQ");
builder.Services.AddHostedService<DomainEventPublisher>();

var app = builder.Build();

var bus = app.Services.GetRequiredService<IBus>();
bus.PubSub.Subscribe<PersonRenamed>("UpdateOwnersNames", p =>
{
});

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

public partial class Program { }