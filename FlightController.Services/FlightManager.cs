using FlightController.Common;
using FlightController.Common.Models;
using FlightController.Common.Services;

namespace FlightController.Services;

public class FlightManager : IFlightManager
{
    private readonly IFlightProducer _producer;
    private readonly IFlightRepository<Flight> _service;

    public FlightManager(IFlightProducer producer,IFlightRepository<Flight> service)
    {
        _producer = producer;
        _service = service;
    }

    public async Task<Flight> CreateAsync(Flight flight)
    {
        await _service.Add(flight);
        await _producer.PublishAsync(flight);
        return flight;
    }

    public Task<List<Flight>> GetLastInQ()
    {
        return _service.GetLastInQ();
    }
}