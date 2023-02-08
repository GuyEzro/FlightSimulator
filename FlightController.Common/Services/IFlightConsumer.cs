using FlightController.Common.Models;

namespace FlightController.Common;

public interface IFlightConsumer
{
    Task<Flight> GetAsync();
}