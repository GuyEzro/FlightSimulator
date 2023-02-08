using System.Text.Json;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using FlightController.Common;
using FlightController.Common.Models;
using Microsoft.Extensions.Configuration;

namespace FlightController.Infrastructure.Queue;

public class FlightConsumerQueue : IFlightConsumer
{
    private readonly IConfiguration _configuration;

    public FlightConsumerQueue(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Flight> GetAsync()
    {
        var connectionString = _configuration["flight_queue"];
        var queueClient = new QueueClient(connectionString, "flights",
            new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });

        if (!await queueClient.ExistsAsync()) return null;
        
        QueueProperties properties = await queueClient.GetPropertiesAsync();

        if (properties.ApproximateMessagesCount <= 0) return null;
        QueueMessage[] retrievedMessage = await queueClient.ReceiveMessagesAsync(1);
        string theMessage = retrievedMessage[0].Body.ToString();
        await queueClient.DeleteMessageAsync(retrievedMessage[0].MessageId, 
            retrievedMessage[0].PopReceipt);

        var flight = JsonSerializer.Deserialize<Flight>(theMessage);
        return flight;

    }
}