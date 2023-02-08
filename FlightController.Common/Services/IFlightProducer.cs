using FlightController.Common.Models;

namespace FlightController.Common;

public interface IFlightProducer
{
    Task<bool> PublishAsync(Flight flight);
}