using Context;
using FlightController.Common;
using FlightController.Common.Models;
using FlightController.Common.Services;
using FlightController.Infrastructure.Queue;
using FlightController.Services;
using FlightController.Web.API.Worker;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddDbContext<FlightsContext>(option =>
   option.UseSqlServer(config.GetConnectionString("main")), ServiceLifetime.Scoped);

builder.Services.AddControllers();
builder.Services.AddTransient<IFlightProducer, FlightProducerQueue>()
    .AddTransient<IFlightConsumer, FlightConsumerQueue>()
    .AddTransient<IFlightManager, FlightManager>()
    .AddTransient<IFlightRepository<Flight>,FlightRepository>()
    .AddSingleton<ChatHub>();


builder.Services.AddCors(p => p.AddPolicy("main", e => e.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<TerminalManagerWorker>();

builder.Services.AddSignalR(options => 
    options.EnableDetailedErrors = true
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("main");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();


app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

app.MapControllers();

app.Run();