using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Queues;
using FlightController.Common;
using FlightController.Common.Models;
using Microsoft.Extensions.Configuration;

namespace FlightController.Infrastructure.Queue;

public class FlightProducerQueue : IFlightProducer
{
    private readonly IConfiguration _configuration;

    public FlightProducerQueue(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> PublishAsync(Flight flight)
    {
        var connectionString = _configuration["flight_queue"];
        var queueClient = new QueueClient(connectionString, "flights",
            new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
        
        await queueClient.CreateIfNotExistsAsync();
        if (!await queueClient.ExistsAsync()) return false;
        
        string message = JsonSerializer.Serialize(flight);
        await queueClient.SendMessageAsync(message);

        return true;
    }
}