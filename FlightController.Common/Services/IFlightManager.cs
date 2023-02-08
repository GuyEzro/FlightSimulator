using FlightController.Common.Models;

namespace FlightController.Common.Services;

public interface IFlightManager
{
    Task<Flight> CreateAsync(Flight flight);
    Task<List<Flight>> GetLastInQ();
}